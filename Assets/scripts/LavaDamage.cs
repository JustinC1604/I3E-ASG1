/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script handles the lava damage to the player.
*/

using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    /// <summary>
    /// Amount of damage dealt to the player per second while in lava.
    /// </summary>
    public int damagePerSecond = 15;

    private bool playerInLava = false;
    private float damageTimer = 0f;
    private PlayerHealth playerHealth;

    /// <summary>
    /// Applies damage every second if the player remains in the lava.
    /// </summary>
    void Update()
    {
        if (playerInLava && playerHealth != null)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f)
            {
                playerHealth.TakeDamage(damagePerSecond);
                damageTimer = 0f;
            }
        }
    }

    /// <summary>
    /// Detects when the player enters the lava trigger and applies instant damage.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond); // Instant damage
            }
            playerInLava = true;
            damageTimer = 0f; // Start timer for periodic damage
        }
    }

    /// <summary>
    /// Resets lava damage tracking when the player exits the lava area.
    /// </summary>
    /// <param name="other">The collider that exited the trigger zone.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInLava = false;
            playerHealth = null;
        }
    }
}

