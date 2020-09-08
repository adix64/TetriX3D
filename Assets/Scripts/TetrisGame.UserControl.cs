using UnityEngine;
using AppEvent;
public partial class TetrisGame : MonoBehaviour
{
    private TetrisButton.TetrisButtonInfo dropPieceButtonNFO;
    private float timeThresholdDirBtn = 0.5f, timeSinceLastMove = 0f;
    delegate void MoveToDirDelegate();
    private void RegisterButtonActions()
    {
        dropPieceButtonNFO = new TetrisButton.TetrisButtonInfo();
        EventSystem<TetrisControlEvent, TetrisButton.TetrisButtonInfo>.Subscribe(
               TetrisControlEvent.ButtonDown, ButtonDown);
        EventSystem<TetrisControlEvent, TetrisButton.TetrisButtonInfo>.Subscribe(
            TetrisControlEvent.ButtonPressed, ButtonPressed);
        EventSystem<TetrisControlEvent, TetrisButton.TetrisButtonInfo>.Subscribe(
           TetrisControlEvent.ButtonUp, ButtonUp);
    }
    private void MoveRight() { MovePiece(Vector2Int.right); }
    private void MoveLeft() { MovePiece(Vector2Int.left); }
    private void MoveDown() { MovePiece(Vector2Int.down); }

    private void ButtonDown(TetrisButton.TetrisButtonInfo buttonInfo)
    {
        if (buttonInfo.buttonAction == TetrisButtonAction.MoveLeft)
        {
            ResetDirBtn();
            MoveLeft();
        }
        else
        if (buttonInfo.buttonAction == TetrisButtonAction.MoveRight)
        {
            ResetDirBtn();
            MoveRight();
        }
        else
        if (buttonInfo.buttonAction == TetrisButtonAction.Drop)
            dropPieceButtonNFO = buttonInfo;
    }
    private void ButtonPressed(TetrisButton.TetrisButtonInfo buttonInfo)
    {
        if (buttonInfo.buttonAction == TetrisButtonAction.MoveLeft)
            MoveToDir(MoveLeft);
        else
        if (buttonInfo.buttonAction == TetrisButtonAction.MoveRight)
            MoveToDir(MoveRight);
        else
        if (buttonInfo.buttonAction == TetrisButtonAction.Drop)
            dropPieceButtonNFO = buttonInfo;
    }
    private void MoveToDir(MoveToDirDelegate moveAction)
    {
        if (timeSinceLastMove > timeThresholdDirBtn)
        {
            if (timeThresholdDirBtn > 0.1f)
                timeThresholdDirBtn *= 0.5f;
            moveAction();
        }
        timeSinceLastMove += Time.deltaTime;
    }
    private void ButtonUp(TetrisButton.TetrisButtonInfo buttonInfo)
    {
        if (buttonInfo.buttonAction == TetrisButtonAction.MoveLeft ||
            buttonInfo.buttonAction == TetrisButtonAction.MoveRight)
            ResetDirBtn();
        else
        if (buttonInfo.buttonAction == TetrisButtonAction.Drop)
            dropPieceButtonNFO = buttonInfo;
    }
    private void ResetDirBtn()
    {
            timeSinceLastMove = 0f;
            timeThresholdDirBtn = gameSpeed * 0.5f;
    }
}