using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerControl : MonoBehaviour {

    public GameObject mBullet;
    public Transform mSpawn;

    public Boundary mBoundary;
    Animator mPlayerAni;
    
    public float mSpeed;

    public float mFireRate;
    private float mNextFire;

    private List<GameObject> mBulletList; 

    // Use this for initialization

    void Awake ()
    {
        mPlayerAni = GetComponent<Animator>();
        mBulletList = new List<GameObject>();
    }
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        InputProgress();
        PositionFix();
        CheckBullet();
	}

    void FixedUpdate()
    {
        
    }

    void PositionFix()
    {
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, mBoundary.xMin, mBoundary.xMax),
            Mathf.Clamp(transform.position.y, mBoundary.yMin, mBoundary.yMax),
            0
        );
    }

    void PlayerHit()
    {
        mPlayerAni.SetTrigger("PlayerHit");
    }

    void PlayerShot()
    {
        mNextFire = Time.time + mFireRate;
        GameObject bullet = Instantiate(mBullet, mSpawn.position, mSpawn.rotation) as GameObject;
        mBulletList.Add(bullet);
    }

    void CheckBullet()
    {
        for (int i = mBulletList.Count - 1; i >= 0; i--)
        {
            if (mBulletList[i].transform.position.x > 10)
            {
                Destroy(mBulletList[i]);
                mBulletList.RemoveAt(i);
            }
        }
    }

    void InputProgress()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * mSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * mSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * mSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * mSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Z) && Time.time > mNextFire)
        {
            PlayerShot();
        }
    }
}
