using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Playercnt : MonoBehaviour
{
    float basespd;
    public GameObject itembox;
    Inventory iv;
    public bool firing;
    public UIJoystick stick;
    Vector3 stickpos;
    public float spd = 5;
    public float rotspd = 120;
    public Transform t;
    PhotonView pv;
    Vector3 curpos;
    Quaternion currot;
    public Transform firepos;
    public GameObject bullet;
    public UIEventTrigger firebtn;
    EventDelegate fireon;
    EventDelegate fireoff;
    public int killscore;
    public float cooltime;
    public UIButton pickbtn;
    EventDelegate picking;
    public UILabel dmgtxt;
    public UILabel hptxt;
    EventDelegate cngbw;
    public UIButton backweaponbtn;
    EventDelegate cngfw;
    public UIButton frontweaponbtn;
    public List<GameObject> grounditem;
    float reloadtime = 3;
    float reloadcool;
    int slotnum;
    bool reload;
    Transform checktr;
    int checkslot;
    bool swing;
    float ptime;
    float ftime;
    float itime;
    public bool ring;
    float rtime;
    Animator AniControll;
    public Transform Player;
    public int Job = 1;
    Vector3 v;
    public List<GameObject> hiteft;


    void Start()
    {
        v = transform.forward;
        AniControll = GetComponent<Animator>();
        basespd = spd;
        iv = GetComponentInChildren<Inventory>();
        iv.p = this;
        frontweaponbtn = GameObject.Find("Frontweapon").GetComponent<UIButton>();
        backweaponbtn = GameObject.Find("Backweapon").GetComponent<UIButton>();
        pickbtn = GameObject.Find("Pickupbtn").GetComponent<UIButton>();
        dmgtxt = GameObject.Find("dmglabel").GetComponent<UILabel>();
        hptxt = GameObject.Find("hplabel").GetComponent<UILabel>();
        firebtn = GameObject.Find("Firebtn").GetComponent<UIEventTrigger>();
        stick = GameObject.Find("handle").GetComponent<UIJoystick>();
        stick.pl = this;
        pv = GetComponent<PhotonView>();
        t = GetComponent<Transform>();
        pv.ObservedComponents[0] = this;
        //카메라 할당
        if (pv.isMine)
        {
            GameObject.Find("Main Camera").GetComponent<SmoothFollow>().target = t;
            gameObject.tag = "Player";
        }
        else
        {
            gameObject.tag = "Enemy";
        }
        //버튼 동적 할당
        fireon = new EventDelegate(this, "Firebtnon");
        fireoff = new EventDelegate(this, "Firebtnoff");
        firebtn.onPress.Add(fireon);
        firebtn.onRelease.Add(fireoff);
        picking = new EventDelegate(this, "Pickupitem");
        pickbtn.onClick.Add(picking);
        cngbw = new EventDelegate(this, "Backweapon");
        backweaponbtn.onClick.Add(cngbw);
        cngfw = new EventDelegate(this, "Frontweapon");
        frontweaponbtn.onClick.Add(cngfw);
    }
    void Update()
    {
        if (pv.isMine)
        {   
            hptxt.text = GetComponent<Playerstat>().hp.ToString();
            //내 캐릭터 움직임
            stickpos.x = stick.position.x;
            stickpos.y = 0;
            stickpos.z = stick.position.y;
            Vector3 dir = stickpos;
            dir.y = 0;
            dir.Normalize();
            if (dir == Vector3.zero)
            {
                RunOff();
                WalkOff();
            }
            else
            {   
                if (stick.roton)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotspd * Time.deltaTime);
                    transform.Translate(dir * spd * Time.deltaTime, Space.World);
                    RunOn();
                    v = dir;
                }
                else
                {
                    if (Vector3.Angle(v, dir) > 90)
                    {
                        transform.Translate(dir * spd * .5f * Time.deltaTime, Space.World);
                        WalkOn();
                        RunOff();
                    }
                    else
                    {
                        transform.Translate(dir * spd * Time.deltaTime, Space.World);
                        RunOn();
                        WalkOff();
                    }
                }
            }
            //상태이상 프로세스
            if (GetComponent<Playerstat>().poison)
            {
                ptime += Time.deltaTime;
                if (ptime >= 1)
                {
                    ptime = 0;
                    Poisoning();
                }
            }
            if (GetComponent<Playerstat>().fire)
            {
                ftime += Time.deltaTime;
                if (ftime >= 1)
                {
                    ftime = 0;
                    Firing();
                }
            }
            if (GetComponent<Playerstat>().ice)
            {
                itime += Time.deltaTime;
                if (itime >= 1)
                {
                    itime = 0;
                    Icing();
                }
            }
            if(ring)
            {
                rtime += Time.deltaTime;
                if(rtime>=1)
                {
                    rtime = 0;
                    Ringing();
                }
            }
            //도트힐 실행
            if (GetComponent<Playerstat>().dothp > 0)
            {
                Dotheal();
            }
            //근접무기 쿨타임 적용
            if (swing)
            {
                cooltime += Time.deltaTime;
                if (cooltime >= iv.usingtime)
                {
                    cooltime = 0;
                    swing = false;
                    AttackOff();
                }
            }
            
            //아이템 및 무기 사용 프로세스
            if (firing)
            {
                if (iv.curammo <= 0 && iv.cursort != "melee")
                {
                    return;
                }
                //버튼 누르는 도중 무기바꾸기 꼼수 방지
                if (checkslot != iv.curslot)
                {
                    cooltime = 0;
                    return;
                }
                //무기 정보 받아와서 사용
                switch (GetComponentInChildren<Inventory>().cursort)
                {
                    case "melee":
                        if (!swing)
                        {
                            AttackOn();
                            swing = true;
                        }
                        break;
                    case "range":
                        cooltime += Time.deltaTime;
                        if (cooltime >= iv.usingtime)
                        {   
                            cooltime = 0;
                            if (iv.curammo > 0)
                            {
                                Fire();
                            }
                        }
                        break;
                    case "insheal":
                        if (GetComponent<Playerstat>().hp >= 100)
                        {
                            break;
                        }
                        if (checktr == transform)
                        {
                            EatOn();
                            switch (iv.weaponindex[iv.curslot])
                            {
                                case 10:
                                    AniControll.SetFloat("Motionadj", (float)7 / 3);
                                    break;
                                case 11:
                                    AniControll.SetFloat("Motionadj", (float)7 / 5);
                                    break;
                            }
                            cooltime += Time.deltaTime;
                            if (cooltime >= iv.usingtime)
                            {
                                cooltime = 0;
                                if (iv.curammo > 0)
                                {
                                    Eat();
                                }
                            }
                        }
                        else
                        {
                            Eatoff();
                            cooltime = 0;
                        }
                        break;
                    case "dotheal":
                        if (checktr == transform)
                        {
                            DrinkOn();
                            switch (iv.weaponindex[iv.curslot])
                            {
                                case 12:
                                    AniControll.SetFloat("Motionadj", (float)4 / 3);
                                    break;
                                case 13:
                                    AniControll.SetFloat("Motionadj", (float)4 / 5);
                                    break;
                            }
                            cooltime += Time.deltaTime;
                            if (cooltime >= iv.usingtime)
                            {
                                cooltime = 0;
                                if (iv.curammo > 0)
                                {
                                    Eat();
                                }
                            }
                        }
                        else
                        {
                            DrinkOff();
                            cooltime = 0;
                        }
                        break;
                }
            }
            else
            {
                AttackOff();
                Eatoff();
                DrinkOff();
            }
            //재장전 프로세스
            if (reload && slotnum == iv.curslot)
            {
                if (iv.remainammoininventory[slotnum] > 0)
                {
                    reloadcool += Time.deltaTime;
                    if (reloadcool >= reloadtime)
                    {
                        reloadcool = 0;
                        reload = false;
                        if (iv.remainammoininventory[slotnum] >= iv.cartridge[slotnum])
                        {
                            iv.curammo = iv.cartridge[slotnum];
                            iv.remainammoinweapon[slotnum] = iv.cartridge[slotnum];
                            iv.remainammoininventory[slotnum] -= iv.cartridge[slotnum];
                        }
                        else
                        {
                            iv.curammo = iv.remainammoininventory[slotnum];
                            iv.remainammoinweapon[slotnum] = iv.remainammoininventory[slotnum];
                            iv.remainammoininventory[slotnum] = 0;
                        }
                    }
                }
                else
                {
                    reloadcool = 0;
                    reload = false;
                    iv.weaponlist[slotnum] = "";
                    iv.weaponindex[slotnum] = 0;
                    iv.cartridge[slotnum] = 0;
                    Backweapon();
                }
            }
            else
            {
                reloadcool = 0;
            }
        }
        //다른 캐릭터 움직임 동기화
        else
        {
            t.position = Vector3.Lerp(t.position, curpos, Time.deltaTime * 10);
            t.rotation = Quaternion.Lerp(t.rotation, currot, Time.deltaTime * 10);
        }
    }
    //캐릭터 움직임 데이터 보내기+받아오기
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(t.position);
            stream.SendNext(t.rotation);
        }
        else
        {
            curpos = (Vector3)stream.ReceiveNext();
            currot = (Quaternion)stream.ReceiveNext();
        }
    }
    //발사버튼(onpress)
    public void Firebtnon()
    {
        checktr = transform;
        checkslot = iv.curslot;
        firing = true;
    }
    public void Firebtnoff()
    {
        firing = false;
        cooltime = 0;
    }
    //피격처리, 아이템 줍기 준비,자기장 인서클
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemybullet")
        {
            Bulletsort(other.GetComponent<Bulletmoving>().eft);
            if (other.GetComponent<Bulletmoving>().eft == "knock")
            {
                Vector3 vec = other.transform.forward;
                transform.Translate(vec, Space.World);
            }
            int dmgtop = 0;
            int dmgtoh = 0;
            switch (GetComponentInChildren<Inventory>().helmetlv)
            {
                case 0:
                    dmgtop = other.GetComponent<Bulletmoving>().dmg;
                    break;
                case 1:
                    dmgtop = (int)(other.GetComponent<Bulletmoving>().dmg * .8f);
                    dmgtoh = (int)(other.GetComponent<Bulletmoving>().dmg * .2f);
                    break;
                case 2:
                    dmgtop = (int)(other.GetComponent<Bulletmoving>().dmg * .7f);
                    dmgtoh = (int)(other.GetComponent<Bulletmoving>().dmg * .3f);
                    break;
                case 3:
                    dmgtop = (int)(other.GetComponent<Bulletmoving>().dmg * .6f);
                    dmgtoh = (int)(other.GetComponent<Bulletmoving>().dmg * .4f);
                    break;
            }
            Hit(dmgtop, dmgtoh, other.GetComponent<Bulletmoving>().master.name);
            dmgtxt.text = GetComponent<Playerstat>().poisonstack.ToString();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Item")
        {
            grounditem.Add(other.gameObject);
        }
        if (other.gameObject.tag == "Ring")
        {
            ring = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            grounditem.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "Ring")
        {
            ring = true;
        }
    }
    //피격 프로세스
    public void Hit(int dmg, int dmg2, string username)
    {
        StartCoroutine(Hitbullet(dmg, dmg2, username));
        pv.RPC("HitRPC", PhotonTargets.Others, dmg, dmg2, username);
    }
    IEnumerator Hitbullet(int dmg, int dmg2, string username)
    {
        Instantiate(hiteft[0], transform);
        GetComponent<Playerstat>().hp -= dmg;
        iv.helmethp -= dmg2;
        if (iv.helmethp <= 0)
        {
            iv.helmetlv = 0;
            iv.helmethp = 0;
            for (int i = 0; i < 3; i++)
            {
                iv.helmetlook[i].SetActive(false);
            }
        }
        if (GetComponent<Playerstat>().hp <= 0)
        {
            DeadOn();
            yield return new WaitForSeconds(2);
            Creategrave();
            GameObject.Find(username).GetComponent<Playercnt>().killscore++;
            gameObject.SetActive(false);
        }
        yield return null;
    }
    //피격 프로세스 중 사망시 무덤 생성
    void Creategrave()
    {
        int num = 0;
        List<int> indexlist = new List<int>();
        List<int> ammolist = new List<int>();
        List<GameObject> gravelist = new List<GameObject>();
        List<Vector3> gravepos = new List<Vector3>();
        for (float i = -1.5f; i < 2; i += 1.5f)
        {
            for (float j = -1.5f; j < 2; j += 1.5f)
            {
                gravepos.Add(transform.position + new Vector3(i, 0, j));
            }
        }
        for (int i = 0; i < 7; i++)
        {
            if (iv.weaponlist[i] != "")
            {
                num++;
                indexlist.Add(iv.weaponindex[i]);
                ammolist.Add(iv.remainammoinweapon[i] + iv.remainammoininventory[i]);
            }
        }
        for (int i = 0; i < num; i++)
        {
            GameObject grave = Instantiate(itembox, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            grave.GetComponent<Itemstat>().index = indexlist[i];
            grave.GetComponent<Itemstat>().bullet = ammolist[i];
            grave.GetComponent<Itemstat>().Decidelook();
            gravelist.Add(grave);
        }
        if (num > 1)
        {
            for (int i = 0; i < num; i++)
            {
                gravelist[i].transform.position = gravepos[i];
            }
        }
    }
    [PunRPC]
    void HitRPC(int dmg, int dmg2, string username)
    {
        StartCoroutine(Hitbullet(dmg, dmg2, username));
    }
    //발사 프로세스
    void Fire()
    {
        StartCoroutine(Createbullet());
        pv.RPC("FireRPC", PhotonTargets.Others);
    }
    IEnumerator Createbullet()
    {
        AttackOn();
        GameObject b = Instantiate(bullet, firepos.position, firepos.rotation);
        b.GetComponent<Bulletmoving>().dmg = GetComponentInChildren<Inventory>().curdmg;
        b.GetComponent<Bulletmoving>().eft = GetComponentInChildren<Inventory>().cureft;
        iv.curammo--;
        iv.remainammoinweapon[iv.curslot]--;
        b.GetComponent<Bulletmoving>().master = gameObject;
        b.GetComponent<Bulletmoving>().Decideeft(iv.weaponindex[iv.curslot]);
        if (pv.isMine)
        {
            b.tag = "Playerbullet";
        }
        else
        {
            b.tag = "Enemybullet";
        }
        if (iv.curammo <= 0)
        {
            reload = true;
            slotnum = iv.curslot;
        }
        yield return new WaitForSeconds(.5f);
        AttackOff();
    }
    [PunRPC]
    void FireRPC()
    {
        StartCoroutine(Createbullet());
    }
    //회복템 사용 프로세스
    void Eat()
    {
        StartCoroutine(Healing());
        pv.RPC("HealingRPC", PhotonTargets.Others);
    }
    IEnumerator Healing()
    {
        switch (iv.cursort)
        {
            case "insheal":
                GetComponent<Playerstat>().hp += iv.curdmg;
                if (GetComponent<Playerstat>().hp > 100)
                {
                    GetComponent<Playerstat>().hp = 100;
                }
                break;
            case "dotheal":
                GetComponent<Playerstat>().dothp += iv.curdmg;
                if (GetComponent<Playerstat>().dothp > 100)
                {
                    GetComponent<Playerstat>().dothp = 100;
                }
                break;

        }
        iv.curammo--;
        iv.remainammoinweapon[iv.curslot]--;

        if (iv.curammo <= 0)
        {
            Eatoff();
            DrinkOff();
            reload = true;
            slotnum = iv.curslot;
        }
        yield return null;
    }
    [PunRPC]
    void HealingRPC()
    {
        StartCoroutine(Healing());
    }
    //도트힐 체력회복 프로세스
    void Dotheal()
    {
        StartCoroutine(Dh());
        pv.RPC("DhRPC", PhotonTargets.Others);
    }
    IEnumerator Dh()
    {
        GetComponent<Playerstat>().dothealtime += Time.deltaTime;
        if (GetComponent<Playerstat>().dothealtime >= GetComponent<Playerstat>().dothealcool)
        {
            GetComponent<Playerstat>().dothealtime = 0;
            GetComponent<Playerstat>().dothp -= 5;
            if (GetComponent<Playerstat>().dothp < 0)
            {
                GetComponent<Playerstat>().dothp = 0;
            }
            GetComponent<Playerstat>().hp += 5;
            if (GetComponent<Playerstat>().hp >= 100)
            {
                GetComponent<Playerstat>().hp = 100;
            }
        }
        yield return null;
    }
    [PunRPC]
    void DhRPC()
    {
        StartCoroutine(Dh());
    }
    //전무기 선택 프로세스
    void Backweapon()
    {
        if (pv.isMine)
        {
            cooltime = 0;
            StartCoroutine(Cd());
            pv.RPC("CdRPC", PhotonTargets.Others);
        }
    }
    IEnumerator Cd()
    {
        int j = iv.curslot;
        iv.Backweapon();
        if(j!=iv.curslot)
        {
            ChangeOn();
            yield return new WaitForSeconds(.5f);
            int i = iv.weaponindex[iv.curslot];
            if (i <= 1)
            {
                Job = 1;
                Charidle();
            }
            else if (i <= 3)
            {
                Job = 2;
                Charidle();
            }
            else if (i <= 6)
            {
                Job = 3;
                Charidle();
            }
            else
            {
                Job = 4;
                Charidle();
            }
        }
    }
    [PunRPC]
    void CdRPC()
    {
        StartCoroutine(Cd());
    }
    //후무기 선택 프로세스
    void Frontweapon()
    {
        if (pv.isMine)
        {
            cooltime = 0;
            StartCoroutine(Fw());
            pv.RPC("FwRPC", PhotonTargets.Others);
        }
    }
    IEnumerator Fw()
    {
        int j = iv.curslot;
        iv.Frontweapon();
        if (j != iv.curslot)
        {
            ChangeOn();
            yield return new WaitForSeconds(.5f);
            int i = iv.weaponindex[iv.curslot];
            if (i <= 1)
            {
                Job = 1;
                Charidle();
            }
            else if (i <= 3)
            {
                Job = 2;
                Charidle();
            }
            else if (i <= 6)
            {
                Job = 3;
                Charidle();
            }
            else
            {
                Job = 4;
                Charidle();
            }
        }
    }
    [PunRPC]
    void FwRPC()
    {
        StartCoroutine(Fw());
    }
    //아이템 줍기 프로세스
    void Pickupitem()
    {
        if (pv.isMine)
        {
            if (grounditem.Count == 0)
            {
                return;
            }
            StartCoroutine(Pick());
            pv.RPC("PickRPC", PhotonTargets.Others);
        }
    }
    IEnumerator Pick()
    {
        PickUpOn();
        Copyitemname(grounditem[0]);
        Destroy(grounditem[0]);
        grounditem.RemoveAt(0);
        yield return new WaitForSeconds(.5f);
        PickUpoff();
    }
    [PunRPC]
    void PickRPC()
    {
        StartCoroutine(Pick());
    }
    //줍기 중 인벤토리에 아이템 이름을 전송하여 인벤 리스트에 아이템 생성
    void Copyitemname(GameObject item)
    {
        Itemstat i = item.GetComponent<Itemstat>();
        iv.Createitem(i.index, i.bullet);
    }
    //피격시 탄종류 구별 프로세스
    void Bulletsort(string eft)
    {
        if (pv.isMine)
        {
            StartCoroutine(Sorting(eft));
            pv.RPC("SortingRPC", PhotonTargets.Others, eft);
        }
    }
    IEnumerator Sorting(string eft)
    {
        switch (eft)
        {
            case "poison":
                GetComponent<Playerstat>().poison = true;
                GetComponent<Playerstat>().poisonstack++;
                break;
            case "dot":
                GetComponent<Playerstat>().fire = true;
                GetComponent<Playerstat>().firestack++;
                break;
            case "slow":
                GetComponent<Playerstat>().ice = true;
                GetComponent<Playerstat>().icestack++;
                break;
            default:
                break;
        }
        yield return null;
    }
    [PunRPC]
    void SortingRPC(string eft)
    {
        StartCoroutine(Sorting(eft));
    }
    //중독 프로세스
    void Poisoning()
    {
        if (pv.isMine)
        {
            StartCoroutine(Poing());
            pv.RPC("PoingRPC", PhotonTargets.Others);
        }
    }
    IEnumerator Poing()
    {
        Instantiate(hiteft[3], transform);
        GetComponent<Playerstat>().hp -= 10;
        if (GetComponent<Playerstat>().hp <= 0)
        {
            DeadOn();
            yield return new WaitForSeconds(2);
            Creategrave();
            gameObject.SetActive(false);
        }
        GetComponent<Playerstat>().poisonstack--;
        if (GetComponent<Playerstat>().poisonstack == 0)
        {
            GetComponent<Playerstat>().poison = false;
        }
        yield return null;
    }
    [PunRPC]
    void PoingRPC()
    {
        StartCoroutine(Poing());
    }
    //화상 프로세스
    void Firing()
    {
        if (pv.isMine)
        {
            StartCoroutine(Fing());
            pv.RPC("FingRPC", PhotonTargets.Others);
        }
    }
    IEnumerator Fing()
    {
        Instantiate(hiteft[1], transform);
        GetComponent<Playerstat>().hp -= 10;
        if (GetComponent<Playerstat>().hp <= 0)
        {
            DeadOn();
            yield return new WaitForSeconds(2);
            Creategrave();
            gameObject.SetActive(false);
        }
        GetComponent<Playerstat>().firestack--;
        if (GetComponent<Playerstat>().firestack == 0)
        {
            GetComponent<Playerstat>().fire = false;
        }
        yield return null;
    }
    [PunRPC]
    void FingRPC()
    {
        StartCoroutine(Fing());
    }
    //빙결 프로세스
    void Icing()
    {
        if (pv.isMine)
        {
            StartCoroutine(Iing());
            pv.RPC("IingRPC", PhotonTargets.Others);
        }
    }
    IEnumerator Iing()
    {
        Instantiate(hiteft[2], transform);
        spd = basespd * .5f;
        GetComponent<Playerstat>().icestack--;
        if (GetComponent<Playerstat>().icestack == 0)
        {
            GetComponent<Playerstat>().ice = false;
            spd = basespd;
        }
        yield return null;
    }
    [PunRPC]
    void IingRPC()
    {
        StartCoroutine(Iing());
    }
    //자기장 프로세스
    void Ringing()
    {
        if (pv.isMine)
        {
            StartCoroutine(Ring());
            pv.RPC("RingRPC", PhotonTargets.Others);
        }
    }
    IEnumerator Ring()
    {
        GetComponent<Playerstat>().hp -= 20;
        if (GetComponent<Playerstat>().hp <= 0)
        {
            DeadOn();
            yield return new WaitForSeconds(2);
            Creategrave();
            gameObject.SetActive(false);
        }
        yield return null;
    }
    [PunRPC]
    void RingRPC()
    {
        StartCoroutine(Ring());
    }

    public void Meleeatk()
    {
        GetComponentInChildren<BoxCollider>().enabled = true;
        GetComponentInChildren<Meleeweapon>().master = gameObject;
    }
    public void Offcol()
    {
        GetComponentInChildren<BoxCollider>().enabled = false;
    }
    public void Charidle() //애니메이션 종류선택
    {
        if (Job == 1)
        {
            AniControll.SetBool("Swardman", true);
            AniControll.SetBool("Archer", false);
            AniControll.SetBool("Magician", false);
            AniControll.SetBool("Food", false);
            AniControll.SetBool("Change", false);
        }
        if (Job == 2)
        {
            AniControll.SetBool("Swardman", false);
            AniControll.SetBool("Archer", true);
            AniControll.SetBool("Magician", false);
            AniControll.SetBool("Food", false);
            AniControll.SetBool("Change", false);
        }
        if (Job == 3)
        {
            AniControll.SetBool("Swardman", false);
            AniControll.SetBool("Food", false);
            AniControll.SetBool("Archer", false);
            AniControll.SetBool("Magician", true);
            AniControll.SetBool("Change", false);
        }
        if (Job == 4)
        {
            AniControll.SetBool("Food", true);
            AniControll.SetBool("Swardman", false);
            AniControll.SetBool("Archer", false);
            AniControll.SetBool("Magician", false);
            AniControll.SetBool("Change", false);
        }
        for (int i = 0; i < iv.weaponlook.Count; i++)
        {
            if(iv.weaponlook[i].name==iv.weaponlist[iv.curslot])
            {
                iv.weaponlook[i].SetActive(true);
            }
            else
            {
                iv.weaponlook[i].SetActive(false);
            }
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
    public void Eatoff()
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