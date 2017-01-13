using UnityEngine;
using System.Collections;

public class EnemyDownAni : MonoBehaviour {
    Animator mEnemyAni;
    void Awake()
    {
        mEnemyAni = GetComponent<Animator>();
    }

    void EnemyDown()
    {
        mEnemyAni.SetTrigger("EnemyDestroy");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            //EnemyDown();
            //Destroy(gameObject, 0.125f);
        }
    }
}
