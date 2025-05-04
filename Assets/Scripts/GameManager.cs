using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;
    public TextMeshProUGUI moneyText;
    public int passiveIncomePerSecond = 1;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InvokeRepeating(nameof(AddPassiveIncome), 1f, 1f);
        UpdateMoneyUI();
    }

    void AddPassiveIncome()
    {
        money += passiveIncomePerSecond;
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: " + money;
    }
}
