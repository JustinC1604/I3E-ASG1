/*
* Author: Chia Jia Cong Justin
* Date: 13 June 2025
* Description: This script handles the interaction with a treasure chest in the game.
* When the player is within range and presses the interaction key, it collects the chest
* and updates the score. It also displays a completion message.
*/

using UnityEngine;
using TMPro;

public class TreasureChestInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public Transform player;
    public TextMeshProUGUI interactionPrompt;
    public GameObject completionMessage;
    public TextMeshProUGUI completionMessageText;
    public TextMeshProUGUI secondaryMessageText; // Optional secondary message
    public int chestValue = 50;

    public AudioClip victorySound;
    [Range(0f, 1f)] public float volume = 1f;

    private AudioSource audioSource;
    private bool isInRange = false;
    private bool isCollected = false;
    private CoinCollection coinCollector;

    void Start()
    {
        interactionPrompt.gameObject.SetActive(false);
        if (completionMessage != null)
            completionMessage.SetActive(false);

        coinCollector = FindObjectOfType<CoinCollection>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volume;
    }

    void Update()
    {
        if (isCollected) return;

        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance <= interactionRange;

        interactionPrompt.gameObject.SetActive(isInRange);

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectChest();
        }
    }

    void CollectChest()
    {
        isCollected = true;
        interactionPrompt.gameObject.SetActive(false);

        if (coinCollector != null)
        {
            coinCollector.AddScore(chestValue);
        }

        if (completionMessage != null)
            completionMessage.SetActive(true);

        if (victorySound != null)
        {
            audioSource.PlayOneShot(victorySound);
            Invoke(nameof(DisableChest), victorySound.length);
        }
        else
        {
            DisableChest();
        }
    }

    void DisableChest()
    {
        gameObject.SetActive(false);
    }
}


