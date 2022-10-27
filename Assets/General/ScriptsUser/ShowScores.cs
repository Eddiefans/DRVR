using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;

public class ShowScores : MonoBehaviour
{

    public Image BarraPractica;
    public Text scorePracticoText;
    public Image BarraTeorica;
    public Text scoreTeoricoText; 
    public Text scoreFinal;
    public Text message;
    public Button buttonPractico;
    public Image lockPractico;

    public void Start(){
    }
    
    public void Update(){
        float[] puntajes =  {0f, 0f, 0f, 0f, 0f, 0f};
        float puntaje = 0;
        bool terminadoP = false, terminadoT = false;
        for(int i = 0; i < 6; i++){
            if(UserInfo.examPuntaje[i]!=-1 && UserInfo.examPuntaje[i]!= null){
                puntajes[i] = UserInfo.examPuntaje[i];
            }
        }
        float[] puntajesP =  {0f, 0f, 0f, 0f, 0f, 0f};
        float puntajeP = 0;
        for(int i = 0; i < 6; i++){
            if(UserInfo.leccionPuntaje[i]!=-1 && UserInfo.leccionPuntaje[i]!= null){
                puntajesP[i] = UserInfo.leccionPuntaje[i];
            }
        }
        puntaje = puntajes[0]*.15f + puntajes[1]*.15f + puntajes[2]*.15f + puntajes[3]*.15f + puntajes[4]*.15f + puntajes[5]*.25f;
        puntajeP = puntajesP[0]*.15f + puntajesP[1]*.15f + puntajesP[2]*.15f + puntajesP[3]*.15f + puntajesP[4]*.15f + puntajesP[5]*.25f;
        UserInfo.puntajeTeorica = (int)puntaje;
        UserInfo.puntajePractica = (int)puntajeP;
        UserInfo.puntajeFinal = (UserInfo.puntajePractica + UserInfo.puntajeTeorica) / 2;

        //falta tener examen final aprobado
        BarraTeorica.fillAmount = (UserInfo.puntajeTeorica / 100f);
        scoreTeoricoText.text = UserInfo.puntajeTeorica + "%";
        if(UserInfo.puntajeTeorica  >= 90 && UserInfo.examPuntaje[5] >=90 && UserInfo.examPuntaje[4] >=90 && UserInfo.examPuntaje[3] >=90 && UserInfo.examPuntaje[2] >=90 && UserInfo.examPuntaje[1] >=90 && UserInfo.examPuntaje[0] >=90){
            BarraTeorica.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            terminadoT = true;
        }
        
        BarraPractica.fillAmount = (UserInfo.puntajePractica / 100f);
        scorePracticoText.text = UserInfo.puntajePractica + "%";
        if(UserInfo.puntajePractica  >= 80 && UserInfo.leccionPuntaje[5] >=80 && UserInfo.leccionPuntaje[4] >=80 && UserInfo.leccionPuntaje[3] >=80 && UserInfo.leccionPuntaje[2] >=80 && UserInfo.leccionPuntaje[1] >=80 && UserInfo.leccionPuntaje[0] >=80){
            BarraPractica.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            terminadoP = true;
        }

        scoreFinal.text = UserInfo.puntajeFinal + "%";
        message.text = "Estás cerca";
        if(UserInfo.puntajeFinal > 79 && UserInfo.puntajeFinal < 90){
            message.gameObject.SetActive(true);
            message.text = "Estás cerca";
        }else if(UserInfo.puntajeFinal > 89 && terminadoT && terminadoP){
            message.gameObject.SetActive(true);
            message.text = "Sistema aprobado";
        }else{
            message.gameObject.SetActive(false);
        }


        if(terminadoT){
            buttonPractico.enabled = true;
            lockPractico.gameObject.SetActive(false);
        }else{
            buttonPractico.enabled = false;
            lockPractico.gameObject.SetActive(true);
        }
    }

}