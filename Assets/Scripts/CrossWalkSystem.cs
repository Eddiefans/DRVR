using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrossWalkSystem : MonoBehaviour
{
    public enum PedestrianStatus
    {
        None,
        Waiting
    }

    public PedestrianStatus pedestrianStatus;

    public enum CarStatus
    {
        None, 
        Passing,
        Waiting
    }

    public CarStatus carStatus;
    
    public UnityEvent FailToWaitForPedestrianEvent;
}
