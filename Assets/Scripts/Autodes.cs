using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodes : MonoBehaviour
{
    public float wanttime = 1;
    void Start()
    {
        Destroy(gameObject, wanttime);
    }
}
