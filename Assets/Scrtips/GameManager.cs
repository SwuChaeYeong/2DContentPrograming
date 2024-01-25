using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�÷��̾� ���ݷ�, �� ���ݷ�, �÷��̾� ���� ü��, ����, ���� ��������, Ŭ������ ��������, �� ���������� ����, 
    public int plDmg, enemDmg, playerCurruntHP, playerCoin, curruntStage, clearStage;

    void Start()
    {
        plDmg = 5;
        enemDmg = 5;
        playerCurruntHP = 100;
        playerCoin = 0;
        curruntStage = 0;
        clearStage = 0;

        //Ȥ�� �ٽ� ó�� ������ ���ƿ��� ��, �������� �ʴ� GameManager ���� ������Ʈ�� �� �� �̻� ���� �ʵ��� ��
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
        //���� ���������� ���� ���ʹ� ������ ����
        if (curruntStage == 1)
            enemDmg = 5;

        if (curruntStage == 2)
            enemDmg = 8;

        if (curruntStage == 3)
            enemDmg = 10;
    }
}
