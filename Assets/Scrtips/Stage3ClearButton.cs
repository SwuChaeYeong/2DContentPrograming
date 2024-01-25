using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage3ClearButton : MonoBehaviour
{
    //�ؽ�Ʈ ���� �� UI Ȱ��ȭ�� ����
    public Text text;
    public GameObject ui;
    public GameObject story;

    //���� ���� �����ϱ� ����
    GameManager gm;

    //���丮 ���
    private string[] st = new string[7];
    private int buttonDownCount = 1;
    
    void Start()
    {
        //GameManager ������Ʈ�� GameManager ��ũ��Ʈ ����
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //���丮 �Է�
        st[0] = "������ ��� ����ġ�� ���� ������ ������ �������ϴ�.";
        st[1] = "�״� �׷��� ������ ����� �Բ��ߴ� �ð��� ��ſ��ٰ� ���մϴ�.";
        st[2] = "�����̶���. ";
        st[3] = "�ŵ�Ǵ� ȯ���� ���� ������ ����� �ŵ쳪���� �ߴ� �� �����ϴ�.";
        st[4] = "�Ѷ� ���ῴ�� ���, �ƴ� ������ ���� ��Ѱ���.";
        st[5] = "�츮�� ��ȭ�� ���ѳ�����...";
        st[6] = "��¼��, �츮���� ���� �� ��ȭ �� ��Ʈ �������� �𸨴ϴ�.";

        //Ŭ���� ����
        gm.playerCoin += 30;
        gm.playerCurruntHP = 100;
    }

    public void storyNextClicked()
    {
        //������ �з���ŭ ���丮�� ����ϸ�, ��ȭâ�� �����ϰ� �������� ��ȯ UI ���� �� ����
        if (buttonDownCount>=st.Length)
        {
            story.SetActive(false);
            ui.SetActive(true);

            return;
        }

        //�ؽ�Ʈ ����(���丮 ����) �� ����
        text.text = st[buttonDownCount];
        buttonDownCount++;
    }

    public void nextStageClicked()
    {
        //���� ȭ������ ����
        SceneManager.LoadScene("Clear");
    }
}
