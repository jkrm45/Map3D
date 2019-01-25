using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAutoMatch : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        if (transform.position.x < -34)
        {
            gameObject.SetActive(false);

        }
        if (transform.position.x > 4.5)
        {
            gameObject.SetActive(false);

        }
        if (transform.position.y < -30)
        {
            gameObject.SetActive(false);

        }
        if (transform.position.y > 8.8)
        {
            gameObject.SetActive(false);

        }
    }
	
	// Update is called once per frame
	void Update () {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="water" || collision.tag =="Rock" || collision.tag =="Tree")
        {
            //transform.position = new Vector3((transform.position.x + 0.5f), 0, 0);

            gameObject.SetActive(false);
        }
        if (collision.tag == "weapon" || collision.tag == "hellmet" || collision.tag == "hellitem" || collision.tag == "drink")
        {
            transform.position = new Vector3((transform.position.x + 0.5f), 0, 0);
            print("아이템충돌!!!");
         
        }
    }

}
