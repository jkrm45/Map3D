using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class PlayerRespwan : MonoBehaviour
{
    public GameObject[] player;
    public GameObject playerprefebs;
    public int playercount = 20; //플레이어 접속수
    // Start is called before the first frame update
    void Start()
    {
        player = new GameObject[playercount];
        for (int i = 0; i < playercount; i++)
        {
            player[i]=Instantiate(playerprefebs, new Vector3(Random.Range(-49, 49), 20, Random.Range(-74, 74)), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
