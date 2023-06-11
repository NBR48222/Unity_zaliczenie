using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Szybkość poruszania gracza
    public float moveSpeed = 5f;

    // Granice ekranu
    private float minX, maxX, minY, maxY;

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
    }
}