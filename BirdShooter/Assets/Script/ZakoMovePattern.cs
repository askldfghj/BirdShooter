using UnityEngine;
using System.Collections.Generic;

public class ZakoMovePattern : MonoBehaviour {

    List<Vector3> path;

    EnemyObjStruct mInfos;

    EnemyControl mEnemyCon;

    enum MovePattern { Left = 0, RunAWay = 1, ZigZag = 2 };

    MovePattern mPattern;

    float mInitSpeed;

    bool mIsCreate;
    bool mIsUp;
    //ZigZag Node

    void Awake()
    {
        mIsUp = false;
        path = new List<Vector3>();
        mEnemyCon = GetComponent<EnemyControl>();
        mInfos = mEnemyCon.GetInfos();
        mInitSpeed = mInfos.Speed;
        mIsCreate = false;
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
                case MovePattern.ZigZag:
                    Pattern2();
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
        path.Add(new Vector3(-1f, gameObject.transform.position.y, 0));
        PatternStart(3f);
    }

    void Pattern1()
    {
        //Run A Way
        path.Add(gameObject.transform.position);
        if (mIsUp)
        {
            
            path.Add(new Vector3(gameObject.transform.position.x - 4f, transform.position.y - 1.125f, 0));
            path.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2.25f, 0));
        }
        else
        {
            path.Add(new Vector3(gameObject.transform.position.x - 4f, transform.position.y + 1.125f, 0));
            path.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.25f, 0));
        }
        PatternStart(2.5f);
    }

    void Pattern2()
    {
        //ZigZag
        path.Add(gameObject.transform.position);
        path.Add(new Vector3(gameObject.transform.position.x - 4f, gameObject.transform.position.y, 0));
        PatternStart(1f, "ZigZag1");
    }

    void ZigZag1()
    {
        path.Clear();
        path.Add(gameObject.transform.position);
        if (mIsUp)
        {
            path.Add(new Vector3(gameObject.transform.position.x + 2f, gameObject.transform.position.y - 1f, 0));
        }
        else
        {
            path.Add(new Vector3(gameObject.transform.position.x + 2f, gameObject.transform.position.y + 1f, 0));
        }
        PatternStart(1f, "ZigZag2");
    }

    void ZigZag2()
    {
        path.Clear();
        path.Add(gameObject.transform.position);
        path.Add(new Vector3(-1f, gameObject.transform.position.y, 0));
        PatternStart(1.5f);
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

    void PatternStop()
    {
        mEnemyCon.InActive();
    }

    public void SetPattern(int index)
    {
        mPattern = (MovePattern)index;
    }
}
