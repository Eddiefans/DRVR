using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;

public class practicalScore : MonoBehaviour
{


    public Image Barra1;
    public Text score1;
    public Image Barra2;
    public Text score2;
    public Image Barra3;
    public Text score3;
    public Image Barra4;
    public Text score4;
    public Image Barra5;
    public Text score5;
    public Image Barra6;
    public Text score6;


    public void Start(){
    }
    
    public void Update(){
        if(UserInfo.leccionPuntaje[0]==-1 || UserInfo.leccionPuntaje[0]== null){
            Barra1.fillAmount = 0;
            score1.text = "0%";
        }else{
            Barra1.fillAmount = (UserInfo.leccionPuntaje[0] / 100f);
            score1.text = UserInfo.leccionPuntaje[0] + "%";
            if(UserInfo.leccionPuntaje[0] >= 80){
                Barra1.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            }
        }

        if(UserInfo.leccionPuntaje[1]==-1 || UserInfo.leccionPuntaje[1]== null){
            Barra2.fillAmount = 0;
            score2.text = "0%";
        }else{
            Barra2.fillAmount = (UserInfo.leccionPuntaje[1] / 100f);
            score2.text = UserInfo.leccionPuntaje[1] + "%";
            if(UserInfo.leccionPuntaje[1] >= 80){
                Barra2.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            }
        }

        if(UserInfo.leccionPuntaje[2]==-1 || UserInfo.leccionPuntaje[2]== null){
            Barra3.fillAmount = 0;
            score3.text = "0%";
        }else{
            Barra3.fillAmount = (UserInfo.leccionPuntaje[2] / 100f);
            score3.text = UserInfo.leccionPuntaje[2] + "%";
            if(UserInfo.leccionPuntaje[2] >= 80){
                Barra3.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            }
        }

        if(UserInfo.leccionPuntaje[3]==-1 || UserInfo.leccionPuntaje[3]== null){
            Barra4.fillAmount = 0;
            score4.text = "0%";
        }else{
            Barra4.fillAmount = (UserInfo.leccionPuntaje[3] / 100f);
            score4.text = UserInfo.leccionPuntaje[3] + "%";
            if(UserInfo.leccionPuntaje[3] >= 80){
                Barra4.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            }
        }

        if(UserInfo.leccionPuntaje[4]==-1 || UserInfo.leccionPuntaje[4]== null){
            Barra5.fillAmount = 0;
            score5.text = "0%";
        }else{
            Barra5.fillAmount = (UserInfo.leccionPuntaje[4] / 100f);
            score5.text = UserInfo.leccionPuntaje[4] + "%";
            if(UserInfo.leccionPuntaje[4] >= 80){
                Barra5.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            }
        }

        if(UserInfo.leccionPuntaje[5]==-1 || UserInfo.leccionPuntaje[5]== null){
            Barra6.fillAmount = 0;
            score6.text = "0%";
        }else{
            Barra6.fillAmount = (UserInfo.leccionPuntaje[5] / 100f);
            score6.text = UserInfo.leccionPuntaje[5] + "%";
            if(UserInfo.leccionPuntaje[5] >= 80){
                Barra6.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            }
        }
    }

}