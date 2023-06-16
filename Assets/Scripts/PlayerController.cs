using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Renderer playerRenderer; // Referencja do komponentu Renderer gracza
    public GameObject[] damageAssets; // Tablica assetów z obrażeniami
    public int maxHealth = 4; // Maksymalna liczba żyć gracza
    public Color damageColor = new Color(1f, 0.4764151f, 0.4764151f, 1f);

    private int currentHealth; // Obecna liczba żyć gracza
    private float flashDuration = 0.1f; // Czas trwania efektu migania
    private float flashTimer; // Timer dla efektu migania
    private bool isFlashing; // Czy efekt migania jest aktywny
    private GameManager gameManager; // Referencja do GameManager
    private Renderer shipRenderer;
    private GameObject ship;

    private void Start()
    {
        // Pobierz komponent Renderer z GameObjectu gracza
        playerRenderer = GetComponent<Renderer>();
        GameObject ship = GameObject.FindGameObjectWithTag("PlayerShip");
        shipRenderer = ship.GetComponent<Renderer>();

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
            shipRenderer.material.color = damageColor;

            // Sprawdź, czy czas trwania efektu migania minął
            if (flashTimer >= flashDuration)
            {
                // Wyłącz efekt migania i przywróć oryginalny kolor gracza
                isFlashing = false;
                shipRenderer.material.color = Color.white;
            }
        }
    }

    public void TakeDamage()
    {

        if (!isFlashing) // Dodaj warunek, aby uniknąć migania, gdy efekt już trwa
        {
            // Odtwórz efekt migania
            isFlashing = true;
            flashTimer = 0f;

            // Zmniejsz liczbę żyć gracza
            currentHealth--;

            if (currentHealth > 0 && currentHealth < maxHealth) // Poprawiony warunek sprawdzający liczbę żyć
            {
                // Dodaj odpowiedni asset z obrażeniami
                GameObject damageAsset = Instantiate(damageAssets[currentHealth - 1], transform.position, Quaternion.identity);
                damageAsset.transform.parent = transform; // Ustawienie obrażeń jako dziecka gracza
            }
            else if (currentHealth == 0)
            {
                gameManager.RestartGame();
            }
        }
    }
}

