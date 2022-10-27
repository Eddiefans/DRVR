using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CarWaypoint : MonoBehaviour
{
    public float newCarSpeed = 5;
    public float newRotationSpeed = 5;
    public bool hasWaitTime;
    public float waitTime = 1;
    public bool checksForRedLight;

    [SerializeField] private GameObject redLight;
}
