using UnityEngine;

public class HighScore : MonoBehaviour
{
    public int highScore = 50000; // Starting score
    public int scoreReductionPerMinute = 1000; // Score reduced every minute

    void Start() // Repeats Reduce score by time every 60 seconds
    {
        InvokeRepeating(nameof(ReduceScoreByTime), 60f, 60f);
    }

    void ReduceScoreByTime() // Time score reduction
    {
        highScore = Mathf.Max(highScore - scoreReductionPerMinute, 0);
        Debug.Log("High Score: " + highScore);
    }

    public void ReduceScoreByMoves(int reductionAmount) // Moves score reduction
    {
        highScore = Mathf.Max(highScore - reductionAmount, 0);
        Debug.Log("High Score after move: " + highScore);
    }
}
