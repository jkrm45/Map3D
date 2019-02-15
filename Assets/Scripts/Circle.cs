using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject circle;
    public GameObject circle2;
    public float firstsirclecool;
    public float scondsirclecool;
    public float thirdsirclecool;
    public float fourhsirclecool;
    public bool first = true;
    public bool scond = false;
    public bool third = false;
    public bool fourh = false;
    public float sirclecooltime;
    public Vector3 before;
    public Vector3 after;
    public PhotonView pv;
    public bool gamestart;

    // Start is called before the first frame update
    void Start()
    {
        pv = PhotonView.Get(this);
        circle = GameObject.Find("DamageZone");
        circle2 = GameObject.Find("DamageZone1");
        Newposion();
    }
    void Update()
    {
        if (PhotonNetwork.connected && gamestart == true)
        {
            Deadzone();
        }
    }
    
    void Newposion()
    {
        float range = (circle.transform.localScale.x - circle2.transform.localScale.x) / 2 *1250; 
        float radius = Random.Range(0, range);
        float rad = Random.Range(0, Mathf.PI* 2);
        float newx = radius* Mathf.Cos(rad);
        float newy = radius*Mathf.Sin(rad);
        circle2.transform.position = circle.transform.position + new Vector3(newx, 0, newy);
    }
    void Deadzone()
    {
        StartCoroutine(Circlemoving());
        pv.RPC("CmRPC", PhotonTargets.Others);
    }
    [PunRPC]
    void CmRPC()
    {
        StartCoroutine(Circlemoving());
    }
    IEnumerator Circlemoving()
    {
        sirclecooltime = sirclecooltime + Time.deltaTime;
        if (first == true)//첫번째
        {
            if (sirclecooltime > firstsirclecool)
            {
                StartCoroutine(Movenextdeadzone());
                if (circle.transform.localScale.x <= 0.11)
                {
                    Debug.Log("!!!");
                    circle2 = GameObject.Find("DamageZone2");
                    Newposion();
                    first = false;
                    scond = true;
                }
            }
        }
        if (scond == true)//두번째
        {
            if (sirclecooltime > scondsirclecool)
            {
                StartCoroutine(Movenextdeadzone());
                if (circle.transform.localScale.x <= 0.055)
                {
                    Debug.Log("!!!");
                    circle2 = GameObject.Find("DamageZone3");
                    Newposion();
                    scond = false;
                    third = true;
                }
            }
        }
        if (third == true)
        {
            if (sirclecooltime > thirdsirclecool)
            {
                StartCoroutine(Movenextdeadzone());
                if (circle.transform.localScale.x <= 0.029)
                {
                    circle2 = GameObject.Find("DamageZone4");
                    Newposion();
                    third = false;
                    fourh = true;
                }
            }
        }
        if (fourh == true)
        {
            sirclecooltime = sirclecooltime + Time.deltaTime;
            if (sirclecooltime > fourhsirclecool)
            {
                StartCoroutine(Movenextdeadzone());
            }
        }
        yield return null;
    }
    IEnumerator Movenextdeadzone()
    {
        Vector3 ori = circle.transform.localScale;
        Vector3 bb = (circle2.transform.localScale - circle.transform.localScale);
        before = circle.transform.position;
        after = circle2.transform.position;
        Vector3 aa = after - before;
        for (float i = 0; i < 1.01; i += 0.009f)
        {
            circle.transform.position = before + i * aa;
            circle.transform.localScale = ori + i * bb;
            yield return new WaitForFixedUpdate();
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gamestart);
            stream.SendNext(sirclecooltime);
            stream.SendNext(circle.transform.position);
            stream.SendNext(circle2.transform.position);
        }
        else
        {
            gamestart = (bool)stream.ReceiveNext();
            sirclecooltime =(float)stream.ReceiveNext();
            circle.transform.position = (Vector3)stream.ReceiveNext();
            circle2.transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}