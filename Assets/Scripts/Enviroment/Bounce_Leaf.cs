using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_Leaf : MonoBehaviour
{
    private BoxCollider2D collid;
    private Animator anim;
    private bool down;
    private GameObject player;
    
  
    // Start is called before the first frame update
     void Start()
    {
        collid = GetComponent<BoxCollider2D>();
         anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
       // collid.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    { 
         // Scores.AddPoint();
     
       
       float playerposY = collision.transform.position.y;
       float plantposy = transform.position.y;
        
        if(playerposY > plantposy)
        {
         // Physics2D.IgnoreCollision(collision.collider,collid,false);
           anim.SetTrigger("bounce");
           down = true;
          collid.enabled = false;
          StartCoroutine(EnableBox(4.0F));
        }
      
    }

    IEnumerator EnableBox(float waitTime) {
         yield return new WaitForSeconds(waitTime);
        collid.enabled = true;
        down = false;
    }


    // Update is called once per frame
    void Update()
    {
      float playerposy = player.transform.position.y - player.GetComponent<BoxCollider2D>().bounds.size.y/2;
      float plantposy = transform.position.y + collid.bounds.size.y/2;

      if (playerposy+0.5 >= plantposy)
      {
       // Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(),collid,false);
        collid.isTrigger = false;
      }
      else
      {
        //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(),collid);
         collid.isTrigger = true;
      }
       anim.SetBool("down",down);
        // collid.enabled = true;
    }
}
