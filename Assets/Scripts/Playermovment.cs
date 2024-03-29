using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playermovment : MonoBehaviour
{
    private Rigidbody2D body;
    private float timeStamp;
    private GameObject myfeetobject;
    private BoxCollider2D myfeetcollider;
    private Animator animate;
    public Health health;
    public Weapen weapen;
    private bool grounded;

    private bool facingright;
    private Vector3 respawnpoint;
    private BoxCollider2D mybodycollider;
    private bool isAlive = true;
    public bool knocked;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float spawncooldown = 2f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Vector2 deadpos = new Vector2(10f, 10f);
    [SerializeField] private AudioSource coincollect;
    [SerializeField] private Text cointext;
    private int coinscollected = 0;

    private float knocktimercd = 1f ,knocktimerf;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;
        animate = GetComponent<Animator>();
        mybodycollider = GetComponent<BoxCollider2D>();
        myfeetobject = body.transform.Find("Feet").gameObject;
        myfeetcollider = myfeetobject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!isAlive)
        {
            if (isGrounded())
            {
                stayinPlace();
            }

            Respawn();
            return;
        }
        Death(false);
        float horizontalInput = getHorizontalInput();
        if (knocked)
        {
 
            knocktimerf += Time.deltaTime;
            if(knocktimerf >= knocktimercd)
            {
                if (isGrounded())
                {
                    knocked = false;
                    knocktimerf = 0;
                }
                return;
            }
            return;
        }
       

        if (weapen.stationarry)
        {
            if (isGrounded())
            {
                stayinPlace();
            }
            return;
        }
        Rotate(horizontalInput);
        Jump();
        airSpeedY();
        Move(horizontalInput);

        



    }

    private void stayinPlace()
    {
        body.velocity = new Vector2(0, body.velocity.y);

    }
    private void airSpeedY()
    {
        int _airSpeedY = (int)body.velocity.y;
        animate.SetInteger("airSpeedY", _airSpeedY);
        if (_airSpeedY > 13)
        {
            body.velocity = new Vector2(body.velocity.x, 13f);
        }
    }
    private float getHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }
    private void Rotate(float horizontalInput)
    {
        if (horizontalInput > 0.01f)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0.0f, transform.localRotation.z);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180.0f, transform.localRotation.z);
        }
    }
    private void Move(float horizontalInput)
    {

        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);


        animate.SetBool("run", horizontalInput != 0);
        animate.SetBool("grounded", isGrounded());
    }
    private void Jump()
    {



        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
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
            Death(false);
        }
        else if (other.tag == "Checkpoint")
        {
            respawnpoint = transform.position;
            other.GetComponent<Collider2D>().enabled = false;
        }
        else if( other.tag == "coin")
        {
            Destroy(other.gameObject);
            coincollect.Play();
            coinscollected++;
            cointext.text = "X" + coinscollected;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(myfeetcollider.bounds.center, myfeetcollider.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }

    public void Death(bool dead)
    {
        if (mybodycollider.IsTouchingLayers(LayerMask.GetMask("Enemies")) ||
        mybodycollider.IsTouchingLayers(LayerMask.GetMask("Traps")))
        {
            isAlive = false;
            body.bodyType = RigidbodyType2D.Static;
        }
        if (!isAlive || dead)
        {

            animate.SetTrigger("dead");
            timeStamp = Time.time + spawncooldown;
            isAlive = false;
            mybodycollider.enabled = false;
            gameObject.layer = 16;
        }
        animate.SetBool("live", isAlive);

    }

    private void Respawn()
    {
        if (Input.GetKey(KeyCode.Q) && timeStamp <= Time.time || timeStamp > spawncooldown * 2)
        {
            mybodycollider.enabled = true;
            transform.position = respawnpoint;
            isAlive = true;
            body.bodyType = RigidbodyType2D.Dynamic;
            animate.SetBool("live", isAlive);
            animate.SetTrigger("spawn");
            health.respawn();
            gameObject.layer = 3;
        }

    }
    public bool buy(int coin)
    {
        if (coinscollected - coin < 0)
        {
            return false;
        }
        else
        {
            coinscollected = coinscollected - coin;
            cointext.text = "X" + coinscollected;
            return true;
        }
    }
}
