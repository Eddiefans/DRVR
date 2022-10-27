    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class ManagerLeccion : MonoBehaviour
{


    // Start is called before the first frame update

    void Start()
    {
      
    }
    void Update()
    {
      
    }
    public void cargaLeccion(string info){
      string [] separados = info.Split(',') ;
      SceneManager.LoadScene(separados[0]);
      if(separados[1] == "0" && separados[2] == "0"){
        if(separados.Length>3){
          ExamNumber.examNumber = Int32.Parse(separados[3]);
        }
        
      }  
    }
}