using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertakeController : MonoBehaviour
{
    [SerializeField] private List<string> detectionList = new List<string>();
    
    public void AddIdToList(string id)
    {
        detectionList.Add(id);
        print("last id added "  + id);
        if (detectionList.Count == 4)
        {
            //print("count is 4 " + detectionList[3]);
            
            if (detectionList[3] == "OvertakeGridRightBack")
            {
                //print("check overtake");
                if (detectionList[0] == "OvertakeGridFront"
                    && detectionList[1] == "OvertakeGridFrontRight"
                    && detectionList[2] == "OvertakeGridRight"
                    && detectionList[3] == "OvertakeGridRightBack")
                {
                    print("rebase completado");
                }
            }
        }

        if (detectionList.Count > 4)
        {
            detectionList.RemoveAt(0);
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     print(other.transform.name);
    //     print("detector " + transform.name);
    // }
}
