using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {


    public GameObject mBullet;
    public Transform mSpawn;


    public float mSpeed;

    public float mFireRate;
    private float mNextFire;

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
        EnemyMove();
        ShootBullet();
        CheckPosi();
    }


    void CheckPosi()
    {
        if (transform.position.x < 0)
        {
            Destroy(gameObject);
        }
    }

    void EnemyMove()
    {
        transform.Translate(Vector3.left * mSpeed * Time.deltaTime);
    }

    void ShootBullet()
    {
        if (Time.time > mNextFire)
        {
            mNextFire = Time.time + mFireRate;
            Instantiate(mBullet, mSpawn.position, mSpawn.rotation);
        }
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            //Destroy(this);
        }
    }
}
