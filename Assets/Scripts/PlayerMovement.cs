using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Szybkość poruszania gracza
    public float moveSpeed = 5f;

    // Granice ekranu
    private float minX, maxX, minY, maxY;

    public GameObject laserPrefab;  // Prefabrykat lasera
    public Transform firePoint;    // Punkt startowy dla wystrzałów
    public float fireRate = 1f;    // Częstotliwość strzałów (na sekundę)
    public int numberOfLasers = 1; // Liczba wystrzałów na raz
    private float nextFireTime = 0f;  // Czas, kiedy będzie możliwe kolejne strzały
    private int playerLevel = 1; //Poziom gracza

    private void Start()
    {
        // Ustalenie granic ekranu na podstawie rozmiaru kamery
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomLeft.x + transform.localScale.x / 2;
        maxX = topRight.x - transform.localScale.x / 2;
        minY = bottomLeft.y + transform.localScale.y / 2;
        maxY = topRight.y - transform.localScale.y / 2;
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
                // Pobranie zmiany pozycji dotyku
                Vector2 touchDeltaPosition = touch.deltaPosition;

                // Obliczenie nowej pozycji gracza
                Vector3 newPosition = transform.position + new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 0) * moveSpeed * Time.deltaTime;

                // Ograniczenie pozycji gracza do granic ekranu
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                // Ustawienie nowej pozycji gracza
                transform.position = newPosition;
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
                       // Dodatkowe działania po zebraniu buffa
    }
}