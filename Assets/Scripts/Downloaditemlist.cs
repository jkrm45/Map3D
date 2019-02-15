using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Downloaditemlist : MonoBehaviour
{   
    string url= "https://assetbundletestbysong.run.goorm.io/post";

    void Start()
    {
        //StartCoroutine(ASD());
        
    }

    void Update()
    {
        
    }
    IEnumerator ASD()
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone)
        {
            string obj1 = File.ReadAllText(Path.Combine(Application.persistentDataPath, "itemlist"));
            print(obj1);
            print(JSON.Parse(obj1));
        }
    }
}