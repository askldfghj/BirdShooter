using UnityEngine;
using System.Collections;

public class E_BulletControl : MonoBehaviour {

    public float mSpeed;

    public Sprite mSpr;

    Animator mAni;

    bool mIsHit;

    void Awake()
    {
        mIsHit = false;
        mAni = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update ()
    {
        if (!mIsHit)
        {
            transform.Translate(Vector3.left * mSpeed * Time.deltaTime);
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
