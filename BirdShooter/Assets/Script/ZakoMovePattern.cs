using UnityEngine;
using System.Collections;

public class ZakoMovePattern : MonoBehaviour {

    EnemyObjStruct mInfos;

    EnemyControl mEnemyCon;

    enum MovePattern { Left = 0, RunAWay = 1 };

    MovePattern mPattern;

    bool mLeftFlag;
    float mInitSpeed;

    void Awake()
    {
        mEnemyCon = GetComponent<EnemyControl>();
        mInfos = mEnemyCon.GetInfos();
        mInitSpeed = mInfos.Speed;
        mPattern = (MovePattern)1;
        mLeftFlag = true;
    }

    void OnEnable()
    {
        mInfos.Speed = mInitSpeed;

        mLeftFlag = true;
    }

    void Update ()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        switch (mPattern)
        {
            case MovePattern.Left:
                MoveLeft();
                break;
            case MovePattern.RunAWay:
                MoveRunAWay();
                break;
        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * mInfos.Speed * Time.deltaTime);
    }

    void MoveRunAWay()
    {
        if (mLeftFlag)
        {
            transform.Translate(new Vector2(Mathf.Cos(Mathf.PI * 2 * 190 / 360), Mathf.Sin(Mathf.PI * 2 * 190 / 360))
                                * mInfos.Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(Mathf.Cos(Mathf.PI * 2 * -10 / 360), Mathf.Sin(Mathf.PI * 2 * -10 / 360))
                                * mInfos.Speed * Time.deltaTime);
        }

        if (mLeftFlag)
        {
            mInfos.Speed -= 0.05f;
        }
        else
        {
            mInfos.Speed += 0.05f;
        }
        if (mInfos.Speed < 0)
        {
            mLeftFlag = false;
        }
    }
}
