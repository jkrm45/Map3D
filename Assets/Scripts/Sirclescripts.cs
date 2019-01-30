using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sirclescripts : MonoBehaviour
{
    public float firstsirclecool;
    public float scondsirclecool;
    public float thirdsirclecool;
    public float fourhsirclecool;
    public bool first = true;
    public bool scond = false;
    public bool third = false;
    public bool fourh = false;
    public float sirclecooltime;
    public Transform fistpositon;
    public GameObject sircle;


    
    // Start is called before the first frame update
    void Start()
    {
        fistpositon.position = new Vector3(Random.Range(-20, 20) , 0, Random.Range(-40, 40));
        sircle.transform.position = fistpositon.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (first == true)//첫번째
        {
            sirclecooltime = sirclecooltime + Time.deltaTime;
            if (sirclecooltime > firstsirclecool)
            {
                sircle.GetComponent<Transform>().localScale += new Vector3(-Time.deltaTime / 100, 0, -Time.deltaTime / 100);
                if (sircle.transform.localScale.x < 0.1)
                {
                    sircle.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    //fistpositon.position=new Vector3(fistpositon-(Random))
                    first = false;
                    scond = true;
                }

            }
        }
        if (scond == true)//두번째
        {
            sirclecooltime = sirclecooltime + Time.deltaTime;
            if (sirclecooltime > scondsirclecool)
            {
                sircle.GetComponent<Transform>().localScale += new Vector3(-Time.deltaTime / 100, 0, -Time.deltaTime / 100);
                if (sircle.transform.localScale.x < 0.05)
                {
                    sircle.GetComponent<Transform>().localScale = new Vector3(0.05f, 0.1f, 0.05f);
                    scond = false;
                    third = true;
                }

            }
        }
        if (third == true)
        {
            sirclecooltime = sirclecooltime + Time.deltaTime;
            if (sirclecooltime > thirdsirclecool)
            {
                sircle.GetComponent<Transform>().localScale += new Vector3(-Time.deltaTime / 100, 0, -Time.deltaTime / 100);
                if (sircle.transform.localScale.x < 0.025)
                {
                    sircle.GetComponent<Transform>().localScale = new Vector3(0.025f, 0.1f, 0.025f);
                    third= false;
                    fourh= true;
                }

            }
        }
        if (fourh== true)
        {
            sirclecooltime = sirclecooltime + Time.deltaTime;
            if (sirclecooltime > fourhsirclecool)
            {
                sircle.GetComponent<Transform>().localScale += new Vector3(-Time.deltaTime / 100, 0, -Time.deltaTime / 100);
                if (sircle.transform.localScale.x < 0.001)
                {
                    sircle.GetComponent<Transform>().localScale = new Vector3(0.001f, 0.1f, 0.001f);
                    fourh= false;
                }

            }
        }


    }



}
