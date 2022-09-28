using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapen weapen;
    public float speed = 20f;
    public bool destroy = true;
    public Rigidbody2D rigidbody2Dbullet;
    public GameObject impactEffect;
    [SerializeField] GameObject parry;
    void Start()
    {
        rigidbody2Dbullet.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 11 && other.tag == "reflect")
        {
            //transform.position = (new Vector3(0,0,0));
            //transform.localRotation = Quaternion.Euler(transform.localRotation.x,180.0f,transform.localRotation.z);
            rigidbody2Dbullet.velocity = transform.right * -speed;
          //   Instantiate(parry_,transform.position,transform.rotation);
          //Instantiate(parry,transform.position,transform.rotation);   
        
        }
        else
        {
            Destroy(gameObject);
            Instantiate(impactEffect,transform.position,transform.rotation);   
        }
        

    }
    // Update is called once per frame

}
