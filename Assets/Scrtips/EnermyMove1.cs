using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnermyMove1 : MonoBehaviour
{
    //���� ���� �� �̵�
    Rigidbody2D rigid2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    //���� UI�� ���� ���� ���� ����
    public int nextMove;

    //ù ��° �������� ���� ü��
    int totalHealth = 30;
    int health = 30;

    //ü�¹�
    public GameObject prfHpBar;
    public GameObject canvas;
    public Image nowHpBar;
    RectTransform hpBar;

    //���� ���� �����ϱ� ����
    GameManager gm;

    void Awake()
    {
        //�� ������Ʈ ����
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //���� �������� ����
        gm.curruntStage = 1;

        //���� AI �Լ� ȣ��
        Think();
    }
    void FixedUpdate()
    {
        //������
        rigid2D.velocity = new Vector2(nextMove, rigid2D.velocity.y);

        //�ٴ����� ray ��
        Vector2 frontVec = new Vector2(rigid2D.position.x + nextMove * + 0.5f, rigid2D.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        //Floor ���̾ �˻�
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Floor"));

        //ray �ٴڿ� �¾��� ���
        if (rayHit.collider == null)
        {
            //�÷��̾��� �ݶ��̴��� ���ļ� ray�� �÷��̾ ���� ������ ��(���� �� ���õ�)
            if (rayHit.distance < 1.0f)
            {
                //�ٴ� ���� ���� �� ���� ��ȯ
                Turn();
            }
        }
    }
    void Update()
    {
        //ü�¹ٰ� ���͸� ����ٴϵ��� ����
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 0.9f, 0));
        hpBar.position = hpBarPos;
    }

    void Think()
    {
        //��, ��, ���� �̵� ���� 
        nextMove = Random.Range(-1, 2);

        //���� �ð� ����
        float nextThinkTime = Random.Range(2f, 5f);

        //�ð����� ����Լ�
        Invoke("Think", nextThinkTime);

        //�ִϸ��̼�
        animator.SetInteger("walkSpeed", nextMove);
        
        //���� ����
        if (nextMove != 0) 
            spriteRenderer.flipX = nextMove == 1;
    }

    void Turn()
    {
        //nextMove�� -1 ���ؼ� ���� ��ȯ
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke(); //������ �ʱ�ȭ
        Invoke("Think", 5);
    }

    void OnHit(int dmg) //�浹 �� ȣ��
    {
        //ü�� ����
        health -= dmg;

        //�����Ÿ��� �ִϸ��̼� ȣ��
        animator.SetBool("isHit", true);

        //hp�� ����
        nowHpBar.fillAmount = (float)health / totalHealth;

        //���࿡ ü���� �� ������ �ı�
        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(hpBar.gameObject);
        }

        //�ٽ� ���� ���� ȣ��
        Invoke("returnSprite", 0.2f);
    }

    void returnSprite()
    {
        //���� ���� ȣ��
        animator.SetBool("isHit", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾ �� ���̾�� ������ onHit ȣ��
        if(collision.gameObject.tag == "Fire")
        {
            OnHit(5);
        }
    }
}
