using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    
    public TextMeshProUGUI text;
    [SerializeField] private Text screenDebugText;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetUIDebugText(string newText)
    {
        screenDebugText.text = newText;
    }

    public virtual void changeText(float speed){
        float actualSpeed = Math.Abs(speed * 3.6f);
        /* if(speed < 0){
            speed = -speed;
        } */
        text.text = Mathf.Round(actualSpeed) + " Km/H";
    }

}
