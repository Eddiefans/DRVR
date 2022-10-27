using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]
public class CarMovement : MonoBehaviour
{   
    public InputManager im;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float strenghtCoefficient = 10000f;
    public float maxTurn = 20f;
    public Transform cm;
    public Rigidbody rb;
    //public LightingManager lm;
    public float brakeStrenght;
    /*public UIManager uim;
    public List<GameObject> tailLights;
    public GameObject windowFront;
    public GameObject destroyedWindowFront;
    public Transform place;
    private MeshDestroy start;
    public AudioSource breakingGlass;
    public AudioSource engineSound, turnOnSound, bienHecho, felicidades;

    private IEnumerator coroutine_s1, coroutine_s2, coroutine_s3, coroutine_s4, felicitacion, coroutine_cambios;
    private bool s1Finished, s2Finished, s3Finished, s4inished, sCambiosFinished;
 */




    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

        if(cm){
            rb.centerOfMass = cm.localPosition;
        }
    }

    private void Awake() {
        
    }

    void Update(){}

        
    


    // Update is called once per frame
    void FixedUpdate()
    {
        //if(bEncender){
            foreach (WheelCollider wheel in throttleWheels)
            {
                if(im.brake){
                    detenerCarro();
                }else{
                    
                        wheel.motorTorque = strenghtCoefficient * Time.deltaTime * im.throttle;
                        wheel.brakeTorque = 0f;
                    
                }
            }

            foreach (WheelCollider wheel in steeringWheels)
            {
                wheel.steerAngle = maxTurn * im.steer;
            }

    }

    public void detenerCarro(){
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = 0f;
            wheel.brakeTorque = brakeStrenght;
        }
    }

 
}