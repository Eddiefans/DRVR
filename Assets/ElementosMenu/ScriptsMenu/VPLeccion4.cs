using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VPLeccion4 : MonoBehaviour
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
      case 1:{
        if(vp.time >= 110 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 2:{
        if(vp.time >= 17 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 3:{
        if(vp.time >= 18 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 4:{
        if(vp.time >= 46 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 5:{
        if(vp.time >= 40 || primer || !vp.isPlaying){
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
        case 1:{
          if(vp.time <= 8){
            vp.time = 0;
          }
          if(vp.time > 8 && vp.time <= 29){
            vp.time = 6;
          }
          if(vp.time > 29 && vp.time <= 45){
            vp.time = 27;
          }
          if(vp.time > 45 && vp.time <= 67){
            vp.time = 43;
          }
          if(vp.time > 67 && vp.time <= 88){
            vp.time = 65;
          }
          if(vp.time > 88 && vp.time <= 112){
            vp.time = 86;
          }
          if(  vp.time > 112){
            vp.time = 110;
          }
          break;
        }
        case 2:{
          if(vp.time <= 8){
            vp.time = 0;
          }
          if(vp.time > 8 && vp.time <= 19){
            vp.time = 6;
          }
          if(  vp.time > 19){
            vp.time = 17;
          }
          break;
        }
        case 3:{
          if(vp.time <= 8){
            vp.time = 0;
          }
          if(vp.time > 8 && vp.time <= 11){
            vp.time = 6;
          }
          if(vp.time > 11 && vp.time <= 20){
            vp.time = 9;
          }
          if(  vp.time > 20){
            vp.time = 18;
          }
          break;
        }
        case 4:{
          if(vp.time <= 20){
            vp.time = 0;
          }
          if(vp.time > 20 && vp.time <= 41){
            vp.time = 18;
          }
          if(vp.time > 41 && vp.time <= 48){
            vp.time = 39;
          }
          if(  vp.time > 48){
            vp.time = 46;
          }
          break;
        }
        case 5:{
          if(vp.time <= 15){
            vp.time = 0;
          }
          if(vp.time > 15 && vp.time <= 42){
            vp.time = 13;
          }
          if(  vp.time > 42){
            vp.time = 40;
          }
          break;
        }
        default:{
          break;
        }
      }

  }
  /*
  1
  2
  3
  4
  5
  */
  public void adelantar(){
    switch(video){
      case 1:{
        if(vp.time < 6){
          vp.time = 6;
        }
        if(vp.time >= 6 && vp.time < 27){
          vp.time = 27;
        }
        if(vp.time >= 27 && vp.time < 43){
          vp.time = 43;
        }
        if(vp.time >= 43 && vp.time < 65){
          vp.time = 65;
        }
        if(vp.time >= 65 && vp.time < 86){
          vp.time = 86;
        }
        if(vp.time >= 86 && vp.time < 110){
          vp.time = 110;
        }
        break;
      }
      case 2:{
        if(vp.time < 6){
          vp.time = 6;
        }
        if(vp.time >= 6 && vp.time < 17){
          vp.time = 17;
        }
        break;
      }
      case 3:{
        if(vp.time < 6){
          vp.time = 6;
        }
        if(vp.time >= 6 && vp.time < 9){
          vp.time = 9;
        }
        if(vp.time >= 9 && vp.time < 18){
          vp.time = 18;
        }
        break;
      }
      case 4:{
        if(vp.time < 18){
          vp.time = 18;
        }
        if(vp.time >= 18 && vp.time < 39){
          vp.time = 39;
        }
        if(vp.time >= 39 && vp.time < 46){
          vp.time = 46;
        }
        break;
      }
      case 5:{
        if(vp.time < 13){
          vp.time = 13;
        }
        if(vp.time >= 13 && vp.time < 40){
          vp.time = 40;
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
      SceneManager.LoadScene("MenuLeccion4");
  }
}
