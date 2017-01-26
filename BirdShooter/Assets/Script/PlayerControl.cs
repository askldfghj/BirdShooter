using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {

    public Boundary mBoundary;
    GameObject mLineLaser;
    Animator mPlayerAni;
    float mNextFire;
    float mNextChase;
    float mNextLaser;
    int mPowerIndex;
    bool mIsFirePush;

    public PlayerObjStruct mInfos;

    enum BulletStyle { Basic, Laser }
    enum BasicBulletStyle { oneway, threeway }
    BulletStyle mCurrentBullet;
    BasicBulletStyle mBaiscStyle;

    bool mIsChase;


    // Use this for initialization

    bool mIsShootHead;

    void Awake()
    {
        mIsFirePush = false;
        mNextFire = 0;
        mNextLaser = 0;
        mPowerIndex = 0;
        mCurrentBullet = BulletStyle.Basic;
        mBaiscStyle = BasicBulletStyle.oneway;
        mIsShootHead = false;
        mIsChase = false;
        mPlayerAni = GetComponent<Animator>();
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
            Mathf.Clamp(transform.position.x, mBoundary.xMin, mBoundary.xMax),
            Mathf.Clamp(transform.position.y, mBoundary.yMin, mBoundary.yMax),
            0
        );
    }

    void PlayerHit()
    {
        mPlayerAni.SetTrigger("PlayerHit");
    }

    void BasicShot()
    {
        mNextFire = Time.time + mInfos.BasicBullet[mPowerIndex].FireRate;
        switch (mBaiscStyle)
        {
            case BasicBulletStyle.oneway:
                GameObject bullet = ObjectPool.mCurrent.GetPoolBasicBullet1();
                if (bullet == null) return;
                bullet.transform.position = mInfos.SpawnTransf[0].position;
                bullet.transform.rotation = mInfos.SpawnTransf[0].rotation;
                bullet.SetActive(true);
                break;
            case BasicBulletStyle.threeway:
                GameObject bullet1 = ObjectPool.mCurrent.GetPoolBasicBullet1();
                if (bullet1 == null) return;
                bullet1.transform.position = mInfos.SpawnTransf[0].position;
                bullet1.transform.rotation = mInfos.SpawnTransf[0].rotation;
                bullet1.SetActive(true);
                GameObject bullet2 = ObjectPool.mCurrent.GetPoolBasicBullet1();
                if (bullet2 == null) return;
                bullet2.transform.position = mInfos.SpawnTransf[0].position;
                bullet2.transform.rotation = Quaternion.Euler(0, 0, 5);
                bullet2.SetActive(true);
                GameObject bullet3 = ObjectPool.mCurrent.GetPoolBasicBullet1();
                if (bullet3 == null) return;
                bullet3.transform.position = mInfos.SpawnTransf[0].position;
                bullet3.transform.rotation = Quaternion.Euler(0, 0, -5);
                bullet3.SetActive(true);
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
        GameObject bullet1 = ObjectPool.mCurrent.GetPoolChaseBullet();
        if (bullet1 == null) return;
        bullet1.transform.position = mInfos.SpawnTransf[1].position;
        bullet1.transform.rotation = mInfos.SpawnTransf[1].rotation;
        bullet1.SetActive(true);

        GameObject bullet2 = ObjectPool.mCurrent.GetPoolChaseBullet();
        if (bullet2 == null) return;
        bullet2.transform.position = mInfos.SpawnTransf[2].position;
        bullet2.transform.rotation = mInfos.SpawnTransf[2].rotation;
        bullet2.SetActive(true);
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
}
