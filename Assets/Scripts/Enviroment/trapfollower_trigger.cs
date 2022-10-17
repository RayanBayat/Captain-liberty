using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapfollower_trigger : MonoBehaviour
{

    public trapfollower trapfollower;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        trapfollower.follow();
    }
}
