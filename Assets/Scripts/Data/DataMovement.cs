using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DataMovement : MonoBehaviour
{
    private Path currentPath;
    private int waypointIndex = 0;
    [SerializeField] private float speed;
    
    private void Update()
    {
        if (CanMove())
        {
            Move();    
        }
        
    }

    private bool CanMove()
    {
        return waypointIndex < currentPath.waypoints.Length;
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPath.waypoints[waypointIndex].position, speed * Time.deltaTime);

        if (CanGoToNextWaypoint())
        {
            waypointIndex++;
        }
    }

    private bool CanGoToNextWaypoint()
    {
        return Vector2.Distance(transform.position, currentPath.waypoints[waypointIndex].position) < 0.1f;
    }

    public void SetCurrentPath(Path path)
    {
        currentPath = path;
    }
}
