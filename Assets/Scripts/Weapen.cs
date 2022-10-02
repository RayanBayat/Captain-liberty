using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapen : MonoBehaviour
{

    public Transform firepoint;
    public GameObject bulleftpref,shield;

    public BoxCollider2D shieldcollider;

    private float attacktimer = 20f;
    public float meleeattackcooldown = 1;
    private float combotimer = 0f;
    private bool shieldOn = false;
    public float shieldtimer;
    private bool comboattack = false;
    [SerializeField] float timetoParry;
    public float combocooldown = 0.5f;
    // Update is called once per frame
    private int attacknumber = 0;

    public bool stationarry = false;
    private Animator anim;
    [SerializeField] private LayerMask playerbullet;

    void Start()
    {
        anim = GetComponent<Animator>();
        shieldcollider = gameObject.transform.GetChild(3).GetComponent<BoxCollider2D>();
        shield = gameObject.transform.GetChild(3).gameObject;
    }
    void Update()
    {
       
    if (shieldtimer <timetoParry)
    {
        reflect_on();
    }
    else
    {
        reflect_off();
    }
    Shield();
    Meleeattack(); 
    

    }
    

    void reflect_on()
    {
        shield.transform.tag = "reflect";
    }
    void reflect_off()
    {
        shield.transform.tag = "Untagged";
    }
    void Shield()
    {
        if (shieldOn)
        {
            shieldtimer += Time.deltaTime;
        }
        else
        {
            shieldtimer = 0;
        }
        if (Input.GetMouseButtonDown(1))
        {
            
            anim.SetBool("shield",true);
            stationarry = true;
            shieldcollider.enabled = true;
            shieldOn = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            shieldOn = false;
             anim.SetBool("shield",false);
            stationarry = false;
            shieldcollider.enabled = false;
        }
    }
    void Meleeattack()
    {
        attacktimer += Time.deltaTime;
        if (comboattack)
        {
            combotimer += Time.deltaTime;
        }
        if(combotimer > combocooldown || attacknumber > 2)
            {
                combotimer = 0;
                attacktimer = 0;
                attacknumber = 0;
                comboattack = false;
                anim.SetInteger("attacknumber",attacknumber);
               // anim.SetTrigger("combodone");
            }

        if (attacktimer >= meleeattackcooldown || 
        (combotimer > 0 && combotimer < combocooldown))
        {
            
            
	       if (Input.GetButtonDown("Fire1"))
	        {
                combotimer = 0;
                comboattack = true;

                if (attacknumber == 0)
                {
                    attacknumber = 1;
                    anim.SetInteger("attacknumber",attacknumber);
	                anim.SetTrigger("attack");
	               
                }
                else if (attacknumber == 1)
                {
                    attacknumber = 2;
                    anim.SetInteger("attacknumber",attacknumber);
	                anim.SetTrigger("attack");
	                
                }
                else if (attacknumber == 2)
                {
                    attacknumber = 3;
                    anim.SetInteger("attacknumber",attacknumber);
	                anim.SetTrigger("attack");
                    
                }
                

	        }

 
        }
    
       // else 
        // {
        //     attacknumber = 0;
        //     combotimer = 0;
        // }
    }
    void Shoot()
    {
        Instantiate(bulleftpref,firepoint.position,firepoint.rotation);
        bulleftpref.layer = LayerMask.NameToLayer("Playerbullets");
        attacknumber = 0;
    }
}
