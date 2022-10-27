using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Manager3 : MonoBehaviour
{
    [SerializeField] private GameObject actionScreen = null;
    [SerializeField] private GameObject menuScreen = null;
    void Awake(){
      
    }
    void Start()
    {
        actionScreen.SetActive(false);
      menuScreen.SetActive(true);
    }

    void Update()
    {
        
    }


    public void cargaLeccionP(){
        int aux = ExamNumber.leccionNumber;
        ExamNumber.leccionNumber = 0;
        ExamNumber.feedbackP = false;
        switch(aux){
            case 1:{
                SceneManager.LoadScene("MenuSeleccion1");
                break;
            }
            case 2:{
                SceneManager.LoadScene("MenuSeleccion2");
                break;
            }
            case 3:{
                SceneManager.LoadScene("MenuSeleccion3");
                break;
            }
            case 4:{
                SceneManager.LoadScene("MenuSeleccion4");
                break;
            }
            case 5:{
                SceneManager.LoadScene("MenuSeleccion5");
                break;
            }
            case 6:{
                SceneManager.LoadScene("MenuSeleccion6");
                break;
            }
            default:{
                break;
            }
        }
    }


    public void accionLeccion(string info){
      string [] separados = info.Split(',');
      if(UserInfo.leccionPuntaje[Int32.Parse(separados[1])-1] != -1){
        actionScreen.SetActive(true);
        menuScreen.SetActive(false);
        ExamNumber.leccionNumber = Int32.Parse(separados[1]);
      }else{
        SceneManager.LoadScene(separados[0]);
      }
    }

    public void regresarP(){
      actionScreen.SetActive(false);
      menuScreen.SetActive(true);
      ExamNumber.leccionNumber = 0;
    }


    public void cargaRetroalimentacion(){
      ExamNumber.feedbackP = true;
      ExamNumber.examNumber = ExamNumber.leccionNumber;
      ExamNumber.leccionNumber = 0;
      SceneManager.LoadScene("retro");
    }

}
