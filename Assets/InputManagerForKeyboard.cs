using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerForKeyboard : MonoBehaviour
{
    public float throttle;
    public float steer;
    public bool l, brake, turnedOn, cinturon, clutch, v1, v2, v3, v4, v5, vR, direDer, direIzq, restart, returned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        l = Input.GetKeyDown(KeyCode.L);
        direDer = Input.GetKeyDown(KeyCode.P);
        direIzq = Input.GetKeyDown(KeyCode.O);
        brake = Input.GetKey(KeyCode.Space);
        turnedOn = Input.GetKey(KeyCode.Q);
        cinturon = Input.GetKey(KeyCode.Z);
        clutch = Input.GetKey(KeyCode.C);
        v1 = Input.GetKey(KeyCode.Alpha1);
        v2 = Input.GetKey(KeyCode.Alpha2);
        v3 = Input.GetKey(KeyCode.Alpha3);
        v4 = Input.GetKey(KeyCode.Alpha4);
        v5 = Input.GetKey(KeyCode.Alpha5);
        restart = Input.GetKeyDown(KeyCode.M);
        returned = Input.GetKeyDown(KeyCode.N);
    }
}
