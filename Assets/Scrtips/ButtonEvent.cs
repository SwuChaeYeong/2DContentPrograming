using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    //제어 변수 접근하기 위함
    GameManager gm;
    void Start()
    {
        //GameManager 오브젝트의 GameManager 스크립트 얻어옴
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    ////메인
    //START 버튼 클릭 시
    public void StartButtonClicked()
    {
        //씬 전환(스테이지 선택 화면으로)
        SceneManager.LoadScene("StageSlect");
    }

    //EXIT 버튼 클릭 시
    public void ExitButtonClicked()
    {
        //게임 종료
        Application.Quit();
    }

    //MAIN 버튼 클릭 시
    public void MainButtonClicked()
    {
        //메인으로
        SceneManager.LoadScene("Main");
    }

    ////스테이지 선택
    //튜토리얼 버튼 클릭 시
    public void TutorialButtonClicked()
    {
        //씬 전환(튜토리얼)
        SceneManager.LoadScene("Tutorial");
    }

    //Stage1 버튼 클릭 시
    public void Stage1ButtonClicked()
    {
        SceneManager.LoadScene("Stage1");
    }

    //Stage2 버튼 클릭 시
    public void Stage2ButtonClicked()
    {
        SceneManager.LoadScene("Stage2");
    }

    //Stage3 버튼 클릭 시
    public void Stage3ButtonClicked()
    {
        SceneManager.LoadScene("Stage3");
    }

    //튜토리얼에서 back 눌렀을 때
    public void TutorialtoStage()
    {
        SceneManager.LoadScene("StageSlect");
        gm.clearStage++;
    }
}
