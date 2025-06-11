using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    public int poisonDamage = 10;
    public float damageInterval = 1f;
    private float damageTimer;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(poisonDamage);
                    damageTimer = 0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer = 0f; // Reset the timer when the player leaves the poison
        }
    }
}
