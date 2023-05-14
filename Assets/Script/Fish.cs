using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour // MonoBehaviour 클래스의 상속을 받음
{
    public Rigidbody2D MyRigidBody2D; // rigidbody를 받아올 변수
    public Director director; // 감독 클래스
    public Player player; // 플레이어 클래스
    public float fishScore; // 물고기 점수
    public int fishScale; // 물고기 크기

    public virtual void Start() // virtual로 선언된 Start 함수, 게임 시작시 실행된다.
    {
        fishScore = 0; // 물고기 점수 0점
        fishScale = 0; // 물고기 크기 0점
    }
    public virtual void Update() // virtual로 선언된 Update 함수, 매 시간 실행된다.
    {
        gameObject.SetActive(false); // 오브젝트 비활성화
    }

    public void OnTriggerEnter2D(Collider2D col) // 충돌시 실행
    {
        switch (col.gameObject.name) // 충돌한 오브젝트의 이름을 받아온다.
        {
            case "Player": // 받아온 이름이 Player면 실행
                if (player.playerScale >= fishScale) // 플레이어 크기가 물고기보다 크거나 같으면 실행
                {
                    director.biteSound.Play(); // 효과음 실행
                    gameObject.SetActive(false); // 물고기 오브젝트 비활성화
                    director.Score += fishScore; // 물고기 점수 만큼 현재 점수에 추가한다.
                    player.playerEXP += 1; // 플레이어 경험치 증가
                }
                else
                {
                    if (player.Life0.activeSelf) // 플레이어의 생명0이 활성화 되어 있다면
                    {
                        player.Life0.SetActive(false); // 플레이어의 생명2를 비활성화 하고
                        director.damageSound.Play(); // 효과음 실행
                    }
                    else if (player.Life1.activeSelf) // 플레이어의 생명1이 활성화 되어 있다면
                    {
                        player.Life1.SetActive(false); // 플레이어의 생명1을 비활성화
                        director.damageSound.Play(); // 효과음 실행
                    }
                    else
                    {
                        player.Life2.SetActive(false); // 플레이어의 생명2을 비활성화
                        director.damageSound.Play(); // 효과음 실행
                        director.GameOver(); // 게임오버 실행
                    }
                }
                break; // 종료
            case "background": // 충돌한 오브젝트의 이름을 받아온다.
                gameObject.SetActive(false); // 물고기 비활성화
                break; // 종료
        }
    }

}
