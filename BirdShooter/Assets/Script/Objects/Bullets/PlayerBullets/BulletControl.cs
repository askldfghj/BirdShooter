using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

    public BulletObjStruct mInfos;

    public Sprite[] mPowerSpr;

    bool mIsHit;
    int mIndex;
    void Awake()
    {
        
    }

	// Use this for initialization
	void Start ()
    {
        
    }

    public void SetBulletSpr(int index)
    {
        mIndex = index;
        gameObject.GetComponent<SpriteRenderer>().sprite = mPowerSpr[index];
    }

    public void SetBulletDamage(float dam)
    {
        mInfos.Damage = dam;
    }

    void OnEnable()
    {
        mIsHit = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if(!mIsHit) transform.Translate(Vector3.right * mInfos.Speed * Time.deltaTime);
        CheckPosi();
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
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("Damaged", mInfos.Damage);
            mIsHit = true;
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
        script.SetEpSprite(mIndex);
        ep.SetActive(true);
    }

    void InActive()
    {
        gameObject.SetActive(false);
    }
}
