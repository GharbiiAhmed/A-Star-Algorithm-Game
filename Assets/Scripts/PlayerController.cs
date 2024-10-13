using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Movement speed of the player
    private PlayerGrid grid;  // Reference to the grid to get the path
    private List<PlayerNode> path = new List<PlayerNode>();
    private int targetIndex;  // The index of the current target node in the path

    void Start()
    {
        grid = FindObjectOfType<PlayerGrid>(); // Get reference to the PlayerGrid component
    }

    void Update()
    {
        // If there's a path and it's valid, move the player along the path
        if (grid.path != null && grid.path.Count > 0)
        {
            path = grid.path;
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        // Check if we still have valid targets in the path to move towards
        if (targetIndex < path.Count)
        {
            Vector3 targetPosition = path[targetIndex].worldPosition;

            // Move towards the next node in the path
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if we reached the current target node, move to the next one
            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
            {
                targetIndex++;
            }
        }
        else
        {
            // If we've reached the end of the path
            OnReachTarget();
        }
    }

    void OnReachTarget()
    {
        // You can trigger some event or stop movement here
        Debug.Log("Reached the target!");
        // Optionally reset target index if you want the player to keep looping or stop
        targetIndex = 0;
    }

    public void StartMoving()
    {
        // Called by some external trigger when movement should start
        if (grid.path != null && grid.path.Count > 0)
        {
            targetIndex = 0;  // Reset index to start moving from the first node in the path
        }
    }
}
