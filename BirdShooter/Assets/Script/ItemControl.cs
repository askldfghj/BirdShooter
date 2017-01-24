using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour {

   

    void Awake()
    {

    }
	void Start ()
    {
	
	}
	
	void Update ()
    {
        transform.Translate(Vector3.left * 2 * Time.deltaTime);
        CheckPosi();
    }

    void CheckPosi()
    {
        if (transform.position.x < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.SendMessage("PowerUp");
            Destroy(gameObject);
        }
    }
}
