using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlBotones : MonoBehaviour
{
    [SerializeField] private Button buttonNoche;
    [SerializeField] private Button buttonLluvia;
    [SerializeField] private Button buttonNiebla;
    [SerializeField] private Image blockNoche;
    [SerializeField] private Image blockLluvia;
    [SerializeField] private Image blockNiebla;
    [SerializeField] private int leccion;
    void Start()
    {
        if(UserInfo.leccionPuntaje[leccion-1] > 79){
            buttonNoche.enabled = true;
            buttonLluvia.enabled = true;
            buttonNiebla.enabled = true;
            buttonNoche.interactable = true;
            buttonNiebla.interactable = true;
            buttonLluvia.interactable = true;
            blockLluvia.gameObject.SetActive(false);
            blockNiebla.gameObject.SetActive(false);
            blockNoche.gameObject.SetActive(false);
        }else{
            buttonNoche.enabled = false;
            buttonLluvia.enabled = false;
            buttonNiebla.enabled = false;
            buttonNoche.interactable = false;
            buttonNiebla.interactable = false;
            buttonLluvia.interactable = false;
            blockLluvia.gameObject.SetActive(true);
            blockNiebla.gameObject.SetActive(true);
            blockNoche.gameObject.SetActive(true);
            
        }
    }
    void Update()
    {
        
    }
}
