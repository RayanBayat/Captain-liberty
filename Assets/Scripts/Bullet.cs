using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    //public Weapen weapen;
    //public Health health;
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
        if (other.gameObject.layer == 11 && other.tag == "reflect" 
            && gameObject.layer == 10)
        {          
            rigidbody2Dbullet.velocity = transform.right * -speed;
        }
        else if(other.gameObject.layer == 3 && gameObject.layer == 10)
        {
          other.gameObject.GetComponent<Health>().TakeDamage();
          Destroy(gameObject);
          Instantiate(impactEffect, transform.position, transform.rotation);
        }
        else
        {
            Destroy(gameObject);
            Instantiate(impactEffect,transform.position,transform.rotation);   
        }
        

    }
    // Update is called once per frame

}
