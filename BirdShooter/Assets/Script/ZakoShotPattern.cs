using UnityEngine;
using System.Collections;

public class ZakoShotPattern : MonoBehaviour
{
    EnemyControl mEnemyCon;
    EnemyObjStruct mInfos;
    IEnumerator mCurrentRoutine;

    GameObject mPlayerObj;

    enum ShotPattern { Left = 0, Chase = 1 };
    ShotPattern mPattern;

    // Use this for initialization
    void Awake()
    {
        mPlayerObj = GameObject.FindGameObjectWithTag("Player");
        mEnemyCon = GetComponent<EnemyControl>();
        mInfos = mEnemyCon.GetInfos();
    }

    // Update is called once per frame

    void OnEnable()
    {

    }

    void Pattern0(float rate)
    {
            GameObject bullet = ObjectPool.mCurrent.GetPoolEnemyBullet();
            if (bullet != null)
            {
                bullet.transform.position = mInfos.SpawnTransf[0].position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
    }

    void Pattern1(float rate)
    {
            GameObject bullet = ObjectPool.mCurrent.GetPoolEnemyBullet();
            float angle;
            float rad;
            if (bullet != null)
            {
                rad = Mathf.Atan2(mPlayerObj.transform.position.y - mInfos.SpawnTransf[0].position.y,
                                  mPlayerObj.transform.position.x - mInfos.SpawnTransf[0].position.x);
                angle = (rad * 180) / Mathf.PI;
                bullet.transform.position = mInfos.SpawnTransf[0].position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
                bullet.SetActive(true);
            }
    }

    public void StartPattern(int index)
    {
        mPattern = (ShotPattern)index;
        switch (mPattern)
        {
            case ShotPattern.Left:
                Pattern0(mInfos.BulletInfo.FireRate);
                break;
            case ShotPattern.Chase:
                Pattern1(mInfos.BulletInfo.FireRate);
                break;
        }
    }

    public void StopPattern()
    {
        StopCoroutine(mCurrentRoutine);
    }
}
