using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageLoader : MonoBehaviour {

    public float mSpawnRate;
    private float mNextSpawn;

    public GameObject mEnemy;

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
            Instantiate(mEnemy, new Vector3(10, 2.3f, 0), transform.rotation);
        }
    }
}
