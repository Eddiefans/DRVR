using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cruce1 : MonoBehaviour
{
  public float Tiempototal = 10f;
  public float tiempohastaNaranja = 5f;
  private int semaforoactual = -1;
  private float controlTiempo = 0;
  private bool mandarAlerta = false;

  public semaforo2[] semaforos ;
  public semaforo2[] semaforos2;
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
          if(semaforoactual == 1){
              semaforos2[0].Enamarillo();
          }
        }else if (controlTiempo>=Tiempototal) {
          mandarAlerta=false;
          controlTiempo = 0;
          semaforos[semaforoactual].Enrojo();
          if(semaforoactual == 1){
              semaforos2[0].Enrojo();
          }
          cambiarSeñal();
        }
        controlTiempo += (1 * Time.deltaTime);
    }

    private void cambiarSeñal(){
      siguienteSemaforo();
      for (int i = 0;i<semaforos.Length; i++) {
        if (i == semaforoactual) {
          semaforos[i].Enverde();
          if(i == 1){
              semaforos2[0].Enverde();
          }
        }else{
          semaforos[i].Enrojo();
          if(i == 1){
              semaforos2[0].Enrojo();
          }
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
