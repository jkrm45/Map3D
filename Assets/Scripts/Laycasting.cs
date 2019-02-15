using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laycasting : MonoBehaviour
{   
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 8))
        {
            print(hit.transform.tag);
        }
        
        Debug.DrawRay(transform.position, fwd, Color.red);
    }
}
