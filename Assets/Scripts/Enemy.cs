using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int currentHealth;

    public EnemyStats stats;

    private Transform target;

    void Start()
    {
        if (stats == null)
        {
            stats = GetComponent<EnemyStats>();
        }

        target = GameObject.FindWithTag("Player").transform;

        currentHealth = stats.maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            transform.Translate(dir * Time.deltaTime * stats.moveSpeed);
        }
    }

    void Die()
    {
        GameManager.Instance.AddMoney(10);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(stats.touchDamage);
            }

            Destroy(gameObject);
        }
    }
}
