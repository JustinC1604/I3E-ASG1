/*
* Author: Chia Jia Cong Justin
* Date: 12 June 2025
* Description: This script manages the interaction with the locked door in the game.
*/

using UnityEngine;
using TMPro;

public class LockedDoorInteraction : MonoBehaviour
{
    public Animator doorAnimator;
    public Collider doorCollider;
    public TextMeshProUGUI interactMessageText;  // "Press E to interact"
    public TextMeshProUGUI lockedMessageText;    // "This door is locked..."
    public UnityEngine.UI.Image keyIconUI;

    public AudioClip lockedDoorSound;
    public AudioClip doorOpenSound;
    [Range(0f, 1f)] public float volume = 1f;

    private AudioSource doorAudioSource;
    private bool playerHasKey = false;
    private bool isPlayerNearby = false;
    private bool doorOpened = false;

    void Start()
    {
        doorAudioSource = gameObject.AddComponent<AudioSource>();
        doorAudioSource.playOnAwake = false;
        doorAudioSource.spatialBlend = 0f;
        doorAudioSource.volume = volume;
    }

    void Update()
    {
        if (isPlayerNearby && playerHasKey && !doorOpened && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

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

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNearby = false;
        interactMessageText?.gameObject.SetActive(false);
        lockedMessageText?.gameObject.SetActive(false);
    }

    public void GivePlayerKey()
    {
        playerHasKey = true;
    }

    private void OpenDoor()
    {
        doorAnimator?.Play("DoorOpen", 0, 0.0f);
        doorCollider.enabled = false;
        interactMessageText?.gameObject.SetActive(false);
        doorOpened = true;
        keyIconUI?.gameObject.SetActive(false);
        PlayDoorOpenSound();
    }

    private void PlayLockedDoorSound()
    {
        if (lockedDoorSound != null)
        {
            doorAudioSource.PlayOneShot(lockedDoorSound);
        }
    }

    private void PlayDoorOpenSound()
    {
        if (doorOpenSound != null)
        {
            doorAudioSource.PlayOneShot(doorOpenSound);
        }
    }
}




