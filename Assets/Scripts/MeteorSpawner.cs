using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject[] meteorPrefabs;   // Tablica prefabrykatów meteorytów
    public float spawnInterval = 1f;   // Interwał między generowaniem meteorytów
    public float spawnForce = 5f;        // Siła wyrzutu meteorytu
    public float rotationSpeed = 100f;   // Prędkość obrotu meteorytu

    private float minX, maxX;            // Granice ekranu

    private void Start()
    {
        // Ustalenie granic ekranu na podstawie rozmiaru kamery
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomLeft.x;
        maxX = topRight.x;

        // Rozpoczęcie generowania meteorytów w interwale czasowym
        InvokeRepeating("SpawnMeteor", 0f, spawnInterval);
    }

    private void SpawnMeteor()
    {
        // Losowy wybór prefabrykatu meteorytu
        int randomIndex = Random.Range(0, meteorPrefabs.Length);
        GameObject meteorPrefab = meteorPrefabs[randomIndex];

        // Wygenerowanie meteorytu
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

        // Dodanie siły wyrzutu do meteorytu
        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * spawnForce, ForceMode2D.Impulse);

        // Dodanie obracającej się animacji do meteorytu
        RotateMeteor rotateScript = meteor.GetComponent<RotateMeteor>();
        if (rotateScript != null)
        {
            rotateScript.rotationSpeed = rotationSpeed;
        }

        // Zniszczenie meteorytu po pewnym czasie, jeśli nie zostanie zniszczony wcześniej
        Destroy(meteor, 5f);
    }
}
