using UnityEngine;
using System.Collections;

public class ChaseBulletItem : MonoBehaviour {

    public BasicObjStruct mInfos;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * mInfos.Speed * Time.deltaTime);
        CheckPosi();
    }

    void CheckPosi()
    {
        if (transform.position.x < mInfos.Bound.xMin ||
            transform.position.x > mInfos.Bound.xMax ||
            transform.position.y < mInfos.Bound.yMin ||
            transform.position.y > mInfos.Bound.yMax)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.SendMessage("AdaptChaser");
            Destroy(gameObject);
        }
    }
}
