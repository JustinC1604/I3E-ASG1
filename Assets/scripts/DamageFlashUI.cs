/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script handles the UI flash effect when the player takes damage.
*/

using UnityEngine;
using UnityEngine.UI;

public class DamageFlashUI : MonoBehaviour
{
    /// <summary>
    /// Reference to the UI Image used for flashing.
    /// </summary>
    public Image flashImage;

    /// <summary>
    /// Duration of the flash effect.
    /// </summary>
    public float flashDuration = 0.2f;

    // Color used when flashing (semi-transparent red)
    private Color targetColor = new Color(1, 0, 0, 0.5f);

    // Color to reset to (fully transparent)
    private Color clearColor = new Color(1, 0, 0, 0);

    /// <summary>
    /// Initializes the flash image to be transparent at the start.
    /// </summary>
    void Start()
    {
        if (flashImage != null)
            flashImage.color = clearColor;
    }

    /// <summary>
    /// Triggers the damage flash effect.
    /// </summary>
    public void TriggerFlash()
    {
        if (flashImage != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashRoutine());
        }
    }

    /// <summary>
    /// Coroutine that handles the timing and color transition of the flash.
    /// </summary>
    private System.Collections.IEnumerator FlashRoutine()
    {
        flashImage.color = targetColor;
        yield return new WaitForSeconds(flashDuration);
        flashImage.color = clearColor;
    }
}


