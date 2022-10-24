using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomAI : MonoBehaviour
{
    [SerializeField] private Transform hitbox;
    // [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float hitboxradius = 1.45f;
    [SerializeField] private float knockbackforce = 10f;
    //[SerializeField] private float knockbackforceup = 2f;
    [SerializeField] private float range, heightrange, distance;
    [SerializeField] private CapsuleCollider2D collid;
    [SerializeField] float attacktimer = 20f;
    [SerializeField] private float attackcd;
    private GameObject player;
    private bool isHit = false, inCombat = false;
    private Animator animate;
    private bool attacking;
    private mushroompatrol mushroompatrol;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask Playerlayer;
    [SerializeField] private AudioSource mushroomattack;
    //private bool hitCooldown = false;
    private int test = 1;
    void Start()
    {
        animate = GetComponent<Animator>();
        mushroompatrol = GetComponent<mushroompatrol>();
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        //    player.GetComponent<Playermovment>().knocked = false;
        //if (mushroompatrol != null)
        //{
        //    mushroompatrol.enabled = !inCombat;
        //}
    }

    public void animationstop()
    {
        mushroompatrol.enabled = false;
    }
    public void animationstart()
    {
        mushroompatrol.enabled = true;
    }
    public void AI()
    {

        if (CanseePlayer() || test > 0)
        {
            Direction();
            test = 0;
            inCombat = true;
            attacktimer += Time.deltaTime;
            animate.SetBool("run", false);
            if (attacktimer >= attackcd)
            {

                
                //knockback();
                animate.SetTrigger("attack1");
                attacktimer = 0;
               

                //attack1 is ranged attack

            }
        }
        else
        {
            inCombat = false;
        }
        animate.SetBool("inCombat", inCombat);
        //stop moving while attacking player
        if (mushroompatrol != null)
        {
            if (!attacking)
            {
                mushroompatrol.enabled = !inCombat;
            }

        }
    }
    public void knockback()
    {

        isHit = Physics2D.OverlapCircle(hitbox.position, hitboxradius, Playerlayer);
        mushroomattack.Play();
        // Debug.Log("hit " + isHit);
        if (isHit == true)
        {
            var direction = transform.right + Vector3.up;
            player.GetComponent<Playermovment>().knocked = true;
            player.GetComponent<Rigidbody2D>().AddForce(direction * knockbackforce, ForceMode2D.Impulse);
            if(!wasshielded())
            {
                player.GetComponent<Health>().TakeDamage();
            }
           
            // Vector2 knockbackdirection = new Vector2(player.transform.position.x - transform.position.x, 0);
            // player.GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackdirection.x, knockbackforceup) * knockbackforce;
        }
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(hitbox.position, hitboxradius);
    }
    private bool CanseePlayer()
    {

        RaycastHit2D hit = Physics2D.BoxCast(collid.bounds.center + transform.right * range * transform.localScale.x * distance,
        new Vector3(collid.bounds.size.x * range, collid.bounds.size.y * heightrange, collid.bounds.size.z), 0, Vector2.left, 0, Playerlayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(collid.bounds.center + transform.right * range * transform.localScale.x * distance,
        new Vector3(collid.bounds.size.x * range, collid.bounds.size.y * heightrange, collid.bounds.size.z));
    }
    public void hurtanim()
    {
        if(!attacking)
        {
            animate.SetTrigger("hurt");
        }
        
    }
    public void deadanim()
    {
        animate.SetTrigger("dead");
        mushroompatrol.enabled = false;
        collid.isTrigger = false;
        gameObject.layer = 12;
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collid.bounds.center, collid.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }
    private void Direction()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 180f, transform.localRotation.z); //rotates 
        }
        else
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0f, transform.localRotation.z);
        }
    }
    private bool wasshielded()
    {
        float shield_to_mob = Mathf.Abs(player.transform.GetChild(3).gameObject.transform.position.x - transform.position.x);
        float player_to_mob = Mathf.Abs(player.transform.GetChild(1).gameObject.transform.position.x - transform.position.x);
        if (player.transform.GetChild(3).gameObject.GetComponent<BoxCollider2D>().enabled)
        {
            if (shield_to_mob < player_to_mob)
            {
                return true;
            }
        }
        return false;
    }
    public void isattackin()
    {
        attacking = true;
    }
    public void isnotattacking()
    {
        attacking = false;
    }

}
