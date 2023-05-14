using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFish : Fish // FISH 클래스를 상속받음
{
    public float fishVectorX; // 물고기 방향

    override public void Start() // 오버라이딩으로 start 재정의
    {
        MyRigidBody2D = GetComponent<Rigidbody2D>(); // 상속받은 MyRigidBody2D 선언
        director = FindObjectOfType<Director>(); // 상속받은 director 선언
        player = FindObjectOfType<Player>(); // 상속받은 player 선언
        fishScore = 1000; // 상속받은 fishScore 선언
        fishVectorX = Random.Range(-8f, 8f); // fishVectorX 선언
        fishScale = 3;  // 상속받은 fishScale 선언
    }

    override public void Update() // 오버라이딩으로 update 재정의
    {
        if (fishVectorX < 0) // 물고기 방향이 0보다 작으면
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1); // 좌우 반전
        }
        MyRigidBody2D.velocity = new Vector2(fishVectorX, MyRigidBody2D.velocity.y); // 물고기는 Vector2 방향으로 움직인다.
    }
}
