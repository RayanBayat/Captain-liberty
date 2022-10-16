using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroom : MonoBehaviour
{
    public mushroomAI enemy;
    public int maxHealth = 100;
    private int currenthp;
    bool enemydead = false;
    void Start()
    {
        currenthp = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemydead)
        {
           // EnemyisDead();
            return;
        }
        enemyAI();
    }
    void enemyAI()
    {
        enemy.AI();
    }
    public void enemytakedamage(int damage)
    {
        currenthp -= damage;
        if (currenthp <= 0)
        {
            enemy.deadanim();
            enemydead = true;
        }
        else
        {
            enemy.hurtanim();
        }
    }
    private void EnemyisDead()
    {
       // enemy.dead();
    }
}
