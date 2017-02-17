using UnityEngine;
using System.Collections.Generic;

public class dfsd : MonoBehaviour {

    List<Vector3> path;

    // Use this for initialization
    void Awake()
    {
        path = new List<Vector3>();
    }

    void OnEnable()
    {
        path.Clear();
        Pattern1();

        Start();
    }

    void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", path.ToArray(), "easetype", iTween.EaseType.linear,
                                              "time", 3f, "oncomplete", "PatternStop"));
    }

    void Pattern1()
    {
        if (gameObject.transform.position.y > 2.25)
        {
            path.Add(gameObject.transform.position);
            path.Add(new Vector3(5.0f, transform.position.y - 1.125f, 0));
            path.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2.25f, 0));
        }
        else
        {
            path.Add(gameObject.transform.position);
            path.Add(new Vector3(5.0f, transform.position.y + 1.125f, 0));
            path.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.25f, 0));
        }
    }

    void PatternStop()
    {
        EnemyControl e = GetComponent<EnemyControl>();
        e.InActive();
    }

    // Update is called once per frame
    void Update () {
        
	}
}
