using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    public int poisonDamagePerSecond = 10;
    private bool playerInPoison = false;
    private float poisonTimer = 0f;
    private PlayerHealth playerHealth;

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInPoison = false;
            playerHealth = null;
        }
    }
}
