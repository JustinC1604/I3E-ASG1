using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    public int damagePerSecond = 15;
    private bool playerInLava = false;
    private float damageTimer = 0f;
    private PlayerHealth playerHealth;

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
            damageTimer = 0f; // start timer for next tick
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInLava = false;
            playerHealth = null;
        }
    }
}
