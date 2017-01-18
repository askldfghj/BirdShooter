using UnityEngine;
using System.Collections;

public class linerend : MonoBehaviour {

    private LineRenderer lineRenderer;
    Vector2 start;
    Vector2 end;
    Vector3 distance;
    RaycastHit2D hit;
    // Use this for initialization
    void Start()
    {
        //라인렌더러 설정
        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.SetColors(Color.red, Color.yellow);
        lineRenderer.SetWidth(2f, 2f);

        //라인렌더러 처음위치 나중위치
        start = transform.position;
        distance = new Vector2(1, 0);
        end = transform.position + distance;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.right * Time.deltaTime * 6f);
        hit = Physics2D.BoxCast(start, new Vector3(3,3), 0f, transform.right, Vector2.Distance(start,end));
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Enemy")
            {
                distance = new Vector2(Vector2.Distance(start, hit.transform.position), 0);
            }
        }
        start = transform.position;
        end = transform.position + distance;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        if (Vector2.Distance(start, end) < 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
