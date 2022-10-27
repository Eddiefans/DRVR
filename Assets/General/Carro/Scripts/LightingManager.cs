using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public List<Light> lights;
    public bool encendido;

    public virtual void ToggleHeadlights(){
        foreach (Light light in lights)
        {
            light.intensity = light.intensity == 0 ? 2 : 0;
            if(light.intensity == 0){ encendido = false;}
            else{ encendido = true;}
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
