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
    /// <summary>
    /// Maximum distance within which the player can interact with the chest.
    /// </summary>
    public float interactionRange = 3f;

    /// <summary>
    /// Reference to the player's Transform.
    /// </summary>
    public Transform player;

    /// <summary>
    /// UI prompt to show when the player can interact.
    /// </summary>
    public TextMeshProUGUI interactionPrompt;

    /// <summary>
    /// UI panel that shows the completion message after collecting the chest.
    /// </summary>
    public GameObject completionMessage;

    /// <summary>
    /// Text element for the main completion message.
    /// </summary>
    public TextMeshProUGUI completionMessageText;

    /// <summary>
    /// Text element for a secondary completion message.
    /// </summary>
    public TextMeshProUGUI secondaryMessageText;

    /// <summary>
    /// Score value awarded for collecting this chest.
    /// </summary>
    public int chestValue = 50;

    /// <summary>
    /// Sound played when the chest is collected.
    /// </summary>
    public AudioClip victorySound;

    /// <summary>
    /// Volume for the audio source playing the victory sound.
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1f;

    /// <summary>
    /// AudioSource component used to play sounds.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Flag indicating whether the player is within interaction range.
    /// </summary>
    private bool isInRange = false;

    /// <summary>
    /// Flag indicating whether the chest has been collected.
    /// </summary>
    private bool isCollected = false;

    /// <summary>
    /// Reference to the CoinCollection script to update score.
    /// </summary>
    private CoinCollection coinCollector;

    /// <summary>
    /// Initializes variables and references.
    /// </summary>
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

    /// <summary>
    /// Checks player distance and interaction input to collect the chest.
    /// </summary>
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

    /// <summary>
    /// Handles chest collection: awards score, shows messages, plays sound, and disables chest.
    /// </summary>
    void CollectChest()
    {
        isCollected = true;
        interactionPrompt.gameObject.SetActive(false);

        if (coinCollector != null)
        {
            coinCollector.AddBonusScore(chestValue);
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

    /// <summary>
    /// Disables the chest GameObject.
    /// </summary>
    void DisableChest()
    {
        gameObject.SetActive(false);
    }
}



