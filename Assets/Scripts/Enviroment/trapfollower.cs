using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapfollower : MonoBehaviour
{
    [SerializeField] private GameObject waypoint;
    [SerializeField] private float speed = 4f;
    public bool dofollow = false;

    void Update()
    {
        if(dofollow)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);

        }
    }
    public void follow()
    {
        dofollow = true;
    }
}
