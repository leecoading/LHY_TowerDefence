using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header ("Turret Rotate Option")]
    private Transform turretTarget;
    private Enemy enemyTarget;

    [SerializeField] Transform rotatePart;
    [SerializeField] float turretRange = 15f;
    [SerializeField] float turretTurnSpeed = 10f;
    public string enemyTag = "Enemy";

    [Header ("Turret Fire Option")]
    [SerializeField] float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Laser Fire Option")]
    public bool useLaser = false;
    public int damageOverTime = 100;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject shootEffect;

    private void Start()
    {
        //0.5초마다 반복해서 UpdateTarget메서드 호출.
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    private void Update()
    {
        if (turretTarget == null)
        {
            if(useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }
            }
            return;
        }

        LockOnTarget();

        if(useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    private void LockOnTarget()
    {
        //transform.Lookat을 쓰지 않고 y축에 관한 연산만 하여 성능 유리.
        Vector3 dir = turretTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotatePart.rotation, lookRotation, Time.deltaTime * turretTurnSpeed).eulerAngles;

        //터렛 회전이 y축을 기준으로 회전하게 설정.
        rotatePart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Laser()
    {
        enemyTarget.TakeDamage(damageOverTime * Time.deltaTime);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, turretTarget.position);

        Vector3 dir = firePoint.position - turretTarget.position;

        impactEffect.transform.position = turretTarget.position + dir.normalized * .5f;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        GameObject effect = Instantiate(shootEffect, firePoint.position, firePoint.rotation);
        Destroy(effect, 1f);

        if (bullet != null)
        {
            bullet.Seek(turretTarget);
        }
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
            enemyTarget = nearstEnemy.GetComponent<Enemy>();
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
