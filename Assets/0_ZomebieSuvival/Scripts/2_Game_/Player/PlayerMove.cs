using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpPower = 3f;
    public float speedUp;

    public bool isJump = false;

    float gravity = -20f;
    float yVelocity = 0;

    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        Move();
        Jump();
        Run();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, y);
        //거리를 정규화 시킨다.
        dir = dir.normalized;
        //메인카메라 기준으로 방향을 변환한다.
        dir = Camera.main.transform.TransformDirection(dir);
        // transform.position += dir * moveSpeed * Time.deltaTime;
        
        //y 중력값 구해서 dir의 y축에 넣어준다.
        yVelocity += gravity * Time.deltaTime;
        dir.y =  yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        //만약 점프중이고, 바닥에 착지했다면
        if (isJump && cc.collisionFlags == CollisionFlags.Below)
        {
            isJump = false;
        }

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            yVelocity = jumpPower;
        }
    }

    void Run()
    {
        if (Input.GetButtonDown("Run"))
        {
            moveSpeed = moveSpeed + speedUp;
        }
        else if (Input.GetButtonUp("Run"))
        {
            moveSpeed = moveSpeed - speedUp;
        }
    }
}
