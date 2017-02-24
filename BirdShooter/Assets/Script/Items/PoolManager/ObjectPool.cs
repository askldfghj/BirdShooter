    using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool mCurrent; //pool 정보

    public GameObject mEnemyBulletObj;
    public GameObject mBasicBulletObj;
    public GameObject mChaseBulletObj;
    public GameObject mBasicBulletEpObj;
    public GameObject mZakoEnemyObj;
    public GameObject mNamedEnemyObj;

    public GameObject mPool;
    public GameObject mEnemySpawnParent;

    public int mEBulletAmount = 20;
    public int mPBasic1Amount = 10;
    public int mPChaseAmount = 10;
    public int mPBasicEpAmount = 10;
    public int mZakoEnemyAmount = 10;
    public int mNamedEnemyAmount = 5;

    List<GameObject> mEBullets;
    List<GameObject> mPBasics_1;
    List<GameObject> mPChases;
    List<GameObject> mPBasicEps;
    List<GameObject> mZakos;
    List<GameObject> mNameds;

    void Awake()
    {
        //static으로 선언한 NewObjPooling current에 접근
        mCurrent = this;
    }

    void Start()
    {
        mEBullets = new List<GameObject>();
        mPBasics_1 = new List<GameObject>();
        mPChases = new List<GameObject>();
        mPBasicEps = new List<GameObject>();
        mZakos = new List<GameObject>();
        mNameds = new List<GameObject>();

        StartCoroutine(CreatePool());
    }

    //코루틴
    //풀 만들기
    IEnumerator CreatePool()
    {
        for (int i = 0; i < mEBulletAmount; i++)
        {
            GameObject enemybullet = (GameObject)Instantiate(mEnemyBulletObj);

            enemybullet.transform.parent = mPool.transform; //Pool오브젝트를 부모로 삼도록 한다.

            enemybullet.SetActive(false);
            mEBullets.Add(enemybullet);
            //비활성후 리스트 추가
            yield return null;
        }

        for (int i = 0; i < mPBasic1Amount; i++)
        {
            GameObject basicbullet = (GameObject)Instantiate(mBasicBulletObj);

            basicbullet.transform.parent = mPool.transform; 

            
            basicbullet.SetActive(false);
            mPBasics_1.Add(basicbullet);
            
            yield return null;
        }

        for (int i = 0; i < mPChaseAmount; i++)
        {
            GameObject chasebullet = (GameObject)Instantiate(mChaseBulletObj);

            chasebullet.transform.parent = mPool.transform; 

            
            chasebullet.SetActive(false);
            mPChases.Add(chasebullet);
        }
        yield return null;

        for (int i = 0; i < mZakoEnemyAmount; i++)
        {
            GameObject zako = (GameObject)Instantiate(mZakoEnemyObj);

            zako.transform.parent = mEnemySpawnParent.transform; 

            
            zako.SetActive(false);
            mZakos.Add(zako);
            yield return null;
        }

        for (int i = 0; i < mNamedEnemyAmount; i++)
        {
            GameObject named = (GameObject)Instantiate(mNamedEnemyObj);

            named.transform.parent = mEnemySpawnParent.transform;


            named.SetActive(false);
            mNameds.Add(named);
            yield return null;
        }

        for (int i = 0; i < mPBasicEpAmount; i++)
        {
            GameObject basicep = (GameObject)Instantiate(mBasicBulletEpObj);

            basicep.transform.parent = mPool.transform; 

            
            basicep.SetActive(false);
            mPBasicEps.Add(basicep);
            yield return null;
        }
    }

    public GameObject GetPoolEnemyBullet()
    {
        for (int i = 0; i < mEBullets.Count; i++)
        {

            //obj.SetActive 가 false면 실행 
            if (!mEBullets[i].activeInHierarchy)
            {
                //false되어있던 obj 리턴
                return mEBullets[i];
            }
        }
        return null;
        // 전부 true일경우 null 리턴
    }

    public GameObject GetPoolBasicBullet1()
    {
        for (int i = 0; i < mPBasics_1.Count; i++)
        {
            if (!mPBasics_1[i].activeInHierarchy)
            {
                return mPBasics_1[i];
            }
        }
        return null;
    }

    public GameObject GetPoolChaseBullet()
    {
        for (int i = 0; i < mPChases.Count; i++)
        {
            if (!mPChases[i].activeInHierarchy)
            {
                return mPChases[i];
            }
        }
        return null;
    }

    public GameObject GetPoolBasicBulletEp()
    {
        for (int i = 0; i < mPBasicEps.Count; i++)
        {
            if (!mPBasicEps[i].activeInHierarchy)
            {
                return mPBasicEps[i];
            }
        }
        return null;
    }

    public GameObject GetPoolZako()
    {
        for (int i = 0; i < mZakos.Count; i++)
        {
            if (!mZakos[i].activeInHierarchy)
            {
                return mZakos[i];
            }
        }
        return null;
    }

    public GameObject GetPoolNamed()
    {
        for (int i = 0; i < mNameds.Count; i++)
        {
            if (!mNameds[i].activeInHierarchy)
            {
                return mNameds[i];
            }
        }
        return null;
    }
}
