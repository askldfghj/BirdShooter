using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageLoader : MonoBehaviour {
    
    public float mGroupSpawnRate;
    public float mNamedSpawnRate;
    private float mNextGroup;
    private float mNextNamed;
    
    public Transform mEnemySpawn;
    

    void Awake()
    {
        mNextGroup = 0;
        mNextNamed = 0;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnZakoGroup();
        //SpawnNamed();
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

    void SpawnNamed()
    {
        if (Time.time > mNextNamed)
        {
            CreateNamed(new Vector3(10, 2.25f, 0), Random.Range(0, 2));
            mNextNamed = Time.time + mNamedSpawnRate;
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


    //자코와 같은 방식으로 네임드 스폰
    void CreateNamed(Vector3 posi, int pattern)
    {
        GameObject Named = ObjectPool.mCurrent.GetPoolNamed();
        if (Named == null) return;
        Named.transform.position = posi;
        Named.transform.rotation = transform.rotation;
        NamedMovePattern nm = Named.GetComponent<NamedMovePattern>();
        nm.SetPattern(pattern);
        Named.SetActive(true);
    }
}
