using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemaforoFlecha : MonoBehaviour
{
  public GameObject verde;
  public GameObject rojo;
  public GameObject amarillo;
  public GameObject alto;
  public GameObject [] flecha;

  /* Para la nueva falta de semaforo en amarillo */
  public GameObject cuboAma;


    // Start is called before the first frame update
    void Start()
    {
        //Enrojo();
    }

    public void Enverde(){
    rojo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    amarillo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    verde.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    if(flecha.Length > 0){
      alto.GetComponent<BoxCollider>().enabled = true;
    }else {
      alto.GetComponent<BoxCollider>().enabled = false;
    }

    for (int i=0; i<flecha.Length; i++) {
      flecha[i].GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }
    }

    public void EnverdeCflecha(){
    rojo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    amarillo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    verde.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    alto.GetComponent<BoxCollider>().enabled = false;
    for (int i=0; i<flecha.Length; i++) {
      flecha[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }
    }

    public void Enrojo(){
    rojo.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    amarillo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    verde.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    alto.GetComponent<BoxCollider>().enabled = true;
    /* Para la nueva falta de semaforo en amarillo */
    cuboAma.SetActive(false);
    }

    public void Enamarillo(){
    rojo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    amarillo.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    verde.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    /* Para la nueva falta de semaforo en amarillo */
    cuboAma.SetActive(false);
    }
}
