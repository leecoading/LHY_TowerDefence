using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = WayPoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    /// <summary>
    /// 이동할 위치(WayPoints)에 Enemy를 이동시키는 코드.
    /// </summary>
    private void GetNextWayPoint()
    {
        if(wavepointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
        }

        wavepointIndex++;
        target = WayPoints.points[wavepointIndex];
    }
}
