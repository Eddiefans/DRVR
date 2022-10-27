using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NearbyCarDetector : MonoBehaviour
{
    public enum RiskState
    {
        OK,
        Warning,
        Danger
    }

    public RiskState riskState;
    
    [SerializeField] private Transform[] nearbyPoints;
    [SerializeField] private List<Transform> nearbyCars = new List<Transform>();
    [SerializeField] private float riskWarningThreshold = 5;
    [SerializeField] private float riskDangerThreshold = 10;
    [SerializeField] private MeasureGameObjectSpeed measureGameObjectSpeed;
    [SerializeField] private float riskLevel = 0;
    [SerializeField] private float collisionAngleThreshold = 20;
    [SerializeField] private bool debugInUI;
    [SerializeField] private UnityEvent NotRespectDistance;
    //[SerializeField] private float distance;
    
    private void Start()
    {
        StartCoroutine(CheckNeabyCars());
    }

    IEnumerator CheckNeabyCars()
    {
        while (enabled)
        {
            yield return Yielders.Get(0.1f);
            for (int i = 0; i < nearbyPoints.Length; i++)
            {
                foreach (var car in nearbyCars)
                {
                    Vector3 otherCarDirection = car.position - transform.position;
                    Debug.DrawRay(transform.position, otherCarDirection, Color.magenta);
                    float angle = Vector3.Angle(transform.forward, otherCarDirection);
                    
                    if (angle < collisionAngleThreshold)//el otro auto está en trayectoria de colisión
                    {
                        float distance = Vector3.Distance(nearbyPoints[i].position,
                            car.position);
                            
                        riskLevel = getDistance(distance) * measureGameObjectSpeed.speedMagnitude;
                        if (riskLevel < riskWarningThreshold)
                        {
                            if(debugInUI)
                                UIManager.Instance.SetUIDebugText("risk level " + riskLevel);
                            riskState = RiskState.OK;
                        }else if (riskLevel > riskWarningThreshold && riskLevel < riskDangerThreshold)
                        {
                            if(debugInUI)
                                UIManager.Instance.SetUIDebugText("Warning!");
                            riskState = RiskState.Warning;
                        }
                        else
                        {
                            if(debugInUI)
                                UIManager.Instance.SetUIDebugText("Danger!");
                            riskState = RiskState.Danger;
                            NotRespectDistance?.Invoke();
                        }
                    }
                }
            }
        }
    }

    private float getDistance(float distance){
        float aux = distance - 24f;
        if(aux < 0) aux = Math.Abs(aux);
        return aux;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarIA"))
        {
            nearbyCars.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CarIA") && nearbyCars.Contains(other.transform))
        {
            nearbyCars.Remove(other.transform);
        }
    }
}
