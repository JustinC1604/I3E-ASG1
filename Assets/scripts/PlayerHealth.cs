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

    public AudioClip damageSound;
    public AudioClip deathSound;
    [Range(0f, 1f)] public float volume = 1f;

    private AudioSource audioSource;
    private bool isDead = false;

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

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }

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

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayDamageSound()
    {
        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
            Debug.Log("🎧 Damage sound played");
        }
    }

    void PlayDeathSound()
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
            Debug.Log("💀 Death sound played");
        }
    }
}


