using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-4 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(+4 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 4 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -4 * Time.deltaTime, 0);
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Translate(0, 0, 4 * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Translate(0, 0, -4 * Time.deltaTime);
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rock")
        {
            print("!");
        }
       
    }
}
