/*
* Author: Chia Jia Cong Justin
* Date: 11/06/2025
* Description: This script controls the door's opening and closing animation
*/


using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private string doorOpen = "DoorOpen";
    [SerializeField] private string doorClose = "DoorClose";

    [Header("Audio Settings")]
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    [Range(0f, 1f)] public float volume = 1f;

    private AudioSource doorAudioSource;
    private bool hasTriggered = false;

    private void Start()
    {
        doorAudioSource = gameObject.AddComponent<AudioSource>();
        doorAudioSource.playOnAwake = false;
        doorAudioSource.spatialBlend = 0f;
        doorAudioSource.volume = volume;
    }

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

        // Deactivate after short delay to allow sound to play
        Invoke(nameof(DeactivateTrigger), 1f);
    }

    private void DeactivateTrigger()
    {
        gameObject.SetActive(false);
    }

    private void PlayOpenSound()
    {
        if (doorOpenSound != null)
        {
            doorAudioSource.PlayOneShot(doorOpenSound);
        }
    }

    private void PlayCloseSound()
    {
        if (doorCloseSound != null)
        {
            doorAudioSource.PlayOneShot(doorCloseSound);
        }
    }
}






