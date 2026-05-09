using UnityEngine;

public class AccelerometerHandler : MonoBehaviour
{
    [SerializeField]
    private Board board;
    public Vector3 lastAcceleration { get; private set; } = Vector3.zero;
    public Vector3 currentAcceleration { get; private set; } = Vector3.zero;
    public Vector3 smoothedAcceleration { get; private set; } = Vector3.zero;

    [Header("Настройки фильтра")]
    [Range(0.01f, 1f)] public float smoothing = 0.15f;        
    [Range(0.1f, 2f)] public float sensitivity = 1.2f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnAccelerometerChanged(string data)
    {
        string[] values = data.Split(',');
        if (values.Length == 3)
        {
            float x = ParseFloat(values[0]);
            float y = ParseFloat(values[1]);
            float z = ParseFloat(values[2]);

            Vector3 rawAccel = new Vector3(-x, y, z) * sensitivity;
            smoothedAcceleration = Vector3.Lerp(smoothedAcceleration, rawAccel, smoothing);

            currentAcceleration = smoothedAcceleration;
        }

        if(currentAcceleration.x == lastAcceleration.x) return;
        lastAcceleration = currentAcceleration;

        if(board != null)
        {
            board.MoveActivePiece(currentAcceleration.x);
        }
    }

    private float ParseFloat(string s)
    {
        s = s.Trim().Replace(',', '.');           
        if (float.TryParse(s, System.Globalization.NumberStyles.Float, 
                           System.Globalization.CultureInfo.InvariantCulture, out float result))
        {
            return result;
        }
        return 0f;
    }
}
