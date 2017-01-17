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
    int mDamage;
    int mPowerIndex;

    public float mChaseBulletRate;

    public GameObject mChaseBullet;
    

    // Use this for initialization

    void Awake ()
    {
        mNextFire = 0;
        mPowerIndex = 0;
        mDamage = 1;
        mPlayerAni = GetComponent<Animator>();
    }
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
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
                PlayerShot();
            }
            if (Time.time > mNextChase)
            {
                PlayerChaseShot();
            }
        }
    }
}
