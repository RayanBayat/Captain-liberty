using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombexplode : MonoBehaviour
{

    // Start is called before the first frame update
    //Even parry uses this script
    [SerializeField] private AudioSource explodesound;
    void Destroyobject()
    {
        explodesound.Play();
        Destroy(gameObject);

    }
}

