using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogitechExmaple : MonoBehaviour
{

    LogitechGSDK.LogiControllerPropertiesData properties;
    public float xAxes, GasInput, BreakInput, ClutchInput;
    public bool isInGear = false;
    public int CurrentGear;
    private void Start(){
        print (LogitechGSDK.LogiSteeringInitialize(false));
    }
    private void Update() {
        if(LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0)){
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);
            HShifter(rec);
            xAxes = rec.lX / 32768F;
            if(rec.lY > 0){
                GasInput =  32768F - rec.lY ;
            }else if(rec.lY < 0){
                GasInput = (rec.lY * -1) + 32768F;
            }
            if(rec.lRz > 0){
                BreakInput =   32768F - rec.lRz  ;
            }else if(rec.lRz < 0){
                BreakInput = (rec.lRz * -1) + 32768F;
            }
            if(rec.rglSlider[0] > 0){
                ClutchInput =   32768F - rec.rglSlider[0]  ;
            }else if(rec.rglSlider[0] < 0){
                ClutchInput = (rec.rglSlider[0] * -1) + 32768F;
            }
        }else{
            print("No Steering Wheel Connected!");
        } 
    }
    void HShifter(LogitechGSDK.DIJOYSTATE2ENGINES shifter){
        isInGear = false;
        for (int i = 0; i < 128; i++)
        {
            if(shifter.rgbButtons[i] == 128){
                if(ClutchInput > 32768f){
                    if(i == 12){
                        CurrentGear = 1;
                    }else if(i == 13){
                        CurrentGear = 2;
                    }else if(i == 14){
                        CurrentGear = 3;
                    }else if(i == 15){
                        CurrentGear = 4;
                    }else if(i == 16){
                        CurrentGear = 5;
                    }else if(i == 17){
                        CurrentGear = 6;
                    }else if(i == 11){
                        CurrentGear = -1;
                    }else{
                        CurrentGear = 0;
                    }
                }
                isInGear = true;
            }
        }
        if(ClutchInput > 32768f && !isInGear){
            CurrentGear = 0;
        }
    }
}
