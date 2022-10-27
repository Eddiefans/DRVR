using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleSpawner : MonoBehaviour{
    public carNode[] waypoints;


    public GameObject[] vehicle;
    public int totalPopulationMax = 20;
    public float maxDistance = 100f;
    public int totalAi = 0 ;
    public List<GameObject> spawnedAi ;

    void Awake(){
        managePopulation();
    }

    void spawnPrefab(int index){     
        
        GameObject clone = Instantiate(vehicle[Random.Range(0,vehicle.Length)] );
        clone.GetComponent<VehicleAiController>().currentNode = waypoints[index].GetComponent<carNode>();
        clone.transform.position = waypoints[index].transform.position;
        clone.transform.Rotate(0 , waypoints[index].transform.eulerAngles.y , 0);
        clone.transform.SetParent(transform.parent);
        spawnedAi.Add(clone);
    }

    public IEnumerator spawner(){

		while(true){
			yield return new WaitForSeconds(20);
            managePopulation();
		}
	

    }

    void managePopulation(){
         
        for (int i = 0; i < waypoints.Length; i += 3){
            if(spawnedAi.Count < totalPopulationMax)
                populationLoop(i);
        }

        for (int i = 0; i < spawnedAi.Count; i++){
            if(Vector3.Distance(transform.position , spawnedAi[i].transform.position) >= maxDistance){
                Destroy(spawnedAi[i]);
                spawnedAi.Remove(spawnedAi[i]);
            }
        }
        totalAi = spawnedAi.Count;

    }

    void populationLoop(int index){
        if(Vector3.Distance(transform.position , waypoints[index].transform.position) <= maxDistance){
            spawnPrefab(index);
        }
    }

    public void restart(int indice){
        spawnedAi.Clear();
        if(spawnedAi.Count == 0){
            Debug.Log("La lista es 0");
        }
        //spawnedAi = new List<GameObject>();
        if(indice == 0){

            GameObject[] objs = GameObject.FindGameObjectsWithTag("CarIA");
            if(objs != null){
                Debug.Log("Si hubo carros para eliminar");
                for (int i = 0; i < objs.Length; i++)
                {
                    Destroy(objs[i]);
                }
            }
        }
        managePopulation();
    }

}
