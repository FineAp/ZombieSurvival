using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public int weaponPower = 15;

    public GameObject bulletEffect;
    public GameObject[] sounds = new GameObject[3];

    GameObject stopSound;
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        LeftMouseButton();
        FireSound();
    }

    void LeftMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //레이를 쏘는 곳 Raycast
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //레이거 부딫힌 대상의 정보 Hit
            RaycastHit hitInfo = new RaycastHit();
            
            //부딫힌 물체가 있으면 이펙트 표시
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy") || hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Boss"))
                {
                    EnemyManager enemyMN = hitInfo.transform.GetComponent<EnemyManager>();
                    enemyMN.HitEnemy(weaponPower);
                }

                bulletEffect.transform.position = hitInfo.point;

                //피격 이펙트의 방향을 부딫힌 지점의 법선 벡터와 일치
                bulletEffect.transform.forward = hitInfo.normal;
                ps.Play();

            }
        }
    }
    //사운드를 담당합니다.
    void FireSound()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(sounds[0]);

        }

        else if(Input.GetMouseButtonUp(0))
        {
            stopSound = GameObject.Find("SoundSmallGun(Clone)");
            Destroy(stopSound, 0.3f);
            // print("Mouse Up OK");
        }
    }

}
