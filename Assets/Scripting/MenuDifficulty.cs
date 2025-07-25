using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
    public void SetEasy() 
    {
        DifficultyManager.Instance.SetDifficulty(GameManager.Difficulty.Easy);
        Debug.Log("Difficulty set to Easy");
    }

    public void SetMedium() 
    {
        DifficultyManager.Instance.SetDifficulty(GameManager.Difficulty.Medium);
        Debug.Log("Difficulty set to Medium");
    }

    public void SetHard() 
    {
        DifficultyManager.Instance.SetDifficulty(GameManager.Difficulty.Hard);
        Debug.Log("Difficulty set to Hard");
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("Puzzle");
    }
}
