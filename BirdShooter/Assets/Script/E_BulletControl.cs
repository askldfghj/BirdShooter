using UnityEngine;
using System.Collections;

public class E_BulletControl : MonoBehaviour
{

    public float mSpeed;
    public float mRotateSpeed;
    public float mTracking;

    public Sprite mSpr;

    Animator mAni;
    Vector3 mDirection;
    float mTrackTime;
    Transform mPlayerTr;

    void Awake()
    {
        mTrackTime = Time.time;
        mAni = GetComponent<Animator>();
        mPlayerTr = GameObject.Find("Player").transform;
    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * mSpeed * Time.deltaTime);
        CheckPosi();
        //if (Time.time - mTrackTime >= mTracking)
        //{
        //    ChasePlayer();
            
        //}

    }

    void CheckPosi()
    {
        if (transform.position.x < 0)
        {
            Destroy(gameObject);
        }
    }

    void ChasePlayerIdle()
    {
        mDirection = mPlayerTr.transform.position - transform.position;
        mDirection = mDirection.normalized;
    }

    void ChasePlayer()
    {
        Vector2 dir = mPlayerTr.position - transform.position; // 유도탄과 타겟 사이의 벡터값 구하기
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180; // 2D 각도값 구하기
        Quaternion tarRot = Quaternion.AngleAxis(angle, Vector3.forward); // 얻어진 2D 좌표계 각도를 Quaternion으로 변환한다.
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, mRotateSpeed * Time.deltaTime); // 목표 각도로 서서히 이동시킨다.
        if (transform.rotation == tarRot) mTrackTime = Time.time; // 목표각도까지 회전했으면 타이머를 리셋한다.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            mAni.Stop();
            gameObject.GetComponent<SpriteRenderer>().sprite = mSpr;
            Destroy(gameObject, 0.1f);
            Destroy(this);
        }
    }
}
