/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script manages the player's health in the game.
*/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using StarterAssets;
public class PlayerHealth : MonoBehaviour
{
    private MonoBehaviour[] controlScripts;

    public int health;
    public int maxHealth = 100;
    public TextMeshProUGUI healthText; // Reference to the UI text element to display health
    public DamageFlashUI damageFlashUI;
    public GameObject gameOverPanel;
    public float restartDelay = 3f;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Initialize health to maxHealth at the start
        UpdateHealthUI(); // Update the UI to reflect the initial health
        if (gameOverPanel != null) gameOverPanel.SetActive(false);


        if (damageFlashUI == null)
        {
            damageFlashUI = FindObjectOfType<DamageFlashUI>();
        }

        // Find and store control scripts 
        controlScripts = new MonoBehaviour[]
        {
            GetComponent<FirstPersonController>(),
            GetComponent<StarterAssetsInputs>(),
            GetComponent<BasicRigidBodyPush>()
        };
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // Prevent taking damage if already dead
        
        health -= amount;
        if (health < 0) health = 0; //Clamp health to 0 minimum

        UpdateHealthUI();

        if (damageFlashUI != null)
            damageFlashUI.TriggerFlash();

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
        isDead = true; // Set the player as dead

        // Disable all control scripts
        foreach (var script in controlScripts)
        {
            if (script != null)
                script.enabled = false;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Invoke("RestartLevel", restartDelay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

