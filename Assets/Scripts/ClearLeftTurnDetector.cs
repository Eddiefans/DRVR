using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClearLeftTurnDetector : MonoBehaviour
{
    public UnityEvent turnedInFrontOfCar;
    [SerializeField] private int minAngleThreshold, maxAngleThreshold;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarIA"))
        {
            VehicleAiController vechiVehicleAiController = other.GetComponent<VehicleAiController>();
            if (vechiVehicleAiController != null)
            {
                if (vechiVehicleAiController.OnRoundabout)
                    return;
            }

            float angle = Vector3.Angle(transform.parent.forward, other.transform.forward);
            Debug.Log(angle);
            if (angle > minAngleThreshold && angle < maxAngleThreshold)
            {
                turnedInFrontOfCar?.Invoke();
            }
        }
    }
}
