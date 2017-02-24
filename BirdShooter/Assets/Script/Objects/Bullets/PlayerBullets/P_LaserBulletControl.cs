using UnityEngine;
using System.Collections;

public class P_LaserBulletControl : MonoBehaviour {

    public float mSpeed;
    public Sprite mSpr;
    Transform mPlayer;
    float mDamage;

    void Awake()
    {
        mDamage = 0.3f;
        mPlayer = GameObject.Find("Player").transform;
    }
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * mSpeed * Time.deltaTime);
        Vector2 vec = transform.position;
        vec.y = mPlayer.position.y;
        transform.position = vec;
        CheckPosi();
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
            gameObject.GetComponent<SpriteRenderer>().sprite = mSpr;
            Destroy(gameObject, 0.1f);
            Destroy(this);
        }
    }
}
