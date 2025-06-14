/*
* Author: Chia Jia Cong Justin
* Date: 12 June 2025
* Description: This script handles the instant death of the player when they enter a lava area.
* When the player collides with the lava, their health is instantly reduced to zero.
*/

using UnityEngine;

public class LavaInstantDeath : MonoBehaviour
{
    /// <summary>
    /// Triggered when another collider enters the lava trigger area.
    /// Instantly reduces the player's health to zero if they are the one who entered.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Instantly reduce the player's health to zero
                playerHealth.TakeDamage(playerHealth.maxHealth);
            }
        }
    }
}

