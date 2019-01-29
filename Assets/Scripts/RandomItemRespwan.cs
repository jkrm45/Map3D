using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RandomItemRespwan : MonoBehaviour {
    public GameObject[] weapon;
    public GameObject weapontype1;
    public GameObject weapontype2;
    public GameObject weapontype3;
    public GameObject weapontype4;
    public GameObject weapontype5;
    public GameObject weapontype6;

    public GameObject[] hellet;
    public GameObject hellmet1;
    public GameObject hellmet2;
    public GameObject hellmet3;

    public GameObject[] hellitem;
    public GameObject hellitem1;
    public GameObject hellitem2;
    //public GameObject hellitem3;

    public GameObject[] drink;
    public GameObject drink1;
    public GameObject drink2;
  

    public int helletarrysize = 30;
    public int weapoarrysize = 50;
    public int hellitemarrysize = 50;
    public int drinkarrysize = 30;

    void Start () {
        hellet = new GameObject[helletarrysize]; //핼맻 배열
        for (int i = 0; i < helletarrysize; i++)
        {
            var n = HellmetRandomitem();
            if (n == 1)
            {
                hellet[i]= Instantiate(hellmet1, new Vector3(Random.Range(-60,60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 2)
            {
                hellet[i] = Instantiate(hellmet2, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 3)
            {
                hellet[i] = Instantiate(hellmet3, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
        }
       weapon = new GameObject[weapoarrysize];  //무기 배열
        for (int i = 0; i < weapoarrysize; i++)
        {
            var n = WeaponRandomitem();
            if (n == 1)
            {
                weapon[i]= Instantiate(weapontype1, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 2)
            {
                weapon[i] = Instantiate(weapontype2, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 3)
            {
                weapon[i] = Instantiate(weapontype3, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 4)
            {
                weapon[i] = Instantiate(weapontype4, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 5)
            {
                weapon[i] = Instantiate(weapontype5, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 6)
            {
                weapon[i] = Instantiate(weapontype6, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
        }

       hellitem = new GameObject[hellitemarrysize]; //힐아이템 배열
        for (int i = 0; i < hellitemarrysize; i++)
        {
            var n = HellitemRandomitem();
            if (n == 1)
            {
                hellitem[i] = Instantiate(hellitem1, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 2)
            {
                hellitem[i] = Instantiate(hellitem2, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            //if (n == 3)
            //{
            //    hellitem[i] = Instantiate(hellitem3, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            //}
        }
       drink = new GameObject[drinkarrysize]; //드링크아이템 배열
        for (int i = 0; i < drinkarrysize; i++)
        {
            var n = drinkRandomitem();
            if (n == 1)
            {
                drink[i] = Instantiate(drink1, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
            if (n == 2)
            {
                drink[i] = Instantiate(drink2, new Vector3(Random.Range(-60, 60), 20, Random.Range(-80, 80)), transform.rotation);
            }
          
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    int HellmetRandomitem() //헬맻 확률
    {
        var Ranint = 0;
        var num= Random.Range(0, 10);
        if (num <= 1)
        {
            Ranint = 3;
        }
        if (num > 1&&num<=4)
        {
            Ranint = 2;
        }
        if (num > 4 &&num<10)
        {
            Ranint = 1;
        }

        return Ranint;
    } 
   

    int WeaponRandomitem()  //무기 종류별 확률
    {
        var Ranint = 0;
        var num = Random.Range(0, 12);
        if (num >= 0&&num<=1)
        {
            Ranint = 5;
        }
        if (num >= 2 && num <= 3)
        {
            Ranint = 4;
        }
        if (num >= 4 && num <= 5)
        {
            Ranint = 3;
        }
        if (num >= 6 && num <= 7)
        {
            Ranint = 2;
        }
        if (num >= 8 && num <= 9)
        {
            Ranint = 1;
        }
        if (num >= 10 && num <=12)
        {
            Ranint = 6;
        }

        return Ranint;
    }


    int HellitemRandomitem() //힐템 확률
    {
        var Ranint = 0;
        var num = Random.Range(0, 10);
        if (num >= 1 && num <= 4)
        {
            Ranint = 2;
        }
        if (num > 4 && num < 10)
        {
            Ranint = 1;
        }

        return Ranint;
    }

    int drinkRandomitem() //음료수 확률
    {
        var Ranint = 0;
        var num = Random.Range(0, 10);
        if (num <= 4)
        {
            Ranint = 1;
        }
        if (num > 4 && num <10)
        {
            Ranint = 2;
        }
        return Ranint;
    }
}
