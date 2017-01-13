using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

    public float mSpeed;
    public Sprite mSpr;

    bool mIsHit;

    void Awake()
    {
        mIsHit = false;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!mIsHit)
        {
            transform.Translate(Vector3.right * mSpeed * Time.deltaTime);
            CheckPosi();
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
            mIsHit = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = mSpr;
            Destroy(gameObject, 0.1f);
        }
    }
}
