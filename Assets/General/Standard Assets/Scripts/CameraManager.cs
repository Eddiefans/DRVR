using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject focus;
    public float distance = 5f;
    public float height = 2f;
    public float dampening;

    public float height2 = 0f;
    public float distance2 = 0f;
    public float offset = 0f;
    
    private int camMode = 0;
    private bool isFirstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            camMode = (camMode + 1) % 2;
        }

        switch (camMode)
        {
            case 1:{
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(-offset, height2, distance2));
                transform.rotation = focus.transform.rotation;
                Camera.main.fieldOfView = 90f;
                isFirstTime = true;
                break;
            }
            case 0:{
                if(isFirstTime){
                    transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(0f, height, -distance));
                    isFirstTime = !isFirstTime;
                }
                transform.position = Vector3.Lerp(transform.position, focus.transform.position + focus.transform.TransformDirection(new Vector3(0f, height, -distance)), dampening * Time.deltaTime);
                transform.LookAt(focus.transform);
                Camera.main.fieldOfView = 60f;
                break;
            }
            default:{
                break;
            } 
    
        }       
    }
}
