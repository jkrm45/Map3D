using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManmeger : MonoBehaviour
{

    private static BgmManmeger _instance = null;

    public static BgmManmeger Instance { get { return _instance; } }
    public AudioSource MusicController; // 음악
    public AudioSource InGameSoundController; // 게임 내부 바람소리 컨트롤 
    public AudioSource ActionController; // 행동소리 컨트롤러
    public AudioClip Walk;
    public AudioClip SwardAttack;
    public AudioClip ArcherAttack;
    public AudioClip MagicAttack1;
    public AudioClip MagicAttack2;
    public AudioClip MagicAttack3;
    public AudioClip Dead;
    public AudioClip hitted;

    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ActionController = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();  //방에 접속하면 하이어라키에 플레이어 찾아게 수정!! 
        //->>>>  BgmManmeger.Instance.ActionController = GameObject.Find("Player").GetComponent<AudioSource>(); 
     
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    
    void WalkSound()//이동시 발걸음  ---->   BgmManmeger.Instance.WalkSound();
    {
        ActionController.clip = Walk;
        ActionController.PlayOneShot(Walk);
    }  

    void SwardAttackSound() //기본칼 , 칼 공격사운드  ---->   BgmManmeger.Instance.SwardAttackSound();
    {
        ActionController.clip = SwardAttack;
        ActionController.PlayOneShot(SwardAttack);
    }
    
    void ArcherAttackSound()//홣 공격사운드  ---->   BgmManmeger.Instance.ArcherAttackSound();
    {
        ActionController.clip = ArcherAttack;
        ActionController.PlayOneShot(ArcherAttack);
    }
     
    void IceMagicAttackSound() //얼음마법사운드  ---->   BgmManmeger.Instance.IceMagicAttackSound();
    {
        ActionController.clip = MagicAttack2;
        ActionController.PlayOneShot(MagicAttack2);
    }
     
    void LightingMagicAttackSound() // 나이트링마법 사운드  ---->   BgmManmeger.Instance.LightingMagicAttackSound() ;
    {
        ActionController.clip = MagicAttack2;
        ActionController.PlayOneShot(MagicAttack2);
    }
     
    void FireMagicAttackSound() // 불마법사운드  ---->   BgmManmeger.Instance.FireMagicAttackSound();
    {
        ActionController.clip = MagicAttack2;
        ActionController.PlayOneShot(MagicAttack2);
    }

    void DeadSound() // 사망사운드  ---->   BgmManmeger.Instance.DeadSound();
    {
        ActionController.clip = Dead;
        ActionController.PlayOneShot(Dead);
    }

    void hittedSound() //피격사운드  ---->   BgmManmeger.Instance.hittedSound();
    {
        ActionController.clip = hitted;
        ActionController.PlayOneShot(hitted);

    }
}
