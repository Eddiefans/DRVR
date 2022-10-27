using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CarWaypointSystem : MonoBehaviour
{
    [Serializable]
    class Path
    {
        public Material pathMaterial;
        public List<GameObject> wayPoints = new List<GameObject>();
    }

    [SerializeField] private List<Path> paths;
    [SerializeField] private int currentPath;
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private GameObject lastWaypointCreated;
    public void CreateNewPath()
    {
        paths.Add(new Path());
        currentPath = paths.Count-1;
    }

    public void AddWaypointToCurrentPath()
    {
        if(paths.Count == 0)
            CreateNewPath();
        
        Vector3 position = Vector3.zero;
        if (lastWaypointCreated != null)
        {
            position = lastWaypointCreated.transform.position + lastWaypointCreated.transform.forward * 5;
        }
        else
        {
            position = transform.position + transform.forward * 5;
        }

        lastWaypointCreated = Instantiate(waypointPrefab, position, Quaternion.identity, transform);
        lastWaypointCreated.name = "Waypoint " + paths[currentPath].wayPoints.Count
                                              + " on path " + currentPath;
        MeshRenderer[] childRenderers = lastWaypointCreated.transform.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in childRenderers)
        {
            renderer.material = paths[currentPath].pathMaterial;
        }
        paths[currentPath].wayPoints.Add(lastWaypointCreated);
    }

    public void DeleteRoad()
    {
        for (int i = 0; i < paths[currentPath].wayPoints.Count; i++)
        {
            DestroyImmediate(paths[currentPath].wayPoints[i]);
        }
        paths[currentPath].wayPoints.Clear();
        paths.RemoveAt(currentPath);
        if (currentPath > 0)
        {
            currentPath--;
        }
    }

    public void DeleteLastWaypoint()
    {
        paths[currentPath].wayPoints.RemoveAt(paths[currentPath].wayPoints.Count-1);
        DestroyImmediate(lastWaypointCreated);
    }
}
