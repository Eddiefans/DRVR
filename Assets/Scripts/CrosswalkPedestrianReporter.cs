using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswalkPedestrianReporter : MonoBehaviour
{
    [SerializeField] private CrossWalkSystem crossWalkSystem;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            crossWalkSystem.pedestrianStatus = CrossWalkSystem.PedestrianStatus.Waiting;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            crossWalkSystem.pedestrianStatus = CrossWalkSystem.PedestrianStatus.None;
        }
    }
}
