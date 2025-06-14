/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script rotates a coin object around its X-axis at a specified speed.
*/

using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    /// <summary>
    /// Speed of rotation in degrees per second.
    /// </summary>
    public float rotationSpeed = 50f;

    private Vector3 startPosition;

    /// <summary>
    /// Stores the initial position of the coin when the game starts.
    /// </summary>
    void Start()
    {
        startPosition = transform.position;
    }

    /// <summary>
    /// Rotates the coin around its X-axis every frame.
    /// </summary>
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}

