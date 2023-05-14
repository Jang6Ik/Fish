using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour // MonoBehaviour 클래스의 상속을 받음
{
    public float moveSpeed; // 플레이어 이동속도
    public int playerScale; // 플레이어의 크기
    public int playerGrow; // 플레이어의 성장치
    public int playerEXP; // 플레이어의 경험치
    public GameObject Life0; // 플레이어 생명0번
    public GameObject Life1; // 플레이어 생명1번
    public GameObject Life2; // 플레이어 생명2번
    public bool isLeft; // 왼쪽인가?
    public bool isLive; // 살아있는가?
    public Director director; // director 변수

    void Start() // 시작할 때 실행된다.
    {
        moveSpeed = 5f; // 이동속도를 5f로 설정
        playerGrow = 10; // 성장치를 10으로 설정
        playerEXP = 0; // 경험치를 0으로 설정
        isLeft = true; // 왼쪽이다. 
        isLive = true; // 살아있다.
    }

    void Update() // 매 시간 실행된다.
    {
        if (playerEXP == playerGrow && playerScale < 5) // 플레이어 경험치가 성장치와 같으며 크기가 5보다 작으면 실행
        {
            playerScale += 1; // 크기를 1증가 시킨다.
            playerGrow += 10; // 성장치를 10 증가 시킨다.
            playerEXP = 0; // 경험치를 0으로 만든다.
            if (transform.localScale.x >= 1) // 플레이어 Scale의 x값이 양수면 실행
            {
                transform.localScale += new Vector3(0.6f, 0.2f, 0); // Vector3의 값만큼 크기를 늘린다.
            }
            if (transform.localScale.x <= 1) // 플레이어 Scale의 x값이 음수면 실행
            {
                transform.localScale += new Vector3(-0.6f, 0.2f, 0); // Vector3의 값만큼 크기를 늘린다.
            }
        }

        if (isLive) // 살아있다면 실행
        {
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -8.5) // 왼쪽 화살표키가 눌렸고 플레이어 x의 값이 -8.5보다 크면 실행
            {
                if (!isLeft) // 왼쪽이 아니면
                {
                    Vector3 objectscale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1)); // 현재 플레이어의 좌우를 바꾼 Scale을 새로운 Vector3로 저장한다.
                    transform.localScale = objectscale; // 플레이어의 Scale을 objectscale로 바꾼다.
                    isLeft = true; // 왼쪽이다.
                }
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // 플레이어는 Vector2 방향으로 움직인다.
            }
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 8.5) // 오른쪽 화살표키가 눌렸고 플레이어 x의 값이 8.5보다 작으면 실행
            {
                if (isLeft) // 왼쪽이면
                {
                    Vector3 objectscale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1)); // 현재 플레이어의 좌우를 바꾼 Scale을 새로운 Vector3로 저장한다.
                    transform.localScale = objectscale; // 플레이어의 Scale을 objectscale로 바꾼다.
                    isLeft = false; // 왼쪽이 아니다.
                }
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); // 플레이어는 Vector2 방향으로 움직인다.
            }
            if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 2.2) // 위쪽 화살표키가 눌렸고 플레이어 y의 값이 2.2보다 작으면 실행
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime); // 플레이어는 Vector2 방향으로 움직인다.
            }
            if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -4.5) // 아래 화살표키가 눌렸고 플레이어 y의 값이 -4.5보다 크면 실행
            {
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime); // 플레이어는 Vector2 방향으로 움직인다.
            }
        }
    }

}
