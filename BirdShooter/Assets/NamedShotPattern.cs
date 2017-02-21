using UnityEngine;
using System.Collections;

public class NamedShotPattern : MonoBehaviour
{

    bool mIsAngleUp;

    float mAngle;

    EnemyObjStruct mInfos;
    EnemyControl mEnemyCon;

    IEnumerator mCurrentRoutine;

    enum ShotPattern { Scatter = 0 };
    ShotPattern mPattern;

    void Awake()
    {
        mAngle = 0f;
        mIsAngleUp = true;
        mEnemyCon = GetComponent<EnemyControl>();
        mInfos = mEnemyCon.GetInfos();
    }

    void OnEnable()
    {
        mAngle = 0f;
        mIsAngleUp = true;
    }

    IEnumerator Pattern0(float rate)
    {
        //흩뿌리기 패턴
        while (true)
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
            GameObject bullet = ObjectPool.mCurrent.GetPoolEnemyBullet();
            if (bullet != null)
            {
                bullet.transform.position = mInfos.SpawnTransf[0].position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, mAngle);
                bullet.SetActive(true);
            }

            yield return new WaitForSeconds(rate);
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

    
    // Update is called once per frame
    void Update()
    {

    }

    public void StartPattern(int index)
    {
        mPattern = (ShotPattern)index;
        switch (mPattern)
        {
            case ShotPattern.Scatter:
                mCurrentRoutine = Pattern0(mInfos.BulletInfo.FireRate);
                StartCoroutine(mCurrentRoutine);
                break;
        }
    }
    public void StopPattern()
    {
        StopCoroutine(mCurrentRoutine);
    }
}
