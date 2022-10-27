using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNukeScript : MonoBehaviour {
 
    //public Transform[] spawnPoints;
    public GameObject enemy;
    public List<GameObject> enemiesList;
    public int spawnTime;
 
 
    // Use this for initialization
    void Start () {

        
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", 1, spawnTime);
        
    }
 
    // Update is called once per frame
    void Update () {
       if (enemiesList.Count > 5){
           Destroy(enemiesList[0]);
           enemiesList.Remove(enemiesList[0]);
       }
    }
 
    void Spawn () {
        // Find a random index between zero and one less than the number of spawn points.
        //int spawnPoints = Random.Range (0, spawnPoints.Length);
 
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        if(checkIfPosEmpty(transform.position))
            enemiesList.Add(Instantiate (enemy, transform.position, transform.rotation));
    }

    public bool checkIfPosEmpty(Vector3 targetPos){
        
    //GameObject [] allMovableThings = GameObject.FindGameObjectsWithTag("Tocus (1)(Clone)");
    foreach(GameObject current in enemiesList)
    {
        double difference = Math.Truncate(current.transform.position.z) - Math.Truncate(targetPos.z);
        if(Math.Truncate(current.transform.position.x) == Math.Truncate(targetPos.x) 
            && (Math.Truncate(current.transform.position.z) == Math.Truncate(targetPos.z) 
                || difference < 5))
            return false;
    }
    return true;
}
 
}