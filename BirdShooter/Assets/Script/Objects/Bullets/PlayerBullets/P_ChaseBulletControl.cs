using UnityEngine;
using System.Collections;

public class P_ChaseBulletControl : MonoBehaviour {
    

    public Sprite mSpr;

    public ChaseBulletObjStruct mInfos;

    Animator mAni;
    float mTrackTime;
    bool mIsTargeting;
    bool mIsEjectd;
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
        mTarget = null;
        mTrackTime = Time.time;
        mIsTargeting = false;
        mIsEjectd = false;
    }

    //여기 문제있음
    // Update is called once per frame
    void Update () {
        //BulletSpeedTrigger();
        //ChaseFunction();
        CheckTarget();
        SearchTarget();
        MoveBullet();
        CheckPosi();
    }

    void MoveBullet()
    {
        JustRight();
        if (mIsTargeting)
        {
            Chasing();
        }
    }


    void SearchTarget()
    {
        if (!mIsTargeting)
        {
            float dist = Vector2.Distance(transform.position, new Vector2(100, 0));
            float shortdist = dist;
            for (int i = 0; i < mTargetParent.childCount; i++)
            {
                if (mTargetParent.GetChild(i).gameObject.activeSelf)
                {
                    dist = Vector2.Distance(transform.position, mTargetParent.GetChild(i).position);

                    if (dist < shortdist)
                    {
                        shortdist = dist;
                        mTarget = mTargetParent.GetChild(i);
                        mIsTargeting = true;
                    }
                }
            }
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

    void CheckTarget()
    {
        if (mTarget == null || !mTarget.gameObject.activeSelf)
        {
            mIsTargeting = false;
        }
    }

    void JustRight()
    {
        transform.Translate(Vector2.right * mInfos.Speed * Time.deltaTime);
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

    void Chasing()
    {
        if (Time.time - mTrackTime >= mInfos.Tracking) // 트래킹할 시간이 됐는지 체크한다.
        {
            Vector2 dir = mTarget.position - transform.position; // 유도탄과 타겟 사이의 벡터값 구하기
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 2D 각도값 구하기
            Quaternion tarRot = Quaternion.AngleAxis(angle, Vector3.forward); // 얻어진 2D 좌표계 각도를 Quaternion으로 변환한다.
            transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, mInfos.RotateSpeed * Time.deltaTime); // 목표 각도로 서서히 이동시킨다.

            if (transform.rotation == tarRot) mTrackTime = Time.time; // 목표각도까지 회전했으면 타이머를 리셋한다.
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("Damaged", mInfos.Damage);
            mAni.Stop();
            CreateExplosion();
            InActive();
        }
    }

    void CreateExplosion()
    {
        GameObject ep = ObjectPool.mCurrent.GetPoolBasicBulletEp();
        if (ep == null) return;
        ep.transform.position = transform.position;
        ep.transform.rotation = transform.rotation;
        BulletEpControl script = ep.GetComponent<BulletEpControl>();
        script.SetEpSprite(0);
        ep.SetActive(true);
    }

    void InActive()
    {
        gameObject.SetActive(false);
    }
}
