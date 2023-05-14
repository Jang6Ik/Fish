using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // Exception을 사용하기 위해 추가한 namespace
using System.IO; // 파일 입출력을 사용하기 위해 추가한 namespace
using System.Runtime.Serialization.Formatters.Binary; // 역직렬화/직렬화 사용하기 위해 추가한 namespace
using UnityEngine.UI; // 유니티 ui를 사용하기 위해 추가한 namespace

// Director는 Class Monobehaviour을 상속 받고, Interface IGameEnd를 상속 받는다.
public class Director : MonoBehaviour, IGameEnd
{
    // 소리 관련 변수
    public AudioSource biteSound; // 물고기 먹을 때 나는 소리
    public AudioSource bombSound; // 폭탄 터질 때 나는 소리
    public AudioSource damageSound; // 물고기 못 먹을 때 나는 소리
    public AudioSource BGM; // bgm
    public Sprite mute; // 음소거 스프라이트
    public Sprite play; // 음소거 스프라이트
    public GameObject BGMbutton; // BGM 버튼

    public Player player; // 플레이어

    // 점수 관련 변수
    public Text scoreText; // 현재 점수를 표시할 UI
    public Text bestscoreText; // 최고 점수를 표시할 UI
    public double Score; // 현재 점수
    public double bestScore; // 최고 점수

    public GameObject gameOver; // 게임 오버 메시지
    public GameObject gameClear; // 게임 클리어 메시지

    void Awake() // start() 보다 먼저 실행된다.
    {
        if (BGM.isPlaying) return; // BGM이 실행 중이면 아무것도 하지 않고
        else
        {
            BGM.Play(); // 실행 중이 아니면 BGM을 실행한다.
        }
    }

    void Start() // 시작 하면 실행
    {
        try // 고급 예외 처리
        {
            FileInfo fi = new FileInfo(Application.dataPath + "Score.txt"); // 경로에 Score.txt 파일의 존재 유무 확인
            if (fi.Exists) // 파일이 존재하면 true, 존재하지 않으면 false
            {
                FileStream fs = new FileStream(Application.dataPath + "Score.txt", FileMode.Open); // 경로에 Score.txt 파일을 연다(open)
                BinaryFormatter bf = new BinaryFormatter(); // 역직렬화를 위한 bf 선언
                bestScore = (double)bf.Deserialize(fs); // Score.txt의 내용을 역직렬화해서 최고 점수로 불러온다. 형변환 해야함 double = object 이기 때문
                fs.Close(); // 파일을 닫는다.
            }
            else
            {
                throw new Exception("Score.txt 파일이 없습니다."); // catch로 강제로 보내줌, 이 때의 에러 내용은 '파일이 없습니다.'
            }
        }
        catch (Exception ex) // 오류를 받으면 실행
        {
            FileStream fs = new FileStream(Application.dataPath + "error.txt", FileMode.Create); // 경로에 error.txt를 만든다(create) 이미 있으면 덮어씌운다.
            BinaryFormatter bf = new BinaryFormatter(); // 직렬화를 위한 bf 
            bf.Serialize(fs, ex.Message); // 에러의 메세지 내용을 error.txt에 직렬화 해서 저장한다.
            fs.Close(); // 파일을 닫는다.
        }
    }


    void Update() // 매 시간 실행한다.
    {
        if (Score >= bestScore) // 현재점수가 최고 점수 보다 크거나 같으면 실행
        {
            if (Score < 100000) // 현재점수가 100000보다 작으면 실행
            {
                bestScore = Score;  // 최고 점수는 현재점수가 된다.

                FileStream fs = new FileStream(Application.dataPath + "Score.txt", FileMode.Create); // 경로에 Score.txt 파일을 만든다.
                BinaryFormatter bf = new BinaryFormatter(); // 직렬화를 위한 bf
                bf.Serialize(fs, bestScore); // 최고 점수를 Score.txt에 직렬화 해서 저장한다.
                fs.Close(); // 파일을 닫는다.
            }

            if (Score >= 100000) // 현재점수가 100000보다 크거나 같으면 실행
            {
                Score = 100000; // 점수를 100000점으로 하고
                bestScore = Score; // 최고 점수도 점수와 같게 해준다

                FileStream fs = new FileStream(Application.dataPath + "Score.txt", FileMode.Create); // 경로에 Score.txt 파일을 만든다.
                BinaryFormatter bf = new BinaryFormatter(); // 직렬화를 위한 bf
                bf.Serialize(fs, bestScore); // 최고 점수를 Score.txt에 직렬화 해서 저장한다.
                fs.Close(); // 파일을 닫는다.

                GameClear(); // GameClear를 실행
            }
        }
        scoreText.text = "Score : " + (int)Score; // 현재 점수 표시, 소수점 제거를 위해 int로 형변환
        bestscoreText.text = "Best Score : " + (int)bestScore; // 최고 점수 표시, 소수점 제거를 위해 int로 형변환
    }

    public void GameOver() // 인터페이스로 상속 받은 함수 정의, 게임오버가 되면 실행
    {
        player.isLive = false; // 플레이어가 죽었다
        gameOver.SetActive(true); // 게임오버 메시지를 띄운다.
        Time.timeScale = 0; // 시간을 멈춘다.
    }

    public void GameClear() // 인터페이스로 상속 받은 함수 정의, 게임 클리어시 실행
    {
        player.isLive = false; // 플레이어가 죽었다고 한다.
        gameClear.SetActive(true); // 게임클리어 메시지를 띄운다.
        Time.timeScale = 0; // 시간을 멈춘다.
    }
}
