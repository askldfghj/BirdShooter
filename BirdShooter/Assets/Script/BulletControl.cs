using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

    public BulletObjStruct mInfos;
    
    public Sprite mSpr;

    bool mIsHit;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start ()
    {
        
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
            //gameObject.GetComponent<SpriteRenderer>().sprite = mSpr;
            mIsHit = true;
            InActive();
            //Invoke("InActive", 0.1f);
        }
    }

    void InActive()
    {
        gameObject.SetActive(false);
    }
}
