using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //�÷��̾� ������Ʈ �޾ƿ�
    GameObject player;
    void Start()
    {
        this.player = GameObject.Find("Player_Witch1");
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾� ��ġ�� ���� ī�޶� �̵���
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
