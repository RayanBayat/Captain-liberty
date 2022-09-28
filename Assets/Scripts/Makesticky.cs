using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Makesticky : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other) 
    {
 
    other.rigidbody.interpolation = RigidbodyInterpolation2D.None;
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        other.rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
    if(other.gameObject.name == "Player")
        {
            other.gameObject.transform.SetParent(transform);
          
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.transform.SetParent(null);
            
        }
    }
}
