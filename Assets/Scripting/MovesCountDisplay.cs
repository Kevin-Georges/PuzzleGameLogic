using UnityEngine;
using UnityEngine.UI;

public class MoveCountDisplay : MonoBehaviour
{
    public Text moveCountText; // Drag your Text UI element here in the inspector
    private GameManager gameManager;

    void Start()
    {
        // Find the GameManager in the scene
        gameManager = Object.FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    void Update()
    {
        if (gameManager != null && moveCountText != null)
        {
            moveCountText.text = "Moves: " + gameManager.moveCount;
        }
    }
}
