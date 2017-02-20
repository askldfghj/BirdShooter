using UnityEngine;
using System.Collections.Generic;

public class NamedMovePattern : MonoBehaviour
{

    List<Vector3> path;

    EnemyObjStruct mInfos;

    EnemyControl mEnemyCon;
    NamedShotPattern mShotcon;
    enum MovePattern { Left = 0, RunAWay = 1 };

    MovePattern mPattern;

    float mInitSpeed;

    bool mIsCreate;
    bool mIsUp;

    Vector3 mVec;

    void Awake()
    {
        mIsUp = false;
        path = new List<Vector3>();
        mEnemyCon = GetComponent<EnemyControl>();
        mShotcon = GetComponent<NamedShotPattern>();
        mInfos = mEnemyCon.GetInfos();
        mInitSpeed = mInfos.Speed;
        mIsCreate = true;
        mVec = Vector3.zero;
    }
    
    void OnEnable()
    {
        if (mIsCreate)
        {
            if (transform.position.y > 2.25)
            {
                mIsUp = true;
            }
            else
            {
                mIsUp = false;
            }
            path.Clear();
            switch (mPattern)
            {
                case MovePattern.Left:
                    Pattern0();
                    break;
                case MovePattern.RunAWay:
                    Pattern1();
                    break;
            }
        }
        else
        {
            mIsCreate = true;
        }
    }

    void Start()
    {

    }

    void Pattern0()
    {
        //GoLeft
        path.Add(gameObject.transform.position);
        path.Add(new Vector3(gameObject.transform.position.x -2f, gameObject.transform.position.y, 0));
        PatternDelayedStart(1f, "StartShot", 10f, "GoLeft");
    }

    void GoLeft()
    {
        mShotcon.StopPattern();
        path.Clear();
        path.Add(gameObject.transform.position);
        path.Add(new Vector3(-1f, gameObject.transform.position.y, 0));
        PatternStart(3f);
    }

    void Pattern1()
    {
        //GoRight
        path.Add(gameObject.transform.position);
        path.Add(new Vector3(gameObject.transform.position.x - 2f, gameObject.transform.position.y, 0));
        PatternDelayedStart(1f, "StartShot", 10f, "GoRight");
    }

    void GoRight()
    {
        mShotcon.StopPattern();
        path.Clear();
        path.Add(gameObject.transform.position);
        path.Add(new Vector3(11f, gameObject.transform.position.y, 0));
        PatternStart(3f);
    }



    void PatternStart(float speed)
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", path.ToArray(), "easetype", iTween.EaseType.linear,
                                              "time", speed, "oncomplete", "PatternStop"));
    }

    void PatternStart(float speed, string method)
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", path.ToArray(), "easetype", iTween.EaseType.linear,
                                              "time", speed, "oncomplete", method));
    }

    void PatternDelayedStart(float speed, string delay_method, float delay)
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", path.ToArray(), "easetype", iTween.EaseType.linear,
                                              "time", speed));
        Invoke(delay_method, delay);
    }

    void PatternDelayedStart(float speed, string method, float delay, string delay_method)
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", path.ToArray(), "easetype", iTween.EaseType.linear,
                                              "time", speed, "oncomplete", method));
        Invoke(delay_method, delay);
    }

    void PatternStop()
    {
        mEnemyCon.InActive();
    }

    public void SetPattern(int index)
    {
        mPattern = (MovePattern)index;
    }

    Vector3 InVector(float x, float y, float z)
    {
        mVec.x = x;
        mVec.y = y;
        mVec.z = z;
        return mVec;
    }

    void StartShot()
    {
        mShotcon.StartPattern(0);
    }
}
