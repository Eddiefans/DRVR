using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System;


public class videosVistos : MonoBehaviour
{


    public void Awake(){
    }

    public GameObject [] imagenes;
    public int leccionS;


    // Start is called before the first frame update
    void Start()
    {
      
        for (int i=0; i<UserInfo.videosArray[leccionS-1].Count; i++) {
          if(UserInfo.videosArray[leccionS-1][i].watched){
              imagenes[i].gameObject.SetActive(true);
          }else{
              imagenes[i].gameObject.SetActive(false);
          }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
