using UnityEngine;
using System.Collections;

public class BulletEpControl : MonoBehaviour {

    public Sprite[] mEpSpr;

    float mTime;

    void Awake()
    {
        
    }


    void Update()
    {
        if (Time.time - mTime > 0.1f)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetEpSprite(int index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = mEpSpr[index];
    }

        void OnEnable()
    {
        mTime = Time.time;
    }
}
