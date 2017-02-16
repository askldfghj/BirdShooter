using UnityEngine;
using System.Collections;

public class dfsd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("path"), "easetype", iTween.EaseType.linear,
        //                                      "time", 3f));

        Vector3 target = new Vector3(3, 3, 0); // 목표점
        Vector3 targer1 = new Vector3(1, 1, 0);
        Hashtable hash = new Hashtable();
        hash.Add("position", target); // 이동 할 위치
        hash.Add("position", targer1);
        hash.Add("speed", 3.0f); // 이동 속도 (작을수록 느림)
        hash.Add("easetype", iTween.EaseType.linear); // 보간법
        iTween.MoveTo(gameObject, hash); //인자 넘겨주기
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
