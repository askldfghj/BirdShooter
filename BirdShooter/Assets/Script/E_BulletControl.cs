using UnityEngine;
using System.Collections;

public class E_BulletControl : MonoBehaviour {

    public float mSpeed;

    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.left * mSpeed * Time.deltaTime);
    }
}
