using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    Animator mPlayerAni;
    int Speed = 3;
    // Use this for initialization

    void Awake ()
    {
        mPlayerAni = GetComponent<Animator>();
    }
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        InputProgress();
	}

    void InputProgress()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
        }
    }
}
