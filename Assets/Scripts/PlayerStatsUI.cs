using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI hpText;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        damageText.text = "Damage: " + playerStats.damage;
        fireRateText.text = "Fire Rate: " + playerStats.fireRate;
        hpText.text = "HP: " + playerStats.currentHP + "/" + playerStats.maxHP;
    }
}
