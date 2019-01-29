using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    public float moveSpeed = 10.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = -20.0f;
    public CharacterController characterController = null;  
    float yVelocity = 0.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

  
    void Update()
    {
        moveLimit();
        move();
      
      
        
    }

    void moveLimit()
    {
        if (transform.position.x < -60)
        {
            transform.position = new Vector3(-60, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 60)
        {
            transform.position = new Vector3(60, transform.position.y, transform.position.z);
        }
        if (transform.position.z > 80)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 80);
        }
        if (transform.position.z < -80)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -80);
        }
    }//플레이어 움직임 니미터;
    void move()
    {
        float y = transform.position.y;

        float x = Input.GetAxis("Horizontal");  // 문자열로 변수설정 (x 값) 
        float z = Input.GetAxis("Vertical");    // 문자열로 변수설정 (y 값)

        Vector3 moveDirection = new Vector3(x, 0, z); //x,z 값으로 이동
        moveDirection *= moveSpeed; //캐릭터 자체 이동속도 곲

        if (characterController.isGrounded)  //캐릭터 컨트롤러가 땅에 있을때.
        {
            yVelocity = 0.0f;
            if (Input.GetButton("Jump"))
            {
                yVelocity = jumpSpeed;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }
        yVelocity += (gravity * Time.deltaTime); //중력에 위한 점프속도값 감소후 추락표현
        moveDirection.y = yVelocity; // 점프키를 눌엇을떄  y값 변화
        characterController.Move(moveDirection * Time.deltaTime);  //컨트롤러 이동키
    }// 플레이어 이동;
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
