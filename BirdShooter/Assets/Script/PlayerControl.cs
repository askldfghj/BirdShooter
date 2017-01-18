using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerControl : MonoBehaviour {

    public Boundary mBoundary;
    public ObjectBasicInfo mInfo;
    Animator mPlayerAni;
    float mNextFire;
    float mNextChase;
    float mNextLaser;
    float mDamage;
    int mPowerIndex;

    public float mChaseBulletRate;
    public float mLaserRate;

    public GameObject mChaseBullet;
    public GameObject[] mLaser;

    // Use this for initialization

    bool mIsShootHead;

    void Awake()
    {
        mNextFire = 0;
        mNextLaser = 0;
        mPowerIndex = 0;
        mIsShootHead = false;
        mDamage = 1;
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

        //if (Physics2D.Raycast(transform.position, transform.right, out hit))
        //{
        //    Debug.Log(hit.transform.tag);
        //    switch (hit.transform.tag)
        //    {
        //        case "Enemy":
        //            //Output message
        //            Debug.Log("Enemy detected");
        //            break;
        //    }
        //}
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
        mDamage = mPowerIndex + 1;
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
        mNextFire = Time.time + mInfo.FireRate;
        GameObject bullet = Instantiate(mInfo.BulletObj, mInfo.SpawnTransf[0].position, mInfo.SpawnTransf[0].rotation)
                            as GameObject;
        BulletControl bc = bullet.GetComponent<BulletControl>();
        bc.SetBulletInfo(mPowerIndex, mDamage);


    }

    void PlayerChaseShot()
    {
        mNextChase = Time.time + mChaseBulletRate;
        Instantiate(mChaseBullet, mInfo.SpawnTransf[1].position, mInfo.SpawnTransf[1].rotation);
        Instantiate(mChaseBullet, mInfo.SpawnTransf[2].position, mInfo.SpawnTransf[2].rotation);
    }

    void PlayerLaserShot()
    {
        mNextLaser = Time.time + mLaserRate;
        Instantiate(mLaser[1], mInfo.SpawnTransf[0].position, mLaser[0].transform.rotation);
    }
    void PlayerLaserShotHead()
    {
        mIsShootHead = true;
        Instantiate(mLaser[2], mInfo.SpawnTransf[0].position, mLaser[0].transform.rotation);
    }
    void PlayerLaserShotTail()
    {
        mIsShootHead = false;
        Instantiate(mLaser[0], mInfo.SpawnTransf[0].position, mLaser[0].transform.rotation);
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
            transform.Translate(Vector3.left * mInfo.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * mInfo.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * mInfo.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * mInfo.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if (Time.time > mNextFire)
            {
                //PlayerShot();
            }
            if (Time.time > mNextChase)
            {
                PlayerChaseShot();
            }
            if (Time.time > mNextLaser && mIsShootHead)
            {
                PlayerLaserShot();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerLaserShotHead();    
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            PlayerLaserShotTail();
        }
    }
}
