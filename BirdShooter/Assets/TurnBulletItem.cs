using UnityEngine;
using System.Collections;

public class TurnBulletItem : MonoBehaviour
{
    public Sprite[] Items;
    public BasicObjStruct mInfos;
    enum BulletStyle { Basic, Laser }
    BulletStyle mCurrentBullet;
    // Use this for initialization
    float mChangeTime;

    void Awake()
    {
        mChangeTime = Time.time;
        mCurrentBullet = (BulletStyle)Random.Range(0, 2);
        gameObject.GetComponent<SpriteRenderer>().sprite = Items[(int)mCurrentBullet];
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * mInfos.Speed * Time.deltaTime);
        if (Time.time - mChangeTime > 2)
        {
            mChangeTime = Time.time;
            ChangeStatus();
        }
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

    void ChangeStatus()
    {
        if (mCurrentBullet == BulletStyle.Basic)
        {
            mCurrentBullet = BulletStyle.Laser;
        }
        else if (mCurrentBullet == BulletStyle.Laser)
        {
            mCurrentBullet = BulletStyle.Basic;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = Items[(int)mCurrentBullet];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (mCurrentBullet == BulletStyle.Basic)
            {
                other.SendMessage("TurnBasic");
            }
            else if (mCurrentBullet == BulletStyle.Laser)
            {
                other.SendMessage("TurnLaser");
            }
            Destroy(gameObject);
        }
    }
}
