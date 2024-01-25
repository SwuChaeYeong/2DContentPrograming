using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2ClearButton : MonoBehaviour
{
    //텍스트 변경 및 UI 활성화를 위함
    public Text text;
    public GameObject ui;
    public GameObject story;

    //제어 변수 접근하기 위함
    GameManager gm;

    //스토리 출력
    private string[] st = new string[6];
    private int buttonDownCount = 1;
    
    void Start()
    {
        //GameManager 오브젝트의 GameManager 스크립트 얻어옴
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //스토리 입력
        st[0] = "마왕성으로 가까워질수록 마물이 많아지는 모양입니다.";
        st[1] = "험난했지만 우리는 지금, 마왕성 앞입니다.";
        st[2] = "굳건히 닫힌 문을 어떻게...";
        st[3] = "어?";
        st[4] = "왜 우리 동료가 이 문을 그토록 쉽게 여는 건가요?";
        st[5] = "그는 우리를 향해 마물을 소환합니다.";

        //클리어 보상
        gm.playerCoin += 20;
    }

    //버튼을 누르면 텍스트 바꿈(스토리 진행)
    public void storyNextClicked()
    {
        //정해진 분량만큼 스토리를 출력하면, 대화창을 삭제하고 스테이지 전환 UI 등장 및 리턴
        if (buttonDownCount>=st.Length)
        {
            story.SetActive(false);
            ui.SetActive(true);

            return;
        }

        //텍스트 지정(스토리 진행) 및 제어
        text.text = st[buttonDownCount];
        buttonDownCount++;
    }

    public void nextStageClicked()
    {
        //씬 전환(스테이지3 화면으로)
        SceneManager.LoadScene("Stage3");
    }
    public void selectClicked()
    {
        //씬 전환(스테이지 선택 화면으로)
        SceneManager.LoadScene("StageSlect");
    }
}
