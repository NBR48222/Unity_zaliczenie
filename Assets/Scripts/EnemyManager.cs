using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject kamikazeEnemyPrefab; // Prefab drona kamikaze
    public GameObject shootingEnemyPrefab; // Prefab strzelającego drona
    public Transform spawnPoint; // Punkt, w którym przeciwnicy będą się pojawiać
    public float spawnDelay = 5f; // Opóźnienie między pojawieniem się przeciwników
    public float waveDelay = 5f; // Opóźnienie między falami przeciwników
    public int initialWaveSize = 5; // Początkowy rozmiar fali przeciwników
    public float kamikazeChance = 0.75f; // Szansa na pojawienie się drona kamikaze w pierwszej fali
    public float shootingChance = 0.25f; // Szansa na pojawienie się strzelającego drona w pierwszej fali
    public float kamikazeChanceIncrement = -0.025f; // Zmiana szansy na drona kamikaze w kolejnych falach
    public float shootingChanceIncrement = 0.025f; // Zmiana szansy na strzelającego drona w kolejnych falach
    public float minKamikazeChance = 0.5f; // Minimalna szansa na drona kamikaze
    public float maxShootingChance = 0.5f; // Maksymalna szansa na strzelającego drona

    private int currentWaveSize; // Obecny rozmiar fali przeciwników
    private float currentKamikazeChance; // Obecna szansa na drona kamikaze
    private float currentShootingChance; // Obecna szansa na strzelającego drona
    private int enemiesRemaining; // Liczba pozostałych przeciwników

    private void Start()
    {
        currentWaveSize = initialWaveSize;
        currentKamikazeChance = kamikazeChance;
        currentShootingChance = shootingChance;
        enemiesRemaining = 0;

        Invoke("SpawnWave", spawnDelay);
    }

    private void SpawnWave()
    {
        for (int i = 0; i < currentWaveSize; i++)
        {
            GameObject enemyPrefab = GetRandomEnemyPrefab();
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            enemiesRemaining++;
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        float randomValue = Random.value;
        if (randomValue < currentKamikazeChance)
        {
            return kamikazeEnemyPrefab;
        }
        else
        {
            return shootingEnemyPrefab;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float xPosition = Random.Range(-5f, 5f); // Zakres losowego położenia X
        float yPosition = spawnPoint.position.y;
        float zPosition = spawnPoint.position.z;
        return new Vector3(xPosition, yPosition, zPosition);
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            Invoke("SpawnWave", waveDelay);
            UpdateWaveParameters();
        }
    }

    private void UpdateWaveParameters()
    {
        currentWaveSize += 2; // Zwiększ rozmiar fali o 2
        currentKamikazeChance += kamikazeChanceIncrement; // Zmień szansę na drona kamikaze
        currentShootingChance += shootingChanceIncrement; // Zmień szansę na strzelającego drona

        // Ogranicz minimalną szansę na drona kamikaze i maksymalną szansę na strzelającego drona
        currentKamikazeChance = Mathf.Clamp(currentKamikazeChance, minKamikazeChance, 1f);
        currentShootingChance = Mathf.Clamp(currentShootingChance, 0f, maxShootingChance);
    }
}

