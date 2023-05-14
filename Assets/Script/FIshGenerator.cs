using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIshGenerator : MonoBehaviour // MonoBehaviour 클래스의 상속을 받음
{
    public GameObject fishObject; // 생성될 물고기 오브젝트
    public int howManyFish; // 몇 마리의 물고기를 미리 생성할것인가.
    public float time; // 시간을 저장할 변수

    List<GameObject> fishList; // 물고기를 담을 리스트
    private int direction; // 물고기가 생성될 방향

    public void Start() // 시작하면 실행
    {
        fishList = new List<GameObject>(); // 물고기 리스트를 게임오브젝트를 담는 리스트로 만든다.
        for (int i = 0; i < howManyFish; i++) // i가 howManyFish보다 작으면 실행 
        {
            GameObject gameObj = Instantiate(fishObject); // fishObject를 gameObj라는 이름으로 새로 만든다.
            gameObj.SetActive(false); // 그 오브젝트를 비활성화 한다.
            fishList.Add(gameObj); // 그 오브젝트를 리스트에 추가한다.
        }
    }

    public void Update() // 매 시간 실행된다.
    {
        direction = Random.Range(0, 2); // directrion은 0 또는 1을 가진다.
        time += Time.deltaTime; // time에 시간을 계속 더해준다.
        if (time >= 1f) // time의 값이 1f 이상이면 실행
        {
            if (direction == 0) // direction의 값이 0이면 실행
            {
                // x = 9f, y = -4f ~ 2f 사이의 랜덤한 값, z는 제네레이션의 z값으로 물고기를 생성한다.
                SpawnFish(new Vector3(9.5f, Random.Range(-4f, 2f), transform.position.z));
                time = 0; // time을 0으로 초기화한다.
            }
            if (direction == 1) // direction의 값이 1이면 실행
            {
                // x = -9f, y = -4f ~ 2f 사이의 랜덤한 값, z는 제네레이션의 z값으로 물고기를 생성한다.
                SpawnFish(new Vector3(-9.5f, Random.Range(-4f, 2f), transform.position.z));
                time = 0; // time을 0으로 초기화한다.
            }
        }
    }

    public GameObject GetObject() // 물고기 리스트에 물고기를 넣기 위한 함수
    {
        for (int i = 0; i < fishList.Count; i++) // i가 리스트 크기보다 작으면 반복
        {
            if (!fishList[i].activeInHierarchy) // 리스트의 i번째 오브젝트가 비활성화 되어있다면
            {
                return fishList[i]; // 리스트의 i번째 오브젝트를 반환한다.
            }
        }
        GameObject gameObj = Instantiate(fishObject); // 모든 리스트가 활성화 중이라면 새로운 오브젝트를 만든다.
        gameObj.SetActive(false); // 새로운 오브젝트를 비활성화 하고
        fishList.Add(gameObj); // 리스트에 추가한 다음
        return gameObj; // 추가된 오브젝트를 반환한다.
    }

    public void SpawnFish(Vector3 spawnPosition) // 물고기를 생성하는 함수
    {
        GameObject newFish = GetObject(); // 물고기 생성
        newFish.transform.position = spawnPosition; // 생성될 물고기의 위치
        newFish.SetActive(true); // 물고기 활성화
    }

}
