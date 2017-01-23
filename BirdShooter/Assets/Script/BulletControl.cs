using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

    public BulletObjStruct mInfos;

    public Sprite mSpr;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.right * mInfos.Speed * Time.deltaTime);
        CheckPosi();
    }

    void CheckPosi()
    {
        if (transform.position.x < mInfos.Bound.xMin ||
            transform.position.x > mInfos.Bound.xMax ||
            transform.position.y < mInfos.Bound.yMin ||
            transform.position.y > mInfos.Bound.yMax)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("Damaged", mInfos.Damage);
            gameObject.GetComponent<SpriteRenderer>().sprite = mSpr;
            Destroy(gameObject, 0.1f);
            Destroy(this);
        }
    }
}
