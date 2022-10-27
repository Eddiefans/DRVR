using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amg2 : MonoBehaviour {

    public Material skyDia;
    public Material skyNoche;
    public Material skyLluvia;
    public Material skyNeblina;
    public GameObject pajaros;

    public GameObject lluviaPS;


    // Use this for initialization
    void Start () {
	}

    public void dia(){
        RenderSettings.skybox = skyDia;
        RenderSettings.fog = false;
        pajaros.gameObject.SetActive(true);
    }

    public void noche(){
        Debug.Log("noche2");
            RenderSettings.skybox = skyNoche;
            RenderSettings.fog = false;
            RenderSettings.ambientIntensity = 0.4f;
            pajaros.gameObject.SetActive(false);
    }
    public void lluvia(){
            RenderSettings.skybox = skyLluvia;
            RenderSettings.fog = false;
            lluviaPS.gameObject.SetActive(true);
            pajaros.gameObject.SetActive(false);
    }
    public void niebla(){
            RenderSettings.skybox = skyNeblina;
            RenderSettings.fog = true;
            pajaros.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setAmbiente(int numero){
        ambienteHandler.setAmbiente(numero);
    }

}

