using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    [SerializeField] private AudioSource coincollect;
    // Start is called before the first frame update
    public void coincollected()
    {
            coincollect.Play();
            Destroy(gameObject);
    }
}
