using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {

    
    GameObject mLineLaser;
    Animator mPlayerAni;
    float mNextFire;
    float mNextChase;
    float mNextLaser;
    float mHealth;
    int mPowerIndex;
    bool mIsFirePush;

    public PlayerObjStruct mInfos;
    public UISprite mMyheart;

    enum BulletStyle { Basic, Laser }
    enum BasicBulletStyle { oneway, threeway }
    BulletStyle mCurrentBullet;
    BasicBulletStyle mBaiscStyle;

    Collider2D mBoxcollider;
    SpriteRenderer mRender;

    bool mIsChase;


    // Use this for initialization

    bool mIsShootHead;

    void Awake()
    {
        mIsFirePush = false;
        mHealth = mInfos.Health;
        mNextFire = 0;
        mNextLaser = 0;
        mPowerIndex = 0;
        mCurrentBullet = BulletStyle.Basic;
        mBaiscStyle = BasicBulletStyle.oneway;
        mIsShootHead = false;
        mIsChase = false;
        mPlayerAni = GetComponent<Animator>();
        mBoxcollider = GetComponent<BoxCollider2D>();
        mRender = GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputProgress();
        PositionFix();
    }

    void FixedUpdate()
    {

    }

    void PowerUp()
    {
        mPowerIndex++;
        if (mPowerIndex > 3)
        {
            mPowerIndex = 3;
        }
    }

    void TurnBasic()
    {
        mCurrentBullet = BulletStyle.Basic;
        Destroy(mLineLaser);
    }

    void TurnLaser()
    {
        mCurrentBullet = BulletStyle.Laser;
        if (mIsFirePush)
        {
            mLineLaser = Instantiate(mInfos.LineLaser.Bullet, mInfos.SpawnTransf[0].position, mInfos.SpawnTransf[0].rotation)
                         as GameObject;
        }
    }

    void ToThreeWay()
    {
        mBaiscStyle = BasicBulletStyle.threeway;
    }

    void AdaptChaser()
    {
        mIsChase = true;
    }

    void PositionFix()
    {
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, mInfos.Bound.xMin, mInfos.Bound.xMax),
            Mathf.Clamp(transform.position.y, mInfos.Bound.yMin, mInfos.Bound.yMax),
            0
        );
    }

    void PlayerHit()
    {
        mPlayerAni.SetTrigger("PlayerHit");
        mHealth--;
        mMyheart.fillAmount = mHealth / mInfos.Health;
        mInfos.Invincible = true;
        mBoxcollider.enabled = false;
        StartCoroutine(ColliderDisable());
        StartCoroutine(CharaBlink());
    }

    void BasicShot()
    {
        mNextFire = Time.time + mInfos.BasicBullet[mPowerIndex].FireRate;
        switch (mBaiscStyle)
        {
            case BasicBulletStyle.oneway:
                CreateBasicBullet(Quaternion.Euler(0, 0, 0));
                break;
            case BasicBulletStyle.threeway:
                CreateBasicBullet(Quaternion.Euler(0, 0, 0));
                CreateBasicBullet(Quaternion.Euler(0, 0, 5));
                CreateBasicBullet(Quaternion.Euler(0, 0, -5));
                break;
        }
    }

    void LineLaserEnd()
    {
        mLineLaser = Instantiate(mInfos.LineLaser.Bullet, mInfos.SpawnTransf[0].position, mInfos.SpawnTransf[0].rotation)
                     as GameObject;
    }

    void LineLaserStart()
    {
        Destroy(mLineLaser);
    }


    void PlayerChaseShot()
    {
        mNextChase = Time.time + mInfos.ChaseBullet.FireRate;
        CreateChaseBullet(1);
        CreateChaseBullet(2);
    }

    void PlayerLaserShot()
    {
        //mNextLaser = Time.time + mLaserRate;
        //Instantiate(mLaser[1], mInfo.SpawnTransf[0].position, mLaser[0].transform.rotation);
    }
    void PlayerLaserShotHead()
    {
        //mIsShootHead = true;
        //Instantiate(mLaser[2], mInfo.SpawnTransf[0].position, mLaser[0].transform.rotation);
    }
    void PlayerLaserShotTail()
    {
        //mIsShootHead = false;
        //Instantiate(mLaser[0], mInfo.SpawnTransf[0].position, mLaser[0].transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            PlayerHit();
        }
    }

    void CreateBasicBullet(Quaternion rot)
    {
        GameObject bullet = ObjectPool.mCurrent.GetPoolBasicBullet1();
        if (bullet == null) return;
        bullet.transform.position = mInfos.SpawnTransf[0].position;
        bullet.transform.rotation = rot;
        BulletControl script = bullet.GetComponent<BulletControl>();
        script.SetBulletSpr(mPowerIndex);
        script.SetBulletDamage((mPowerIndex + 1));
        bullet.SetActive(true);
        
    }

    void CreateChaseBullet(int transnum)
    {
        GameObject bullet = ObjectPool.mCurrent.GetPoolChaseBullet();
        if (bullet == null) return;
        bullet.transform.position = mInfos.SpawnTransf[transnum].position;
        bullet.transform.rotation = mInfos.SpawnTransf[transnum].rotation;
        bullet.SetActive(true);
    }

    

    void InputProgress()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * mInfos.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * mInfos.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * mInfos.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * mInfos.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if (Time.time > mNextChase && mIsChase)
            {
                PlayerChaseShot();
            }

            if (Time.time > mNextFire && mCurrentBullet == BulletStyle.Basic)
            {
                BasicShot();
            }
            
            if (Time.time > mNextLaser && mIsShootHead)
            {
                //PlayerLaserShot();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //PlayerLaserShotHead();
            mIsFirePush = true;
            if (mCurrentBullet == BulletStyle.Laser)
            {
                LineLaserEnd();
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            //PlayerLaserShotTail();
            mIsFirePush = false;
            if (mCurrentBullet == BulletStyle.Laser)
            {
                LineLaserStart();
            }
        }
    }

    IEnumerator ColliderDisable()
    {
        yield return new WaitForSeconds(2);
        mBoxcollider.enabled = true;
        mInfos.Invincible = false;
    }

    IEnumerator CharaBlink()
    {
        while (mInfos.Invincible)
        {
            mRender.enabled = false;
            yield return new WaitForSeconds(0.05f);
            mRender.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        mRender.enabled = true;
    }

    public float GetHealth()
    {
        return mHealth;
    }
}
