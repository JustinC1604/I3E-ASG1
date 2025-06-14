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
    /// <summary>
    /// Current coin score of the player
    /// </summary>
    public int Coin { get; private set; } = 0;

    /// <summary>
    /// Value of a single coin
    /// </summary>
    public int coinValue = 10;

    /// <summary>
    /// UI Text to display the score
    /// </summary>
    public TextMeshProUGUI coinText;

    /// <summary>
    /// Reference to the key object to spawn
    /// </summary>
    public GameObject keyObject;

    /// <summary>
    /// Text UI element to display messages
    /// </summary>
    public TextMeshProUGUI messageText;

    /// <summary>
    /// Duration for which messages are displayed
    /// </summary>
    public float messageDuration = 3f;

    private bool keySpawned = false;

    /// <summary>
    /// Score required to unlock the key
    /// </summary>
    public int scoreToUnlockKey = 50;

    /// <summary>
    /// Sound played when collecting a coin
    /// </summary>
    public AudioClip coinCollectSound;

    [Range(0f, 1f)]
    public float volume = 1f;

    /// <summary>
    /// Prompt displayed when player is near a coin
    /// </summary>
    public TextMeshProUGUI interactPrompt;

    private GameObject currentCoin = null;
    private AudioSource coinAudioSource;

    /// <summary>
    /// UI elements for coin collection counters
    /// </summary>
    public TextMeshProUGUI coinsCollectedText;
    public TextMeshProUGUI coinsLeftText;
    public TextMeshProUGUI allCoinsCollectedText;
    public int totalCoins = 5;

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

        if (interactPrompt != null)
        {
            interactPrompt.gameObject.SetActive(false);
        }

        coinAudioSource = gameObject.AddComponent<AudioSource>();
        coinAudioSource.playOnAwake = false;
        coinAudioSource.spatialBlend = 0f;
        coinAudioSource.volume = volume;

        UpdateCoinCounterUI(); // Initialize counter display
    }

    private void Update()
    {
        if (currentCoin != null && Input.GetKeyDown(KeyCode.E))
        {
            CollectCoin(currentCoin);
            currentCoin = null;

            if (interactPrompt != null)
            {
                interactPrompt.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            currentCoin = other.gameObject;

            if (interactPrompt != null)
            {
                interactPrompt.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Coin") && other.gameObject == currentCoin)
        {
            currentCoin = null;

            if (interactPrompt != null)
            {
                interactPrompt.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Handles logic when a coin is collected
    /// </summary>
    private void CollectCoin(GameObject coin)
    {
        coin.GetComponent<Collider>().enabled = false;

        PlayCoinSound();

        Coin += coinValue;
        UpdateScoreUI();
        Destroy(coin);

        if (Coin >= scoreToUnlockKey && keyObject != null && !keySpawned)
        {
            keyObject.SetActive(true);
            keySpawned = true;

            if (messageText != null)
            {
                StartCoroutine(ShowMessage("The key has spawned in the center chamber!", messageDuration));
            }
        }

        UpdateCoinCounterUI();
    }

    /// <summary>
    /// Updates the score UI
    /// </summary>
    private void UpdateScoreUI()
    {
        if (coinText != null)
        {
            coinText.text = "Score: " + Coin.ToString();
        }
    }

    /// <summary>
    /// Displays a temporary message on the screen
    /// </summary>
    private IEnumerator ShowMessage(string message, float duration)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Adds a value to the current score
    /// </summary>
    public void AddScore(int value)
    {
        Coin += value;
        UpdateScoreUI();
        UpdateCoinCounterUI();
    }

    /// <summary>
    /// Plays the coin collection sound
    /// </summary>
    private void PlayCoinSound()
    {
        if (coinCollectSound != null)
        {
            coinAudioSource.PlayOneShot(coinCollectSound);
        }
    }

    /// <summary>
    /// Updates coin-related UI elements like how many collected and how many left
    /// </summary>
    private void UpdateCoinCounterUI()
    {
        int coinsCollected = Coin / coinValue;
        int coinsLeft = totalCoins - coinsCollected;

        if (coinsCollectedText != null)
            coinsCollectedText.text = "Coins collected: " + coinsCollected;

        if (coinsLeftText != null)
            coinsLeftText.text = "Coins left: " + Mathf.Max(coinsLeft, 0);

        if (allCoinsCollectedText != null)
            allCoinsCollectedText.gameObject.SetActive(coinsLeft <= 0);

        if (coinsLeftText != null)
            coinsLeftText.gameObject.SetActive(coinsLeft > 0);
    }

    /// <summary>
    /// Adds bonus points to score
    /// </summary>
    public void AddBonusScore(int value)
    {
        Coin += value;
        UpdateScoreUI();
    }
}



