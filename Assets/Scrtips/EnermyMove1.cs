using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnermyMove1 : MonoBehaviour
{
    //물리 설정 및 이동
    Rigidbody2D rigid2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    //몬스터 UI를 위한 랜덤 방향 설정
    public int nextMove;

    //첫 번째 스테이지 몬스터 체력
    int totalHealth = 30;
    int health = 30;

    //체력바
    public GameObject prfHpBar;
    public GameObject canvas;
    public Image nowHpBar;
    RectTransform hpBar;

    //제어 변수 접근하기 위함
    GameManager gm;

    void Awake()
    {
        //각 컴포넌트 얻어옴
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //현재 스테이지 설정
        gm.curruntStage = 1;

        //몬스터 AI 함수 호출
        Think();
    }
    void FixedUpdate()
    {
        //움직임
        rigid2D.velocity = new Vector2(nextMove, rigid2D.velocity.y);

        //바닥으로 ray 쏨
        Vector2 frontVec = new Vector2(rigid2D.position.x + nextMove * + 0.5f, rigid2D.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        //Floor 레이어만 검사
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Floor"));

        //ray 바닥에 맞았을 경우
        if (rayHit.collider == null)
        {
            //플레이어의 콜라이더와 겹쳐서 ray가 플레이어에 맞은 것으로 됨(다음 거 무시됨)
            if (rayHit.distance < 1.0f)
            {
                //바닥 끝과 만날 시 방향 전환
                Turn();
            }
        }
    }
    void Update()
    {
        //체력바가 몬스터를 따라다니도록 설정
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.9f, 0));
        hpBar.position = hpBarPos;
    }

    void Think()
    {
        //좌, 우, 멈춤 이동 랜덤 
        nextMove = Random.Range(-1, 2);

        //지연 시간 랜덤
        float nextThinkTime = Random.Range(2f, 5f);

        //시간지연 재귀함수
        Invoke("Think", nextThinkTime);

        //애니메이션
        animator.SetInteger("walkSpeed", nextMove);
        
        //방향 조절
        if (nextMove != 0) 
            spriteRenderer.flipX = nextMove == 1;
    }

    void Turn()
    {
        //nextMove에 -1 곱해서 방향 전환
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke(); //딜레이 초기화
        Invoke("Think", 5);
    }

    void OnHit(int dmg) //충돌 시 호출
    {
        //체력 감소
        health -= dmg;

        //깜빡거리는 애니메이션 호출
        animator.SetBool("isHit", true);

        //hp바 설정
        nowHpBar.fillAmount = (float)health / totalHealth;

        //만약에 체력이 다 닳으면 파괴
        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(hpBar.gameObject);
        }

        //다시 원래 상태 호출
        Invoke("returnSprite", 0.2f);
    }

    void returnSprite()
    {
        //원래 상태 호출
        animator.SetBool("isHit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어가 쏜 파이어볼과 맞으면 onHit 호출
        if(collision.gameObject.tag == "Fire")
        {
            OnHit(5);
        }
    }
}
