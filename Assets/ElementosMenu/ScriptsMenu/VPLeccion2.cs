using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VPLeccion2 : MonoBehaviour
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
      case 3:{
        if(vp.time >= 25 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 7:{
        if(vp.time >= 58 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 6:{
        if(vp.time >= 56 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 2:{
        if(vp.time >= 91 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 5:{
        if(vp.time >= 34 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 4:{
        if(vp.time >= 67 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 1:{
        if(vp.time >= 38 || primer || !vp.isPlaying){
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
        case 3:{
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
        case 7:{
          if(vp.time <= 16){
            vp.time = 0;
          }
          if(vp.time > 16 && vp.time <= 23){
            vp.time = 14;
          }
          if(vp.time > 23 && vp.time <= 33){
            vp.time = 21;
          }
          if(vp.time > 33 && vp.time <= 50){
            vp.time = 31;
          }
          if(vp.time > 50 && vp.time <= 55){
            vp.time = 48;
          }
          if(vp.time > 55 && vp.time <= 60){
            vp.time = 53;
          }
          if(  vp.time > 60){
            vp.time = 58;
          }
          break;
        }
        case 6:{
          if(vp.time <= 6){
            vp.time = 0;
          }
          if(vp.time > 6 && vp.time <= 22){
            vp.time = 4;
          }
          if(vp.time > 22 && vp.time <= 48){
            vp.time = 20;
          }
          if(  vp.time > 48 && vp.time <= 58){
            vp.time = 46;
          }
          if(vp.time > 58){
            vp.time = 56;
          }
          break;
        }
        case 2:{
          if(vp.time <= 6){
            vp.time = 0;
          }
          if(vp.time > 6 && vp.time <= 18){
            vp.time = 4;
          }
          if(vp.time > 18 && vp.time <= 33){
            vp.time = 16;
          }
          if(vp.time > 33 && vp.time <= 49){
            vp.time = 31;
          }
          if(vp.time > 49 && vp.time <= 65){
            vp.time = 47;
          }
          if(vp.time > 65 && vp.time <= 79){
            vp.time = 63;
          }
          if(vp.time > 79 && vp.time <= 93){
            vp.time = 77;
          }
          if(  vp.time > 93){
            vp.time = 91;
          }
          break;
        }
        case 5:{
          if(vp.time <= 11){
            vp.time = 0;
          }
          if(vp.time > 11 && vp.time <= 24){
            vp.time = 9;
          }
          if(vp.time > 24 && vp.time <= 36){
            vp.time = 22;
          }
          if(  vp.time > 36){
            vp.time = 34;
          }
          break;
        }
        case 4:{
          if(vp.time <= 10){
            vp.time = 0;
          }
          if(vp.time > 10 && vp.time <= 26){
            vp.time = 8;
          }
          if(vp.time > 26 && vp.time <= 50){
            vp.time = 24;
          }
          if(vp.time > 50 && vp.time <= 69){
            vp.time = 48;
          }
          if(  vp.time > 69){
            vp.time = 67;
          }
          break;
        }
        case 1:{
          if(vp.time <= 6){
            vp.time = 0;
          }
          if(vp.time > 6 && vp.time <= 27){
            vp.time = 4;
          }
          if(vp.time > 27 && vp.time <= 40){
            vp.time = 25;
          }
          if(  vp.time > 40){
            vp.time = 38;
          }
          break;
        }
        default:{
          break;
        }
      }

  }
  /*
  3
  7
  6
  2
  5
  4
  1
  */
  public void adelantar(){
    switch(video){
      case 3:{
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
      case 7:{
        if(vp.time < 14){
          vp.time = 14;
        }
        if(vp.time >= 14 && vp.time < 21){
          vp.time = 21;
        }
        if(vp.time >= 21 && vp.time < 31){
          vp.time = 31;
        }
        if(vp.time >= 31 && vp.time < 48){
          vp.time = 48;
        }
        if(vp.time >= 48 && vp.time < 53){
          vp.time = 53;
        }
        if(vp.time >= 53 && vp.time < 58){
          vp.time = 58;
        }
        break;
      }
      case 6:{
        if(vp.time < 4){
          vp.time = 4;
        }
        if(vp.time >= 4 && vp.time < 20){
          vp.time = 20;
        }
        if(vp.time >= 20 && vp.time < 46){
          vp.time = 46;
        }
        if(vp.time >= 46 && vp.time < 56){
          vp.time = 56;
        }
        break;
      }
      case 2:{
        if(vp.time < 4){
          vp.time = 4;
        }
        if(vp.time >= 4 && vp.time < 16){
          vp.time = 16;
        }
        if(vp.time >= 16 && vp.time < 31){
          vp.time = 31;
        }
        if(vp.time >= 31 && vp.time < 47){
          vp.time = 47;
        }
        if(vp.time >= 47 && vp.time < 63){
          vp.time = 63;
        }
        if(vp.time >= 63 && vp.time < 77){
          vp.time = 77;
        }
        if(vp.time >= 77 && vp.time < 91){
          vp.time = 91;
        }
        break;
      }
      case 5:{
        if(vp.time < 9){
          vp.time = 9;
        }
        if(vp.time >= 9 && vp.time < 22){
          vp.time = 22;
        }
        if(vp.time >= 22 && vp.time < 34){
          vp.time = 34;
        }
        break;
      }
      case 4:{
        if(vp.time < 8){
          vp.time = 8;
        }
        if(vp.time >= 8 && vp.time < 24){
          vp.time = 24;
        }
        if(vp.time >= 24 && vp.time < 48){
          vp.time = 48;
        }
        if(vp.time >= 48 && vp.time < 67){
          vp.time = 67;
        }
        break;
      }
      case 1:{
        if(vp.time < 4){
          vp.time = 4;
        }
        if(vp.time >= 4 && vp.time < 25){
          vp.time = 25;
        }
        if(vp.time >= 25 && vp.time < 38){
          vp.time = 38;
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
      SceneManager.LoadScene("MenuLeccion2");
  }

}
