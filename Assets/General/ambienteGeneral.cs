using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ambienteGeneral : MonoBehaviour {

    [SerializeField] Material skyDia;
    [SerializeField] Material skyNoche;
    [SerializeField] Material skyLluvia;
    [SerializeField] Material skyNeblina;
    [SerializeField] GameObject pajaros;
    [SerializeField] GameObject light;

    public GameObject lluviaPS;

    public Button siguiente;
    
 
    void Start () {
        //siguiente.gameObject.SetActive(false);
        
        switch(ambienteHandler.getAmbiente())
        {
            case 1:
                dia();
                break;
            case 2:
                noche();
                break;
            case 3:
                lluvia();
                break;
            case 4:
                niebla();
                break;
        }
	}

    void dia(){
        Debug.Log("dia");
        RenderSettings.skybox = skyDia;
        RenderSettings.fog = false;
        lluviaPS.gameObject.SetActive(false);
        if (pajaros && lluviaPS)
        {
            pajaros.gameObject.SetActive(true);
            lluviaPS.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Fata configurar elementos de ambiente en esta escena");
        }
    }

    void noche(){
        Debug.Log("noche");
            RenderSettings.skybox = skyNoche;
            RenderSettings.fog = false;
            RenderSettings.ambientIntensity = 1.04f;
            lluviaPS.gameObject.SetActive(false);
            if (pajaros && lluviaPS && light)
            {
                pajaros.gameObject.SetActive(false);
                lluviaPS.gameObject.SetActive(false);
                light.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Fata configurar elementos de ambiente en esta escena");
            }


    }
    void lluvia(){
        Debug.Log("lluiva");
            RenderSettings.skybox = skyLluvia;
            RenderSettings.fog = false;
            if (pajaros && lluviaPS)
            {
                pajaros.gameObject.SetActive(false);
                lluviaPS.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Fata configurar elementos de ambiente en esta escena");
            }
    }
    void niebla(){
        Debug.Log("niebla");
            RenderSettings.skybox = skyNeblina;
            RenderSettings.fog = true;
            lluviaPS.gameObject.SetActive(false);
            if (pajaros && lluviaPS)
            {
                pajaros.gameObject.SetActive(false);
                lluviaPS.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Fata configurar elementos de ambiente en esta escena");
            }
    }

    void setAmbiente(int numero){
        ambienteHandler.setAmbiente(numero);
        siguiente.gameObject.SetActive(true);
    }

    void regresar(){
        ambienteHandler.setAmbiente(0);
        siguiente.gameObject.SetActive(false);
    }

}

