using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDetector : MonoBehaviour
{
    [SerializeField] private VehicleAiController vehicleAiController;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CarIA"))
        {
            if (transform.parent.GetInstanceID() != other.transform.GetInstanceID())
            {
                VehicleAiController otherAiController = other.transform.GetComponent<VehicleAiController>();
                if (otherAiController != null)
                {
                    if(vehicleAiController.OnRoundabout && !otherAiController.OnRoundabout)
                        return;
                }
                vehicleAiController.hasVehicleInFront = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CarIA"))
        {
            if (transform.parent.GetInstanceID() != other.transform.GetInstanceID())
            {
                vehicleAiController.hasVehicleInFront = false;
            }
        }
    }
}
