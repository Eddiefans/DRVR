using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class Manager2 : MonoBehaviour
{

    //Screens
    [SerializeField] private GameObject actionScreen = null;
    [SerializeField] private GameObject menuScreen = null;

    // Start is called before the first frame update

    void Start()
    {
      
    }

    void Awake(){
      //actionScreen.SetActive(false);
      menuScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void cargaLeccion(string info){
      string [] separados = info.Split(',') ;
      SceneManager.LoadScene(separados[0]);
      if(separados[1] == "0" && separados[2] == "0"){
        if(separados.Length>3){
          ExamNumber.examNumber = Int32.Parse(separados[3]);
        }
      }else{
        if(UserInfo.videosArray[Int32.Parse(separados[2])-1][Int32.Parse(separados[1])-1].watched == false){
          UserInfo.primerVideo = true;
        }
        UserInfo.videosArray[Int32.Parse(separados[2])-1][Int32.Parse(separados[1])-1].watched = true;
        StartCoroutine(cargaDB( Int32.Parse(separados[1]) , Int32.Parse(separados[2]) ));
        
      }  
    }

    public void cargaRetroalimentacion(string info){
      ExamNumber.directFeedback = true;
      string [] separados = info.Split(',') ;
      SceneManager.LoadScene(separados[0]);
      ExamNumber.examNumber = Int32.Parse(separados[3]);
    }

    public void accionExamen(string info){
      string [] separados = info.Split(',');
      if(UserInfo.examPuntaje[Int32.Parse(separados[3])-1] != -1){
        actionScreen.SetActive(true);
        menuScreen.SetActive(false);
      }else{
        ExamNumber.examNumber = Int32.Parse(separados[3]);
        SceneManager.LoadScene(separados[0]);
      }
    }

    public void regresar(){
      actionScreen.SetActive(false);
      menuScreen.SetActive(true);
    }

    private IEnumerator cargaDB(int seccion, int leccion)
    {
      WWWForm form = new WWWForm();
      form.AddField("video_number", seccion);
      form.AddField("lesson",leccion);
      form.AddField("username", UserInfo.username);
      WWW w = new WWW("http://izyventa.com/elena/MenuVideos/videosVistos.php", form);
      yield return w;
    }


    

}
