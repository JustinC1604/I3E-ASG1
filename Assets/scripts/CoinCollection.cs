/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script handles the collection of coins in the game.
* When the player collides with a coin, it increases the score and updates the UI.
*/


using UnityEngine;
using TMPro;
using System.Collections;

public class CoinCollection : MonoBehaviour
{
    public int Coin { get; private set; } = 0;
    public int coinValue = 10;
    public TextMeshProUGUI coinText;

    public GameObject keyObject;
    public TextMeshProUGUI messageText;
    public float messageDuration = 3f;
    private bool keySpawned = false;

    public int scoreToUnlockKey = 50;

    public AudioClip coinCollectSound;
    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource coinAudioSource;

    private void Start()
    {
        if (keyObject != null)
        {
            keyObject.SetActive(false);
        }

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        // Forcefully add and configure a dedicated 2D AudioSource for coin sounds
        coinAudioSource = gameObject.AddComponent<AudioSource>();
        coinAudioSource.playOnAwake = false;
        coinAudioSource.spatialBlend = 0f; // Force 2D
        coinAudioSource.volume = volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.GetComponent<Collider>().enabled = false;

            PlayCoinSound();

            Coin += coinValue;
            UpdateScoreUI();
            Debug.Log(Coin);
            Destroy(other.gameObject);

            if (Coin >= scoreToUnlockKey && keyObject != null && !keySpawned)
            {
                keyObject.SetActive(true);
                keySpawned = true;

                if (messageText != null)
                {
                    StartCoroutine(ShowMessage("The key has spawned in the center chamber!", messageDuration));
                }
            }
        }
    }

    private void UpdateScoreUI()
    {
        if (coinText != null)
        {
            coinText.text = "Score: " + Coin.ToString();
        }
    }

    private IEnumerator ShowMessage(string message, float duration)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }

    public void AddScore(int value)
    {
        Coin += value;
        UpdateScoreUI();
    }

    private void PlayCoinSound()
    {
        if (coinCollectSound != null)
        {
            coinAudioSource.PlayOneShot(coinCollectSound);
            Debug.Log("✅ Coin sound played");
        }
        else
        {
            Debug.LogWarning("⚠️ Coin collect sound clip not assigned!");
        }
    }
}
