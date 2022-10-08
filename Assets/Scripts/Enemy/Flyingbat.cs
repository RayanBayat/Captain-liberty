using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingbat : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float attackcd;
    [SerializeField] private float range;
    [SerializeField] private float heightrange;
    public int maxHealth = 100;
    private int currenthp;
    public GameObject bulleftpref;
    [SerializeField] private int damage;
    [SerializeField]private BoxCollider2D collid;
    [SerializeField] private LayerMask Playerlayer;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask groundlayer;
    private Animator animate;
    public Transform firepoint;
    private float attacktimer = 20f;
    private Batpatrol batpatrol;
    bool localdead;
    private void Awake() {
        animate = GetComponent<Animator>();
        batpatrol = GetComponent<Batpatrol>();
    }

    void Start()
    {
        currenthp = maxHealth;
    }
    void Update()
    {

        //  GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if (localdead)
        {
            batdead();
            return;
        }
        batAI();
        


//Enemiesbullets
    }
    private void batAI()
    {
        if (CanseePlayer())
        {

            attacktimer += Time.deltaTime;

            if (attacktimer >= attackcd)
            {

                attacktimer = 0;
                //attack1 is ranged attack
                animate.SetTrigger("attack1");
            }
        }
        if (batpatrol != null)
        {
            batpatrol.enabled = !CanseePlayer();
        }
    }
    
    public void battakeDamage(int damage)
    {
        Debug.Log(currenthp);
        currenthp -= damage;
        if(currenthp <= 0)
        {
            animate.SetTrigger("die");
            batdead();
        }
        else
        {
            animate.SetTrigger("hurt");
        }
    }
    private void batdead()
    {
 
            if (isGrounded() && collid.isTrigger == false)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                collid.isTrigger = true;
            }
    
        

    }
    private bool CanseePlayer() 
    {
        RaycastHit2D hit = Physics2D.BoxCast(collid.bounds.center + transform.right * range * transform.localScale.x * distance,
        new Vector3(collid.bounds.size.x * range, collid.bounds.size.y * heightrange, collid.bounds.size.z),0,Vector2.left,0,Playerlayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(collid.bounds.center + transform.right * range * transform.localScale.x * distance,
        new Vector3(collid.bounds.size.x * range, collid.bounds.size.y * heightrange, collid.bounds.size.z));
    }
    void Shoot()
    {
        Instantiate(bulleftpref,firepoint.position,firepoint.rotation);
        bulleftpref.layer = LayerMask.NameToLayer("Enemiesbullets");
        
    }
    void setDead()
    {
        batpatrol.enabled = false;
        collid.isTrigger = false;
        localdead = true;
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collid.bounds.center, collid.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }
}
