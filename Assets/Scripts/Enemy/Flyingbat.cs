using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingbat : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float attackcd;
    [SerializeField] private float range;
    [SerializeField] private float heightrange;
    public GameObject bulleftpref;
    [SerializeField] private int damage;
    [SerializeField]private BoxCollider2D collid;
    [SerializeField] private LayerMask Playerlayer;
    [SerializeField] private float distance;
    private Animator animate;
    public Transform firepoint;
    private float attacktimer = 20f;
    private Batpatrol batpatrol;
    private void Awake() {
        animate = GetComponent<Animator>();
        batpatrol = GetComponent<Batpatrol>();
    }
    void Update()
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
//Enemiesbullets
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
}
