using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //���̾�� ��輱, ��, Ȥ�� ���� ������ �ı���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderFire" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Floor")
            Destroy(gameObject);
    }
}
