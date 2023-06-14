using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Pobierz nazwę aktualnej sceny
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Załaduj ponownie scenę, aby zrestartować grę
        SceneManager.LoadScene(currentSceneName);
    }
}