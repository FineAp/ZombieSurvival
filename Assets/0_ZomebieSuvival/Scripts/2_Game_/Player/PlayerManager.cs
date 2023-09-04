using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float hitSpeed = 0.7f;
    public int hp = 100;

    public Slider hpSlider;
    public GameObject hitEffect;
    public GameObject gunChangeSound;
    public GameObject[] guns = new GameObject[3];

    public GameObject lastCanvas;
    private GameObject[] scoreText = new GameObject[2];
    private GameObject bgmSound;
    private GameObject enemySound;
    private GameObject die;
    private int maxHp = 100;

    private Animator[] anim = new Animator[2];
    void Start()
    {
        guns[0].SetActive(true);
        enemySound = GameObject.Find("SoundManage");
        bgmSound = GameObject.Find("SoundBGM");
        scoreText[0] = GameObject.Find("Score");
        scoreText[1] = GameObject.Find("ScoreText");

        anim[0] = scoreText[0].GetComponent<Animator>();
        anim[1] = scoreText[1].GetComponent<Animator>();
        
    }
    void Update()
    {
        hpSliderbar();
        ChangeGun();
    }

    public void DamagedAction(int damage)
    {
        //플레이어 사망
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
            lastCanvas.SetActive(true);
            enemySound.SetActive(false);
            bgmSound.SetActive(false);

            // Destroy(this.gameObject);

            anim[0].SetTrigger("Score");
            anim[1].SetTrigger("ScoreText");

        
            this.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(PlayerHitEffect());

        }
        print(hp);
    }

    void hpSliderbar()
    {
        hpSlider.value = (float)hp / (float)maxHp;
    }

    void ChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("Alpha1");
            guns[0].SetActive(true);
            guns[1].SetActive(false);
            guns[2].SetActive(false);

            Instantiate(gunChangeSound);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            guns[0].SetActive(false);
            guns[1].SetActive(true);
            guns[2].SetActive(false);

            Instantiate(gunChangeSound);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            guns[0].SetActive(false);
            guns[1].SetActive(false);
            guns[2].SetActive(true);

            Instantiate(gunChangeSound);
        }
    }

    IEnumerator PlayerHitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(hitSpeed);
        hitEffect.SetActive(false);
    }
}


