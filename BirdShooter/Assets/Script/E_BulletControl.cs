using UnityEngine;
using System.Collections;

public class E_BulletControl : MonoBehaviour {

    public float mSpeed;

    public Sprite mSpr;

    Animator mAni;
    Vector3 mDirection;
    bool mIsHit;
    int mChaseCount;
    GameObject mPlayer;

    void Awake()
    {
        mChaseCount = 0;
        mIsHit = false;
        mAni = GetComponent<Animator>();
        mPlayer = GameObject.Find("Player");
    }

	// Use this for initialization
	void Start () {
        ChasePlayerIdle();

    }

	// Update is called once per frame
	void Update ()
    {
        if (!mIsHit)
        {
            //ChasePlayer();
            transform.Translate(mDirection * mSpeed * Time.deltaTime);
            CheckPosi();
        }
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
        mDirection = mPlayer.transform.position - transform.position;
        mDirection = mDirection.normalized;
    }

    void ChasePlayer()
    {
        Vector2 dir = transform.position - mPlayer.transform.position;
        dir = dir.normalized;
        Vector3 axis = Vector3.Cross(dir, transform.forward);
        Quaternion newrota = Quaternion.AngleAxis(Time.deltaTime * 45, axis) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, newrota, 50 * Time.deltaTime);
        Vector3 pos = Vector3.forward * Time.deltaTime * 3;
        transform.Translate(pos);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            mIsHit = true;
            mAni.Stop();
            gameObject.GetComponent<SpriteRenderer>().sprite = mSpr;
            Destroy(gameObject, 0.1f);
        }
    }
}
