using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TrafficLightController : MonoBehaviour
{

    public GameObject green, red, yellow;
    public int time;
    public bool [] isOn = new bool [3];

    // Start is called before the first frame update
    void Start()
    {
        green.SetActive(false);
        red.SetActive(false);
        yellow.SetActive(false);/*
        for(int i = 0; i < 2; i++){
            isOn[i] = false;
        }
        isOn[2] = true;
        InvokeRepeating ("ChangeLight", 1, time); */
        StartCoroutine (ChangeLight());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeLight(){
        if(isOn[2]){
            green.SetActive(false);
            isOn[2] = false;
            yellow.SetActive(true);

            DateTime dt = DateTime.Now + TimeSpan.FromSeconds(20);
    
            do { } while (DateTime.Now < dt);

            yellow.SetActive(false);
            red.SetActive(true);
            isOn[0] = true;
        }else{
            green.SetActive(true);
            isOn[2] = true;
            red.SetActive(false);
            isOn[0] = false;
        } 
        while(true){
            green.SetActive(true);
            red.SetActive(false);
            yellow.SetActive(false);
            yield return new WaitForSeconds(7.0f);
            yellow.SetActive(true);
            red.SetActive(false);
            green.SetActive(false);
            yield return new WaitForSeconds(3.0f);
            red.SetActive(true);
            green.SetActive(false);
            yellow.SetActive(false);
            yield return new WaitForSeconds(7.0f);
        }
    }

    IEnumerator Time(){
        yellow.SetActive(true);
        yield return new WaitForSeconds(5);
    } 
     public static void MyDelay(int seconds)
    {
        DateTime dt = DateTime.Now + TimeSpan.FromSeconds(seconds);
    
        do { } while (DateTime.Now < dt);
    } 
}
