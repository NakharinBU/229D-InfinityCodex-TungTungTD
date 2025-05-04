using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int damage = 10;
    public float fireRate = 1f;
    public int maxHP = 100;
    public int currentHP;
    public PlayAgain playerDied;
    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Destroy(gameObject);
            Die();
        }
    }

    void Die()
    {
        if (playerDied != null)
        {
            playerDied.ShowDied();
        }
    }

}
