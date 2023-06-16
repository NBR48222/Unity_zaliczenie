using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawdź, czy kolizja dotyczy przeciwnika
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);  // Zniszcz laser
        }
    }
}
