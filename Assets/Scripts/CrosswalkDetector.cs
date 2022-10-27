using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswalkDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crosswalk"))
        {
            print("crosswalk detected, STOP!");
        }
    }
}
