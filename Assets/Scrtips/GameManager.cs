using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //플레이어 공격력, 적 공격력, 플레이어 현재 체력, 코인, 현재 스테이지, 클리어한 스테이지, 각 스테이지별 보상, 
    public int plDmg, enemDmg, playerCurruntHP, playerCoin, curruntStage, clearStage;

    void Start()
    {
        plDmg = 5;
        enemDmg = 5;
        playerCurruntHP = 100;
        playerCoin = 0;
        curruntStage = 0;
        clearStage = 0;

        //혹시 다시 처음 씬으로 돌아왔을 때, 삭제되지 않는 GameManager 게임 오브젝트가 두 개 이상 되지 않도록 함
        var obj = FindObjectsOfType<GameManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //현재 스테이지에 따라 에너미 데미지 설정
        if (curruntStage == 1)
            enemDmg = 5;

        if (curruntStage == 2)
            enemDmg = 8;

        if (curruntStage == 3)
            enemDmg = 10;
    }
}
