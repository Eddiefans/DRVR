using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private TrafficCrossing trafficCrossing;
    [SerializeField] private byte streetNumber;
    [SerializeField] private GameObject greenLight;
    [SerializeField] private GameObject yellowLight;
    [SerializeField] private GameObject redLight;

    enum ForceLightState
    {
        None, 
        Green, 
        Yellow,
        Red
    }
    [SerializeField] private ForceLightState forceLightState;

    public enum LightState
    {
        Green,
        Yellow,
        Red
    }
    public LightState currentLightState;
    
    private void OnEnable()
    {
        trafficCrossing.StateChange += OnCrossingStateChange;
    }

    void OnCrossingStateChange(int newState)
    {
        StartCoroutine(LightChangeRoutine(newState));
    }

    IEnumerator LightChangeRoutine(int newState)
    {
        if (newState == streetNumber)
        {
            yield return Yielders.Get(trafficCrossing.lightSwitchTime);
            currentLightState = LightState.Green;
            yellowLight.SetActive(false);
            redLight.SetActive(false);
            greenLight.SetActive(true);
        }
        else if(greenLight.activeSelf)
        {
            greenLight.SetActive(false);
            yellowLight.SetActive(true);
            currentLightState = LightState.Yellow;
            yield return Yielders.Get(trafficCrossing.lightSwitchTime);
            currentLightState = LightState.Red;
            yellowLight.SetActive(false);
            redLight.SetActive(true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetInstanceID() != GameManager.Instance.playerCarInstanceID)
            return;
        
        if (other.CompareTag("CarIA"))
        {
            switch (forceLightState)
            {
                case ForceLightState.Green:
                    trafficCrossing.ForceStateUpdate(streetNumber);
                    break;
                case ForceLightState.Yellow:
                    trafficCrossing.ForceStateUpdate(streetNumber);
                    break;
                case ForceLightState.Red:
                    trafficCrossing.ForceStateUpdate(streetNumber);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CarIA") && other.transform.GetInstanceID() == GameManager.Instance.playerCarInstanceID)
        {
            print("El auto salió en " + currentLightState);
            float angle = Vector3.Angle(transform.forward, other.transform.forward);
            if (angle > trafficCrossing.exitAngleThreshold)//El auto no salió por el frente o salió en la dirección incorrecta
            {
                trafficCrossing.badAngleOnExitEvent?.Invoke();
            }

            switch (currentLightState)
            {
                case LightState.Green:
                    trafficCrossing.exitOnGreenLightEvent?.Invoke();
                    break;
                case LightState.Yellow:
                    trafficCrossing.exitOnYellowLightEvent?.Invoke();
                    break;
                case LightState.Red:
                    trafficCrossing.exitOnRedLightEvent?.Invoke();
                    break;
            }
        }
    }
}
