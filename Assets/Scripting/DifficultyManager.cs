using UnityEngine;
public class DifficultyManager : MonoBehaviour {
    public static DifficultyManager Instance { get; private set; }
    public GameManager.Difficulty SelectedDifficulty { get; private set; } = GameManager.Difficulty.Easy;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make it persist across scenes
        } else {
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(GameManager.Difficulty difficulty) {
        SelectedDifficulty = difficulty;
    }
}
