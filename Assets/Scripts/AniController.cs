using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    private static AniController _instance = null;
    public static AniController Instance { get { return _instance; } }
    public Animator AniControll;
    public Transform Player;
    public int Job = 1;
    // Start is called before the first frame update

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {

        AniControll = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RunOff();
        WalkOff();
        AttackOff();
        DrinkOff();
        Eatpoff();
        PickUpoff();
        Changeoff();
        if (Input.GetKey(KeyCode.Q))
        {
            
            Job = 1;
            Chrickidle();
        }
        if (Input.GetKey(KeyCode.W))
        {
            Job = 2;
            Chrickidle();
        }
        if (Input.GetKey(KeyCode.E))
        {
            Job = 3;
            Chrickidle();
        }
        if (Input.GetKey(KeyCode.R))
        {
            Job = 4;
            Chrickidle();
        }
        if (Input.GetKey(KeyCode.A))
        {
            ChangeOn();
        }
        if (Input.GetKey(KeyCode.Z))
        {
            RunOn();
        }
        if (Input.GetKey(KeyCode.S))
        {
            WalkOn();
        }
        if (Input.GetKey(KeyCode.D))
        {
            AttackOn();
        }
        if (Input.GetKey(KeyCode.F))
        {
            DrinkOn();
        }
        if (Input.GetKey(KeyCode.G))
        {
            EatOn();
        }
        if (Input.GetKey(KeyCode.F))
        {
            PickUpOn();
        }
    }
    //key값"  Archer , Magician . Swardman , Food
    //key값    Attack , Die , Run , Back
    //key 값 
    //key 값 Drink , Eat , Pickup , Change


    public void Chrickidle() //애니메이션 종류선택
    {
        if (Job==1)
        {
          
            AniControll.SetBool("Swardman", true);
            AniControll.SetBool("Archer", false);
            AniControll.SetBool("Magician", false);
            AniControll.SetBool("Food", false);
            AniControll.SetBool("Change", false);
         
        }
        if (Job ==2)
        {
            AniControll.SetBool("Swardman", false);
            AniControll.SetBool("Archer", true);
            AniControll.SetBool("Magician", false);
            AniControll.SetBool("Food", false);
            AniControll.SetBool("Change", false);
        }
        if (Job ==3)
        {
            AniControll.SetBool("Swardman", false);
            AniControll.SetBool("Food", false);
            AniControll.SetBool("Archer", false);
            AniControll.SetBool("Magician", true);
            AniControll.SetBool("Change", false);
        }
        if (Job ==4)
        {
            AniControll.SetBool("Food", true);
            AniControll.SetBool("Swardman", false);
            AniControll.SetBool("Archer", false);
            AniControll.SetBool("Magician", false);
            AniControll.SetBool("Change", false);
        }
    }

    public void RunOn() // 달리기 불값 
    {
        AniControll.SetBool("Run", true);
    }
    public void RunOff()
    {
        AniControll.SetBool("Run", false);
    }


    public void WalkOn()//천천히 걷기
    {
        AniControll.SetBool("Back", true);
    }
    public void WalkOff()
    {
        AniControll.SetBool("Back", false);
    }

    public void AttackOn() //어택 함수호출
    {
        AniControll.SetBool("Attack", true);
    }
    public void AttackOff()
    {
        AniControll.SetBool("Attack", false);
    }

    public void DeadOn()  //죽는모션호출
    {
        AniControll.SetBool("Die", true);
    }
    public void DeadOff()
    {
        AniControll.SetBool("Die", false);
    }


    public void DrinkOn()//마시는 모션
    {
        AniControll.SetBool("Drink", true);
    }
    public void DrinkOff()
    {
        AniControll.SetBool("Drink", false);
    }

    public void PickUpOn()//줍는모션
    {
        AniControll.SetBool("Pickup", true);
    }
    public void PickUpoff()
    {
        AniControll.SetBool("Pickup", false);
    }

    public void EatOn()//먹는 모션
    {
        AniControll.SetBool("Eat", true);
    }
    public void Eatpoff()
    {
        AniControll.SetBool("Eat", false);
    }

    public void ChangeOn()//바꾸는 모션
    {
        AniControll.SetBool("Change", true);
    }
    public void Changeoff()
    {
        AniControll.SetBool("Change", false);
    }






}
