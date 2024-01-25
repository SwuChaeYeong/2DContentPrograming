using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActive : MonoBehaviour
{
    //제어 변수 접근하기 위함
    GameManager gm;

    //스테이지 제어 배열
    public GameObject[] bnt;

    // Start is called before the first frame update
    void Start()
    {
        //GameManager 오브젝트의 GameManager 스크립트 얻어옴
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //클리어 스테이지에 따라 스테이지 선택 
        if (gm.curruntStage < 4)
        {
            for (int i = 0; i <= gm.clearStage; i++)
                bnt[i].GetComponent<Button>().interactable = true;
        }
    }
}
