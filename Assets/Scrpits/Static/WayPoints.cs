using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] points;

    //움직일 수 있는 위치를 정해주는 코드.
    //WayPoints의 자식 위치를 가져와서 해당 자식마다 위치 저장.
    private void Awake()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
