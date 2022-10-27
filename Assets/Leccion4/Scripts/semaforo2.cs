using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semaforo2 : MonoBehaviour
{
  public GameObject verde;
  public GameObject rojo;
  public GameObject amarillo;
  public GameObject [] peatones;
  public GameObject alto;

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
    for (int i = 0; i < peatones.Length; i++) {
      peatones[i].GetComponent<BoxCollider>().enabled = true;
    }
    alto.GetComponent<BoxCollider>().enabled = false;
    }

    public void Enrojo(){
    rojo.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    amarillo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    verde.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    for (int i = 0; i < peatones.Length; i++) {
      peatones[i].GetComponent<BoxCollider>().enabled = false;
    }
    alto.GetComponent<BoxCollider>().enabled = true;
    /* Para la nueva falta de semaforo en amarillo */
    //cuboAma.SetActive(false);
    }

    public void Enamarillo(){
    rojo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    amarillo.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    verde.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    /* Para la nueva falta de semaforo en amarillo */
    //cuboAma.SetActive(true);
    }
}
