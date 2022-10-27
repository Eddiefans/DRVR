using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;



public class VPLeccion1 : MonoBehaviour
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
        if(vp.time >= 80 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 5:{
        if(vp.time >= 82 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 4:{
        if(vp.time >= 131 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 3:{
        if(vp.time >= 51 || primer || !vp.isPlaying){
          adelantarB.gameObject.SetActive(false);
        }else{
          adelantarB.gameObject.SetActive(true);
        }
        break;
      }
      case 2:{
        if(vp.time >= 51 || primer || !vp.isPlaying){
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
          if(vp.time <= 13){
            vp.time = 0;
          }
          if(vp.time > 13 && vp.time <= 30){
            vp.time = 11;
          }
          if(vp.time > 30 && vp.time <= 36){
            vp.time = 28;
          }
          if(vp.time > 36 && vp.time <= 52){
            vp.time = 34;
          }
          if(vp.time > 52 && vp.time <= 72){
            vp.time = 50;
          }
          if(vp.time > 72 && vp.time <= 82){
            vp.time = 70;
          }
          if(  vp.time > 82){
            vp.time = 80;
          }
          break;
        }
        case 5:{
          if(vp.time <= 13){
            vp.time = 0;
          }
          if(vp.time > 13 && vp.time <= 29){
            vp.time = 11;
          }
          if(vp.time > 29 && vp.time <= 46){
            vp.time = 27;
          }
          if(vp.time > 46 && vp.time <= 84){
            vp.time = 44;
          }
          if(  vp.time > 84){
            vp.time = 82;
          }
          break;
        }
        case 4:{
          if(vp.time <= 15){
            vp.time = 0;
          }
          if(vp.time > 15 && vp.time <= 29){
            vp.time = 13;
          }
          if(vp.time > 29 && vp.time <= 42){
            vp.time = 27;
          }
          if(vp.time > 42 && vp.time <= 52){
            vp.time = 40;
          }
          if(vp.time > 52 && vp.time <= 59){
            vp.time = 50;
          }
          if(vp.time > 59 && vp.time <= 70){
            vp.time = 57;
          }
          if(vp.time > 70 && vp.time <= 89){
            vp.time = 68;
          }
          if(vp.time > 89 && vp.time <= 105){
            vp.time = 87;
          }
          if(vp.time > 105 && vp.time <= 116){
            vp.time = 103;
          }
          if(vp.time > 116 && vp.time <= 124){
            vp.time = 114;
          }
          if(vp.time > 124 && vp.time <= 133){
            vp.time = 122;
          }
          if(  vp.time > 133){
            vp.time = 131;
          }
          break;
        }
        case 3:{
          if(vp.time <= 15){
            vp.time = 0;
          }
          if(vp.time > 15 && vp.time <= 19){
            vp.time = 13;
          }
          if(vp.time > 19 && vp.time <= 22){
            vp.time = 17;
          }
          if(vp.time > 22 && vp.time <= 27){
            vp.time = 20;
          }
          if(vp.time > 27 && vp.time <= 31){
            vp.time = 25;
          }
          if(vp.time > 31 && vp.time <= 34){
            vp.time = 29;
          }
          if(vp.time > 34 && vp.time <= 53){
            vp.time = 32;
          }
          if(  vp.time > 53){
            vp.time = 51;
          }
          break;
        }
        case 2:{
          if(vp.time <= 16){
            vp.time = 0;
          }
          if(vp.time > 16 && vp.time <= 22){
            vp.time = 14;
          }
          if(vp.time > 22 && vp.time <= 53){
            vp.time = 20;
          }
          if(  vp.time > 53){
            vp.time = 51;
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
  5
  4
  3
  2
  */
  public void adelantar(){

    switch(video){
      case 1:{
        if(vp.time < 11){
          vp.time = 11;
        }
        if(vp.time >= 11 && vp.time < 28){
          vp.time = 28;
        }
        if(vp.time >= 28 && vp.time < 34){
          vp.time = 34;
        }
        if(vp.time >= 34 && vp.time < 50){
          vp.time = 50;
        }
        if(vp.time >= 50 && vp.time < 70){
          vp.time = 70;
        }
        if(vp.time >= 70 && vp.time < 80){
          vp.time = 80;
        }
        break;
      }
      case 5:{
        if(vp.time < 11){
          vp.time = 11;
        }
        if(vp.time >= 11 && vp.time < 27){
          vp.time = 27;
        }
        if(vp.time >= 27 && vp.time < 44){
          vp.time = 44;
        }
        if(vp.time >= 44 && vp.time < 82){
          vp.time = 82;
        }
        break;
      }
      case 4:{
        if(vp.time < 13){
          vp.time = 13;
        }
        if(vp.time >= 13 && vp.time < 27){
          vp.time = 27;
        }
        if(vp.time >= 27 && vp.time < 40){
          vp.time = 40;
        }
        if(vp.time >= 40 && vp.time < 50){
          vp.time = 50;
        }
        if(vp.time >= 50 && vp.time < 57){
          vp.time = 57;
        }
        if(vp.time >= 57 && vp.time < 68){
          vp.time = 68;
        }
        if(vp.time >= 68 && vp.time < 87){
          vp.time = 87;
        }
        if(vp.time >= 87 && vp.time < 103){
          vp.time = 103;
        }
        if(vp.time >= 103 && vp.time < 114){
          vp.time = 114;
        }
        if(vp.time >= 114 && vp.time < 122){
          vp.time = 122;
        }
        if(vp.time >= 122 && vp.time < 131){
          vp.time = 131;
        }
        break;
      }

      case 3:{
        if(vp.time < 13){
          vp.time = 13;
        }
        if(vp.time >= 13 && vp.time < 17){
          vp.time = 17;
        }
        if(vp.time >= 17 && vp.time < 20){
          vp.time = 20;
        }
        if(vp.time >= 20 && vp.time < 25){
          vp.time = 25;
        }
        if(vp.time >= 25 && vp.time < 29){
          vp.time = 29;
        }
        if(vp.time >= 29 && vp.time < 32){
          vp.time = 32;
        }
        if(vp.time >= 32 && vp.time < 51){
          vp.time = 51;
        }
        break;
      }
      case 2:{
        if(vp.time < 14){
          vp.time = 14;
        }
        if(vp.time >= 14 && vp.time < 20){
          vp.time = 20;
        }
        if(vp.time >= 20 && vp.time < 51){
          vp.time = 51;
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
      SceneManager.LoadScene("MenuLeccion1");
  }


}
