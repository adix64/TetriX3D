using UnityEngine;
using AppEvent;
public class UserInfo : MonoBehaviour
{
    int score = 0;
    int bestScore = 0;
    float gamePace = 1f;
    TMPro.TMP_Text[] uiText;
    // uiText[0]==SCORE uiText[1]==BEST uiText[2]==SPEED
    private int decimatedScore = 0;
    void Start()
    {
        uiText = GetComponentsInChildren<TMPro.TMP_Text>();
        EventSystem<TetrisGameEvent>.Subscribe(TetrisGameEvent.ClearRow, UpdateScoreClearRow);
        EventSystem<TetrisGameEvent>.Subscribe(TetrisGameEvent.GameOver, ResetCallback);
        EventSystem<TetrisControlEvent>.Subscribe(TetrisControlEvent.Restart, ResetCallback);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateUItext();
    }
    void ResetCallback()
    {
        score = 0;
        gamePace = 1f;
        UpdateUItext();
    }
    void UpdateScoreClearRow()
    {
        score += 10;
        ComputeGamePace();
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        UpdateUItext();
    }
    void ComputeGamePace()
    {
        if (score > 2000)
            return; // too fast, don't bother
        int newDecimatedScore = score / 30;
        if (newDecimatedScore > decimatedScore)
        {
            float newGamePace = 1f + ((float)newDecimatedScore * .2f);
            EventSystem<TetrisGameEvent, float>.TriggerEvent(TetrisGameEvent.SetGamePace, newGamePace);
            gamePace = newGamePace;
            decimatedScore = newDecimatedScore;
            UpdateUItext();
        }
    }
    private void UpdateUItext()
    {
        uiText[0].text = "<size=60%>SCORE</size>\n" + score.ToString();
        uiText[1].text = "<size=60%>BEST</size>\n" + bestScore.ToString();
        uiText[2].text = "<size=60%>SPEED</size>\n" + string.Format("{0:0.0} x", gamePace);
    }

}
