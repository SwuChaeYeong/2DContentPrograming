using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //물리 설정 및 이동, 점프
    Rigidbody2D rigid2D;
    public float maxSpeed;
    public float jumpPower;

    //플레이어 애니메이션 및 좌우 반전(방향 따라)
    SpriteRenderer spriteRenderer;
    Animator animator;

    //UI 제어
    public Text coinText;
    public GameObject menu;
    public GameObject pauseButton;

    //제어 변수 접근하기 위함
    GameManager gm;

    //파이어볼
    public GameObject fireObj1;

    //플레이어 체력
    int totalHealth = 100;
    int health;

    //체력바
    public GameObject prfHpBar;
    public Image nowHpBar;

    void Awake()
    {
        //각 컴포넌트 얻어옴
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        nowHpBar = prfHpBar.transform.GetChild(0).GetComponent<Image>();

        //플레이어 체력 얻어옴(다음 스테이지에서도 유지되도록)
        health = gm.playerCurruntHP;
    }

    void Update()
    {
        //스피드 멈추기
        if(Input.GetButtonUp("Horizontal"))
        {
            this.rigid2D.velocity = new Vector2(this.rigid2D.velocity.normalized.x * 0.5f, this.rigid2D.velocity.y);
        }

        //방향 전환
        if (Input.GetButtonDown("Horizontal"))
            //GetAxisRaw > 입력 값을 축 값으로 받아옴, -1, 0, 1을 반환
            //Horizontal은 가로, Vertical은 세로. 왼쪽화살표를 누르면 -1, 오른쪽 화살표는 1
            ////Input.GetAxisRaw("Horizontal")가 1이면(오른쪽 방향 누르면) flipX = True(좌우반전)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;

        //애니메이션
        if (this.rigid2D.velocity.normalized.x == 0)
            //멈췄을 때, walk>idle
            this.animator.SetBool("isWalking", false);
        else
            //움직일 때, idle>walk
            this.animator.SetBool("isWalking", true);

        //점프 & 무한점프X(현재 애니메이션 isJumping이 false일 때)
        if(Input.GetButtonDown("Jump") && !this.animator.GetBool("isJumping"))
        {
            this.rigid2D.AddForce(Vector2.up * this.jumpPower, ForceMode2D.Impulse);
            this.animator.SetBool("isJumping", true);
        }

        //파이어볼 발사
        Fire();

        //체력바 제어
        nowHpBar.fillAmount = (float)health / totalHealth;
        gm.playerCurruntHP = health;

        //코인 표시
        coinText.text = "코인:" + gm.playerCoin;

        //ESC 버튼 클릭 시 일시정지
        if(Input.GetButtonDown("Cancel"))
        {
            if(menu.activeSelf)
            {
                menu.SetActive(false);
                Time.timeScale = 1;
                pauseButton.SetActive(true);
            }
                
            else
            {
                menu.SetActive(true);
                Time.timeScale = 0;
                pauseButton.SetActive(false);
            }    
        }
    }

    private void FixedUpdate()
    {
        //좌우 이동
        float h = Input.GetAxisRaw("Horizontal");
        this.rigid2D.AddForce(Vector2.right*h,ForceMode2D.Impulse);

        //최고 속도 설정
        if (this.rigid2D.velocity.x > maxSpeed) //오른쪽
            this.rigid2D.velocity = new Vector2(maxSpeed, this.rigid2D.velocity.y);

        else if (this.rigid2D.velocity.x < -maxSpeed) //왼쪽
            this.rigid2D.velocity = new Vector2(-maxSpeed, this.rigid2D.velocity.y);

        //아래로 떨어지고 있을 때만 검사 (처음 점프를 땅 위에서 하므로)
        if (this.rigid2D.velocity.y < 0) 
        {
            //바닥으로 ray 쏨
            Debug.DrawRay(this.rigid2D.position, Vector3.down, new Color(0, 1, 0));

            //Floor 레이어만 검사
            RaycastHit2D rayHit = Physics2D.Raycast(this.rigid2D.position, Vector3.down, 2, LayerMask.GetMask("Floor"));

            //ray 바닥에 맞았을 경우
            if (rayHit.collider != null)
            {
                //플레이어의 콜라이더와 겹쳐서 ray가 플레이어에 맞은 것으로 됨(다음 거 무시됨)
                if (rayHit.distance < 1.0f)
                    this.animator.SetBool("isJumping", false);
            }
        }

        //만약에 체력이 다 닳으면 게임 오버 씬으로
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
            gm.playerCurruntHP = 100;
            health = 100;
        }
    }

    void Fire()
    {
        //좌측 버튼 클릭시
        if(Input.GetMouseButtonDown(0))
        {
            if(!spriteRenderer.flipX)
            {
                //플레이어 방향 따라 파이어볼 회전 및 발사 설정
                GameObject fire = Instantiate(fireObj1, transform.position, Quaternion.Euler(0, 0, 90f));

                Rigidbody2D rigidF = fire.GetComponent<Rigidbody2D>();
                rigidF.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
            }
            else
            {
                //플레이어 방향 따라 파이어볼 회전 및 발사 설정
                GameObject fire = Instantiate(fireObj1, transform.position, Quaternion.Euler(0, 0, -90f));

                Rigidbody2D rigidF = fire.GetComponent<Rigidbody2D>();
                rigidF.AddForce(Vector2.right * 15, ForceMode2D.Impulse);
            }
        }
    }

    void OnHit(int dmg) //충돌 시 호출
    {
        //체력 감소
        health -= dmg;

        //깜빡거리는 애니메이션 호출
        animator.SetBool("isHit", true);

        //일정 시간 후 다시 원래 상태 호출
        Invoke("returnSprite", 0.2f);
    }

    void returnSprite()
    {
        //원래 상태 호출
        animator.SetBool("isHit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //배너와 충돌 시 다음 스테이지로 넘어감
        if (collision.gameObject.tag == "Banner")
        {
            //다음 스테이지 씬 호출
            SceneManager.LoadScene("Stage"+gm.curruntStage+"Clear");
            //클리어한 스테이지 제어
            gm.clearStage++;
        }

        if (collision.gameObject.tag == "BorderFire")
        {
            //아래로 떨어지면 다시 위로 옮겨주고 체력 감소
            transform.position = new Vector3(0, 4, 0);
            health -= 10;
        }

        //적과 충돌 시 OnHit 호출
        if (collision.gameObject.tag == "Enemy")
            OnHit(gm.enemDmg);
    }

    public void pauseClicked()  //일시정지 버튼 클릭 시
    {
        //일시 정지 메뉴 활성화 및 모든 시간 멈춤(게임 멈춤)
        menu.SetActive(true);
        Time.timeScale = 0;

        //일시정지 버튼 비활성화
        pauseButton.SetActive(false);
    }
    public void continueClicked()   //계속하기 버튼 클릭 시
    {
        //일시 정지 메뉴 비활성화 및 시간 다시 흐름 (게임 재생)
        menu.SetActive(false);
        Time.timeScale = 1;

        //일시정지 버튼 활성화
        pauseButton.SetActive(true);
    }
    public void mainClicked()   //메인 버튼 클릭 시
    {
        //메인화면 씬 재생 및 시간 다시 흐르게 함(아니면 오류)
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
    }
}
