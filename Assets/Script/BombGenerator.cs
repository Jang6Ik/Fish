using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour // MonoBehaviour 클래스의 상속을 받음
{
    public GameObject bombObject; // 생성될 폭탄 오브젝트
    public int howManyBomb; // 얼마나 많은 폭탄을 미리 생성할것인가.
    public float time; // 시간을 저장
    List<GameObject> bombList; // 폭탄을 담을 리스트

    public struct BOMB // 구조체 BOMB
    {
        public float x; // x값을 가진다.
        public float y; // y값을 가진다.
        public BOMB(float x, float y) // 생성자
        {
            this.x = x; // 입력한 x의 값이 BOMB의 x값
            this.y = y; // 입력한 y의 값이 BOMB의 y값
        }
    }

    public void Start() // 시작하면 실행
    {
        bombList = new List<GameObject>(); // 폭탄 리스트를 게임오브젝트를 담는 리스트로 만든다.
        for (int i = 0; i < howManyBomb; i++) // i가 howManyBomb 작으면 실행 
        {
            GameObject gameObj = Instantiate(bombObject); // bombObject gameObj라는 이름으로 새로 만든다.
            gameObj.SetActive(false); // 그 오브젝트를 비활성화 한다.
            bombList.Add(gameObj); // 그 오브젝트를 리스트에 추가한다.
        }
    }

    public void Update() // 매 시간 실행된다.
    {
        BOMB bomb = new BOMB(Random.Range(-8f, 8f), Random.Range(-4f, 2f)); // bomb의 x값과 y값을 랜덤으로 저장한다.
        time += Time.deltaTime; // time에 시간을 계속 더해준다.
        if (time >= 2f) // time의 값이 1f 이상이면 실행
        {
            SpawnBomb(new Vector3(bomb.x, bomb.y, transform.position.z)); // 폭탄 생성
            time = 0; // time을 다시 0으로
        }
    }

    public GameObject GetObject() // 오브젝트 생성할때 실행
    {
        for (int i = 0; i < bombList.Count; i++) // i가 리스트 크기보다 작으면 반복
        {
            if (!bombList[i].activeInHierarchy) // 리스트의 i번째 오브젝트가 비활성화 되어있다면
            {
                return bombList[i]; // 리스트의 i번째 오브젝트를 반환한다.
            }
        }
        GameObject gameObj = Instantiate(bombObject); // 모든 리스트가 활성화 중이라면 새로운 오브젝트를 만든다.
        gameObj.SetActive(false); // 새로운 오브젝트를 비활성화 하고
        bombList.Add(gameObj); // 리스트에 추가한 다음
        return gameObj; // 추가된 오브젝트를 반환한다.
    }

    public void SpawnBomb(Vector3 spawnPosition) // 폭탄 생성
    {
        GameObject newFish = GetObject(); // GetObject()로 폭탄 생성
        newFish.transform.position = spawnPosition; // 생성될 폭탄의 위치
        newFish.SetActive(true); // 폭탄 활성화
    }

}
