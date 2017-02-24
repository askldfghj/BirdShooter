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

    void Pattern0()
    {
            GameObject bullet = ObjectPool.mCurrent.GetPoolEnemyBullet();
            if (bullet != null)
            {
                bullet.transform.position = mInfos.SpawnTransf[0].position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
    }

    void Pattern1()
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

    public void SetPattern(int shotpattern)
    {
        mPattern = (ShotPattern)shotpattern;
    }
    public void StartPattern()
    {
        switch (mPattern)
        {
            case ShotPattern.Left:
                Pattern0();
                break;
            case ShotPattern.Chase:
                Pattern1();
                break;
        }
    }

    public void StopPattern()
    {
        StopCoroutine(mCurrentRoutine);
    }
}
