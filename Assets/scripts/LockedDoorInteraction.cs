/*
* Author: Chia Jia Cong Justin
* Date: 12 June 2025
* Description: This script manages the interaction with the locked door in the game.
*/

using UnityEngine;
using TMPro;

public class LockedDoorInteraction : MonoBehaviour
{
    /// <summary>
    /// Animator component controlling the door animations.
    /// </summary>
    public Animator doorAnimator;

    /// <summary>
    /// Collider component attached to the door to block passage.
    /// </summary>
    public Collider doorCollider;

    /// <summary>
    /// UI Text that shows the message "Press E to interact".
    /// </summary>
    public TextMeshProUGUI interactMessageText;

    /// <summary>
    /// UI Text that shows the message "This door is locked...".
    /// </summary>
    public TextMeshProUGUI lockedMessageText;

    /// <summary>
    /// UI Image displaying the key icon when player has a key.
    /// </summary>
    public UnityEngine.UI.Image keyIconUI;

    /// <summary>
    /// Sound played when player tries to open the locked door without a key.
    /// </summary>
    public AudioClip lockedDoorSound;

    /// <summary>
    /// Sound played when the door successfully opens.
    /// </summary>
    public AudioClip doorOpenSound;

    /// <summary>
    /// Volume for door audio sounds, ranges between 0 and 1.
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 0.8f;

    /// <summary>
    /// AudioSource component used to play door sounds.
    /// </summary>
    private AudioSource doorAudioSource;

    /// <summary>
    /// Tracks whether the player currently has the key to open the door.
    /// </summary>
    private bool playerHasKey = false;

    /// <summary>
    /// Tracks whether the player is within interaction range of the door.
    /// </summary>
    private bool isPlayerNearby = false;

    /// <summary>
    /// Tracks if the door has already been opened.
    /// </summary>
    private bool doorOpened = false;

    /// <summary>
    /// Initialize audio source settings.
    /// </summary>
    void Start()
    {
        doorAudioSource = gameObject.AddComponent<AudioSource>();
        doorAudioSource.playOnAwake = false;
        doorAudioSource.spatialBlend = 0f;
        doorAudioSource.volume = volume;
    }

    /// <summary>
    /// Check player input to open the door if conditions are met.
    /// </summary>
    void Update()
    {
        if (isPlayerNearby && playerHasKey && !doorOpened && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    /// <summary>
    /// Detect when player enters the door's trigger area and show appropriate UI messages.
    /// </summary>
    /// <param name="other">Collider that enters the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNearby = true;

        if (playerHasKey && !doorOpened)
        {
            interactMessageText?.gameObject.SetActive(true);
        }
        else if (!playerHasKey)
        {
            lockedMessageText?.gameObject.SetActive(true);
            PlayLockedDoorSound();
        }
    }

    /// <summary>
    /// Detect when player leaves the door's trigger area and hide UI messages.
    /// </summary>
    /// <param name="other">Collider that exits the trigger</param>
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNearby = false;
        interactMessageText?.gameObject.SetActive(false);
        lockedMessageText?.gameObject.SetActive(false);
    }

    /// <summary>
    /// Assign the key to the player to allow door unlocking.
    /// </summary>
    public void GivePlayerKey()
    {
        playerHasKey = true;
    }

    /// <summary>
    /// Play door open animation, disable collider and update UI accordingly.
    /// </summary>
    private void OpenDoor()
    {
        doorAnimator?.Play("DoorOpen", 0, 0.0f);
        doorCollider.enabled = false;
        interactMessageText?.gameObject.SetActive(false);
        doorOpened = true;
        keyIconUI?.gameObject.SetActive(false);
        PlayDoorOpenSound();
    }

    /// <summary>
    /// Play the sound effect for locked door interaction.
    /// </summary>
    private void PlayLockedDoorSound()
    {
        if (lockedDoorSound != null)
        {
            doorAudioSource.PlayOneShot(lockedDoorSound);
        }
    }

    /// <summary>
    /// Play the sound effect when the door opens.
    /// </summary>
    private void PlayDoorOpenSound()
    {
        if (doorOpenSound != null)
        {
            doorAudioSource.PlayOneShot(doorOpenSound);
        }
    }
}





