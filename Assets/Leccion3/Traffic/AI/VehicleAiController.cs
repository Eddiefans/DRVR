using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VehicleAiController : MonoBehaviour{
    
    public List<WheelCollider> wheelsX;
    public float totalPower, maxVelocity;
    public float vertical , horizontal ;
    public carNode currentNode;
    public GameObject safeZone1, safeZone2, safeZone3;
    public float dist, dist2, dist3;
    public long brakeStrenght = 999999999999;
    public GameObject car;
    public bool isBeeingHit, bForceVelocity, bCarForSeccion1p2, bIsBraking;
    public Rigidbody rb;
    public List<GameObject> tailLights;
    public bool hasVehicleInFront;
    public bool OnRoundabout;
    public bool vehicleOnRoundabout;
    
    [SerializeField] private bool onRedLight;
    
    private float radius = 8 , distance;
    private Vector3 velocity ,Destination, lastPosition;
    private bool forceMaxVelocity;
    private float time;
    // [SerializeField] private  int layerMask = 1<<9;
    [SerializeField] private LayerMask layerMask;
    

    void Start(){
        if(!bForceVelocity)
            StartCoroutine(checkCarNode());
        
        rb.GetComponent<Rigidbody>();

        // layerMask = ~layerMask;
    }

    void FixedUpdate(){
        foreach (GameObject tl in tailLights)
        {
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
        
        if(!bIsBraking) throttleVehicle();
        drawLines();
        
        try{
            checkDistance();
            steerVehicle();
        }
        catch{}
        
    }

    private void drawLines(){
        Vector3 forward = safeZone1.transform.TransformDirection(Vector3.forward) * dist;
        Debug.DrawRay(safeZone1.transform.position, forward, Color.green);
        
        Vector3 forward2 = safeZone2.transform.TransformDirection(Vector3.forward) * dist2;
        Debug.DrawRay(safeZone2.transform.position, forward2, Color.red);

        Vector3 forward3 = safeZone3.transform.TransformDirection(Vector3.forward) * dist3;
        Debug.DrawRay(safeZone3.transform.position, forward3, Color.blue);
    }


    public void throttleVehicle(){
        bool isHit = false;
        rb.drag = 0.1f;

        foreach (var item in wheelsX){
            RaycastHit hit1 = new RaycastHit();
            RaycastHit hit2 = new RaycastHit();
            RaycastHit hit3 = new RaycastHit();
            if(Physics.Raycast(safeZone1.transform.position, safeZone1.transform.TransformDirection(Vector3.forward), out hit1, dist,layerMask)){
                isHit = true;
            }else{
                if(Physics.Raycast(safeZone2.transform.position, safeZone2.transform.TransformDirection(Vector3.forward), out hit2, dist2,layerMask)){
                    isHit = true;
                }else{
                    if(Physics.Raycast(safeZone3.transform.position, safeZone3.transform.TransformDirection(Vector3.forward), out hit3, dist3,layerMask)){
                        isHit = true;
                    }else{
                        isHit = false;
                    } 
                }
            }
          
            isBeeingHit = isHit;
            if(isHit){
                //Debug.DrawLine(safeZone1.transform.position, hit1.point);
                item.motorTorque = 0f;
                item.brakeTorque = brakeStrenght;
                rb.drag = 100f;
            }else if (hasVehicleInFront || onRedLight || vehicleOnRoundabout)
            {
                item.motorTorque = 0;
                item.brakeTorque = brakeStrenght;
                rb.drag = 5f;
            }else if (car.GetComponent<Rigidbody>().velocity.magnitude * 3.6f > maxVelocity){
                item.motorTorque = 0f;
                item.brakeTorque = brakeStrenght;
            }else{
                item.motorTorque = totalPower;
                item.brakeTorque = 0;
            }
            
        }
    }

    IEnumerator checkCarNode()
    {
        while(true){
            string tag = currentNode.tag;
            switch (tag)
            {
                case "Destroy":{
                    Destroy(car);
                    break;
                }
                case "CarreteraVuelta":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 2000; dist = 2;}
                    break;
                }
                case "Carretera":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 5000; dist = 2;}
                    break;
                }
                case "CarreteraMax":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 10000; dist = 2;}
                    break;
                }
                case "Glorieta":{
                    totalPower = 35;
                    if(!forceMaxVelocity){ 
                        maxVelocity = 10; 
                        dist = 2;
                    }
                    break;
                }
                case "lento":{
                    totalPower = 35;
                    if(!forceMaxVelocity){ 
                        maxVelocity = 10; 
                        dist = 3;
                    }
                    break;
                }
                case "pesado":{
                    totalPower = 150;
                    if(!forceMaxVelocity){ 
                        maxVelocity = 30; 
                        dist = 3;
                    }
                    break;
                }
                case "CambioR":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 10; dist = 3; dist2 = 8;}
                    break;
                }
                case "CambioL":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 10; dist = 3; dist3 = 7;}
                    break;
                }
                 case "CambioRs":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 10; dist2 = 5;}
                    break;
                }
                case "CambioLs":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 10; dist3 = 4;}
                    break;
                }
                  case "retorno":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 5; dist = 1;}
                    break;
                }
                case "v20":{
                    //totalPower = 35;
                    if(!forceMaxVelocity){ maxVelocity = 20; dist = 2;}
                    break;
                }
                case "turbo":{
                    totalPower = 200;
                    if(!forceMaxVelocity){ maxVelocity = 100000;
                    dist = 3;}
                    break;
                }
                 case "turboMax":{
                    totalPower = 2000;
                    if(!forceMaxVelocity){ maxVelocity = 100000000;
                    dist = 3;}
                    break;
                }
                 case "turboMaxXXX":{
                    totalPower = 80000000000000;
                    if(!forceMaxVelocity){ maxVelocity = 10000000000000000000;
                    dist = 3;}
                    break;
                }
                case "cIzq":{
                    //totalPower = 100;
                    if(!forceMaxVelocity){ 
                        maxVelocity = 60;
                        dist = 3;
                    }
                    break;
                }
                case "cDer":{
                    //totalPower = 80;
                    if(!forceMaxVelocity){ 
                        maxVelocity = 30;
                        dist = 2;
                    }
                    break;
                }
                case "cCentral":{
                    //totalPower = 60;
                    if(!forceMaxVelocity){ 
                        maxVelocity = 50;
                        dist = 2;
                    }
                    break;
                }
                default:{
                    //totalPower = 70;
                    if(!forceMaxVelocity){ maxVelocity = 20;
                    dist = 2;
                    dist2 = 1;
                    dist3= 1;}
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        
        
    }

    void checkDistance(){

            if(Vector3.Distance(transform.position , currentNode.transform.position) <= 3){
                reachedDestination();
            }
    }

        
    private void reachedDestination(){
        if(currentNode.gameObject.tag == "reset" && bForceVelocity){
            StartCoroutine(checkCarNode());
            bForceVelocity = false;
        }
       
        if(currentNode.nextWaypoint == null && currentNode.link == null){

            Destroy(car);
        }else{
            if(currentNode.link != null && Random.Range(0 , 100) <= 30 && !bCarForSeccion1p2){
                currentNode = currentNode.link;
                maxVelocity = 15;
                forceMaxVelocity = true;
            }else{
                currentNode = currentNode.nextWaypoint;
                forceMaxVelocity = false;
            }
        }
    }


    private void steerVehicle(){

        Vector3 relativeVector = transform.InverseTransformPoint(currentNode.transform.position);
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x  / relativeVector.magnitude) * 2;
        horizontal = newSteer;
       

        if (horizontal > 0 ) {
            wheelsX[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            wheelsX[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        } else if (horizontal < 0 ) {                                                          
            wheelsX[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            wheelsX[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
        } else {
            wheelsX[0].steerAngle =0;
            wheelsX[1].steerAngle =0;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(currentNode != null)
            Gizmos.DrawSphere(currentNode.transform.position ,0.5f);
        //Gizmos.DrawRay(safeZone1.transform.position, safeZone1.transform.forward * dist);
    }

    public void detenerCarro(){
        foreach (GameObject tl in tailLights)
        {
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.5f, 0.111f, 0.111f) );
        }
        foreach (var item in wheelsX){
            
            item.motorTorque = 0f;
            item.brakeTorque = brakeStrenght;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TrafficLightController"))
        {
            float angle = Vector3.Angle(transform.forward, other.transform.forward);
            TrafficLight trafficLight = other.GetComponent<TrafficLight>();
            if (trafficLight.currentLightState == TrafficLight.LightState.Red && angle < 30)
            {
                onRedLight = true;   
            }
            else
            {
                onRedLight = false;
            }
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Roundabout"))
    //     {
    //         OnRoundabout = true;
    //     }
    // }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TrafficLightController"))
        {
            onRedLight = false;
        }
        
        // if (other.CompareTag("Roundabout"))
        // {
        //     OnRoundabout = false;
        // }
    }
}
