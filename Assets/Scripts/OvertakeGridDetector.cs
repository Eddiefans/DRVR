using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertakeGridDetector : MonoBehaviour
{
    [SerializeField] private OvertakeController overtakeController;
    [SerializeField] private string id;
    
    void Start()
    {
        id = transform.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarIA"))
        {
            VehicleAiController aiController = other.GetComponent<VehicleAiController>();
            if(aiController != null)
                overtakeController.AddIdToList(id);
        }
    }
}
