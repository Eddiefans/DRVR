using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSemaforo4 : MonoBehaviour
{
  public float Tiempototal = 10f;
  public float tiempohastaNaranja = 5f;
  private int semaforoactual = -1;
  private float controlTiempo = 0;
  private bool mandarAlerta = false;

  public Semaforo[] semaforos ;
  public Semaforo[] semaforos2;
  public GameObject[] altos;
  public GameObject[] altos2;

  /* Para la nueva falta de semaforo en amarillo */
  public GameObject[] amarillo1;
  public GameObject[] amarillo2;

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
          semaforos2[semaforoactual].Enamarillo();
          /* Para la nueva falta de semaforo en amarillo */
          amarillo1[semaforoactual].SetActive(true);
          amarillo2[semaforoactual].SetActive(true);

        }else if (controlTiempo>=Tiempototal) {
          mandarAlerta=false;
          controlTiempo = 0;
          semaforos[semaforoactual].Enrojo();
          semaforos2[semaforoactual].Enrojo();
          /* Para la nueva falta de semaforo en amarillo */
          amarillo1[semaforoactual].SetActive(false);
          amarillo2[semaforoactual].SetActive(false);
          cambiarSeñal();
        }
        controlTiempo += (1 * Time.deltaTime);
    }

    private void cambiarSeñal(){
      siguienteSemaforo();
      for (int i = 0;i<semaforos.Length; i++) {
        if (i == semaforoactual) {
          semaforos[i].Enverde();
          semaforos2[i].Enverde();
          altos[i].GetComponent<BoxCollider>().enabled = false;
          altos2[i].GetComponent<BoxCollider>().enabled = false;
        }else{
          semaforos[i].Enrojo();
          semaforos2[i].Enrojo();
          altos[i].GetComponent<BoxCollider>().enabled = true;
          altos2[i].GetComponent<BoxCollider>().enabled = true;

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
