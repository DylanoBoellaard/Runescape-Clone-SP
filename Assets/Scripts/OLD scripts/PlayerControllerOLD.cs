using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOLD : MonoBehaviour
{
    // Allow full control for the player (up, down, left, right)

    private float speed = 10.0f;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Change to WASD only, not arrow keys as well
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

        // Resets the player's position to 0,2,0 when player goes below 0. Perhaps change it to a 'reset' layer instead to be able to add land below y = 0?
        if (playerRb.position.y < 0)
        {
            transform.position = new Vector3(0, 2, 0);
        }
    }
}
