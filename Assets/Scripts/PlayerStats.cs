using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int damage = 10;
    public float fireRate = 1f;
    public int maxHP = 100;
    public int currentHP;

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
            
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        //SceneManager.LoadScene("GameOverScene");
    }
}
