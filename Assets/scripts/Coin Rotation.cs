/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script rotates a coin object around its X-axis at a specified speed.
*/


using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    //Speed of rotation
    public float rotationSpeed = 50f;

    private Vector3 startPosition;
    
    void Start()
    {
        // Store the initial position of the coin
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0,  0);
    }
}
