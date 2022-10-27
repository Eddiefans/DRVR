using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VPLeccion3 : MonoBehaviour
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
      case 2:{
        if(vp.time >= 127 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 1:{
        if(vp.time >= 62 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 3:{
        if(vp.time >= 79 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 4:{
        if(vp.time >= 44 || primer || !vp.isPlaying){
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
        case 2:{
          if(vp.time <= 12){
            vp.time = 0;
          }
          if(vp.time > 12 && vp.time <= 19){
            vp.time = 10;
          }
          if(vp.time > 19 && vp.time <= 46){
            vp.time = 17;
          }
          if(vp.time > 46 && vp.time <= 62){
            vp.time = 44;
          }
          if(vp.time > 62 && vp.time <= 74){
            vp.time = 60;
          }
          if(vp.time > 74 && vp.time <= 94){
            vp.time = 72;
          }
          if(vp.time > 94 && vp.time <= 106){
            vp.time = 92;
          }
          if(vp.time > 106 && vp.time <= 129){
            vp.time = 104;
          }
          if(vp.time > 129){
            vp.time = 127;
          }

          break;
        }
        case 1:{
          if(vp.time <= 7){
            vp.time = 0;
          }
          if(vp.time > 7 && vp.time <= 28){
            vp.time = 5;
          }
          if(vp.time > 28 && vp.time <= 37){
            vp.time = 26;
          }
          if(vp.time > 37 && vp.time <= 49){
            vp.time = 35;
          }
          if(vp.time > 49 && vp.time <= 54){
            vp.time = 47;
          }
          if(vp.time > 54 && vp.time <= 64){
            vp.time = 52;
          }
          if(  vp.time > 64){
            vp.time = 62;
          }
          break;
        }
        case 3:{
          if(vp.time <= 28){
            vp.time = 0;
          }
          if(vp.time > 28 && vp.time <= 35){
            vp.time = 26;
          }
          if(vp.time > 35 && vp.time <= 43){
            vp.time = 33;
          }
          if(vp.time > 43 && vp.time <= 62){
            vp.time = 41;
          }
          if(vp.time > 62 && vp.time <= 71){
            vp.time = 60;
          }
          if(vp.time > 71 && vp.time <= 76){
            vp.time = 69;
          }
          if(vp.time > 76 && vp.time <= 81){
            vp.time = 74;
          }
          if(  vp.time > 81){
            vp.time = 79;
          }
          break;
        }
        case 4:{
          if(vp.time <= 7){
            vp.time = 0;
          }
          if(vp.time > 7 && vp.time <= 19){
            vp.time = 5;
          }
          if(vp.time > 19 && vp.time <= 32){
            vp.time = 17;
          }
          if(vp.time > 32 && vp.time <= 46){
            vp.time = 30;
          }
          if(  vp.time > 46){
            vp.time = 44;
          }
          break;
        }
        default:{
          break;
        }
      }

  }
  /*
  2
  1
  3
  4
  */
  public void adelantar(){
    switch(video){
      case 2:{
        if(vp.time < 10){
          vp.time = 10;
        }
        if(vp.time >= 10 && vp.time < 17){
          vp.time = 17;
        }
        if(vp.time >= 17 && vp.time < 44){
          vp.time = 44;
        }
        if(vp.time >= 44 && vp.time < 60){
          vp.time = 60;
        }
        if(vp.time >= 60 && vp.time < 72){
          vp.time = 72;
        }
        if(vp.time >= 72 && vp.time < 92){
          vp.time = 92;
        }
        if(vp.time >= 92 && vp.time < 104){
          vp.time = 104;
        }
        if(vp.time >= 104 && vp.time < 127){
          vp.time = 127;
        }
        break;
      }
      case 1:{
        if(vp.time < 5){
          vp.time = 5;
        }
        if(vp.time >= 5 && vp.time < 26){
          vp.time = 26;
        }
        if(vp.time >= 26 && vp.time < 35){
          vp.time = 35;
        }
        if(vp.time >= 35 && vp.time < 47){
          vp.time = 47;
        }
        if(vp.time >= 47 && vp.time < 52){
          vp.time = 52;
        }
        if(vp.time >= 52 && vp.time < 62){
          vp.time = 62;
        }
        break;
      }
      case 3:{
        if(vp.time < 26){
          vp.time = 26;
        }
        if(vp.time >= 26 && vp.time < 33){
          vp.time = 33;
        }
        if(vp.time >= 33 && vp.time < 41){
          vp.time = 41;
        }
        if(vp.time >= 41 && vp.time < 60){
          vp.time = 60;
        }
        if(vp.time >= 60 && vp.time < 69){
          vp.time = 69;
        }
        if(vp.time >= 69 && vp.time < 74){
          vp.time = 74;
        }
        if(vp.time >= 74 && vp.time < 79){
          vp.time = 79;
        }
        break;
      }
      case 4:{
        if(vp.time < 5){
          vp.time = 5;
        }
        if(vp.time >= 5 && vp.time < 17){
          vp.time = 17;
        }
        if(vp.time >= 17 && vp.time < 30){
          vp.time = 30;
        }
        if(vp.time >= 30 && vp.time < 44){
          vp.time = 44;
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
      SceneManager.LoadScene("MenuLeccion3");
  }

}
