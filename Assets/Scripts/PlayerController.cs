using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Renderer playerRenderer; // Referencja do komponentu Renderer gracza
    public GameObject[] damageAssets; // Tablica assetów z obrażeniami
    public int maxHealth = 4; // Maksymalna liczba żyć gracza

    private int currentHealth; // Obecna liczba żyć gracza
    private float flashDuration = 0.1f; // Czas trwania efektu migania
    private float flashTimer; // Timer dla efektu migania
    private bool isFlashing; // Czy efekt migania jest aktywny
    private GameManager gameManager; // Referencja do GameManager

    private void Start()
    {
        // Pobierz komponent Renderer z GameObjectu gracza
        playerRenderer = GetComponent<Renderer>();

        // Ustaw obecną liczbę żyć na maksymalną liczbę żyć
        currentHealth = maxHealth;

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        // Sprawdź, czy efekt migania jest aktywny
        if (isFlashing)
        {
            // Aktualizuj timer
            flashTimer += Time.deltaTime;

            // Sprawdź, czy czas trwania efektu migania minął
            if (flashTimer >= flashDuration)
            {
                // Wyłącz efekt migania i przywróć oryginalny kolor gracza
                isFlashing = false;
                playerRenderer.material.color = Color.white;
            }
        }
    }

    public void TakeDamage()
    {
        // Odtwórz efekt migania
        isFlashing = true;
        flashTimer = 0f;

        // Zmniejsz liczbę żyć gracza
        currentHealth--;

        // Sprawdź, czy liczba żyć jest większa lub równa zeru
        if (currentHealth > 0 && currentHealth != 4)
        {
            // Dodaj odpowiedni asset z obrażeniami
            Instantiate(damageAssets[currentHealth-1], transform.position, Quaternion.identity);
        } else if (currentHealth == 0)
        {
            gameManager.RestartGame();
        }
    }
}
