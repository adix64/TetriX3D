using UnityEngine;
using UnityEngine.EventSystems;

public class DropPieceButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    public float timePressed = 0f;
    private void Start()
    {
        AppEvent.EventSystem<AppEvent.TetrisGameEvent>.Subscribe(
                AppEvent.TetrisGameEvent.BakePiece, ResetTimePressed);
        AppEvent.EventSystem<AppEvent.TetrisControlEvent>.Subscribe(
                AppEvent.TetrisControlEvent.Resume, ResetButton);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }
    void Update()
    {
        if (buttonPressed)
            timePressed += Time.deltaTime;
    }
    void ResetTimePressed()
    {
        timePressed = 0f;
    }
    void ResetButton()
    {
        ResetTimePressed();
        buttonPressed = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        ResetButton();
    }
}

