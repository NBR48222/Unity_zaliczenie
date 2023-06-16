using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserMovement : MonoBehaviour
{
    public float speed = 10f;  // Prędkość poruszania lasera
    public Vector2 direction = Vector2.down;  // Kierunek poruszania lasera
    public float lifetime = 3f;  // Czas życia lasera

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
