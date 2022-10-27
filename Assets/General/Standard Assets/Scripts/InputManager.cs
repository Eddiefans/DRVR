using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class InputManager : MonoBehaviour
{
    public float throttle;
    public float steer;
    public bool l, brake, turnedOn, cinturon, clutch, v1, v2, v3, v4, v5, vR, direDer, direIzq, brakeNormal, lockButton;

    public bool restart, returned;
    
    LogitechGSDK.LogiControllerPropertiesData properties;
    public float xAxes, GasInput, BrakeInput, ClutchInput;
    public bool isInGear = false;
    public int CurrentGear;
    
    private void Start() {

        LogitechGSDK.LogiSteeringInitialize(false);
        properties.wheelRange = 900;
        properties.forceEnable = true;
        LogitechGSDK.LogiPlayConstantForce(0, 50);
        LogitechGSDK.LogiPlayCarAirborne(0);
        
    }
    // Update is called once per frame
    void Update()
    {
        if(LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0)){
            LogitechGSDK.LogiPlayCarAirborne(0);
            LogitechGSDK.LogiPlayCarAirborne(0);
            LogitechGSDK.LogiPlayConstantForce(0, 50);
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);
            HShifter(rec);
            if(!lockButton)CheckButtons(rec);
            xAxes = Map(rec.lX, -32768, 32768, -1.3F, 1.3f);
            //xAxes = (rec.lX + 32768F) / 65535F;
            if(rec.lY > 0){
                GasInput =  32768F - rec.lY ;
            }else if(rec.lY < 0){
                GasInput = (rec.lY * -1) + 32768F;
            }
            if(rec.lRz > 0){
                BrakeInput =   32768F - rec.lRz  ;
            }else if(rec.lRz < 0){
                BrakeInput = (rec.lRz * -1) + 32768F;
            }
            if(rec.rglSlider[0] > 0){
                ClutchInput =   32768F - rec.rglSlider[0]  ;
            }else if(rec.rglSlider[0] < 0){
                ClutchInput = (rec.rglSlider[0] * -1) + 32768F;
            }
            if(ClutchInput > 32768f){
                clutch = true;
            }else{
                clutch = false;
            }
        }else{
            print("No Steering Wheel Connected!");
        }
        if(CurrentGear == 0){
            throttle = 0;
        }else if(CurrentGear == 6){
            throttle = (GasInput/-65536);
        }else{
            throttle = (GasInput/65536);
        }
        /*
        switch (CurrentGear)
        {
            case 1:{
                throttle = (GasInput/65536)/2;
                break;
            }
            case 2:{
                throttle = (GasInput/65536);
                break;
            }
            case 3:{
                throttle = (GasInput/65536)*1.5F;
                break;
            }
            case 4:{
                throttle = (GasInput/65536)*2;
                break;
            }
            case 5:{
                throttle = (GasInput/65536)*10F;
                break;
            }
            case 6:{
                throttle = (GasInput/-65536);
                break;
            }
            case 0:{
                if(throttle > 0){
                    throttle = throttle - .001F;
                }
                if(throttle < 0){
                    throttle = throttle + .001F;
                }
                break;
            }
            default:{
                break;
            }
        }*/
        if(BrakeInput > 15000){
            BrakeInput = 15000;
        }
        if(BrakeInput < 10){
            brakeNormal = false;
        }else{
            brakeNormal = true;
        }
        /* if(GasInput == 0){
            if(throttle > 0){
                throttle = throttle - (throttle*(BrakeInput/15000));
            }

        } */
        steer = xAxes;
        
        
        /*
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
        */
    }

    private static float Map(float x, float in_min, float in_max, float out_min, float out_max) {
      return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    void HShifter(LogitechGSDK.DIJOYSTATE2ENGINES shifter){
        isInGear = false;
        for (int i = 0; i < 128; i++)
        {
            if(shifter.rgbButtons[i] == 128){
                //if(ClutchInput > 32768f){
                    if(i == 12){
                        CurrentGear = 1;
                        v1 = false;
                        v2 = false;
                        v3 = false;
                        v4 = false;
                        v5 = false;
                        vR = false;
                        v1 = true;
                    }else if(i == 13){
                        CurrentGear = 2;
                        v1 = false;
                        v2 = false;
                        v3 = false;
                        v4 = false;
                        v5 = false;
                        vR = false;
                        v2 = true;
                    }else if(i == 14){
                        CurrentGear = 3;
                        v1 = false;
                        v2 = false;
                        v3 = false;
                        v4 = false;
                        v5 = false;
                        vR = false;
                        v3  = true;
                    }else if(i == 15){
                        CurrentGear = 4;
                        v1 = false;
                        v2 = false;
                        v3 = false;
                        v4 = false;
                        v5 = false;
                        vR = false;
                        v4 = true;
                    }else if(i == 16){
                        CurrentGear = 5;
                        v1 = false;
                        v2 = false;
                        v3 = false;
                        v4 = false;
                        v5 = false;
                        vR = false;
                        v5 = true;
                    }else if(i == 17 || i == 11){
                        CurrentGear = 6;
                        v1 = false;
                        v2 = false;
                        v3 = false;
                        v4 = false;
                        v5 = false;
                        vR = true;
                    }else{
                        
                        CurrentGear = 0;
                    }
                //}
                isInGear = true;
                
            }
        }
        if(ClutchInput > 32768f && !isInGear){
            CurrentGear = 0;
            v1 = false;
            v2 = false;
            v3 = false;
            v4 = false;
            v5 = false;
            vR = false;
        }
        /* if(!isInGear){
            CurrentGear = 0;
            v1 = false;
            v2 = false;
            v3 = false;
            v4 = false;
            v5 = false;
            vR = false;
        } */
    }

    void CheckButtons(LogitechGSDK.DIJOYSTATE2ENGINES shifter){

        for (int i = 0; i < 128; i++)
        {
            if(shifter.rgbButtons[i] == 128){

                if(i == 0){
                    StartCoroutine(prenderCor());
                    StartCoroutine(lockButtons());
                }else if(i == 1){
                    brake = !brake;
                }else if(i == 2){
                    StartCoroutine(cinturonCor());
                    StartCoroutine(lockButtons());
                }else if(i == 3){
                    StartCoroutine(lucesCor());
                    StartCoroutine(lockButtons());
                }else if(i == 8){
                    Debug.Log("entre a restart");
                    StartCoroutine(restartCor());
                    StartCoroutine(lockButtons());
                }else if(i == 9){
                    StartCoroutine(returnedCor());
                    StartCoroutine(lockButtons());
                }
            }
        }
    }

    IEnumerator restartCor(){
        restart = !restart;
        yield return new WaitForSeconds(0.1f);
        restart = !restart;
    }

    IEnumerator returnedCor(){
        returned = !returned;
        yield return new WaitForSeconds(0.1f);
        returned = !returned;
    }
    
    IEnumerator prenderCor(){
        turnedOn = !turnedOn;
        Debug.Log("jajaja");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("jsjsjs");
        turnedOn = !turnedOn;
    }

    IEnumerator cinturonCor(){
        cinturon = !cinturon;
        Debug.Log("jajaja");
        yield return new WaitForSeconds(0.01f);
        Debug.Log("jsjsjs");
        cinturon = !cinturon;
    }

    IEnumerator lucesCor(){
        l = !l;
        Debug.Log("jajaja");
        yield return new WaitForSeconds(0.005f);
        Debug.Log("jsjsjs");
        l = !l;
    }

    IEnumerator lockButtons(){
        lockButton = true;
        yield return new WaitForSeconds(1f);
        lockButton = false;
    }
}
