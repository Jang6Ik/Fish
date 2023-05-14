using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour // MonoBehaviour 클래스의 상속을 받음
{
    private Director director; // 감독 클래스 
    private Player player; // 플레이어 클래스
    public float time; // 시간 저장

    void Start() // 시작하면 실행
    {
        director = FindObjectOfType<Director>(); // 감독 클래스 찾기
        player = FindObjectOfType<Player>(); // 플레이어 클래스 찾기
    }

    void Update() // 매 시간 실행
    {
        time += Time.deltaTime; // 시간을 매 초 더해준다.
        if (time >= 5.0f) // 시간이 5이상이면 실행
        {
            gameObject.SetActive(false); // 폭탄을 비활성화
            time = 0; // 시간을 0으로 초기화
        }
    }

    private void OnTriggerEnter2D(Collider2D col) // 오브젝트가 충돌시 실행된다.
    {
        if (col.gameObject.name == "Player") // 충돌한 오브젝트의 이름이 Player이면 실행
        {
            if (player.Life0.activeSelf) // 생명0이 활성화 되어 있다면
            {
                gameObject.SetActive(false); // 폭탄 비활성화
                player.Life0.SetActive(false); // 생명2를 비활성화 하고
                director.bombSound.Play(); // 효과음 실행
            }
            else if (player.Life1.activeSelf) // 생명1이 활성화 되어 있다면
            {
                gameObject.SetActive(false); // 폭탄 비활성화
                player.Life1.SetActive(false); // 생명1을 비활성화
                director.bombSound.Play(); // 효과음 실행
            }
            else // 그 외 (생명2가 활성화 되어 있다면)
            {
                gameObject.SetActive(false); // 폭탄 비활성화
                player.Life2.SetActive(false); // 생명2을 비활성화
                director.bombSound.Play(); // 효과음 실행
                director.GameOver(); // 게임오버
            }
        }
    }
}
