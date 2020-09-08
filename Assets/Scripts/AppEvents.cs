namespace AppEvent
{
    public enum TetrisGameEvent
    {
        StepGameLoop, BakePiece, ClearRow, GameOver, NextPiece, SetGamePace
    }
    public enum TetrisControlEvent
    {
        ButtonDown, ButtonUp, ButtonPressed, Pause, Resume, Restart
    }
    public enum TetrisButtonAction
    {
        MoveLeft, MoveRight, Drop, Rotate
    }
}
