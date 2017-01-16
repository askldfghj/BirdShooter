using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {

    BoxCollider2D mBox2d;
    Animator mEnemyAni;
    float mNextFire;

    public GameObject mItem;

    public ObjectBasicInfo mInfo;
    

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
        if (mInfo.Health <= 0)
        {
            EnemyDown();
            mBox2d.enabled = false;
            Destroy(gameObject, 0.125f);
            Destroy(this);
        }
    }

    void CheckPosi()
    {
        if (transform.position.x < 0)
        {
            Destroy(gameObject);
        }
    }
    

    void EnemyMove()
    {
        transform.Translate(Vector3.left * mInfo.Speed * Time.deltaTime);
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
            mNextFire = Time.time + mInfo.FireRate;
            Instantiate(mInfo.BulletObj, mInfo.SpawnTransf.position, mInfo.SpawnTransf.rotation);
        }
    }

    void Damaged(int dam)
    {
        mInfo.Health -= dam;
    }


    void EnemyDown()
    {
        CreateItem();
        mEnemyAni.SetTrigger("EnemyDestroy");
    }
}
