using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    public float speed = 5f; // Prędkość poruszania się przeciwnika

    private Transform player; // Transform gracza
    private GameObject spawnArea;


    private void Start()
    {
        // Znajdź GameObject z tagiem "Player" i pobierz jego transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spawnArea = GameObject.FindGameObjectWithTag("SpawnArea");
    }

    private void Update()
    {
        if (player != null)
        {
            // Poruszaj się w kierunku gracza
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        EnemyManager enemyManager = spawnArea.GetComponent<EnemyManager>();
        if (other.gameObject.CompareTag("PlayerLaser"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            enemyManager.EnemyDestroyed();
        }else if(other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage();
            }

            Destroy(gameObject);
            enemyManager.EnemyDestroyed();
        }

    }
}
