using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Overtake : MonoBehaviour
{

    [SerializeField] private Transform carOvertaken;
    [SerializeField] private float angle;
    [SerializeField] private int carID;
    [SerializeField] private float exitAreaDistance;
    [SerializeField] private Vector3 exitAreaDirection;
    [SerializeField] private float entryAngleThreshold = 30;
    [SerializeField] private float exitAngle;
    [SerializeField] private float exitAngleThreshold = 120;
    [SerializeField] private UnityEvent overtakeEvent;
    [SerializeField] private bool debug;
    
    private bool drawLine;
    void Update()
    {
        if (carOvertaken != null)
        {
            angle = Vector3.Angle(transform.forward, carOvertaken.forward);
        }

        if (drawLine && debug)
        {
            Debug.DrawRay(transform.parent.position, exitAreaDirection, Color.cyan);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarIA") && carOvertaken == null)
        {
            //Revisa si el auto a rebasar va en la misma dirección
            if (Vector3.Angle(transform.parent.forward, other.transform.forward) < entryAngleThreshold)
            {
                carOvertaken = other.transform;
                carID = other.GetInstanceID();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CarIA") && other.GetInstanceID() == carID)
        {
            exitAreaDirection = other.transform.position - transform.parent.position;
            drawLine = true;
            exitAngle = Vector3.Angle(transform.parent.forward, exitAreaDirection);
            if (exitAngle > exitAngleThreshold)
            {
                overtakeEvent?.Invoke();
            }
            carOvertaken = null;
        }
    }

    public void OvertakeHappened()
    {
        print("Rebase");
    }
    
}
