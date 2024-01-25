using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage3ClearButton : MonoBehaviour
{
    //텍스트 변경 및 UI 활성화를 위함
    public Text text;
    public GameObject ui;
    public GameObject story;

    //제어 변수 접근하기 위함
    GameManager gm;

    //스토리 출력
    private string[] st = new string[7];
    private int buttonDownCount = 1;
    
    void Start()
    {
        //GameManager 오브젝트의 GameManager 스크립트 얻어옴
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //스토리 입력
        st[0] = "마물을 모두 물리치자 힘이 약해진 마왕이 쓰러집니다.";
        st[1] = "그는 그래도 나머지 용사들과 함께했던 시간이 즐거웠다고 말합니다.";
        st[2] = "진심이라고요. ";
        st[3] = "거듭되는 환생을 통해 위대한 존재로 거듭나고자 했던 것 같습니다.";
        st[4] = "한때 동료였던 용사, 아니 마왕의 끝은 어떠한가요.";
        st[5] = "우리는 평화를 지켜냈지만...";
        st[6] = "어쩌면, 우리에게 남은 건 금화 두 세트 뿐일지도 모릅니다.";

        //클리어 보상
        gm.playerCoin += 30;
        gm.playerCurruntHP = 100;
    }

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
        //엔딩 화면으로 가기
        SceneManager.LoadScene("Clear");
    }
}
