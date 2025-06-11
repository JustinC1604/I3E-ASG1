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
