using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInRoundaboutDetector : MonoBehaviour
{
    [SerializeField] private VehicleAiController vehicleAiController;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CarIA"))
        {
            if (transform.parent.GetInstanceID() != other.transform.GetInstanceID())
            {
                VehicleAiController otherAiController = other.GetComponent<VehicleAiController>();
                if (otherAiController != null)
                {
                    if(otherAiController.OnRoundabout)
                        vehicleAiController.vehicleOnRoundabout = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CarIA"))
        {
            if (transform.parent.GetInstanceID() != other.transform.GetInstanceID())
            {
                vehicleAiController.vehicleOnRoundabout = false;
                // if(other.GetComponent<VehicleAiController>().OnRoundabout)
                //     vehicleAiController.vehicleOnRoundabout = false;
            }
        }
    }
}
