using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomAI : MonoBehaviour
{
    [SerializeField] private Transform hitbox;
   // [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private float hitboxradius = 1.45f;
    [SerializeField] private float knockbackforce = 10f;
    [SerializeField] private float knockbackforceup = 2f;
    private GameObject player;
    private bool isHit = false;
    private bool hitCooldown = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
    //    player.GetComponent<Playermovment>().knocked = false;
        isHit = Physics2D.OverlapCircle(hitbox.position, hitboxradius, enemy);
       // Debug.Log("hit " + isHit);
        if (isHit == true)
        {
            player.GetComponent<Playermovment>().knocked = true;
            knockback();
        }
    }

    public void knockback()
    {
        var direction = transform.right + Vector3.up;
        player.GetComponent<Rigidbody2D>().AddForce(direction * knockbackforce, ForceMode2D.Impulse);
       // Vector2 knockbackdirection = new Vector2(player.transform.position.x - transform.position.x, 0);
       // player.GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackdirection.x, knockbackforceup) * knockbackforce;
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(hitbox.position, hitboxradius);
    }
}
