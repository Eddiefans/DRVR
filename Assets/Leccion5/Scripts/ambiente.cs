using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambiente : MonoBehaviour {

    public Material skyDia;
    public Material skyNoche;
    public Material skyLluvia;
    public Material skyNeblina;

    public GameObject lluviaPS;

    public CarController5 carController5;
    public List<vehicleSpawner> vehicleSpawners;


    // Use this for initialization
    void Start () {

        
            RenderSettings.skybox = skyDia;
            RenderSettings.fog = false;
            
		
	}

    public void dia(){
        RenderSettings.skybox = skyDia;
        RenderSettings.fog = false;
    }
    public void noche(){
        if(!carController5.noche){
            RenderSettings.skybox = skyNoche;
            RenderSettings.fog = false;
            carController5.restart("noche");
            for(int i = 0; i < vehicleSpawners.Count; i++){
                vehicleSpawners[i].restart(i);
            }
            /* foreach (var vehicleSpawner in vehicleSpawners)
            {
                vehicleSpawner.restart();
            } */
        }
    }
    public void lluvia(){
        if(!carController5.lluvia){
            RenderSettings.skybox = skyLluvia;
            RenderSettings.fog = false;
            lluviaPS.gameObject.SetActive(true);
            carController5.restart("lluvia");
            for(int i = 0; i < vehicleSpawners.Count; i++){
                vehicleSpawners[i].restart(i);
            }
            /* foreach (var vehicleSpawner in vehicleSpawners)
            {
                vehicleSpawner.restart();
            } */
        }
    }
    public void niebla(){
        if(!carController5.neblina){
            RenderSettings.skybox = skyNeblina;
            RenderSettings.fog = true;
            carController5.restart("neblina");
            for(int i = 0; i < vehicleSpawners.Count; i++){
                vehicleSpawners[i].restart(i);
            }
            /* foreach (var vehicleSpawner in vehicleSpawners)
            {
                vehicleSpawner.restart();
            } */
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}