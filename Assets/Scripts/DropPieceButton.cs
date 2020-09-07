using UnityEngine;
using UnityEngine.EventSystems;

public class DropPieceButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    public float timePressed = 0f;
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }
    void Update()
    {
        if (buttonPressed)
            timePressed += Time.deltaTime;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        timePressed = 0f;
    }
}

