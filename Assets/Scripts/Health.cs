using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    private Animator animate;
    public Playermovment PM;
    public HealthBar hpbar;

    private void Start()
    {
        animate = GetComponent<Animator>();
        currentHealth = maxHealth;

    }
    public void TakeDamage()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentHealth--;
        hpbar.decreaceHealth();
        if(currentHealth > 0)
        {
            animate.SetTrigger("damagetake");
        }
        else
        {
            PM.Death(true);
        }
    }
}
