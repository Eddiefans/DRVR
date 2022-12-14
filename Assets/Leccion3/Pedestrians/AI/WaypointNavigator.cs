using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour{
    
    CharacterNavigationController controller;
    public Waypoint currentWaypoint;

    int direction;

    private void Awake() {
        controller = GetComponent<CharacterNavigationController>();
    }

    void Start(){
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        controller.SetDestination(currentWaypoint.getPosition());
    }

   void Update() {
        if(controller.reachedDestination){
            bool shouldBranch = false;

            if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f,1f) <= currentWaypoint.branchRatio ? true : false;
            }
            
            if(shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
            }
            else
            {
                if(direction == 0)
                {
                    if (currentWaypoint.nextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypont;
                        direction = 1;
                    }
                    
                }
                else if(direction == 1)
                {
                    if (currentWaypoint.previousWaypont != null)
                    {
                        currentWaypoint = currentWaypoint.previousWaypont;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        direction = 0;
                    }
                }
            }
            
            controller.SetDestination(currentWaypoint.getPosition());
        }
    }
}