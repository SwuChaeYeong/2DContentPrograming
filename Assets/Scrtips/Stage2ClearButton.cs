using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2ClearButton : MonoBehaviour
{
    //�ؽ�Ʈ ���� �� UI Ȱ��ȭ�� ����
    public Text text;
    public GameObject ui;
    public GameObject story;

    //���� ���� �����ϱ� ����
    GameManager gm;

    //���丮 ���
    private string[] st = new string[6];
    private int buttonDownCount = 1;
    
    void Start()
    {
        //GameManager ������Ʈ�� GameManager ��ũ��Ʈ ����
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //���丮 �Է�
        st[0] = "���ռ����� ����������� ������ �������� ����Դϴ�.";
        st[1] = "�賭������ �츮�� ����, ���ռ� ���Դϴ�.";
        st[2] = "������ ���� ���� ���...";
        st[3] = "��?";
        st[4] = "�� �츮 ���ᰡ �� ���� ����� ���� ���� �ǰ���?";
        st[5] = "�״� �츮�� ���� ������ ��ȯ�մϴ�.";

        //Ŭ���� ����
        gm.playerCoin += 20;
    }

    //��ư�� ������ �ؽ�Ʈ �ٲ�(���丮 ����)
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
        //�� ��ȯ(��������3 ȭ������)
        SceneManager.LoadScene("Stage3");
    }
    public void selectClicked()
    {
        //�� ��ȯ(�������� ���� ȭ������)
        SceneManager.LoadScene("StageSlect");
    }
}
