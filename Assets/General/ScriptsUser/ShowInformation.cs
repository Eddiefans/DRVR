using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;

public class ShowInformation : MonoBehaviour
{
    public static string username;
    public static string firstname;  
    public static string email;     
    public Text usernameText;
    public Text firstNameText;
    public Text emailText;

    public void Start(){
      usernameText.text  = username;
      firstNameText.text  = firstname;  
      emailText.text  = email;    
    }
    
    public void Update(){
        
    }

}