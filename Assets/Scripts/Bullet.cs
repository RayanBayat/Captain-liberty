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
            gameObject.layer = 8;
            return;
        }
        else if (other.gameObject.layer == 3 && gameObject.layer == 10)
        {
            other.gameObject.GetComponent<Health>().TakeDamage();

        }
        else if (other.gameObject.layer == 9 && gameObject.layer == 8)
        {
            if (other.name == "Bat")
            {
                other.GetComponent<Enemy>()
                    .enemytakedamage(1000);
            }
            else if (other.name == "Goblin")
            {
                other.GetComponent<goblin>()
                    .enemytakedamage(1000);
            }
            else if (other.name == "Mushroom")
            {
                other.GetComponent<mushroom>()
                    .enemytakedamage(1000);
            }
            //other.GetComponent<Enemy>().enemytakedamage(50);

        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);



    }
    // Update is called once per frame

}
