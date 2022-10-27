using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawnerL5 : MonoBehaviour
{
    public GameObject[] pedestrian;
    public int pedestriansToSpawn;

    void Start(){
        StartCoroutine(Spawn());
    }

    public void restart(){
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        int count = 0;
        while(count < pedestriansToSpawn){
            GameObject obj = Instantiate(pedestrian[Random.Range(0,pedestrian.Length)]);
            Transform child = transform.GetChild(9);
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;

            yield return new WaitForEndOfFrame();

            count++;
        }
    }
}
