using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batpatrol : MonoBehaviour
{
    // Start is called before the first frame update
 
    [SerializeField] private GameObject[] waypoints;
    
    private int currentWaypointIndex = 0;
    private float lastxpos;

    [SerializeField] private float speed = 4f;

    // Update is called once per frame
    private void Start() {
        lastxpos = transform.position.x;
    }
    void Update()
    {

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position,transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        Direction();
        lastxpos = transform.position.x;
    }
    private void Direction()
    {
        if (lastxpos > transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x,180f,transform.localRotation.z);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x,0f,transform.localRotation.z);
        }
    }
}
