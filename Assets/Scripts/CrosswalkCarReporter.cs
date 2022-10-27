using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrosswalkCarReporter : MonoBehaviour
{
    [SerializeField] private CrossWalkSystem crossWalkSystem;
    private MeasureGameObjectSpeed objectSpeed;
    [SerializeField] private float carSpeed;
    [SerializeField] private string carName;
    [SerializeField] private int carID;
    [SerializeField] private float pedestrianWaitingTimeAccumulation;
    [SerializeField] private float accumulationLimit = 4;
    private bool failEventRaised;

    
    private void OnTriggerStay(Collider other)
    {
        /*Quitar el comentario de este if si se quiere que solo el auto manejado por el usuario inferfiera con 
         los cruces peatonales*/
        
        // if (other.transform.GetInstanceID() == GameManager.Instance.playerCarInstanceID)
        // {
        if (other.CompareTag("CarIA"))
        {
            if (objectSpeed == null)
            {
                objectSpeed = other.transform.GetComponent<MeasureGameObjectSpeed>();
                carID = other.transform.GetInstanceID();
                carName = other.transform.name;
            }
            else
            {
                carSpeed = objectSpeed.speedMagnitude;
                if (carSpeed < 0.1f)//la velocidad del auto es casi 0, cediendo el paso al peatón
                {
                    crossWalkSystem.carStatus = CrossWalkSystem.CarStatus.Waiting;
                }
                else
                {
                    if (crossWalkSystem.pedestrianStatus == CrossWalkSystem.PedestrianStatus.Waiting 
                        && other.transform.GetInstanceID() == GameManager.Instance.playerCarInstanceID)
                    {
                        pedestrianWaitingTimeAccumulation += Time.deltaTime;
                        if (pedestrianWaitingTimeAccumulation > accumulationLimit && !failEventRaised)
                        {
                            failEventRaised = true;
                            crossWalkSystem.FailToWaitForPedestrianEvent?.Invoke();
                        }
                    }

                    crossWalkSystem.carStatus = CrossWalkSystem.CarStatus.Passing;
                }
            }
        }
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("CarIA"))
        {
            carName = null;
            carID = 0;
            crossWalkSystem.carStatus = CrossWalkSystem.CarStatus.None;
        }
        // if (other.transform.GetInstanceID() == GameManager.Instance.playerCarInstanceID)
        // {
        //     crossWalkSystem.carStatus = CrossWalkSystem.CarStatus.None;
        // }
    }
}
