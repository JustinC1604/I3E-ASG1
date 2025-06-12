using UnityEngine;
using TMPro;

public class TreasureChestInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public Transform player;
    public TextMeshProUGUI interactionPrompt;
    public GameObject completionMessage; 
    public TextMeshProUGUI completionMessageText;
    public TextMeshProUGUI secondaryMessageText; // Optional secondary message
    public int chestValue = 50;

    private bool isInRange = false;
    private bool isCollected = false;
    private CoinCollection coinCollector;

    void Start()
    {
        interactionPrompt.gameObject.SetActive(false);
        if (completionMessage != null)
            completionMessage.SetActive(false);

        coinCollector = FindObjectOfType<CoinCollection>();
    }

    void Update()
    {
        if (isCollected) return;

        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance <= interactionRange;

        interactionPrompt.gameObject.SetActive(isInRange);

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectChest();
        }
    }

    void CollectChest()
    {
        isCollected = true;
        interactionPrompt.gameObject.SetActive(false);

        if (coinCollector != null)
        {
            coinCollector.AddScore(chestValue);
        }

        gameObject.SetActive(false); // Hide chest
        

        if (completionMessage != null)
            completionMessage.SetActive(true);
    }
}