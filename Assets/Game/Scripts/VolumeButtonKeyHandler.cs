using UnityEngine;

public class VolumeButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Board board;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnVolumeDownPressed(string empty)
    {
        if (board != null)
        {
            board.HardDropActivePiece();
            Debug.Log("HardDropping");
        }
    }

    public void OnVolumeUpPressed(string empty)
    {
        if(board != null)
        {
            board.RotateActivePiece();
            Debug.Log("Rotated");
        }
    }
}