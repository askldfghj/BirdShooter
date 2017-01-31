﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool mCurrent;

    public GameObject mEnemyBulletObj;
    public GameObject mBasicBulletObj;
    public GameObject mChaseBulletObj;
    public GameObject mBasicBulletEpObj;
    public GameObject mZakoEnemyObj;

    public GameObject mPool;
    public GameObject mEnemySpawnParent;

    public int mEBulletAmount = 20;
    public int mPBasic1Amount = 10;
    public int mPChaseAmount = 10;
    public int mPBasicEpAmount = 10;
    public int mZakoEnemyAmount = 10;

    List<GameObject> mEBullets;
    List<GameObject> mPBasics_1;
    List<GameObject> mPChases;
    List<GameObject> mPBasicEps;
    List<GameObject> mZakos;

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

        for (int i = 0; i < mEBulletAmount; i++)
        {
            GameObject enemybullet = (GameObject)Instantiate(mEnemyBulletObj);

            enemybullet.transform.parent = mPool.transform; //[자식] obj_A -> [부모] Play_Obj 밑으로 생성하기

            //PoolObj -> obj에 저장
            enemybullet.SetActive(false);
            mEBullets.Add(enemybullet);
            // Instantiate로 그려지고 비활성화된 상태의 오브젝트를 PoolObjs에 차곡차곡 넣는다.
        }
        for (int i = 0; i < mPBasic1Amount; i++)
        {
            GameObject basicbullet = (GameObject)Instantiate(mBasicBulletObj);

            basicbullet.transform.parent = mPool.transform; //[자식] obj_B -> [부모] Play_Obj 밑으로 생성하기

            //GomPoolObj -> obj에 저장
            basicbullet.SetActive(false);
            mPBasics_1.Add(basicbullet);
            // Instantiate로 그려지고 비활성화된 상태의 오브젝트를 PoolObjs에 차곡차곡 넣는다.
        }
        for (int i = 0; i < mPChaseAmount; i++)
        {
            GameObject chasebullet = (GameObject)Instantiate(mChaseBulletObj);

            chasebullet.transform.parent = mPool.transform; //[자식] obj_B -> [부모] Play_Obj 밑으로 생성하기

            //GomPoolObj -> obj에 저장
            chasebullet.SetActive(false);
            mPChases.Add(chasebullet);
            // Instantiate로 그려지고 비활성화된 상태의 오브젝트를 PoolObjs에 차곡차곡 넣는다.
        }
        for (int i = 0; i < mPBasicEpAmount; i++)
        {
            GameObject basicep = (GameObject)Instantiate(mBasicBulletEpObj);

            basicep.transform.parent = mPool.transform; //[자식] obj_B -> [부모] Play_Obj 밑으로 생성하기

            //GomPoolObj -> obj에 저장
            basicep.SetActive(false);
            mPBasicEps.Add(basicep);
            // Instantiate로 그려지고 비활성화된 상태의 오브젝트를 PoolObjs에 차곡차곡 넣는다.
        }
        for (int i = 0; i < mZakoEnemyAmount; i++)
        {
            GameObject zako = (GameObject)Instantiate(mZakoEnemyObj);

            zako.transform.parent = mEnemySpawnParent.transform; //[자식] obj_B -> [부모] Play_Obj 밑으로 생성하기

            //GomPoolObj -> obj에 저장
            zako.SetActive(false);
            mZakos.Add(zako);
            // Instantiate로 그려지고 비활성화된 상태의 오브젝트를 PoolObjs에 차곡차곡 넣는다.
        }
    }



    public GameObject GetPoolEnemyBullet()
    {
        for (int i = 0; i < mEBullets.Count; i++)
        {

            //obj.SetActive 가 false면 실행 
            if (!mEBullets[i].activeInHierarchy)
            {
                //GM의 CUBE_A()에서 넘어온 obj.SetActive(true)된 Cube_A 호출
                return mEBullets[i];
            }
        }
        return null;
        // PoolObjs에 prefab이 남아있지 않으면 null을 넘겨준다.
    }

    public GameObject GetPoolBasicBullet1()
    {
        for (int i = 0; i < mPBasics_1.Count; i++)
        {

            //obj.SetActive 가 false면 실행 
            if (!mPBasics_1[i].activeInHierarchy)
            {
                //GM의 CUBE_A()에서 넘어온 obj.SetActive(true)된 Cube_A 호출
                return mPBasics_1[i];
            }
        }
        return null;
        // PoolObjs에 prefab이 남아있지 않으면 null을 넘겨준다.
    }

    public GameObject GetPoolChaseBullet()
    {
        for (int i = 0; i < mPChases.Count; i++)
        {
            //obj.SetActive 가 false면 실행 
            if (!mPChases[i].activeInHierarchy)
            {
                //GM의 CUBE_A()에서 넘어온 obj.SetActive(true)된 Cube_A 호출
                return mPChases[i];
            }
        }
        return null;
        // PoolObjs에 prefab이 남아있지 않으면 null을 넘겨준다.
    }

    public GameObject GetPoolBasicBulletEp()
    {
        for (int i = 0; i < mPBasicEps.Count; i++)
        {
            //obj.SetActive 가 false면 실행 
            if (!mPBasicEps[i].activeInHierarchy)
            {
                //GM의 CUBE_A()에서 넘어온 obj.SetActive(true)된 Cube_A 호출
                return mPBasicEps[i];
            }
        }
        return null;
        // PoolObjs에 prefab이 남아있지 않으면 null을 넘겨준다.
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
}
