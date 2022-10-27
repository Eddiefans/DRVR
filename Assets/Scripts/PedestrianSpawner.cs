using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PedestrianSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] pedestrianPrefabs;
    [SerializeField] [Range(0,100)] private int spawnPossibility = 50;
    [SerializeField] private Transform pedestrianParent;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private int maxPedestriansOnScene = 40;
    private bool refreshingCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint") && GameManager.Instance.pedestriansOnScene < maxPedestriansOnScene)
        {
            int spawnChance = Random.Range(0, 100);
            if (spawnChance < spawnPossibility)
            {
                GameObject clone = Instantiate(pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Length)],
                    other.transform.position,
                    Quaternion.identity, pedestrianParent);
                clone.GetComponent<WaypointNavigator>().currentWaypoint = other.GetComponent<Waypoint>();
                GameManager.Instance.pedestriansOnScene++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            GameManager.Instance.pedestriansOnScene--;
            Destroy(other.gameObject);
        }
    }

    // private void Update()
    // {
    //     if (GameManager.Instance.pedestriansOnScene < 20 && !refreshingCollider)
    //     {
    //         StartCoroutine(RefreshColliderRoutine());
    //     }
    // }
    //
    // IEnumerator RefreshColliderRoutine()
    // {
    //     refreshingCollider = true;
    //     boxCollider.enabled = false;
    //     yield return Yielders.Get(1);
    //     boxCollider.enabled = true;
    //     yield return Yielders.Get(1);
    //     refreshingCollider = false;
    // }
}
