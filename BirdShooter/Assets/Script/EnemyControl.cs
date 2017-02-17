using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {

    BoxCollider2D mBox2d;
    Animator mEnemyAni;
    float mNextFire;

    int mAngle;
    float mHealth;

    bool mIsAngleUp;
    bool goleft;
    public GameObject mItem;


    float Speed;

    public EnemyObjStruct mInfos;
    void Awake()
    {
        mHealth = mInfos.Health;
        mIsAngleUp = true;
        mAngle = 0;
        mNextFire = 0;
        Speed = 5;
        mEnemyAni = GetComponent<Animator>();
        mBox2d = GetComponent<BoxCollider2D>();

        goleft = true;
    }
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        //EnemyMove(); //공통아님
        //if (goleft)
        //{
        //    Speed -= 0.05f;
        //}
        //else
        //{
        //    Speed += 0.05f;
        //}
        //if (Speed < 0)
        //{
        //    goleft = false;
        //}
        CheckPosi();
    }

    void OnEnable()
    {
        goleft = true;
        mInfos.Health = mHealth;
        Speed = 5;
    }

    void FixedUpdate()
    {
        if (mInfos.Health <= 0)
        {
            //EnemyDown();
            InActive();
        }
    }

    public void InActive()
    {
        gameObject.SetActive(false);
    }

    void CheckPosi()
    {
        if (transform.position.x < mInfos.Bound.xMin ||
            transform.position.x > mInfos.Bound.xMax ||
            transform.position.y < mInfos.Bound.yMin ||
            transform.position.y > mInfos.Bound.yMax)
        {
            InActive();
        }
    }

    void ChangeAngle()
    {
        if (mIsAngleUp)
        {
            mAngle += 7;
        }
        else
        {
            mAngle -= 7;
        }
    }
        
    void CreateItem()
    {
        if (Random.Range(0f, 1f) > 0.7f)
        {
            Instantiate(mItem, transform.position, transform.rotation);
        }
    }

    void ShootBullet()
    {
        if (Time.time > mNextFire)
        {
            ChangeAngle();
            if (mAngle >= 60)
            {
                mIsAngleUp = false;
            }
            else if (mAngle <= -60)
            {
                mIsAngleUp = true;
            }
            mNextFire = Time.time + mInfos.BulletInfo.FireRate;
            GameObject bullet = ObjectPool.mCurrent.GetPoolEnemyBullet();
            if (bullet == null) return;
            bullet.transform.position = mInfos.SpawnTransf[0].position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, mAngle);
            bullet.SetActive(true);
        }
    }

    void Damaged(float dam)
    {
        mInfos.Health -= dam;
    }

    public EnemyObjStruct GetInfos()
    {
        return mInfos; 
    }

}
