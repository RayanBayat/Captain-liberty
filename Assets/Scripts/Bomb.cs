using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float bombSpeed;
    public GameObject impactEffect;
    public Vector3 Luanchoffset;
    private bool thrown;
    // Start is called before the first frame update
    void Start()
    {

            var direction = transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * bombSpeed, ForceMode2D.Impulse);
  
        transform.Translate(Luanchoffset);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.right * bombSpeed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3 && gameObject.layer == 10)
        {
            other.gameObject.GetComponent<Health>().TakeDamage();

        }
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);

    }
}
