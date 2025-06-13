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
    public float interactionRange = 3f;
    public Transform player;
    public TextMeshProUGUI interactionPrompt;
    public Image keyIconUI;
    public LockedDoorInteraction lockedDoor;

    public AudioClip keyCollectSound;
    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource keyAudioSource;

    private bool isInRange = false;
    private bool isCollected = false;

    void Start()
    {
        keyAudioSource = gameObject.AddComponent<AudioSource>();
        keyAudioSource.playOnAwake = false;
        keyAudioSource.spatialBlend = 0f;
        keyAudioSource.volume = volume;
    }

    void Update()
    {
        if (isCollected) return;

        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance <= interactionRange;

        interactionPrompt.gameObject.SetActive(isInRange);

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectKey();
        }
    }

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

    private void PlayKeySound()
    {
        if (keyCollectSound != null)
        {
            keyAudioSource.PlayOneShot(keyCollectSound);
            Debug.Log("✅ Key collect sound played");
        }
        else
        {
            Debug.LogWarning("⚠️ Key collect sound clip not assigned!");
        }
    }

    private IEnumerator DisableAfterSound()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}


