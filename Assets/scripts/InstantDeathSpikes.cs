/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script handles instant death spikes that kill the player on contact.
* It deals all remaining health as damage to the player when they enter the trigger area.
*/


using UnityEngine;

public class InstantDeathSpikes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerHealth.health); // Deal all remaining health as damage
            }
        }
    }
}
