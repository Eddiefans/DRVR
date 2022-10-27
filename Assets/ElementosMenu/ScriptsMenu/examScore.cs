using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;

public class examScore : MonoBehaviour
{


    public Image BarraExamen;
    public Text scoreExamenText;
    public int leccion;

    public void Start(){
    }
    
    public void Update(){
        if(UserInfo.examPuntaje[leccion-1]==-1 || UserInfo.examPuntaje[leccion-1]== null){
            BarraExamen.fillAmount = 0;
            scoreExamenText.text = "0%";
        }else{
            BarraExamen.fillAmount = (UserInfo.examPuntaje[leccion-1] / 100f);
            scoreExamenText.text = UserInfo.examPuntaje[leccion-1] + "%";
            if(UserInfo.examPuntaje[leccion-1] >= 90){
                BarraExamen.GetComponent<Image>().color = new Color32(0, 255, 171, 156);
            }
        }
            

        
    }

}