using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDrone : MonoBehaviour
{
    public float speed = 3f; // Prędkość poruszania się drona
    public float shootingInterval = 1.5f; // Interwał między strzałami drona
    public GameObject bulletPrefab; // Prefab pocisku drona
    private float maxX, minX;
    private GameObject spawnArea;
    private float shootingTimer; // Timer dla interwału strzałów
    

    private void Start()
    {
        // Ustalenie granic ekranu na podstawie rozmiaru kamery
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
        spawnArea = GameObject.FindGameObjectWithTag("SpawnArea");

        minX = bottomLeft.x;
        maxX = topRight.x;
        // Rozpocznij strzelanie drona
        shootingTimer = shootingInterval;
    }

    private void Update()
    {
        // Poruszaj się w prawo i lewo
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > maxX || transform.position.x < minX)
        {
            // Odwróć kierunek, jeśli dron wyjdzie poza określony zakres
            speed *= -1;
        }

        // Strzelaj z określonym interwałem
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0f)
        {
            Shoot();
            shootingTimer = shootingInterval;
        }
    }

    private void Shoot()
    {
        // Wystrzel pocisk z drona
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerLaser"))
        {
            Destroy(other.gameObject); // Zniszcz laser gracza
            Destroy(gameObject); // Zniszcz przeciwnika
            EnemyManager enemyManager = spawnArea.GetComponent<EnemyManager>();
            enemyManager.EnemyDestroyed();
        }
    }
}
