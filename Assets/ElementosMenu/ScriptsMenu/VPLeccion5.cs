using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VPLeccion5 : MonoBehaviour
{
    public VideoPlayer vp;
    [SerializeField] private Image pausa;
    [SerializeField] private Image reanudar;
    [SerializeField] private Text textVideo;
    [SerializeField] private Button adelantarB;
    [SerializeField] private Button atrasarB;
    [SerializeField] private Button salirButon;
    [SerializeField] private int video;
    private bool primer;


	void Start () {
    reanudar.gameObject.SetActive(false);
    pausa.gameObject.SetActive(true);
    textVideo.text = "Pausar";
    if(UserInfo.primerVideo == true){
      UserInfo.primerVideo = false;
      adelantarB.gameObject.SetActive(false);
      primer = true;
    }else{
      adelantarB.gameObject.SetActive(true);
      primer = false;
    }
	}


  void Update()
    {

      if(primer){
        salirButon.gameObject.SetActive(false);
      }else{
        salirButon.gameObject.SetActive(true);
      }

      if (vp.isPlaying){
        atrasarB.gameObject.SetActive(true);
        reanudar.gameObject.SetActive(false);
        pausa.gameObject.SetActive(true);
        textVideo.text = "Pausar";
      }else{
          atrasarB.gameObject.SetActive(false);
          reanudar.gameObject.SetActive(true);
          pausa.gameObject.SetActive(false);
          textVideo.text = "Reanudar";
      }
      if(vp.time > vp.length-1){
        primer = false;
      }
      switch(video){
      case 5:{
        if(vp.time >= 34 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 1:{
        if(vp.time >= 103 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 3:{
        if(vp.time >= 35 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 2:{
        if(vp.time >= 74 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 4:{
        if(vp.time >= 25 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 6:{
        if(vp.time >= 53 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      default:{
        break;
      }
    }

    }
  public void atrasar(){
      switch(video){
        case 5:{
          if(vp.time <= 7){
            vp.time = 0;
          }
          if(vp.time > 7 && vp.time <= 13){
            vp.time = 5;
          }
          if(vp.time > 13 && vp.time <= 31){
            vp.time = 11;
          }
          if(vp.time > 31 && vp.time <= 36){
            vp.time = 29;
          }
          if(  vp.time > 36){
            vp.time = 34;
          }
          break;
        }
        case 1:{
          if(vp.time <= 7){
            vp.time = 0;
          }
          if(vp.time > 7 && vp.time <= 15){
            vp.time = 5;
          }
          if(vp.time > 15 && vp.time <= 26){
            vp.time = 13;
          }
          if(vp.time > 26 && vp.time <= 57){
            vp.time = 24;
          }
          if(vp.time > 57 && vp.time <= 84){
            vp.time = 55;
          }
          if(vp.time > 84 && vp.time <= 91){
            vp.time = 82;
          }
          if(vp.time > 91 && vp.time <= 105){
            vp.time = 89;
          }
          if(  vp.time > 105){
            vp.time = 103;
          }
          break;
        }
        case 3:{
          if(vp.time <= 7){
            vp.time = 0;
          }
          if(vp.time > 7 && vp.time <= 37){
            vp.time = 5;
          }
          if(  vp.time > 37){
            vp.time = 35;
          }
          break;
        }
        case 2:{
          if(vp.time <= 7){
            vp.time = 0;
          }
          if(vp.time > 7 && vp.time <= 13){
            vp.time = 5;
          }
          if(vp.time > 13 && vp.time <= 29){
            vp.time = 11;
          }
          if(vp.time > 29 && vp.time <= 51){
            vp.time = 27;
          }
          if(vp.time > 51 && vp.time <= 63){
            vp.time = 49;
          }
          if(vp.time > 63 && vp.time <= 76){
            vp.time = 61;
          }
          if(  vp.time > 76){
            vp.time = 74;
          }
          break;
        }
        case 4:{
          if(vp.time <= 7){
            vp.time = 0;
          }
          if(vp.time > 7 && vp.time <= 15){
            vp.time = 5;
          }
          if(vp.time > 15 && vp.time <= 27){
            vp.time = 13;
          }
          if(  vp.time > 27){
            vp.time = 25;
          }
          break;
        }
        case 6:{
          if(vp.time <= 7){
            vp.time = 0;
          }
          if(vp.time > 7 && vp.time <= 15){
            vp.time = 5;
          }
          if(vp.time > 15 && vp.time <= 23){
            vp.time = 13;
          }
          if(vp.time > 23 && vp.time <= 33){
            vp.time = 21;
          }
          if(vp.time > 33 && vp.time <= 43){
            vp.time = 31;
          }
          if(vp.time > 43 && vp.time <= 50){
            vp.time = 41;
          }
          if(vp.time > 50 && vp.time <= 55){
            vp.time = 48;
          }
          if(  vp.time > 55){
            vp.time = 53;
          }
          break;
        }
        default:{
          break;
        }
      }

  }
  /*
  5
  1
  3
  2
  4
  6
  */
  public void adelantar(){
    switch(video){
      case 5:{
        if(vp.time < 5){
          vp.time = 5;
        }
        if(vp.time >= 5 && vp.time < 11){
          vp.time = 11;
        }
        if(vp.time >= 11 && vp.time < 29){
          vp.time = 29;
        }
        if(vp.time >= 29 && vp.time < 34){
          vp.time = 34;
        }
        break;
      }
      case 1:{
        if(vp.time < 5){
          vp.time = 5;
        }
        if(vp.time >= 5 && vp.time < 13){
          vp.time = 13;
        }
        if(vp.time >= 13 && vp.time < 24){
          vp.time = 24;
        }
        if(vp.time >= 24 && vp.time < 55){
          vp.time = 55;
        }
        if(vp.time >= 55 && vp.time < 82){
          vp.time = 82;
        }
        if(vp.time >= 82 && vp.time < 89){
          vp.time = 89;
        }
        if(vp.time >= 89 && vp.time < 103){
          vp.time = 103;
        }
        break;
      }
      case 3:{
        if(vp.time < 5){
          vp.time = 5;
        }
        if(vp.time >= 5 && vp.time < 35){
          vp.time = 35;
        }
        break;
      }
      case 2:{
        if(vp.time < 5){
          vp.time = 5;
        }
        if(vp.time >= 5 && vp.time < 11){
          vp.time = 11;
        }
        if(vp.time >= 11 && vp.time < 27){
          vp.time = 27;
        }
        if(vp.time >= 27 && vp.time < 49){
          vp.time = 49;
        }
        if(vp.time >= 49 && vp.time < 61){
          vp.time = 61;
        }
        if(vp.time >= 61 && vp.time < 74){
          vp.time = 74;
        }
        break;
      }
      case 4:{
        if(vp.time < 5){
          vp.time = 5;
        }
        if(vp.time >= 5 && vp.time < 13){
          vp.time = 13;
        }
        if(vp.time >= 13 && vp.time < 25){
          vp.time = 25;
        }
        break;
      }
      case 6:{
        if(vp.time < 5){
          vp.time = 5;
        }
        if(vp.time >= 5 && vp.time < 13){
          vp.time = 13;
        }
        if(vp.time >= 13 && vp.time < 21){
          vp.time = 21;
        }
        if(vp.time >= 21 && vp.time < 31){
          vp.time = 31;
        }
        if(vp.time >= 31 && vp.time < 41){
          vp.time = 41;
        }
        if(vp.time >= 41 && vp.time < 48){
          vp.time = 48;
        }
        if(vp.time >= 48 && vp.time < 53){
          vp.time = 53;
        }
        break;
      }
      default:{
        break;
      }
    }
  }

  public void pausar(){
    if (!vp.isPlaying){
        vp.Play();
    }else{
        vp.Pause();
    }
  }

  public void salir(){
      SceneManager.LoadScene("menuLeccion5");
  }

}
