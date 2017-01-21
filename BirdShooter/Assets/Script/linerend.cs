using UnityEngine;
using System.Collections;

public class linerend : MonoBehaviour {

    private LineRenderer lineRenderer;
    Vector3 start;
    Vector3 end;
    Vector3 distance;
    RaycastHit2D hit;
    GameObject mPlayer;
    int mLayerMask;
    
    // Use this for initialization

    void Awake()
    {
        mLayerMask = 1 << 8;
        mPlayer = GameObject.Find("BulletSpawn");
    }

    void Start()
    {
        //라인렌더러 설정
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetColors(Color.red, Color.yellow);
        lineRenderer.SetWidth(0.01f, 0.01f);

        //라인렌더러 처음위치 나중위치
        start = mPlayer.transform.position;
        distance = new Vector2(10, 0);
        end = transform.position + distance;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 6f);
        //hit = Physics2D.BoxCast(start, new Vector3(3,3), 0f, transform.right, Vector2.Distance(start,end));
        hit = Physics2D.Raycast(start, Vector2.right, 10f, mLayerMask);
        distance = new Vector2(10, 0);
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Enemy")
            {
                distance = new Vector2(hit.distance, 0);
                hit.collider.SendMessage("Damaged", 0.1f);
            }
        }
        start = mPlayer.transform.position;
        end = start + distance;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        
    }
}
