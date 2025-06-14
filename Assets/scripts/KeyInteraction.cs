/*
* Author: Chia Jia Cong Justin
* Date: 12 June 2025
* Description: This script allows the player to interact with a key object in the game.
* When the player is within range and presses the interaction key, the key is collected.
*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class KeyInteraction : MonoBehaviour
{
    /// <summary>
    /// Maximum distance at which the player can interact with the key.
    /// </summary>
    public float interactionRange = 3f;

    /// <summary>
    /// Reference to the player's transform.
    /// </summary>
    public Transform player;

    /// <summary>
    /// UI prompt shown when the player is in range to interact.
    /// </summary>
    public TextMeshProUGUI interactionPrompt;

    /// <summary>
    /// UI icon to show the key has been collected.
    /// </summary>
    public Image keyIconUI;

    /// <summary>
    /// Reference to the locked door script to notify when key is collected.
    /// </summary>
    public LockedDoorInteraction lockedDoor;

    /// <summary>
    /// Audio clip that plays when the key is collected.
    /// </summary>
    public AudioClip keyCollectSound;

    /// <summary>
    /// Volume of the key collection sound.
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource keyAudioSource;
    private bool isInRange = false;
    private bool isCollected = false;

    void Start()
    {
        // Set up audio source for playing key collection sound
        keyAudioSource = gameObject.AddComponent<AudioSource>();
        keyAudioSource.playOnAwake = false;
        keyAudioSource.spatialBlend = 0f;
        keyAudioSource.volume = volume;
    }

    void Update()
    {
        if (isCollected) return;

        // Check player distance to determine if interaction is possible
        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance <= interactionRange;

        // Show/hide interaction prompt based on range
        interactionPrompt.gameObject.SetActive(isInRange);

        // Collect key when player presses 'E'
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectKey();
        }
    }

    /// <summary>
    /// Handles the collection of the key and related effects.
    /// </summary>
    void CollectKey()
    {
        isCollected = true;
        interactionPrompt.gameObject.SetActive(false);

        if (keyIconUI != null)
        {
            keyIconUI.gameObject.SetActive(true);
        }

        if (lockedDoor != null)
        {
            lockedDoor.GivePlayerKey();
        }

        PlayKeySound();

        // Delay deactivation so sound can finish playing
        StartCoroutine(DisableAfterSound());
    }

    /// <summary>
    /// Plays the key collection sound.
    /// </summary>
    private void PlayKeySound()
    {
        if (keyCollectSound != null)
        {
            keyAudioSource.PlayOneShot(keyCollectSound);
        }
    }

    /// <summary>
    /// Coroutine to deactivate the key object after a short delay.
    /// </summary>
    private IEnumerator DisableAfterSound()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}



