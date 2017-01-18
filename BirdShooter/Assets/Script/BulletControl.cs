using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

    public float mSpeed;

    public Sprite[] mBulletSprite;
    public Sprite[] mBulletEpSprite;

    
    float mDamage;
    int mPowerIndex;

    void Awake()
    {
        mPowerIndex = 0;
        mDamage = 1;
    }

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = mBulletSprite[mPowerIndex];
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.right * mSpeed * Time.deltaTime);
        CheckPosi();
    }

    public void SetBulletInfo(int index, float damage)
    {
        mPowerIndex = index;
        mDamage = damage;
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
            gameObject.GetComponent<SpriteRenderer>().sprite = mBulletEpSprite[mPowerIndex];
            Destroy(gameObject, 0.1f);
            Destroy(this);
        }
    }
}
