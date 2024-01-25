using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage1ClearButton : MonoBehaviour
{
    //텍스트 변경 및 UI 활성화를 위함
    public Text text;
    public GameObject ui;
    public GameObject story;

    //제어 변수 접근하기 위함
    GameManager gm;

    //스토리 출력
    private string[] st = new string[2];
    private int buttonDownCount = 1;
    
    void Start()
    {
        //GameManager 오브젝트의 GameManager 스크립트 얻어옴
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //스토리 입력
        st[0] = "벌써 마물이 마을까지 내려오는군요.";
        st[1] = "무사히 마을을 구한 우리는, 마을 여관에서 잠시 쉬고 다시 길을 떠납니다.";

        //클리어 보상
        gm.playerCoin += 10;
    }

    //버튼을 누르면 텍스트 바꿈(스토리 진행)
    public void storyNextClicked()
    {
        //정해진 분량만큼 스토리를 출력하면, 대화창을 삭제하고 스테이지 전환 UI 등장 및 리턴
        if (buttonDownCount >= st.Length) 
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
        //씬 전환(스테이지2 화면으로)
        SceneManager.LoadScene("Stage2");
    }
    public void selectClicked()
    {
        //씬 전환(스테이지 선택 화면으로)
        SceneManager.LoadScene("StageSlect");
    }
}
