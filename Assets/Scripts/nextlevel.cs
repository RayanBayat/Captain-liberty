using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextlevel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levelend;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelend.SetActive(true);
    }
}
