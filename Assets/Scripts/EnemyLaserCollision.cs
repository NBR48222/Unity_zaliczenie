using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdź, czy kolizja dotyczy gracza
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);  // Zniszcz laser
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage();
            }
        }
    }
}
