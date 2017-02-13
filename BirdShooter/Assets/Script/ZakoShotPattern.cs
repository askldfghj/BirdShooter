using UnityEngine;
using System.Collections;

public class ZakoShotPattern : MonoBehaviour
{
    float mNextFire;
    int mAngle;


    EnemyControl mEnemyCon;
    EnemyObjStruct mInfos;

    // Use this for initialization
    void Awake()
    {
        mEnemyCon = GetComponent<EnemyControl>();
        mNextFire = 0;
        mAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    void OnEnable()
    {
        mInfos = mEnemyCon.GetInfos();
    }

    void ShootBullet()
    {
        if (Time.time > mNextFire)
        {
            mNextFire = Time.time + mInfos.BulletInfo.FireRate;
            GameObject bullet = ObjectPool.mCurrent.GetPoolEnemyBullet();
            if (bullet == null) return;
            bullet.transform.position = mInfos.SpawnTransf[0].position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, mAngle);
            bullet.SetActive(true);
        }
    }
}
