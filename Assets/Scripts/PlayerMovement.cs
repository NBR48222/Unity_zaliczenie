    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Szybkość poruszania gracza
    public int numberOfLasers = 1; // Liczba wystrzałów na raz
    public float fireRate = 1.2f;    // Częstotliwość strzałów (na sekundę)
    public Transform firePoint;    // Punkt startowy dla wystrzałów
    public GameObject laserPrefab;  // Prefabrykat lasera
    private float nextFireTime = 0f;  // Czas, kiedy będzie możliwe kolejne strzały
    private int playerLevel = 1; //Poziom gracza

    private void Start()
    {

    }

    private void Update()
    {
        // Sprawdzenie, czy wystąpił dotyk na ekranie
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Sprawdzenie rodzaju dotyku
            if (touch.phase == TouchPhase.Moved)
            {
                // Pobranie pozycji dotyku w przestrzeni ekranu
                Vector3 touchPosition = touch.position;

                // Przeliczenie pozycji dotyku na pozycję w przestrzeni gry
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                // Ustalenie nowej pozycji gracza na podstawie pozycji dotyku
                transform.position = new Vector3(worldTouchPosition.x, worldTouchPosition.y, transform.position.z);
            }


        }

        if (Input.touchCount > 0 && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;  // Ustalamy czas kolejnych strzałów
            Fire();  // Wywołujemy funkcję strzału
        }
    }

    void Fire()
    {
        if (playerLevel == 1)
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
        else if (playerLevel >= 2)
        {
            Vector3 offset = new Vector3(0.2f, 0, 0); // Lekki odstęp między laserami
            Instantiate(laserPrefab, transform.position - offset, Quaternion.identity);
            Instantiate(laserPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Buff"))
        {
            CollectBuff(other.gameObject);
        }
    }

    void CollectBuff(GameObject buff)
    {
        Destroy(buff); // Zniszczenie buffa
        playerLevel++; // Zwiększenie poziomu gracza
    }
}
