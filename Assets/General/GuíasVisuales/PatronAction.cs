using UnityEngine;
using System.Collections;

public class PatronAction : MonoBehaviour 
{
   // Public variables
   public GameObject ObjetoActivador;
   public float TiempoAntesDeAccion;
   public float TiempoDespuesDeAccion;

   // Private variables
   private float timeToStart;
   private float timeToEnd;
   private bool workDone;

void Start()
{
   // Justo al inicio deshabilitamos el script (será activado por el script 
   // 'ComportamientoPatron.cs' cuando el objeto activador llegue al hito)
   this.enabled = false;

   timeToStart = 0;
   timeToEnd = 0;
   workDone = false;
}

void Update()
{
   if (!workDone)
   {
      timeToStart += Time.deltaTime;

      // No realizamos la acción hasta que no pase el tiempo 'TiempoAntesDeAccion'
      if (timeToStart >= TiempoAntesDeAccion)
      {
         // IMPLEMENTACIÓN DE LA ACCIÓN...
         // ...

         workDone = true;
      }
   }
   else
   {
      timeToEnd += Time.deltaTime;

      //No avanzamos al siguiente hito hasta que no pase el tiempo 'TiempoDespuesDeAccion'
      if (timeToEnd >= TiempoDespuesDeAccion)
      {
         // Se inicializa el script
         Start();

         // Indicamos que se puede pasar el siguiente hito
         ObjetoActivador.GetComponentInParent<ComportamientoPatron>().CanGoToNextMilestone = true;
       }
   }
}

}

