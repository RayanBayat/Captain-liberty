//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    public enemyAI enemy;
//    public int maxHealth = 100;
//    private int currenthp;
//    bool enemydead = false;
//    void Start()
//    {
//        currenthp = maxHealth;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (enemydead)
//        {
//            EnemyisDead();
//            return;
//        }
//        enemyAI();
//    }

//    void enemyAI()
//    {
//        enemy.AI();
//    }
//    public void enemytakedamage(int damage)
//    {
//        Debug.Log(currenthp);
//        currenthp -= damage;
//        if (currenthp <= 0)
//        {
//            enemy.dead();
//            enemydead = true;
//        }
//        else
//        {
//            enemy.hurt();
//        }
//    }

//    private void EnemyisDead()
//    {
//        enemy.deadcorpse();
//    }
//    //private bool CanseePlayer()
//    //{
//    //    return enemy.CanseePlayer();
//    //}
//}
