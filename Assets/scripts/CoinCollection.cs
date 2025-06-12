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
    private int Coin = 0;
    public int coinValue = 10;
    public TextMeshProUGUI coinText;

    public GameObject keyObject; 
    public TextMeshProUGUI messageText;
    public float messageDuration = 3f;
    private bool keySpawned = false;

    public int scoreToUnlockKey = 50;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.GetComponent<Collider>().enabled = false;

            Coin += coinValue;
            coinText.text = "Score: " + Coin.ToString();
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

    private IEnumerator ShowMessage(string message, float duration)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }
}
