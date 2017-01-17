using UnityEngine;
using System.Collections;

public class P_ChaseBulletControl : MonoBehaviour {

    public float mSpeed;
    public float mRotateSpeed;
    public float mTracking;

    public Sprite mSpr;

    Animator mAni;
    float mTrackTime;
    int mDamage;
    bool mIsNoEnemy;
    Transform mTargetParent;
    Transform mTarget;

    // Use this for initialization

    void Awake()
    {
        mIsNoEnemy = true;
        mTrackTime = Time.time;
        mAni = GetComponent<Animator>();
        mDamage = 1;
        mTargetParent = GameObject.Find("EnemySpawnParent").transform;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * mSpeed * Time.deltaTime);
        if (mTarget == null)
        {
            mIsNoEnemy = SearchTarget();
        }
        if (Time.time - mTrackTime >= mTracking && !mIsNoEnemy) // 트래킹할 시간이 됐는지 체크한다.
        {
            Vector2 dir = mTarget.position - transform.position; // 유도탄과 타겟 사이의 벡터값 구하기
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 2D 각도값 구하기
            Quaternion tarRot = Quaternion.AngleAxis(angle, Vector3.forward); // 얻어진 2D 좌표계 각도를 Quaternion으로 변환한다.
            transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, mRotateSpeed * Time.deltaTime); // 목표 각도로 서서히 이동시킨다.

            if (transform.rotation == tarRot) mTrackTime = Time.time; // 목표각도까지 회전했으면 타이머를 리셋한다.
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
        if (transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("Damaged", mDamage);
            mAni.Stop();
            gameObject.GetComponent<SpriteRenderer>().sprite = mSpr;
            Destroy(gameObject, 0.1f);
            Destroy(this);
        }
    }
}
