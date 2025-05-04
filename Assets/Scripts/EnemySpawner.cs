using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public AudioClip[] waveStartSounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateWaveText();
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        if (currentWaveIndex < waveStartSounds.Length && waveStartSounds[currentWaveIndex] != null)
        {
            audioSource.PlayOneShot(waveStartSounds[currentWaveIndex]);
        }

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
                SceneManager.LoadScene("EndCredit");
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
