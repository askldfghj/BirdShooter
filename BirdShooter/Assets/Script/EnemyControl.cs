using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {

    BoxCollider2D mBox2d;
    Animator mEnemyAni;
    
    float mHealth;
    public GameObject mItem;

    public EnemyObjStruct mInfos;

    
    void Awake()
    {
        mHealth = mInfos.Health;
        mEnemyAni = GetComponent<Animator>();
        mBox2d = GetComponent<BoxCollider2D>();
    }
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckPosi();
    }

    void OnEnable()
    {
        mInfos.Health = mHealth;
    }

    void FixedUpdate()
    {
        if (mInfos.Health <= 0)
        {
            //EnemyDown();
            InActive();
        }
    }

    public void InActive()
    {
        gameObject.SetActive(false);
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
        
    void CreateItem()
    {
        if (Random.Range(0f, 1f) > 0.7f)
        {
            Instantiate(mItem, transform.position, transform.rotation);
        }
    }

    void Damaged(float dam)
    {
        mInfos.Health -= dam;
    }

    public EnemyObjStruct GetInfos()
    {
        return mInfos; 
    }

}
