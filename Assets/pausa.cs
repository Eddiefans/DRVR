using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pausa : MonoBehaviour
{

    [SerializeField] private GameObject textoPausar;
    [SerializeField] private GameObject textoReanudar;
    private AudioSource[] audiosL1;
    private AudioSource[] audiosL2;
    private AudioSource[] audiosL3;
    private AudioSource[] audiosL4;
    private AudioSource[] audiosL5;
    private AudioSource[] audiosEx;
    

    // Start is called before the first frame update
    void Start()
    {
            textoPausar.SetActive(true);
            textoReanudar.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
     {
         pausarL3();
     }

    }

    public void pausarL1()

    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            textoPausar.SetActive(false);
            textoReanudar.SetActive(true);
            audiosL1 = GameObject.Find("Leccion1").GetComponentsInChildren<AudioSource>();
            foreach (var item in audiosL1)
            {
                item.Pause();
            }
        }
        else
        {
            Time.timeScale = 1;
            textoPausar.SetActive(true);
            textoReanudar.SetActive(false);
            foreach (var item in audiosL1)
            {
                item.UnPause();
            }
        }
    }
    public void pausarL2()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            textoPausar.SetActive(false);
            textoReanudar.SetActive(true);
            audiosL2 = GameObject.Find("Leccion2").GetComponentsInChildren<AudioSource>();
            foreach (var item in audiosL2)
            {
                item.Pause();
            }
        }
        else
        {
            Time.timeScale = 1;
            textoPausar.SetActive(true);
            textoReanudar.SetActive(false);
            foreach (var item in audiosL2)
            {
                item.UnPause();
            }
        }
    }
    public void pausarL3()
    { 
        if(Time.timeScale == 1)
        {
            
            Time.timeScale = 0;
            textoPausar.SetActive(false);
            textoReanudar.SetActive(true);
            audiosL3 = GameObject.Find("Leccion3").GetComponentsInChildren<AudioSource>();
            foreach (var item in audiosL3)
            {
                item.Pause();
                Debug.Log("pausado");
            }
        }
        else
        {
            Time.timeScale = 1;
            textoPausar.SetActive(true);
            textoReanudar.SetActive(false);
            audiosL3 = GameObject.Find("Leccion3").GetComponentsInChildren<AudioSource>();
            foreach (var item in audiosL3)
            {
                item.UnPause();
                
                Debug.Log("noPausado");
            }
        }
    }
    public void pausarL4()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            textoPausar.SetActive(false);
            textoReanudar.SetActive(true);
            audiosL4 = GameObject.Find("Leccion4").GetComponentsInChildren<AudioSource>();
            foreach (var item in audiosL4)
            {
                item.Pause();
                
            }
            Debug.Log("pausado");
        }
        else
        {
            Time.timeScale = 1;
            textoPausar.SetActive(true);
            textoReanudar.SetActive(false);
            foreach (var item in audiosL4)
            {
                item.UnPause();
                
            }
            Debug.Log("noPausado");
        }
    }
    public void pausarL5()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            textoPausar.SetActive(false);
            textoReanudar.SetActive(true);
            audiosL5 = GameObject.Find("Leccion5").GetComponentsInChildren<AudioSource>();
            foreach (var item in audiosL5)
            {
                item.Pause();
            }
        }
        else
        {
            Time.timeScale = 1;
            textoPausar.SetActive(true);
            textoReanudar.SetActive(false);
            foreach (var item in audiosL5)
            {
                item.UnPause();
            }
        }
    }
    public void pausarEx()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            textoPausar.SetActive(false);
            textoReanudar.SetActive(true);
            audiosEx = GameObject.Find("ExamenP").GetComponentsInChildren<AudioSource>();
            foreach (var item in audiosEx)
            {
                item.Pause();
            }
        }
        else
        {
            Time.timeScale = 1;
            textoPausar.SetActive(true);
            textoReanudar.SetActive(false);
            foreach (var item in audiosEx)
            {
                item.UnPause();
            }
        }
    }


}
