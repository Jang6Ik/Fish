using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 유니티 ui를 사용하기 위해 추가한 namespace
using UnityEngine.SceneManagement; // 유니티 SceneManager를 사용하기 위해 추가한 namespace 

public class Button : MonoBehaviour // MonoBehaviour 클래스의 상속을 받음
{
    public Director director; // 감독 클래스 찾기

    public void StartGame() // 게임 시작 버튼
    {
        SceneManager.LoadScene("Fish"); // 게임 씬을 Fish로 바꿈
        Time.timeScale = 1; // 게임 시간을 멈춘다.
    }

    public void MainMenu() // 메인 메뉴로 가는 버튼
    {
        SceneManager.LoadScene("MainMenu"); // 게임 씬을 MainMenu의 이름인 씬으로 바꾼다.
        Time.timeScale = 1; // 게임 시간을 흐르게 함
    }

    public void ExitGame() // 게임 종료 버튼
    {
        Application.Quit(); // 게임 종료
    }

    public void BackGroundMusicOffButton() // 배경음악 키고 끄는 버튼
    {
        if (director.BGM.isPlaying) // 배경음악이 재생되고 있으면
        {
            director.BGM.Pause(); // 배경음악을 멈춘다.
            director.BGMbutton.GetComponent<Image>().sprite = director.mute; // 스프라이트를 mute로 바꿈
        }
        else // 아니면
        {
            director.BGM.Play(); // 배경음악을 재생한다.
            director.BGMbutton.GetComponent<Image>().sprite = director.play; // 스프라이트를 play로 바꿈
        }
    }
}
