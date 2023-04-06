using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float speed;      // 캐릭터 움직임 스피드.
    [SerializeField] private float jumpSpeed;  // 캐릭터 점프 힘.
    [SerializeField] private float gravity;    // 캐릭터에게 작용하는 중력.

    private float originSpeed;
    private bool canDash = true;
    private Camera _cam;
    private CharacterController controller; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private Vector3 MoveDir;                // 캐릭터의 움직이는 방향.

    void Awake()
    {
        MoveDir = Vector3.zero;
        controller = GetComponent<CharacterController>();
        _cam = Camera.main;
        originSpeed = speed;
    }

    void Update()
    {
        //    mouseX += Input.GetAxis("Mouse X");
        //    transform.eulerAngles = new Vector3(0, mouseX, 0);

        //float h = Input.GetAxisRaw("Horizontal");
        //transform.rotation = Quaternion.Euler(0, h * Time.deltaTime * rotateSpeed, 0) * transform.rotation;
        // 현재 캐릭터가 땅에 있는가?
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Dash();
        Move();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            canDash = false;
            StartCoroutine(IncreaseSpeed());
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        speed += 5;
        yield return new WaitForSeconds(0.5f);
        speed = originSpeed;
        canDash = true;
    }

    private void Move()
    {
        transform.eulerAngles = new Vector3(0, _cam.transform.eulerAngles.y, 0);

        if (controller.isGrounded)
        {
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDir = transform.TransformDirection(MoveDir);
            MoveDir *= speed;
            // 캐릭터 점프
            if (Input.GetButton("Jump"))
                MoveDir.y = jumpSpeed;
        }


        MoveDir.y -= gravity * Time.deltaTime; //중력 연산

        // 캐릭터 움직임.
        controller.Move(MoveDir * Time.deltaTime);
    }
}

