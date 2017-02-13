using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageLoader : MonoBehaviour {

    public float mSpawnRate;
    private float mNextSpawn;
    
    public GameObject mEnemy;
    public Transform mEnemySpawn;
    

    void Awake()
    {
        mNextSpawn = 0;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnEnemy();
	}

    void SpawnEnemy()
    {
        if (Time.time > mNextSpawn)
        {
            CreatZako();
            mNextSpawn = Time.time + mSpawnRate;
        }
    }

    void CreatZako()
    {
        GameObject Zako = ObjectPool.mCurrent.GetPoolZako();
        if (Zako == null) return;
        Zako.transform.position = new Vector3(10, Random.Range(0.12f, 4.33f), 0);
        Zako.transform.rotation = transform.rotation;
        Zako.SetActive(true);
    }
}
