using UnityEngine;
using System.Collections;

public class E_BulletControl : MonoBehaviour
{
    public float mRotateSpeed;
    public float mTracking;

    public Sprite mSpr;

    public BulletObjStruct mInfos;

    Animator mAni;

    bool mIsHit;

    void Awake()
    {
        mAni = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {


    }

    void OnEnable()
    {
        mIsHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!mIsHit) transform.Translate(Vector2.left * mInfos.Speed * Time.deltaTime);
        CheckPosi();
        //if (Time.time - mTrackTime >= mTracking)
        //{
        //    ChasePlayer();
            
        //}

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
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
