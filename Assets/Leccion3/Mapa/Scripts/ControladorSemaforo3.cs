using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSemaforo3 : MonoBehaviour
{
  public float Tiempototal = 10f;
  public float tiempohastaNaranja = 5f;
  private int semaforoactual = -1;
  private float controlTiempo = 0;
  private bool mandarAlerta = false;

  public Semaforo[] semaforos ;
  public GameObject[] altos;

  /* Para la nueva falta de semaforo en amarillo */
  public GameObject[] amarillo1;


    // Start is called before the first frame update
    void Start()
    {
        cambiarSeñal();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlTiempo >= tiempohastaNaranja && !mandarAlerta) {
          mandarAlerta = true;
          semaforos[semaforoactual].Enamarillo();
          /* Para la nueva falta de semaforo en amarillo */
          amarillo1[semaforoactual].SetActive(true);
        }else if (controlTiempo>=Tiempototal) {
          mandarAlerta=false;
          controlTiempo = 0;
          semaforos[semaforoactual].Enrojo();
          /* Para la nueva falta de semaforo en amarillo */
          amarillo1[semaforoactual].SetActive(false);
          cambiarSeñal();
        }
        controlTiempo += (1 * Time.deltaTime);
    }

    private void cambiarSeñal(){
      siguienteSemaforo();
      for (int i = 0;i<semaforos.Length; i++) {
        if (i == semaforoactual) {
          semaforos[i].Enverde();
          altos[i].GetComponent<BoxCollider>().enabled = false;
        }else{
          semaforos[i].Enrojo();
          altos[i].GetComponent<BoxCollider>().enabled = true;
        }
      }
    }

    private void siguienteSemaforo(){
      if (semaforoactual<(semaforos.Length-1)) {
        semaforoactual++;
      }else{
        semaforoactual=0;
      }
    }
}
