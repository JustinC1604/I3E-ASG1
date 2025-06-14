/*
* Author: Chia Jia Cong Justin
* Date: 11/06/2025
* Description: This script controls the door's opening and closing animation
*/

using UnityEngine;

public class DoorController : MonoBehaviour
{
    /// <summary>
    /// Reference to the Animator component controlling the door.
    /// </summary>
    [SerializeField] private Animator myDoor = null;

    /// <summary>
    /// If true, triggers the door opening animation.
    /// </summary>
    [SerializeField] private bool openTrigger = false;

    /// <summary>
    /// If true, triggers the door closing animation.
    /// </summary>
    [SerializeField] private bool closeTrigger = false;

    /// <summary>
    /// Name of the animation trigger for opening the door.
    /// </summary>
    [SerializeField] private string doorOpen = "DoorOpen";

    /// <summary>
    /// Name of the animation trigger for closing the door.
    /// </summary>
    [SerializeField] private string doorClose = "DoorClose";

    [Header("Audio Settings")]

    /// <summary>
    /// Sound to play when the door opens.
    /// </summary>
    public AudioClip doorOpenSound;

    /// <summary>
    /// Sound to play when the door closes.
    /// </summary>
    public AudioClip doorCloseSound;

    /// <summary>
    /// Volume level for the door audio.
    /// </summary>
    [Range(0f, 1f)] public float volume = 1f;

    private AudioSource doorAudioSource;

    /// <summary>
    /// Flag to ensure the door is only triggered once.
    /// </summary>
    private bool hasTriggered = false;

    /// <summary>
    /// Initializes the AudioSource component with proper settings.
    /// </summary>
    private void Start()
    {
        doorAudioSource = gameObject.AddComponent<AudioSource>();
        doorAudioSource.playOnAwake = false;
        doorAudioSource.spatialBlend = 0f;
        doorAudioSource.volume = volume;
    }

    /// <summary>
    /// Handles trigger events when the player enters the door trigger zone.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;

        if (openTrigger)
        {
            myDoor.Play("DoorOpen", 0, 0.0f);
            PlayOpenSound();
        }
        else if (closeTrigger)
        {
            myDoor.Play("DoorClose", 0, 0.0f);
            PlayCloseSound();
        }

        // Deactivate after delay to allow the sound to finish playing
        Invoke(nameof(DeactivateTrigger), 1f);
    }

    /// <summary>
    /// Deactivates this trigger object.
    /// </summary>
    private void DeactivateTrigger()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Plays the door opening sound effect.
    /// </summary>
    private void PlayOpenSound()
    {
        if (doorOpenSound != null)
        {
            doorAudioSource.PlayOneShot(doorOpenSound);
        }
    }

    /// <summary>
    /// Plays the door closing sound effect.
    /// </summary>
    private void PlayCloseSound()
    {
        if (doorCloseSound != null)
        {
            doorAudioSource.PlayOneShot(doorCloseSound);
        }
    }
}







