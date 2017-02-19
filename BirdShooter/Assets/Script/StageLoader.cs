using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageLoader : MonoBehaviour {

    public float mSpawnRate;
    public float mGroupSpawnRate;
    private float mNextGroup;
    
    public GameObject mEnemy;
    public Transform mEnemySpawn;
    

    void Awake()
    {
        mNextGroup = 0;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnZakoGroup();
	}

    //지정된 Rate 간격으로 Zako그룹을 스폰한다.
    void SpawnZakoGroup()
    {
        if (Time.time > mNextGroup)
        {
            StartCoroutine(ZakoGroup(5, 0.3f));
            mNextGroup = Time.time + mGroupSpawnRate;
        }
    }

    //코루틴
    //many만큼의 Zako를 sec 간격으로 스폰
    IEnumerator ZakoGroup(int many, float sec)
    {
        int count = 0;
        Vector3 posi = new Vector3(10, Random.Range(0.12f, 4.33f), 0);
        int pattern = Random.Range(0, 3);
        while (count < many)
        {
            CreateZako(posi, pattern);
            count++;
            yield return new WaitForSeconds(sec);
        }
    }

    //지정된 위치에 이동패턴을 지정해 Zako를 pool에서 active
    void CreateZako(Vector3 posi, int pattern)
    {
        GameObject Zako = ObjectPool.mCurrent.GetPoolZako();
        if (Zako == null) return;
        Zako.transform.position = posi;
        Zako.transform.rotation = transform.rotation;
        ZakoMovePattern zm = Zako.GetComponent<ZakoMovePattern>();
        zm.SetPattern(pattern);
        Zako.SetActive(true);
    }
}
