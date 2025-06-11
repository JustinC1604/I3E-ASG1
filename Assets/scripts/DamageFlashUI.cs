/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script handles the UI flash effect when the player takes damage.
*/

using UnityEngine;
using UnityEngine.UI;

public class DamageFlashUI : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;
    private Color targetColor = new Color(1, 0, 0, 0.5f); // semi-transparent red
    private Color clearColor = new Color(1, 0, 0, 0);     // transparent

    void Start()
    {
        if (flashImage != null)
            flashImage.color = clearColor;
    }

    public void TriggerFlash()
    {
        if (flashImage != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashRoutine());
        }
    }

    private System.Collections.IEnumerator FlashRoutine()
    {
        flashImage.color = targetColor;
        yield return new WaitForSeconds(flashDuration);
        flashImage.color = clearColor;
    }
}

