using UnityEngine;
using System.Collections.Generic;

public class ZakoMovePattern : MonoBehaviour {

    List<Vector3> path;

    EnemyObjStruct mInfos;

    EnemyControl mEnemyCon;

    enum MovePattern { Left = 0, RunAWay = 1 };

    MovePattern mPattern;

    float mInitSpeed;

    void Awake()
    {
        path = new List<Vector3>();
        mEnemyCon = GetComponent<EnemyControl>();
        mInfos = mEnemyCon.GetInfos();
        mInitSpeed = mInfos.Speed;
        mPattern = (MovePattern)1;
    }

    void OnEnable()
    {
        mInfos.Speed = mInitSpeed;
        path.Clear();
        Pattern1();

        Start();
    }


    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", path.ToArray(), "easetype", iTween.EaseType.linear,
                                              "time", 3f, "oncomplete", "PatternStop"));
    }

    void Pattern1()
    {
        if (gameObject.transform.position.y > 2.25)
        {
            path.Add(gameObject.transform.position);
            path.Add(new Vector3(gameObject.transform.position.x-4f, transform.position.y - 1.125f, 0));
            path.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2.25f, 0));
        }
        else
        {
            path.Add(gameObject.transform.position);
            path.Add(new Vector3(gameObject.transform.position.x - 4f, transform.position.y + 1.125f, 0));
            path.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.25f, 0));
        }
    }

    void PatternStop()
    {
        mEnemyCon.InActive();
    }
}
