using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Piece : MonoBehaviour
{
    public Board board {get; private set;}
    public TetrominoData data {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public Vector3Int position {get; private set;}
    private InputAction movementAction;
    private Vector2 currentInput;
    private InputAction dropAction;

    public void Awake()
    {
        movementAction = InputSystem.actions.FindAction("Move");
        dropAction = InputSystem.actions.FindAction("Jump");
    }

    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        if(this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    public void Update()
    {
        this.board.Clear(this);
        currentInput = movementAction.ReadValue<Vector2>();

        if (currentInput.x < 0)
        {
            Move(Vector2Int.left);
            Debug.Log("LEFTTT");
        }
        else if (currentInput.x > 0)
        {
            Move(Vector2Int.right);
            Debug.Log("RIGHT");
        }

        if (currentInput.y < 0)
        {
            Move(Vector2Int.down);
        }

        if (dropAction.WasPerformedThisFrame())
        {
            HardDrop();
        }

        this.board.Set(this);
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid)
        {
            this.position = newPosition;
        }

        return valid;
    }
}
