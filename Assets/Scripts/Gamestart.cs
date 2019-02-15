using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Gamestart : MonoBehaviour
{
    public TextAsset itemlist;
    public int minpnum = 1;
    public float countdown = 10;
    public PhotonView pv;
    public GameObject itembox;
    public int headcount = 40;
    public int weaponcount = 80;
    public int inshealcount = 60;
    public int dothealcount = 60;
    public int curhead;
    public int curweapon;
    public int curins;
    public int curdot;
    int k;
    public Circle c;

    void Start()
    {
        pv = PhotonView.Get(this);
    }

    void Update()
    {
        if(!GetComponent<Photoninit>().ready)
        {
            return;
        }
        if(!PhotonNetwork.isMasterClient)
        {
            enabled = false;
            return;
        }
        if (PhotonNetwork.room.PlayerCount >= minpnum)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                Itemspawn();
                c.gamestart = true;
                PhotonNetwork.room.IsOpen = false;
                enabled = false;
            }
        }
    }
    //게임 시작전 아이템 스폰 프로세스
    void Itemspawn()
    {
        StartCoroutine(Tem());
        pv.RPC("Randomitem", PhotonTargets.Others, k);
    }
    IEnumerator Tem()
    {
        int j = headcount + weaponcount + inshealcount + dothealcount;
        for (int i = 0; i < j; i++)
        {
            Randomitemindex();
        }
        yield return null;
    }
    void Randomitemindex()
    {   
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                if (curhead >= headcount)
                {
                    Randomitemindex();
                    break;
                }
                else
                {
                    curhead++;
                    k = Random.Range(0, 10);
                    if (k == 9)
                    {   
                        Randomitem(9);
                    }
                    else if (k >= 6)
                    {
                        Randomitem(8);
                    }
                    else
                    {
                        Randomitem(7);
                    }
                }
                break;
            case 1:
                if (curweapon >= weaponcount)
                {
                    Randomitemindex();
                    break;
                }
                else
                {
                    curweapon++;
                    k = Random.Range(0, 13);
                    if (k == 0)
                    {
                        Randomitem(1);
                    }
                    else if (k <= 3)
                    {
                        Randomitem(2);
                    }
                    else if (k <= 6)
                    {
                        Randomitem(3);
                    }
                    else if (k <= 8)
                    {
                        Randomitem(4);
                    }
                    else if (k <= 10)
                    {
                        Randomitem(5);
                    }
                    else if (k <= 12)
                    {
                        Randomitem(6);
                    }
                }
                break;
            case 2:
                if (curins >= inshealcount)
                {
                    Randomitemindex();
                    break;
                }
                else
                {
                    curins++;
                    k = Random.Range(0, 3);
                    if (k == 0)
                    {
                        Randomitem(10);
                    }
                    else if (k <= 3)
                    {
                        Randomitem(11);
                    }
                }
                break;
            case 3:
                if (curdot >= dothealcount)
                {
                    Randomitemindex();
                    break;
                }
                else
                {
                    curdot++;
                    k = Random.Range(0, 3);
                    if (k == 0)
                    {
                        Randomitem(12);
                    }
                    else if (k <= 3)
                    {
                        Randomitem(13);
                    }
                }
                break;
        }
    }
    [PunRPC]
    void Randomitem(int index)
    {
        var item = JSON.Parse(itemlist.text);
        GameObject tem = PhotonNetwork.InstantiateSceneObject("Itembox", new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), Quaternion.identity, 0, null);
        tem.GetComponent<Itemstat>().index = index;
        tem.GetComponent<Itemstat>().bullet = item[index]["bullet"];
        tem.GetComponent<Itemstat>().Decidelook();
    }
}