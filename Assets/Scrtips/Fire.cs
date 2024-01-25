using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //파이어볼이 경계선, 적, 혹은 땅을 만나면 파괴됨
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderFire" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Floor")
            Destroy(gameObject);
    }
}
