using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundaboutDetector : MonoBehaviour
{
    [SerializeField] private VehicleAiController vechicleAiController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Roundabout"))
        {
            vechicleAiController.OnRoundabout = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Roundabout"))
        {
            vechicleAiController.OnRoundabout = false;
        }
    }
}
