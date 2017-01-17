using UnityEngine;
using System.Collections;

public class BGControl : MonoBehaviour {

    public float mSpeed;

    Material mMaterial;

    void Awake()
    {
        mMaterial = GetComponent<Renderer>().material;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 newOffset = mMaterial.mainTextureOffset;

        newOffset.Set(newOffset.x + (mSpeed * Time.deltaTime), 0);

        mMaterial.mainTextureOffset = newOffset;
	}
}
