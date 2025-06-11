/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script handles the collection of coins in the game.
* When the player collides with a coin, it increases the score and updates the UI.
*/


using UnityEngine;
using TMPro;

public class CoinCollection : MonoBehaviour
{
    private int Coin = 0;
    public int coinValue = 10;

    public TextMeshProUGUI coinText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin")
        {
            Coin += coinValue;
            coinText.text = "Score: " + Coin.ToString();
            // Log the number of coins collected
            Debug.Log(Coin);
            Destroy(other.gameObject);
        }
    }
}
