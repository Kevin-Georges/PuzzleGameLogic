using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public HighScore highScoreScript;  // Reference to the HighScore script
    private TextMeshProUGUI highScoreText; // TextMesh Pro component for displaying high score

    void Start()
    {
        // Assign the TextMeshProUGUI component
        highScoreText = GetComponent<TextMeshProUGUI>();

        // Find the HighScore script if not manually set in the Inspector
        if (highScoreScript == null)
        {
            highScoreScript = Object.FindAnyObjectByType<HighScore>();
        }
    }

    void Update()
    {
        // Update the high score display
        highScoreText.text = "Score: " + highScoreScript.highScore.ToString();
    }
}
