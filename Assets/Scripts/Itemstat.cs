using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemstat : MonoBehaviour
{
    public int index;
    public int bullet;
    public List<GameObject> look;
    public Transform t;
    PhotonView pv;
    public int nextindex;
    public int nextbullet;
    private void Start()
    {
        t = GetComponent<Transform>();
        pv = PhotonView.Get(this);
        pv.ObservedComponents[0] = this;
    }
    public void Decidelook()
    {
        for (int i = 0; i < look.Count; i++)
        {
            if (i == index)
            {
                look[i].SetActive(true);
            }
            else
            {
                look[i].SetActive(false);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Map")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {   
        if(stream.isWriting)
        {
            stream.SendNext(index);
            stream.SendNext(bullet);
        }
        else
        {
            index = (int)stream.ReceiveNext();
            bullet = (int)stream.ReceiveNext();
        }
        Decidelook();
    }
}