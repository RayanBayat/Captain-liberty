using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroller : MonoBehaviour
{
   private float length, startpos;
   public new GameObject camera;
   [SerializeField] public float parallexEffect;
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (camera.transform.position.x * (1-parallexEffect));
        float distance = (camera.transform.position.x * parallexEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
       
        if(temp > startpos + length) 
        {
            startpos += length;
        }
        else if ( temp < startpos - length) 
        {
            startpos -= length;
        }
    }
}
