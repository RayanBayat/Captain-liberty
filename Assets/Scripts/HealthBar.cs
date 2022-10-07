using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    int heart ,currhp;
    public GameObject heartobject;
    public Health health;
    public Transform parentObject;
    public GameObject prefab;
    void Start()
    {
        heart = health.maxHealth-1;
        currhp = heart;
        for (int i = 0;i <= heart; i++)
        {
            Instantiate(prefab, parentObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q) && currhp >= 0)
        //{
        //    gameObject.transform.GetChild(currhp).gameObject.SetActive(false);
        //    currhp--;
        //}
        //else if(Input.GetKeyDown(KeyCode.K) && currhp < heart)
        //{
        //    currhp++;
        //    gameObject.transform.GetChild(currhp).gameObject.SetActive(true);
           
        //}
    }

    public void decreaceHealth()
    {
        if (currhp >= 0)
        {
            gameObject.transform.GetChild(currhp).gameObject.SetActive(false);
            currhp--;
        }
    }
    public void increaceHealth()
    {
        if (currhp < heart)
        {
            currhp++;
            gameObject.transform.GetChild(currhp).gameObject.SetActive(true);

        }
    }
}
