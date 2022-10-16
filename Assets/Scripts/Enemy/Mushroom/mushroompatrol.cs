using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroompatrol : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject[] waypoints;
    private Animator animate;
    private int currentWaypointIndex = 0;
    private float lastxpos, wait = 0;
    private bool running = true;


    [SerializeField] private float speed = 4f, standtime = 2f;

    // Update is called once per frame
    private void Start()
    {
        animate = GetComponent<Animator>();
        lastxpos = transform.position.x;
    }
    void Update()
    {

        animate.SetBool("run", running);
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            if (standtime > wait)
            {
                Direction();
                running = false;
                wait += Time.deltaTime;
                animate.SetBool("inCombat", true);
                return;
            }
            animate.SetBool("inCombat", false);
            wait = 0;
            running = true;
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
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z); //rotates 
        }
        else
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
        }
    }

}
