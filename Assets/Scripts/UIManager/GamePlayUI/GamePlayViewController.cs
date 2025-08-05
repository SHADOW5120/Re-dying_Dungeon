using UnityEngine;

public class GamePlayViewController : MonoBehaviour
{
    [SerializeField] private GameObject pauseView;
    [SerializeField] private GameObject winView;
    [SerializeField] private GameObject loseView;

    public void OpenWinView()
    {
        pauseView.SetActive(false);
        winView.SetActive(true);
        loseView.SetActive(false);
    }

    public void OpenLoseView()
    {
        pauseView.SetActive(false);
        winView.SetActive(false);
        loseView.SetActive(true);
    }

    public void OpenPauseView()
    {
        pauseView.SetActive(true);
        winView.SetActive(false);
        loseView.SetActive(false);
    }

    public void OpenGamePlayView()
    {
        pauseView.SetActive(false);
        winView.SetActive(false);
        loseView.SetActive(false);
    }
}
