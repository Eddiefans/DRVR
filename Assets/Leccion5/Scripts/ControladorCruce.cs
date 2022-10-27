using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCruce : MonoBehaviour
{
  public float Tiempototal = 10f;
  public float tiempohastaNaranja = 5f;
  private int semaforoactual = -1;
  private float controlTiempo = 0;
  private bool mandarAlerta = false;

  public semaforo2[] semaforos ;

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

        }else if (controlTiempo>=Tiempototal) {
          mandarAlerta=false;
          controlTiempo = 0;
          semaforos[semaforoactual].Enrojo();

          cambiarSeñal();
        }
        controlTiempo += (1 * Time.deltaTime);
    }

    private void cambiarSeñal(){
      siguienteSemaforo();
      for (int i = 0;i<semaforos.Length; i++) {
        if (i == semaforoactual) {
          semaforos[i].Enverde();

        }else{
          semaforos[i].Enrojo();

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
