using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterNavigationController : MonoBehaviour
{
    public Vector3 destination;
    public bool reachedDestination;
    public float stopDistance = 1;
    public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public float movementSpeed;
    
    [SerializeField] private bool watingOnCrosswalk;
    [SerializeField] private bool justWaitedOnCrosswalk;
    [SerializeField] private float distanceToTargetWaypoint;
    [SerializeField] private WaypointNavigator navigator;
    [SerializeField] private Animator animator;
    Vector3 lastPosition;
    Vector3 velocity;
    private void Start()
    {
        movementSpeed = Random.Range(minSpeed, maxSpeed);
    }
    private void Update()
    {
        if (watingOnCrosswalk)
            return;
        
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
            }
            else
            {
                reachedDestination = true;
            }

            velocity = (transform.position - lastPosition) / Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;
            var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
            var rightDotProduct = Vector3.Dot(transform.right, velocity);
            
            // distanceToTargetWaypoint = Vector3.Distance(transform.position, destination);
            //
            // if (distanceToTargetWaypoint < 3 && !justWaitedOnCrosswalk)
            // {
            //     if (navigator.currentWaypoint != null)
            //     {
            //         if (navigator.currentWaypoint.isCrosswalk)
            //         {
            //             StartCoroutine(StopWaitingRoutine());
            //         }
            //     }
            // }
        }
    }

    private CrossWalkSystem crossWalkSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crosswalk"))
        {
            if (other.GetComponent<CrosswalkPedestrianReporter>() != null)
            {
                crossWalkSystem = other.transform.parent.GetComponent<CrossWalkSystem>();
                StartCoroutine(StopWaitingRoutine());    
            }
            
        }
    }

    IEnumerator StopWaitingRoutine()
    {
        animator.SetBool("IsWalking", false);
        watingOnCrosswalk = true;
        justWaitedOnCrosswalk = true;
        yield return Yielders.Get(3);
        while (crossWalkSystem.carStatus == CrossWalkSystem.CarStatus.Passing)
        {
            print("waiting for the car to pass");
            yield return null;
        }
        yield return Yielders.Get(3);
        watingOnCrosswalk = false;
        animator.SetBool("IsWalking", true);
        yield return Yielders.Get(5);
        justWaitedOnCrosswalk = false;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
    
    // void UpdateWaipoint()
    // {
    //     bool shouldBranch = false;
    //
    //     if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
    //     {
    //         shouldBranch = Random.Range(0f,1f) <= currentWaypoint.branchRatio ? true : false;
    //     }
    //         
    //     if(shouldBranch)
    //     {
    //         currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
    //     }
    //     else
    //     {
    //         if(direction == 0)
    //         {
    //             if (currentWaypoint.nextWaypoint != null)
    //             {
    //                 currentWaypoint = currentWaypoint.nextWaypoint;
    //             }
    //             else
    //             {
    //                 currentWaypoint = currentWaypoint.previousWaypont;
    //                 direction = 1;
    //             }
    //                 
    //         }
    //         else if(direction == 1)
    //         {
    //             if (currentWaypoint.previousWaypont != null)
    //             {
    //                 currentWaypoint = currentWaypoint.previousWaypont;
    //             }
    //             else
    //             {
    //                 currentWaypoint = currentWaypoint.nextWaypoint;
    //                 direction = 0;
    //             }
    //         }
    //     }
    //         
    //     controller.SetDestination(currentWaypoint.getPosition());
    // }
}
