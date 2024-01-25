using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    //���� ���� �����ϱ� ����
    GameManager gm;
    void Start()
    {
        //GameManager ������Ʈ�� GameManager ��ũ��Ʈ ����
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    ////����
    //START ��ư Ŭ�� ��
    public void StartButtonClicked()
    {
        //�� ��ȯ(�������� ���� ȭ������)
        SceneManager.LoadScene("StageSlect");
    }

    //EXIT ��ư Ŭ�� ��
    public void ExitButtonClicked()
    {
        //���� ����
        Application.Quit();
    }

    //MAIN ��ư Ŭ�� ��
    public void MainButtonClicked()
    {
        //��������
        SceneManager.LoadScene("Main");
    }

    ////�������� ����
    //Ʃ�丮�� ��ư Ŭ�� ��
    public void TutorialButtonClicked()
    {
        //�� ��ȯ(Ʃ�丮��)
        SceneManager.LoadScene("Tutorial");
    }

    //Stage1 ��ư Ŭ�� ��
    public void Stage1ButtonClicked()
    {
        SceneManager.LoadScene("Stage1");
    }

    //Stage2 ��ư Ŭ�� ��
    public void Stage2ButtonClicked()
    {
        SceneManager.LoadScene("Stage2");
    }

    //Stage3 ��ư Ŭ�� ��
    public void Stage3ButtonClicked()
    {
        SceneManager.LoadScene("Stage3");
    }

    //Ʃ�丮�󿡼� back ������ ��
    public void TutorialtoStage()
    {
        SceneManager.LoadScene("StageSlect");
        gm.clearStage++;
    }
}
