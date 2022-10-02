using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovment : MonoBehaviour
{
    private Rigidbody2D body;
    private float timeStamp;
    private GameObject myfeetobject;
    private BoxCollider2D myfeetcollider;
    private Animator animate;

    public Weapen weapen;
    private bool grounded;

    private bool facingright;
    private Vector3 respawnpoint;
    private BoxCollider2D mybodycollider;
    private bool isAlive = true;
    
     [SerializeField] float moveSpeed = 5f;
      [SerializeField] float spawncooldown = 2f;
     [SerializeField] float jumpHeight = 3f;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Vector2 deadpos =new Vector2(10f,10f);

     private void Awake() {
         body = GetComponent<Rigidbody2D>();
         body.freezeRotation = true;
         animate = GetComponent<Animator>();
         mybodycollider = GetComponent<BoxCollider2D>();
         myfeetobject = body.transform.Find("Feet").gameObject;
         myfeetcollider = myfeetobject.GetComponent<BoxCollider2D>();
     }
   
    private void Update() {
        float horizontalInput = getHorizontalInput();
        Rotate(horizontalInput);
        if (!isAlive || weapen.stationarry)
        {
            if (isGrounded())
            {
            stayinPlace();
            }

            Respawn();
            return;
        }
        // if (Input.GetKey(KeyCode.E))
        // {
        //    live = false;
        //     animate.SetTrigger("dead");
        // }
        //  if (Input.GetKey(KeyCode.Q))
        // {
        //      live = true;
        //       animate.SetTrigger("spawn");
        // }

        Jump();
        airSpeedY();
        Move(horizontalInput);
        
        Death();
        

        
    }

    private void stayinPlace()
    {
        body.velocity = new Vector2(0,body.velocity.y);
    }
    private void airSpeedY()
    {
        int _airSpeedY = (int)body.velocity.y;
        animate.SetInteger("airSpeedY",_airSpeedY);
        if (_airSpeedY > 13)
        {
            body.velocity = new Vector2(body.velocity.x,13f);
        }
    }
    private float getHorizontalInput()
    {
         return Input.GetAxis("Horizontal");
    }
    private void Rotate(float horizontalInput)
    {
        if (horizontalInput>0.01f)
       {
             transform.localRotation = Quaternion.Euler(transform.localRotation.x,0.0f,transform.localRotation.z);
       }
       else if(horizontalInput<-0.01f)
       {
             transform.localRotation = Quaternion.Euler(transform.localRotation.x,180.0f,transform.localRotation.z);
       }
    }
    private void Move(float horizontalInput)
    {
        
        body.velocity = new Vector2(horizontalInput*moveSpeed,body.velocity.y);
       

        animate.SetBool("run", horizontalInput != 0);
        animate.SetBool("grounded",isGrounded());
    }
    private void Jump(){

                

       if (Input.GetKey(KeyCode.Space) && isGrounded() )
       {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animate.SetTrigger("jump");
       }


       
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
         if (other.tag == "FallDetector")
         {
            isAlive = false;
            Death();
         }
         else if (other.tag == "Checkpoint")
         {
            respawnpoint = transform.position;
            other.GetComponent<Collider2D>().enabled = false;
         }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(myfeetcollider.bounds.center,myfeetcollider.bounds.size,0, Vector2.down,0.1f,groundlayer);
        return raycastHit.collider != null;
    }

    private void Death()
    {
        if(mybodycollider.IsTouchingLayers(LayerMask.GetMask("Enemies")) || 
        mybodycollider.IsTouchingLayers(LayerMask.GetMask("Traps")))
        {
            isAlive = false;
            body.bodyType = RigidbodyType2D.Static;
        }
        if (!isAlive)
        {
           
            animate.SetTrigger("dead");
            timeStamp = Time.time + spawncooldown;
            
        }
       animate.SetBool("live",isAlive);

    }
 
    private void Respawn()
    {
        if (Input.GetKey(KeyCode.Q) && timeStamp<=Time.time)
        {
       
        transform.position = respawnpoint;
         isAlive = true;
         body.bodyType = RigidbodyType2D.Dynamic;
         animate.SetBool("live",isAlive);
        animate.SetTrigger("spawn");
            
        }

        //

    }
}
