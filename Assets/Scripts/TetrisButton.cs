using UnityEngine;
using UnityEngine.EventSystems;
using AppEvent;
public class TetrisButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [System.Serializable]
    public class TetrisButtonInfo
    {
        public float timePressed = 0f;
        public bool buttonPressed = false;
        public TetrisButtonAction buttonAction = TetrisButtonAction.Drop;
    };
    [SerializeField]
    public TetrisButtonInfo buttonInfo;
    private void Start()
    {
        EventSystem<TetrisGameEvent>.Subscribe(TetrisGameEvent.BakePiece, ResetButton);
        EventSystem<TetrisControlEvent>.Subscribe(TetrisControlEvent.Resume, ResetButton);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonInfo.buttonPressed = true;
        EventSystem<TetrisControlEvent, TetrisButtonInfo>.TriggerEvent(
                            TetrisControlEvent.ButtonDown, buttonInfo);
    }
    void Update()
    {
        if (buttonInfo.buttonPressed)
        {
            buttonInfo.timePressed += Time.deltaTime;
            EventSystem<TetrisControlEvent, TetrisButtonInfo>.TriggerEvent(
                                TetrisControlEvent.ButtonPressed, buttonInfo);
        }
    }
    void ResetTimePressed()
    {
        buttonInfo.timePressed = 0f;
    }
    void ResetButton()
    {
        ResetTimePressed();
        buttonInfo.buttonPressed = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        ResetButton();
        EventSystem<TetrisControlEvent, TetrisButtonInfo>.TriggerEvent(
                            TetrisControlEvent.ButtonUp, buttonInfo);
    }
}

