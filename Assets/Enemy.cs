using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Flyingbat batEnemy;
    private Animator animate;
    public int maxHealth = 100;
    private int currenthp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enemytakedamage(int damage)
    {
        Debug.Log(currenthp);
        currenthp -= damage;
        if (currenthp <= 0)
        {
            animate.SetTrigger("die");
          //  battakeDamage();
        }
        else
        {
            animate.SetTrigger("hurt");
        }
    }
}
