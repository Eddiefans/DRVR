using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedingDetector : MonoBehaviour
{
    
    private MeasureGameObjectSpeed objectToMeasure;
    [SerializeField] private float unitConversionRatio = 20;
    [SerializeField] private int speedLimit = 40;
    void Update()
    {
        if (objectToMeasure == null)
            return;
        
        float speed = objectToMeasure.speedMagnitude * unitConversionRatio;
        if (speed > speedLimit)
        {
            GameManager.Instance.speedLimitState = GameManager.SpeedLimitState.AboveSpeedLimit;
        }
        else
        {
            GameManager.Instance.speedLimitState = GameManager.SpeedLimitState.OK;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetInstanceID() == GameManager.Instance.playerCarInstanceID)
        {
            objectToMeasure = other.transform.GetComponent<MeasureGameObjectSpeed>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetInstanceID() == GameManager.Instance.playerCarInstanceID)
        {
            objectToMeasure = null;
        }
    }
}
