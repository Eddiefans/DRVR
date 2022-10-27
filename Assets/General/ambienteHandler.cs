using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambienteHandler
{
    private static int ambiente;

    public static void setAmbiente(int am){
       
        ambiente = am;
        if(ambiente == am){
            Debug.Log("set");
        }
    }

    public static int getAmbiente(){
        /* int aux = ambiente; 
        ambiente = 0;  */
        return ambiente;
    }
}
