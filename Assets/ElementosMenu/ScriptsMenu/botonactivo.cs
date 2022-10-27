using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System;


public class botonactivo : MonoBehaviour
{


    public void Awake(){
    }

    public GameObject imagen;
    public Button boton;
    public Color cambio;
    public int leccion;


    // Start is called before the first frame update
    void Start()
    {
      bool completado = true;
      for(int i = 0; i < UserInfo.videosArray[leccion-1].Count; i++){
         if(!UserInfo.videosArray[leccion-1][i].watched){
            completado = false;
         }
      }
      if(completado){
         ColorBlock cb = boton.colors;
            cb.normalColor = cambio;
            cb.pressedColor = cambio;

            boton.colors = cb;
            imagen.gameObject.SetActive(false);
            boton.enabled = true;
      }else{
         imagen.gameObject.SetActive(true);
           boton.enabled = false;
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
