using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System;


public class leccionesTeoricasCheck : MonoBehaviour
{


    public void Awake(){
    }

    public Image check1;
    public Image check2;
    public Image check3;
    public Image check4;
    public Image check5;
    public Image check6;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Image  block2;
    public Image  block3;
    public Image  block4;
    public Image  block5;
    public Image  block6;


  


    void Start()
    {
      if (UserInfo.examPuntaje[0] >= 90){
        check1.gameObject.SetActive(true);
        block2.gameObject.SetActive(false);
        button2.enabled = true;
      }else{
        check1.gameObject.SetActive(false);
        block2.gameObject.SetActive(true);
        button2.enabled = false;
      }

      if (UserInfo.examPuntaje[1] >= 90){
        check2.gameObject.SetActive(true);
        block3.gameObject.SetActive(false);
        button3.enabled = true;
      }else{
        check2.gameObject.SetActive(false);
        block3.gameObject.SetActive(true);
        button3.enabled = false;
      }

      if (UserInfo.examPuntaje[2] >= 90){
        check3.gameObject.SetActive(true);
        block4.gameObject.SetActive(false);
        button4.enabled = true;
      }else{
        check3.gameObject.SetActive(false);
        block4.gameObject.SetActive(true);
        button4.enabled = false;
      }

      if (UserInfo.examPuntaje[3] >= 90){
        check4.gameObject.SetActive(true);
        block5.gameObject.SetActive(false);
        button5.enabled = true;
      }else{
        check4.gameObject.SetActive(false);
        block5.gameObject.SetActive(true);
        button5.enabled = false;
      }

      if (UserInfo.examPuntaje[4] >= 90){
        check5.gameObject.SetActive(true);
        block6.gameObject.SetActive(false);
        button6.enabled = true;
      }else{
        check5.gameObject.SetActive(false);
        block6.gameObject.SetActive(true);
        button6.enabled = false;
      }

      if (UserInfo.examPuntaje[5] >= 90){
        check6.gameObject.SetActive(true);
      }else{
        check6.gameObject.SetActive(false);
      }

    }
    void Update()
    {

    }
}
