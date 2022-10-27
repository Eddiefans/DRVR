using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCar : MonoBehaviour
{

    enum State
    {
        Moving,
        Waiting
    }

    [SerializeField] private State carState;
    [SerializeField] private GameObject startRoad;
    [SerializeField] private Transform currentWaypoint;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private float waypointDistanceCheck = 5;
    [SerializeField] private int nextWaypointIndex;
    void Start()
    {
        UpdateWaypoint();
    }
    
    void Update()
    {
        if (currentWaypoint == null || carState == State.Waiting)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, currentWaypoint.position);
        print(distance);
        if (distance > waypointDistanceCheck)
        {
            //Updates movement
            transform.position += transform.forward * (Time.deltaTime * movementSpeed);
            //Updates rotation
            Quaternion targetRotation = Quaternion.LookRotation(currentWaypoint.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }else{
            UpdateWaypoint();
        }
    }

    void UpdateWaypoint()
    {
        if (startRoad.transform.childCount > 0)
        {
            currentWaypoint = startRoad.transform.GetChild(nextWaypointIndex);
            nextWaypointIndex++;
            CarWaypoint waypoint = currentWaypoint.GetComponent<CarWaypoint>();
            movementSpeed = waypoint.newCarSpeed;
            rotationSpeed = waypoint.newRotationSpeed;
            if (waypoint.hasWaitTime)
            {
                StartCoroutine(WaitAndMove(waypoint.waitTime));
            }
        }
        else
        {
            Debug.LogError("No hay waypoints en el camino seleccionado");
        }
    }

    IEnumerator WaitAndMove(float waitTime)
    {
        carState = State.Waiting;
        yield return new WaitForSeconds(waitTime);
        carState = State.Moving;
    }
}
