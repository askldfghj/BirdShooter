using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {

    public Boundary mBoundary;
    Animator mPlayerAni;
    float mNextFire;
    float mNextChase;
    float mNextLaser;
    int mPowerIndex;

    public PlayerObjStruct mInfos;

    // Use this for initialization

    bool mIsShootHead;

    void Awake()
    {
        mNextFire = 0;
        mNextLaser = 0;
        mPowerIndex = 0;
        mIsShootHead = false;
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

    void PlayerShot()
    {
        mNextFire = Time.time + mInfos.BasicBullet[mPowerIndex].FireRate;
        GameObject bullet = Instantiate(mInfos.BasicBullet[mPowerIndex].Bullet, 
                                        mInfos.SpawnTransf[0].position, mInfos.SpawnTransf[0].rotation)
                            as GameObject;
    }

    void TestKeyDown()
    {
        //mLineLaser = Instantiate(mInfo.BulletObj, mInfo.SpawnTransf[0].position, mInfo.SpawnTransf[0].rotation)
        //                    as GameObject;
    }

    void TestKeyUp()
    {
        //Destroy(mLineLaser);
    }


    void PlayerChaseShot()
    {
        mNextChase = Time.time + mInfos.ChaseBullet.FireRate;
        Instantiate(mInfos.ChaseBullet.Bullet, mInfos.SpawnTransf[1].position, mInfos.SpawnTransf[1].rotation);
        Instantiate(mInfos.ChaseBullet.Bullet, mInfos.SpawnTransf[2].position, mInfos.SpawnTransf[2].rotation);
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
            if (Time.time > mNextFire)
            {
                PlayerShot();
            }
            if (Time.time > mNextChase)
            {
                PlayerChaseShot();
            }
            if (Time.time > mNextLaser && mIsShootHead)
            {
                //PlayerLaserShot();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //PlayerLaserShotHead();
            //TestKeyDown(); 
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            //PlayerLaserShotTail();
            //TestKeyUp();
        }
    }
}
