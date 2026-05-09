using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverBackground;
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private GameObject restartButton;
    
    void Awake()
    {
        HideGameOverScreen();
    }

    public void ShowGameOverScreen()
    {
        gameOverBackground.SetActive(true);
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
    }

    public void HideGameOverScreen()
    {
        gameOverBackground.SetActive(false);
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
    }
}
