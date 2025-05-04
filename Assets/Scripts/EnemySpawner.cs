using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Wave[] waves;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI countdownText; 

    public float countdownTime = 5f;

    void Start()
    {
        UpdateWaveText();
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        float timeLeft = countdownTime;
        while (timeLeft > 0)
        {
            countdownText.text = "Next Wave in: " + Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        countdownText.text = "";

        if (currentWaveIndex < waves.Length)
        {
            int rewardPerWave = 50;
            GameManager.Instance.AddMoney(rewardPerWave);

            Wave currentWave = waves[currentWaveIndex];
            Debug.Log("Starting Wave: " + currentWave.name);

            for (int i = 0; i < currentWave.count; i++)
            {
                SpawnEnemy(currentWave.enemyPrefab);
                yield return new WaitForSeconds(1f / currentWave.spawnRate);
            }

            // 👉 รอจนกว่าศัตรูหมด
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            currentWaveIndex++;
            UpdateWaveText();

            if (currentWaveIndex < waves.Length)
            {
                StartCoroutine(StartNextWave());
            }
            else
            {
                waveText.text = "All Waves Complete!";
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    void UpdateWaveText()
    {
        waveText.text = "Wave: " + (currentWaveIndex + 1).ToString();
    }
}
