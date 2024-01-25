using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //���� ���� �� �̵�, ����
    Rigidbody2D rigid2D;
    public float maxSpeed;
    public float jumpPower;

    //�÷��̾� �ִϸ��̼� �� �¿� ����(���� ����)
    SpriteRenderer spriteRenderer;
    Animator animator;

    //UI ����
    public Text coinText;
    public GameObject menu;
    public GameObject pauseButton;

    //���� ���� �����ϱ� ����
    GameManager gm;

    //���̾
    public GameObject fireObj1;

    //�÷��̾� ü��
    int totalHealth = 100;
    int health;

    //ü�¹�
    public GameObject prfHpBar;
    public Image nowHpBar;

    void Awake()
    {
        //�� ������Ʈ ����
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        nowHpBar = prfHpBar.transform.GetChild(0).GetComponent<Image>();

        //�÷��̾� ü�� ����(���� �������������� �����ǵ���)
        health = gm.playerCurruntHP;
    }

    void Update()
    {
        //���ǵ� ���߱�
        if(Input.GetButtonUp("Horizontal"))
        {
            this.rigid2D.velocity = new Vector2(this.rigid2D.velocity.normalized.x * 0.5f, this.rigid2D.velocity.y);
        }

        //���� ��ȯ
        if (Input.GetButtonDown("Horizontal"))
            //GetAxisRaw > �Է� ���� �� ������ �޾ƿ�, -1, 0, 1�� ��ȯ
            //Horizontal�� ����, Vertical�� ����. ����ȭ��ǥ�� ������ -1, ������ ȭ��ǥ�� 1
            ////Input.GetAxisRaw("Horizontal")�� 1�̸�(������ ���� ������) flipX = True(�¿����)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;

        //�ִϸ��̼�
        if (this.rigid2D.velocity.normalized.x == 0)
            //������ ��, walk>idle
            this.animator.SetBool("isWalking", false);
        else
            //������ ��, idle>walk
            this.animator.SetBool("isWalking", true);

        //���� & ��������X(���� �ִϸ��̼� isJumping�� false�� ��)
        if(Input.GetButtonDown("Jump") && !this.animator.GetBool("isJumping"))
        {
            this.rigid2D.AddForce(Vector2.up * this.jumpPower, ForceMode2D.Impulse);
            this.animator.SetBool("isJumping", true);
        }

        //���̾ �߻�
        Fire();

        //ü�¹� ����
        nowHpBar.fillAmount = (float)health / totalHealth;
        gm.playerCurruntHP = health;

        //���� ǥ��
        coinText.text = "����:" + gm.playerCoin;

        //ESC ��ư Ŭ�� �� �Ͻ�����
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
        //�¿� �̵�
        float h = Input.GetAxisRaw("Horizontal");
        this.rigid2D.AddForce(Vector2.right*h,ForceMode2D.Impulse);

        //�ְ� �ӵ� ����
        if (this.rigid2D.velocity.x > maxSpeed) //������
            this.rigid2D.velocity = new Vector2(maxSpeed, this.rigid2D.velocity.y);

        else if (this.rigid2D.velocity.x < -maxSpeed) //����
            this.rigid2D.velocity = new Vector2(-maxSpeed, this.rigid2D.velocity.y);

        //�Ʒ��� �������� ���� ���� �˻� (ó�� ������ �� ������ �ϹǷ�)
        if (this.rigid2D.velocity.y < 0) 
        {
            //�ٴ����� ray ��
            Debug.DrawRay(this.rigid2D.position, Vector3.down, new Color(0, 1, 0));

            //Floor ���̾ �˻�
            RaycastHit2D rayHit = Physics2D.Raycast(this.rigid2D.position, Vector3.down, 2, LayerMask.GetMask("Floor"));

            //ray �ٴڿ� �¾��� ���
            if (rayHit.collider != null)
            {
                //�÷��̾��� �ݶ��̴��� ���ļ� ray�� �÷��̾ ���� ������ ��(���� �� ���õ�)
                if (rayHit.distance < 1.0f)
                    this.animator.SetBool("isJumping", false);
            }
        }

        //���࿡ ü���� �� ������ ���� ���� ������
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
            gm.playerCurruntHP = 100;
            health = 100;
        }
    }

    void Fire()
    {
        //���� ��ư Ŭ����
        if(Input.GetMouseButtonDown(0))
        {
            if(!spriteRenderer.flipX)
            {
                //�÷��̾� ���� ���� ���̾ ȸ�� �� �߻� ����
                GameObject fire = Instantiate(fireObj1, transform.position, Quaternion.Euler(0, 0, 90f));

                Rigidbody2D rigidF = fire.GetComponent<Rigidbody2D>();
                rigidF.AddForce(Vector2.left * 15, ForceMode2D.Impulse);
            }
            else
            {
                //�÷��̾� ���� ���� ���̾ ȸ�� �� �߻� ����
                GameObject fire = Instantiate(fireObj1, transform.position, Quaternion.Euler(0, 0, -90f));

                Rigidbody2D rigidF = fire.GetComponent<Rigidbody2D>();
                rigidF.AddForce(Vector2.right * 15, ForceMode2D.Impulse);
            }
        }
    }

    void OnHit(int dmg) //�浹 �� ȣ��
    {
        //ü�� ����
        health -= dmg;

        //�����Ÿ��� �ִϸ��̼� ȣ��
        animator.SetBool("isHit", true);

        //���� �ð� �� �ٽ� ���� ���� ȣ��
        Invoke("returnSprite", 0.2f);
    }

    void returnSprite()
    {
        //���� ���� ȣ��
        animator.SetBool("isHit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��ʿ� �浹 �� ���� ���������� �Ѿ
        if (collision.gameObject.tag == "Banner")
        {
            //���� �������� �� ȣ��
            SceneManager.LoadScene("Stage"+gm.curruntStage+"Clear");
            //Ŭ������ �������� ����
            gm.clearStage++;
        }

        if (collision.gameObject.tag == "BorderFire")
        {
            //�Ʒ��� �������� �ٽ� ���� �Ű��ְ� ü�� ����
            transform.position = new Vector3(0, 4, 0);
            health -= 10;
        }

        //���� �浹 �� OnHit ȣ��
        if (collision.gameObject.tag == "Enemy")
            OnHit(gm.enemDmg);
    }

    public void pauseClicked()  //�Ͻ����� ��ư Ŭ�� ��
    {
        //�Ͻ� ���� �޴� Ȱ��ȭ �� ��� �ð� ����(���� ����)
        menu.SetActive(true);
        Time.timeScale = 0;

        //�Ͻ����� ��ư ��Ȱ��ȭ
        pauseButton.SetActive(false);
    }
    public void continueClicked()   //����ϱ� ��ư Ŭ�� ��
    {
        //�Ͻ� ���� �޴� ��Ȱ��ȭ �� �ð� �ٽ� �帧 (���� ���)
        menu.SetActive(false);
        Time.timeScale = 1;

        //�Ͻ����� ��ư Ȱ��ȭ
        pauseButton.SetActive(true);
    }
    public void mainClicked()   //���� ��ư Ŭ�� ��
    {
        //����ȭ�� �� ��� �� �ð� �ٽ� �帣�� ��(�ƴϸ� ����)
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
    }
}
