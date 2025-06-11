/*
* Author: Chia Jia Cong Justin
* Date: 11 June 2025
* Description: This script controls the rotation of the key
*/


using UnityEngine;

public class KeyRotation : MonoBehaviour
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
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
