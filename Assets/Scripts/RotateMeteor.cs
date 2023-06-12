using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMeteor : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private void Update()
    {
        // Obracanie meteorytu wokół własnej osi
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}