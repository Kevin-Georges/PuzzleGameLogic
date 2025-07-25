using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform; // Parent object to hold the puzzle pieces
    [SerializeField] private Transform piecePrefab; // Prefab used for individual puzzle pieces

    private List<Transform> pieces; // List to track all puzzle pieces
    private int emptyLocation; // Tracks the location of the empty slot
    private int size; // The grid size based on difficulty
    public bool puzzleComplete = false; // Flag to indicate if the puzzle is complete

    public int moveCount = 0; // Tracks the number of moves made
    public HighScore highScore; // Reference to the HighScore script

    // Enum for puzzle difficulty levels
    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty = Difficulty.Easy; // Default difficulty

    // Creates the puzzle grid with pieces
    private void CreateGamePieces(float gapThickness)
    {
        ClearPreviousPieces(); // Remove existing pieces

        // Set the grid size based on the selected difficulty
        switch (currentDifficulty)
        {
            case Difficulty.Easy: size = 3; break;
            case Difficulty.Medium: size = 4; break;
            case Difficulty.Hard: size = 5; break;
        }

        pieces = new List<Transform>(); // Initialize the list to hold pieces
        float width = 1f / size; // Calculate the size of each tile

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                // Instantiate a new piece and add it to the list
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);

                // Position the piece in the grid
                piece.localPosition = new Vector3(
                    -1 + (2 * width * col) + width, 
                    +1 - (2 * width * row) - width, 
                    0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                // Handle the empty piece
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    // Assign UV mapping for the piece to display the correct texture
                    AssignUV(piece, col, row, width, gapThickness);
                }
            }
        }

        Shuffle(); // Shuffle the puzzle at the start
    }

    // Clears existing puzzle pieces
    private void ClearPreviousPieces()
    {
        if (pieces != null)
        {
            foreach (Transform piece in pieces)
            {
                Destroy(piece.gameObject); // Destroy each piece
            }
        }
    }

    // Assign UV mapping to pieces for texture alignment
    private void AssignUV(Transform piece, int col, int row, float width, float gapThickness)
    {
        float gap = gapThickness / 2;
        Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
        uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
        uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
        uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
        mesh.uv = uv;
    }

    // Initialize the puzzle when the scene starts
    void Start()
    {
        CreateGamePieces(0.01f); // Create the initial puzzle

        // Automatically locate the HighScore script if not manually assigned
        if (highScore == null)
        {
            highScore = Object.FindFirstObjectByType<HighScore>();
        }
    }

    // Update method to handle user input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Shuffle on 'R' key press
        {
            Shuffle();
        }

        if (Input.GetMouseButtonDown(0)) // Handle mouse click on puzzle pieces
        {
            HandlePieceClick();
        }

        if (!puzzleComplete) // Check if the puzzle is completed
        {
            CheckPuzzleCompletion();
        }
    }

    // Handles the movement of puzzle pieces
    private void HandlePieceClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i] == hit.transform)
                {
                    // Attempt to swap the clicked piece with the empty piece
                    if (SwapIfValid(i, -size, size) || 
                        SwapIfValid(i, +size, size) || 
                        SwapIfValid(i, -1, 0) || 
                        SwapIfValid(i, +1, size - 1))
                    {
                        moveCount++; // Increment the move count
                        highScore?.ReduceScoreByMoves(10); // Deduct points for the move
                        break;
                    }
                }
            }
        }
    }

    // Swap pieces if the move is valid
    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            // Swap the positions and update the emptyLocation
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i + offset].localPosition) = 
                (pieces[i + offset].localPosition, pieces[i].localPosition);
            emptyLocation = i;
            return true;
        }
        return false;
    }

    // Shuffle the puzzle and reset the move count
    private void Shuffle()
    {
        int count = 0;
        int last = 0;

        // Perform random swaps to shuffle the puzzle
        while (count < (size * size * size))
        {
            int rnd = Random.Range(0, size * size);
            if (rnd == last) continue;

            last = emptyLocation;
            if (SwapIfValid(rnd, -size, size) || 
                SwapIfValid(rnd, +size, size) || 
                SwapIfValid(rnd, -1, 0) || 
                SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }
        }

        moveCount = 0; // Reset move count
        highScore?.ReduceScoreByMoves(5000); // Deduct points for reshuffling
    }

    // Check if all pieces are in their correct positions
    private void CheckPuzzleCompletion()
    {
        puzzleComplete = true;
        for (int i = 0; i < pieces.Count; i++)
        {
            int row = i / size;
            int col = i % size;

            // Calculate the correct position for each piece
            Vector3 correctPosition = new Vector3(
                -1 + (2f / size * col) + (1f / size),
                1 - (2f / size * row) - (1f / size),
                0);

            if (pieces[i].localPosition != correctPosition)
            {
                puzzleComplete = false;
                break;
            }
        }

        if (puzzleComplete)
        {
            Debug.Log("Puzzle Complete!"); // Notify puzzle completion
        }
    }

    // Update the difficulty and recreate the puzzle
    public void SetDifficulty(Difficulty newDifficulty)
    {
        currentDifficulty = newDifficulty;
        CreateGamePieces(0.01f);
    }
}
