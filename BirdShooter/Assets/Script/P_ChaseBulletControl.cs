using UnityEngine;
using System.Collections;

public class P_ChaseBulletControl : MonoBehaviour {
    

    public Sprite mSpr;

    public ChaseBulletObjStruct mInfos;

    Animator mAni;
    float mTrackTime;
    bool mIsNoEnemy;
    bool mIsEjectd;
    bool mIsHit;
    Transform mTargetParent;
    Transform mTarget;

    // Use this for initialization

    void Awake()
    {
        mAni = GetComponent<Animator>();
        mTargetParent = GameObject.Find("EnemySpawnParent").transform;
    }
    void Start ()
    {
        
    }

    void OnEnable()
    {
        mTrackTime = Time.time;
        mIsNoEnemy = SearchTarget();
        mIsEjectd = false;
        mIsHit = false;
    }

    // Update is called once per frame
    void Update () {
        if (!mIsHit)
        {
            BulletSpeedTrigger();
            ChaseFunction();
        }
        CheckPosi();
    }

    bool SearchTarget()
    {
        if (mTargetParent.childCount <= 0)
        {
            return true;
        }
        else
        {
            mTarget = mTargetParent.GetChild(Random.Range(0, mTargetParent.childCount));
            return false;
        }
    }

    void CheckPosi()
    {
        if (transform.position.x < mInfos.Bound.xMin ||
            transform.position.x > mInfos.Bound.xMax ||
            transform.position.y < mInfos.Bound.yMin ||
            transform.position.y > mInfos.Bound.yMax)
        {
            InActive();
        }
    }

    void BulletSpeedTrigger()
    {
        if (!mIsEjectd)
        {
            transform.Translate(Vector2.right * mInfos.EjectSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * mInfos.Speed * Time.deltaTime);
        }
    }

    void ChaseFunction()
    {
        if (mTarget == null)
        {
            mIsNoEnemy = SearchTarget();
            
        }
        if (Time.time - mTrackTime >= mInfos.Tracking && !mIsNoEnemy) // 트래킹할 시간이 됐는지 체크한다.
        {
            mIsEjectd = true;
            Vector2 dir = mTarget.position - transform.position; // 유도탄과 타겟 사이의 벡터값 구하기
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 2D 각도값 구하기
            Quaternion tarRot = Quaternion.AngleAxis(angle, Vector3.forward); // 얻어진 2D 좌표계 각도를 Quaternion으로 변환한다.
            transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, mInfos.RotateSpeed * Time.deltaTime); // 목표 각도로 서서히 이동시킨다.

            if (transform.rotation == tarRot) mTrackTime = Time.time; // 목표각도까지 회전했으면 타이머를 리셋한다.
        }
        if (mTarget == null && Time.time - mTrackTime > 1.5)
        {
            InActive();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("Damaged", mInfos.Damage);
            mAni.Stop();
            InActive();
            mIsHit = true;
        }
    }

    void InActive()
    {
        gameObject.SetActive(false);
    }
}
