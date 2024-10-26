using System.Collections;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public float requiredDriftTime = 5f;  // Time player needs to drift in circle
    public float detectionRadius = 3f;    // Adjustable circle radius

    private float driftTimer = 0f;
    private bool playerInRange = false;

    private void OnDrawGizmosSelected()
    {
        // Draw the detection circle in the editor for visualization
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Update()
    {
        if (playerInRange)
        {
            driftTimer += Time.deltaTime;
            if (driftTimer >= requiredDriftTime)
            {
                PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
                if (player != null)
                {
                    player.IncreaseScore();  // Increase score
                }
                Destroy(gameObject);       // Remove the flag
            }
        }
        else
        {
            driftTimer = 0f;  // Reset timer if player leaves circle
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && player.IsDrifting())
            {
                playerInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            driftTimer = 0f;  // Reset the timer when player leaves
        }
    }
}

