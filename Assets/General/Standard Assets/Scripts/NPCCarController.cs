using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]
public class NPCCarController : MonoBehaviour
{   
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float strenghtCoefficient = 10000f;
    public float maxTurn = 20f;
    public Transform cm;
    public Rigidbody rb;
    public LightingManager lm;
    public float brakeStrenght;
    public UIManager uim;
    public List<GameObject> tailLights;
    public GameObject mainCar;
    public Transform pointer;
    public float dist;
    public GameObject safeZone1, safeZone2, safeZone3;
    


    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();

        if(cm){
            rb.centerOfMass = cm.localPosition;
        }
    }

    void Update()
    {
        /* if(im.l){
            lm.ToggleHeadlights();
        }

        foreach (GameObject tl in tailLights)
        {
            tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", im.brake ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
        }
        
        uim.changeText(transform.InverseTransformVector(rb.velocity).z); */
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isHit = false;
        //dist = Vector3.Distance(pointer.position, mainCar.transform.position);
        foreach (WheelCollider wheel in throttleWheels)
        {
            RaycastHit hit1, hit2, hit3;
            if(Physics.Raycast(safeZone1.transform.position, safeZone1.transform.forward, out hit1, dist)){
                isHit = true;
            }else{
                if(Physics.Raycast(safeZone2.transform.position, safeZone2.transform.forward, out hit2, dist)){
                    isHit = true;
                }else{
                    if(Physics.Raycast(safeZone3.transform.position, safeZone3.transform.forward, out hit3, dist)){
                        isHit = true;
                    }else{
                        isHit = false;
                    } 
                }
            }

            /* if(Physics.Raycast(safeZone2.transform.position, safeZone2.transform.forward, out hit2, dist)){

                    isHit = true;

            }else{
                isHit = false;
            }
            if(Physics.Raycast(safeZone3.transform.position, safeZone3.transform.forward, out hit3, dist)){

                    isHit = true;

            }else{
                isHit = false;
            } */

            if(isHit){
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeStrenght;
            }else{
                wheel.motorTorque = strenghtCoefficient * Time.deltaTime;
                wheel.brakeTorque = 0f;
            }
            /* if(transform.InverseTransformVector(rb.velocity).z > 10){
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeStrenght;
            }else{
                wheel.motorTorque = strenghtCoefficient * Time.deltaTime;
                wheel.brakeTorque = 0f;
            } */
        }


        foreach (WheelCollider wheel in steeringWheels)
        {
            /* wheel.steerAngle = maxTurn * im.steer; */
            
            /* do{
                wheel.steerAngle = maxTurn ;
            }while (Vector3.Distance(pointer.position, mainCar.transform.position) < dist); */
            /* transform.position = Vector3.MoveTowards(transform.position, mainCar.transform.position, .03f); */
            transform.LookAt(mainCar.transform);
            /* do{
                wheel.steerAngle = -maxTurn ;
            }while (Vector3.Distance(pointer.position, mainCar.transform.position) < dist); */
            
        }

    }
}
