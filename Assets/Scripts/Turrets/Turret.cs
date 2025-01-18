using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretTarget;
    [SerializeField] Transform rotatePart;
    [SerializeField] float turretRange = 15f;
    [SerializeField] float turretTurnSpeed = 10f;
    public string enemyTag = "Enemy";

    private void Start()
    {
        //0.5초마다 반복해서 UpdateTarget메서드 호출.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (turretTarget == null)
            return;

        //transform.Lookat을 쓰지 않고 y축에 관한 연산만 하여 성능 유리.
        Vector3 dir = turretTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotatePart.rotation, lookRotation, Time.deltaTime * turretTurnSpeed).eulerAngles;

        //터렛 회전이 y축을 기준으로 회전하게 설정.
        rotatePart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    /// <summary>
    /// 타겟이 터렛 범위안에 들어왔을 때 타겟을 정하는 코드
    /// </summary>
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearstEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //터렛의 위치부터 적의 위치까지.
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearstEnemy = enemy;
            }
        }

        if(nearstEnemy != null && shortestDistance <= turretRange)
        {
            turretTarget = nearstEnemy.transform;
        }
        else
        {
            turretTarget = null;
        }
    }

    /// <summary>
    /// 터렛의 범위를 표시.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
