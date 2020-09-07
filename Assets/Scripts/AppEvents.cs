namespace AppEvent
{
    public enum TetrisGameEvent
    {
        StepGameLoop, BakePiece, ClearRow, GameOver, NextPiece
    }
    public enum TetrisControlEvent
    {
        MoveLeft, MoveRight, Drop, Rotate, Pause, Resume, Restart
    }
}
