using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActive : MonoBehaviour
{
    //���� ���� �����ϱ� ����
    GameManager gm;

    //�������� ���� �迭
    public GameObject[] bnt;

    // Start is called before the first frame update
    void Start()
    {
        //GameManager ������Ʈ�� GameManager ��ũ��Ʈ ����
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ŭ���� ���������� ���� �������� ���� 
        if (gm.curruntStage < 4)
        {
            for (int i = 0; i <= gm.clearStage; i++)
                bnt[i].GetComponent<Button>().interactable = true;
        }
    }
}
