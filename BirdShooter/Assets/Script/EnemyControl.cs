using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {


    public GameObject mBullet;
    public Transform mSpawn;

    public float mSpeed;

    public float mFireRate;
    private float mNextFire;

    private List<GameObject> mBulletList;

    void Awake()
    {
        mBulletList = new List<GameObject>();
    }
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        EnemyMove();
        CheckBullet();
        ShootBullet();
	}

    void CheckBullet()
    {
        for (int i = mBulletList.Count - 1; i >= 0; i--)
        {
            if (mBulletList[i].transform.position.x < 0)
            {
                Destroy(mBulletList[i]);
                mBulletList.RemoveAt(i);
            }
        }
    }

    void EnemyMove()
    {
        transform.Translate(Vector3.left * mSpeed * Time.deltaTime);
    }

    void ShootBullet()
    {
        if (Time.time > mNextFire)
        {
            mNextFire = Time.time + mFireRate;
            GameObject bullet = Instantiate(mBullet, mSpawn.position, mSpawn.rotation) as GameObject;
            mBulletList.Add(bullet);
        }
    }
}
