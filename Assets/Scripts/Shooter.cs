using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject target;
    public Rigidbody2D bulletPrefab;
    public Transform firePoint;
    public PlayerStats playerStats;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);

                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider != null)
                {
                    target.transform.position = new Vector2(hit.point.x, hit.point.y);

                    Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);

                    Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                    firedBullet.velocity = projectileVelocity;

                    Bullet bulletScript = firedBullet.GetComponent<Bullet>();
                    if (bulletScript != null)
                    {
                        bulletScript.SetDamage(playerStats.damage);
                    }

                    nextFireTime = Time.time + (1f / playerStats.fireRate);
                }
            }
        }
    }
    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityX = distance.x / time;

        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        return new Vector2(velocityX, velocityY);
    }
}
