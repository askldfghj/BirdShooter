using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {

    BoxCollider2D mBox2d;
    Animator mEnemyAni;
    float mNextFire;

    public GameObject mItem;
    

    public EnemyObjStruct mInfos;
    void Awake()
    {
        mNextFire = 0;
        mEnemyAni = GetComponent<Animator>();
        mBox2d = GetComponent<BoxCollider2D>();
    }
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        EnemyMove(); //공통아님
        ShootBullet(); //공통아님
        CheckPosi();
    }

    void FixedUpdate()
    {
        if (mInfos.Health <= 0)
        {
            EnemyDown();
            mBox2d.enabled = false;
            Destroy(gameObject, 0.125f);
            Destroy(this);
        }
    }

    void CheckPosi()
    {
        if (transform.position.x < mInfos.Bound.xMin ||
            transform.position.x > mInfos.Bound.xMax ||
            transform.position.y < mInfos.Bound.yMin ||
            transform.position.y > mInfos.Bound.yMax)
        {
            Destroy(gameObject);
        }
    }
    

    void EnemyMove()
    {
        transform.Translate(Vector3.left * mInfos.Speed * Time.deltaTime);
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
            mNextFire = Time.time + mInfos.BulletInfo.FireRate;
            Instantiate(mInfos.BulletInfo.Bullet, mInfos.SpawnTransf[0].position, mInfos.SpawnTransf[0].rotation);
        }
    }

    void Damaged(float dam)
    {
        mInfos.Health -= dam;
    }


    void EnemyDown()
    {
        CreateItem();
        mEnemyAni.SetTrigger("EnemyDestroy");
    }
}
