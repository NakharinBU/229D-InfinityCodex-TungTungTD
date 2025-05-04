using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;

    public Button damageButton;
    public Button fireRateButton;
    public Button hpButton;
    public Button skipButton;

    public PlayerStats playerStats;
    public GameManager gameManager;

    public int upgradeCost = 500;

    void Start()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager is not assigned in UpgradeManager!");
            return;
        }
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in UpgradeManager!");
            return;
        }

        upgradePanel.SetActive(false);
        skipButton.gameObject.SetActive(false);

        damageButton.onClick.AddListener(() => Upgrade("damage"));
        fireRateButton.onClick.AddListener(() => Upgrade("fireRate"));
        hpButton.onClick.AddListener(() => Upgrade("hp"));
        skipButton.onClick.AddListener(CloseUpgradePanel);
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        Time.timeScale = 0f;

        if (gameManager.money < upgradeCost)
        {
            skipButton.gameObject.SetActive(true);
        }
        else
        {
            skipButton.gameObject.SetActive(false);
        }
    }

    void Upgrade(string type)
    {
        if (gameManager.money < upgradeCost)
        {
            Debug.Log("❌ เงินไม่พออัปเกรด");
            return;
        }

        gameManager.money -= upgradeCost;
        gameManager.UpdateMoneyUI();

        switch (type)
        {
            case "damage":
                playerStats.damage += 20;
                break;
            case "fireRate":
                playerStats.fireRate += 0.25f;
                break;
            case "hp":
                playerStats.maxHP += 20;
                playerStats.currentHP = playerStats.maxHP;
                break;
        }

        CloseUpgradePanel();
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
