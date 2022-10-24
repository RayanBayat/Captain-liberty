using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allowtojumpon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myfeetobject;


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("myfeetobject.transform.position.x : " +  myfeetobject.transform.position.x);
        //Debug.Log("transform.position.x : " +transform.position.x);
        if (myfeetobject.transform.position.y < transform.position.y)
        {
            gameObject.layer = 15;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
