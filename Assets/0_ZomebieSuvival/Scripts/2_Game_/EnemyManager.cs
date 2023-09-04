using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    
    public float findDistance = 10f;
    public float attackDistance = 3f;
    public float moveSpeed = 5f;
    public float moveDistance = 20f;

    public int attackPower = 10;
    public int maxHp = 50;
    public int hp;
    public int scoreValue = 10;
    
    private float current_Time = 0;
    private float attackDelay = 2f;
    
    //GameObject []
    // clear[0] sound;
    // clear[1] winImg;
    // clear[2] win_Sound;
    // clear[3] win_Score;
    // clear[4] player;
    // clear[5] enemySound;

    public GameObject[] clear = new GameObject[5];
    public GameObject attackSound;
    public Slider hpSlider;
    
    EnemyState m_State;
    Transform player;
    CharacterController cc;
    Vector3 originPos;
    Quaternion originRot;

    Animator[] anim = new Animator[2];

    void Start()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();

        originPos = transform.position;
        originRot = transform.rotation;

        hp = maxHp;
        
        anim[0] = transform.GetComponentInChildren<Animator>();
        anim[1] = clear[3].GetComponent<Animator>();

    }

    void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
        }

        hpSliderbar();
    
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("Idle -> Move");

            anim[0].SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("Move -> Return");
        }

        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = player.position - transform.position;
            dir = dir.normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
        }

        else
        {
            m_State = EnemyState.Attack;
            print("Move -> Attack");
            current_Time = attackDelay;
            anim[0].SetTrigger("MoveToAttackDelay");
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            current_Time += Time.deltaTime;

            if (current_Time > attackDelay)
            {
                player.GetComponent<PlayerManager>().DamagedAction(attackPower);
                print("Attack");
                current_Time = 0;
                anim[0].SetTrigger("StartAttack");
            }
        }

        else
        {
            m_State = EnemyState.Move;
            print("Attack -> Move");
            current_Time = 0;
            anim[0].SetTrigger("AttackToMove");
        }
    }

    void Return()
    {
        if(Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
        }

        else
        {
            transform.position = originPos;
            transform.rotation = originRot;
            hp = maxHp;
            m_State = EnemyState.Idle;
            print("Return -> Idle");
            anim[0].SetTrigger("MoveToIdle");
        }
    }

    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
        attackSound.SetActive(false);

        ScoreManager.score += scoreValue;
        
        // if (this.gameObject.layer == LayerMask.NameToLayer("Boss"))
        // {
        //     print("BossDie");
        //     clear[0].SetActive(false);
        //     clear[1].SetActive(true);
        //     clear[2].SetActive(true);
        //     clear[4].SetActive(false);
        //     Destroy(clear[5]);
        //     anim[1].SetTrigger("WinScore");


        // }
    }

    void hpSliderbar()
    {
        hpSlider.value = (float)hp / (float)maxHp;
    }

    public void HitEnemy(int hitPower)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }

        hp -= hitPower;

        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("Damged");
            Damaged();
        }

        else
        {
            m_State = EnemyState.Die;
            print("Die");
            if (this.gameObject.layer == LayerMask.NameToLayer("Boss"))
            {
                print("bossdie");
                clear[0].SetActive(false);
                clear[1].SetActive(true);
                clear[2].SetActive(true);
                clear[4].SetActive(false);
                Destroy(clear[5]);
                anim[1].SetTrigger("WinScore");
            }
            anim[0].SetTrigger("Die");
            Die();
        }

        print(hp);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attackSound.SetActive(true);
        }

        else if (attackSound.activeSelf == false)
        {
            attackSound.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attackSound.SetActive(false);
        
        }
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);
        m_State = EnemyState.Move;
        print("Damged -> Move");
        anim[0].SetTrigger("IdleToMove");
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
}
