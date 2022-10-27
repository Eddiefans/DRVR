using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PedestrianDetector : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] componentsToDiableOnPedestrianHit;
    public UnityEvent pedestrianHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pedestrian") || other.CompareTag("PedestrianL5"))
        {
            pedestrianHit?.Invoke();
            print("Atropellaste un peatón");
           // transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //GameManager.Instance.gameSpeed = 0;
            foreach (var c in componentsToDiableOnPedestrianHit)
            {
                c.enabled = false;
            }
        }
    }
}
