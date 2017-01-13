using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

    public float mSpeed;

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
        transform.Translate(Vector3.right * mSpeed * Time.deltaTime);
    }
}
