/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script manages the player's health in the game.
*/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public TextMeshProUGUI healthText; // Reference to the UI text element to display health
    public GameObject gameOverPanel;
    public float restartDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Initialize health to maxHealth at the start
        UpdateHealthUI(); // Update the UI to reflect the initial health
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateHealthUI(); // Update the UI after taking damage

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }
    
    void Die()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Optionally disable movement here, if needed

        Invoke("RestartLevel", restartDelay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

