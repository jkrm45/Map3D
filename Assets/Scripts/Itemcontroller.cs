using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Itemcontroller : MonoBehaviour
{
    public TextAsset data;
    public string itemname;
    public int index;
    public int bullet;
    // Start is called before the first frame update
    private void Awake()
    {
       
      
    }

    void Start()
    {
        var N = JSON.Parse(data.text);
        for (int i = 0; i < N.Count; i++)
        {
            if (N[i]["name"]==itemname)
            {
                //Debug.Log(N[i]["name"]);
                index = N[i]["index"];
                bullet = N[i]["bullet"];
            }
            //else
            //{
            //    Debug.Log("!!!");
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        //var N = JSON.Parse(data.text);
        //for (int i = 0; i < N.Count; i++)
        //{
        //    if (N[i]["name"] = itemname)
        //    {
        //        index = N[i]["index"];
        //        bullet = N[i]["bullet"];
        //    }
        //}
        if (transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("!!");
            
        }
    }
}
