using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //플레이어 오브젝트 받아옴
    GameObject player;
    void Start()
    {
        this.player = GameObject.Find("Player_Witch1");
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 위치에 따라 카메라가 이동함
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
