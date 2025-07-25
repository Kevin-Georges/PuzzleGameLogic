using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRemover : MonoBehaviour
{
    [SerializeField] private GameManager puzzleManager; // Reference to the script containing puzzleComplete.
    private Collider2D boxCollider;

    void Start()
    {
        // Get the BoxCollider2D component from this GameObject.
        boxCollider = GetComponent<Collider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D not found on this GameObject.");
        }

        // Ensure puzzleManager is assigned.
        if (puzzleManager == null)
        {
            Debug.LogError("GameManager reference not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (puzzleManager != null && puzzleManager.puzzleComplete)
        {
            // Disable the collider when the puzzle is complete.
            if (boxCollider != null && boxCollider.enabled)
            {
                boxCollider.enabled = false;
                Debug.Log("BoxCollider2D disabled because the puzzle is complete.");
            }
        }
    }
}