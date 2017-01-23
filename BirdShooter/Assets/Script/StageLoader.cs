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
            mNextSpawn = Time.time + mSpawnRate;
            GameObject enemy = Instantiate(mEnemy, new Vector3(11, Random.Range(0.12f, 4.33f), 0), transform.rotation) 
                               as GameObject;
            enemy.transform.parent = mEnemySpawn;
        }
    }
}
