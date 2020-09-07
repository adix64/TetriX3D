using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppEvent;
public class GameUI : MonoBehaviour
{
    public GameObject gamePausePanel;
    public GameObject inGameUIPanel;
    public GameObject gameOverPanel;
    public GameObject pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem<TetrisGameEvent>.Subscribe(TetrisGameEvent.GameOver, GameOver);
    }

    public void RestartGame()
    {
        EventSystem<TetrisControlEvent>.TriggerEvent(TetrisControlEvent.Restart);
        EventSystem<TetrisControlEvent>.TriggerEvent(TetrisControlEvent.Resume);
        SetUIvisibility(true, false, false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame()
    {
        EventSystem<TetrisControlEvent>.TriggerEvent(TetrisControlEvent.Pause);
        SetUIvisibility(false, true, false);
    }
    public void ResumeGame()
    {
        EventSystem<TetrisControlEvent>.TriggerEvent(TetrisControlEvent.Resume);
        SetUIvisibility(true, false, false);
    }
    public void GameOver()
    {
        EventSystem<TetrisControlEvent>.TriggerEvent(TetrisControlEvent.Pause);
        SetUIvisibility(false, false, true);
    }
    private void SetUIvisibility(bool gameGui, bool gamePause, bool gameOver)
    {
        inGameUIPanel.SetActive(gameGui);
        gamePausePanel.SetActive(gamePause);
        gameOverPanel.SetActive(gameOver);
    }
}
