using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaforo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject verde;
    public GameObject rojo;
    public GameObject amarillo;
      // Start is called before the first frame update
      void Start()
      {
          //Enrojo();
      }

      public void Enverde(){
      rojo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
      amarillo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
      verde.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
      }

      public void Enrojo(){
      rojo.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
      amarillo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
      verde.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
      }

      public void Enamarillo(){
      rojo.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
      amarillo.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
      verde.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
      }
}
