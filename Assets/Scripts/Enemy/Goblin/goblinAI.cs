using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinAI : MonoBehaviour
{
    [SerializeField] private float attackcd;
    [SerializeField] private float bombrange;
    [SerializeField] private float bombheightrange;
    [SerializeField] private float bombdistance;
    [SerializeField] private float meleerange;
    [SerializeField] private float swordrange;
    [SerializeField] private float swordheightrange;
    [SerializeField] private float sworddistance;
    [SerializeField] private AudioSource bomblaunch;
    public GameObject bulleftpref;
    [SerializeField] private BoxCollider2D collid;
    [SerializeField] private LayerMask Playerlayer;

    [SerializeField] private LayerMask groundlayer;
    [SerializeField] Transform firepoint;
    [SerializeField] float attacktimer = 20f;
    private bool inCombat;
    private Animator animate;
    private goblinPatrol goblinpatrol;
    void Start()
    {
        animate = GetComponent<Animator>();
        goblinpatrol = GetComponent<goblinPatrol>();
    }
    public void AI()
    {
      
        if (CanseePlayerRanged())
        {

            inCombat = true;
            attacktimer += Time.deltaTime;

            if (attacktimer >= attackcd)
            {

                attacktimer = 0;
                //attack1 is ranged attack
                animate.SetTrigger("attack2");
            }
        }
        else if (CanseePlayerMelee())
        {
            inCombat = true;
            attacktimer += Time.deltaTime;

            if (attacktimer >= attackcd)
            {

                attacktimer = 0;
                //attack1 is ranged attack
                animate.SetTrigger("attack1");
            }
        }
        else
        {
            inCombat = false;
        }
        animate.SetBool("inCombat", inCombat);
        //stop moving while attacking player
        if (goblinpatrol != null)
        {
            goblinpatrol.enabled = !inCombat;
        }
    }
    private bool CanseePlayerRanged()
    {

        RaycastHit2D hit = Physics2D.BoxCast(collid.bounds.center + transform.right * bombrange * transform.localScale.x * bombdistance,
        new Vector3(collid.bounds.size.x * bombrange, collid.bounds.size.y * bombheightrange, collid.bounds.size.z), 0, Vector2.left, 0, Playerlayer);
        return hit.collider != null;
    }
    private bool CanseePlayerMelee()
    {

        RaycastHit2D hit = Physics2D.BoxCast(collid.bounds.center + transform.right * swordrange * transform.localScale.x * sworddistance,
        new Vector3(collid.bounds.size.x * swordrange, collid.bounds.size.y * swordheightrange, collid.bounds.size.z), 0, Vector2.left, 0, Playerlayer);
        return hit.collider != null;
    }
    void Sword(int damage)
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(firepoint.position, meleerange, Playerlayer);

        foreach (Collider2D player in hitPlayer)
        {
            if (player.name == "Player")
            {
                player.GetComponent<Health>().TakeDamage();
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(collid.bounds.center + transform.right * bombrange * transform.localScale.x * bombdistance,
        new Vector3(collid.bounds.size.x * bombrange, collid.bounds.size.y * bombheightrange, collid.bounds.size.z));
    }
    //private void OnDrawGizmos() 
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(collid.bounds.center + transform.right * swordrange * transform.localScale.x * sworddistance,
    //    new Vector3(collid.bounds.size.x * swordrange, collid.bounds.size.y * swordheightrange, collid.bounds.size.z));
    //}
    private void OnDrawGizmosSelected()
    {
        if (firepoint == null)
            return;
        Gizmos.DrawWireSphere(firepoint.position, meleerange);
    }
    public void hurtanim()
    {
        animate.SetTrigger("hurt");
    }
    public void deadanim()
    {
        animate.SetTrigger("die");
        goblinpatrol.enabled = false;
        collid.isTrigger = false;
        gameObject.layer = 12;
    }
    public void dead()
    {

        //if (isGrounded() && collid.isTrigger == false)
        //{
        //    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //    collid.isTrigger = true;
        //}



    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collid.bounds.center, collid.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }
    void Shoot()
    {
        Instantiate(bulleftpref, firepoint.position, firepoint.rotation);
        bomblaunch.Play();
        bulleftpref.layer = LayerMask.NameToLayer("Enemiesbullets");

    }
}
