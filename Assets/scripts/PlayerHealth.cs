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
    /// <summary>
    /// Array of control scripts to disable upon player death.
    /// </summary>
    private MonoBehaviour[] controlScripts;

    /// <summary>
    /// Current health of the player.
    /// </summary>
    public int health;

    /// <summary>
    /// Maximum health the player can have.
    /// </summary>
    public int maxHealth = 100;

    /// <summary>
    /// Reference to the UI Text element that displays the player's health.
    /// </summary>
    public TextMeshProUGUI healthText;

    /// <summary>
    /// Reference to the DamageFlashUI script to trigger damage flash effects.
    /// </summary>
    public DamageFlashUI damageFlashUI;

    /// <summary>
    /// Reference to the Game Over panel UI object.
    /// </summary>
    public GameObject gameOverPanel;

    /// <summary>
    /// Delay in seconds before the level restarts after death.
    /// </summary>
    public float restartDelay = 3f;

    /// <summary>
    /// Sound played when player takes damage.
    /// </summary>
    public AudioClip damageSound;

    /// <summary>
    /// Sound played when player dies.
    /// </summary>
    public AudioClip deathSound;

    /// <summary>
    /// Volume for the audio source playing damage and death sounds.
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1f;

    /// <summary>
    /// AudioSource component to play sounds.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Flag to check if the player is dead.
    /// </summary>
    private bool isDead = false;

    /// <summary>
    /// Initialize health, UI, audio, and control scripts references.
    /// </summary>
    void Start()
    {
        health = maxHealth;
        UpdateHealthUI();
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (damageFlashUI == null)
        {
            damageFlashUI = FindObjectOfType<DamageFlashUI>();
        }

        controlScripts = new MonoBehaviour[]
        {
            GetComponent<FirstPersonController>(),
            GetComponent<StarterAssetsInputs>(),
            GetComponent<BasicRigidBodyPush>()
        };

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volume;
    }

    /// <summary>
    /// Reduces player's health by a specified damage amount and handles death or damage effects.
    /// </summary>
    /// <param name="amount">Amount of damage to apply.</param>
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        if (health < 0) health = 0;

        UpdateHealthUI();

        if (damageFlashUI != null)
            damageFlashUI.TriggerFlash();

        if (health <= 0)
        {
            PlayDeathSound();
            Die();
        }
        else
        {
            PlayDamageSound();
        }
    }

    /// <summary>
    /// Updates the health display UI text.
    /// </summary>
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }

    /// <summary>
    /// Handles player death by disabling controls, showing game over UI, and scheduling level restart.
    /// </summary>
    void Die()
    {
        isDead = true;

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

    /// <summary>
    /// Restarts the current scene.
    /// </summary>
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Plays the sound effect for damage taken.
    /// </summary>
    void PlayDamageSound()
    {
        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    /// <summary>
    /// Plays the sound effect for player death.
    /// </summary>
    void PlayDeathSound()
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}



