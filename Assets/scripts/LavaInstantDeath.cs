/*
* Author: Chia Jia Cong Justin
* Date: 12 June 2025
* Description: This script handles the instant death of the player when they enter a lava area.
* When the player collides with the lava, their health is instantly reduced to zero.
*/

using UnityEngine;

public class LavaInstantDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerHealth.maxHealth); // Instantly reduce health to 0
            }
        }
    }
}
