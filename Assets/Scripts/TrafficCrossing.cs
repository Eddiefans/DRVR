using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrafficCrossing : MonoBehaviour
{
    public enum State
    {
        Street1,
        Street2,
        Street3,
        Street4
    }

    public State currentCrossingState;
    public float lightSwitchTime = 2;
    public float exitAngleThreshold = 30;
    public UnityAction<int> StateChange;
    public UnityEvent exitOnRedLightEvent;
    public UnityEvent exitOnYellowLightEvent;
    public UnityEvent exitOnGreenLightEvent;
    public UnityEvent badAngleOnExitEvent;
    
    [SerializeField] private byte streetAmount = 3;
    [SerializeField] private int crossingStateChangeTime = 5;
    [SerializeField] private byte currentState = 0;
    [SerializeField] private byte stateForceSleepTime = 5;
    
    private bool sleep;
    
    private State CurrentCrossingState
    {
        get => currentCrossingState;
        set
        {
            currentCrossingState = value;
            StateChange?.Invoke((int)value);
        }
    }

    void Start()
    {
        StartCoroutine(UpdateState());
    }

    IEnumerator UpdateState()
    {
        while (enabled)
        {
            if (!sleep)
            {
                if (currentState < streetAmount-1)
                {
                    currentState++;
                }
                else
                {
                    currentState = 0;
                }
                CurrentCrossingState = (State) currentState;
            }
            yield return Yielders.Get(crossingStateChangeTime);
        }
    }
    
    public void ForceStateUpdate(byte newState)
    {
        currentState = newState;
        CurrentCrossingState = (State) currentState;
        sleep = true;
        Invoke("AwakeAfterForcedState", stateForceSleepTime);
    }

    void AwakeAfterForcedState()
    {
        sleep = false;
    }

    public void OnGreenLight()
    {
        print("El auto pasó en luz verde");
    }
    public void OnYellowLight()
    {
        print("El auto pasó en luz amarilla");
    }
    public void OnRedLight()
    {
        print("El auto pasó en luz roja");
    }

    public void OnBadAngle()
    {
        print("El auto salió en un ángulo no adecuado");
    }
}
