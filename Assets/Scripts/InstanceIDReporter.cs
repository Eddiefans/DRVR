using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceIDReporter : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.playerCarInstanceID = transform.GetInstanceID();
    }
}
