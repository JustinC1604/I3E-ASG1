/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script handles the poison damage to the player.
*/

using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    /// <summary>
    /// Amount of poison damage dealt to the player per second.
    /// </summary>
    public int poisonDamagePerSecond = 10;

    /// <summary>
    /// Flag to check if the player is currently inside the poison area.
    /// </summary>
    private bool playerInPoison = false;

    /// <summary>
    /// Timer to track when to apply the next poison damage tick.
    /// </summary>
    private float poisonTimer = 0f;

    /// <summary>
    /// Reference to the PlayerHealth component of the player.
    /// </summary>
    private PlayerHealth playerHealth;

    /// <summary>
    /// Applies poison damage to the player every second while inside the poison area.
    /// </summary>
    void Update()
    {
        if (playerInPoison && playerHealth != null)
        {
            poisonTimer += Time.deltaTime;
            if (poisonTimer >= 1f)
            {
                playerHealth.TakeDamage(poisonDamagePerSecond);
                poisonTimer = 0f;
            }
        }
    }

    /// <summary>
    /// Detect when the player enters the poison area and apply initial damage.
    /// </summary>
    /// <param name="other">Collider that enters the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(poisonDamagePerSecond); // Instant damage
            }
            playerInPoison = true;
            poisonTimer = 0f;
        }
    }

    /// <summary>
    /// Detect when the player exits the poison area and stop applying damage.
    /// </summary>
    /// <param name="other">Collider that exits the trigger</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInPoison = false;
            playerHealth = null;
        }
    }
}

