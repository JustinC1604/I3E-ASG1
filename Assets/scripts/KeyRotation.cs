/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script controls the rotation of the key.
*/

using UnityEngine;

public class KeyRotation : MonoBehaviour
{
    /// <summary>
    /// Speed at which the key rotates.
    /// </summary>
    public float rotationSpeed = 50f;

    private Vector3 startPosition;
    
    /// <summary>
    /// Stores the initial position of the key when the game starts.
    /// </summary>
    void Start()
    {
        startPosition = transform.position;
    }

    /// <summary>
    /// Continuously rotates the key around the Y-axis in world space.
    /// </summary>
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
