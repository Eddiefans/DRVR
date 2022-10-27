using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pausaEX : MonoBehaviour
{
    private AudioSource[] audios1;
    [SerializeField] private GameObject textoPausar;
    [SerializeField] private GameObject textoReanudar;

    // Start is called before the first frame update
    void Start()
    {
        textoPausar.SetActive(true);
        textoReanudar.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
    }

   public void pausarEx()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            textoPausar.SetActive(false);
            textoReanudar.SetActive(true);
            audios1 = GameObject.Find("Examen").GetComponentsInChildren<AudioSource>();
            foreach (var item in audios1)
            {
                item.Pause();
            }
            
        }
        else
        {
            Time.timeScale = 1;
            textoPausar.SetActive(true);
            textoReanudar.SetActive(false);
            foreach (var item in audios1)
            {
                item.UnPause();
            }
           
        }
    }


}
