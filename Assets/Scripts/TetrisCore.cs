using UnityEngine;
using AppEvent;
class TetrisCore
{
    const int maxPiecePadding = 4;
    private int TetrisWidth, TetrisHeight;
    public TetrisPiece CurrPiece{ get; private set;}
    public TetrisPiece NextPiece{ get; private set;}
    public Vector2Int PiecePos { get; private set;}
    public Vector2Int ProjectionPos { get; private set;}
    public bool[,] TetrisBoard { get; private set; }
    public Color[,] ColourBoard { get; private set; }
    private TemplatePiece[] templatePieces;
    private int nextPieceTemplateIndex = 0;
    bool loseState = true;
    public TetrisCore(TemplatePiece[] pTemplatePieces, int tw = 10, int th = 20)
    {
        TetrisWidth = tw;
        TetrisHeight = th;
        TetrisBoard = new bool [TetrisWidth, TetrisHeight];
        ColourBoard = new Color[TetrisWidth, TetrisHeight];
        templatePieces = pTemplatePieces;
        CurrPiece = new TetrisPiece();
        NextPiece = new TetrisPiece();
        ClearBoard();
        EventSystem<TetrisControlEvent>.Subscribe(TetrisControlEvent.Restart, ClearBoard);
    }
    private void ClearBoard()
    {
        for (int y = 0; y < TetrisHeight; y++)
            for (int x = 0; x < TetrisWidth; x++)
            {
                TetrisBoard[x, y] = false;
                ColourBoard[x, y] = Color.black;
            }
        GeneratePiece();
    }

    public void StepGame()
    {
        PiecePos += Vector2Int.down;
        if (!ValidBoard(PiecePos))
        {
            PiecePos += Vector2Int.up;
            if (!BakePieceOnBoard())
            {
                EventSystem<TetrisGameEvent>.TriggerEvent(TetrisGameEvent.GameOver);
                return;
            }
            ClearRows();
            GeneratePiece();
        }
        ComputeProjection();
    }
    private void ComputeProjection()
    {
        for (int row = PiecePos.y; row >= -maxPiecePadding; row--)
        {
            ProjectionPos = new Vector2Int(PiecePos.x, row);
            if (!ValidBoard(ProjectionPos))
            {
                ProjectionPos = new Vector2Int(PiecePos.x, row+1);
                break;
            }
        }
    }
    public void MovePiece(Vector2Int offset)
    {
        PiecePos += offset;
        if (!ValidBoard(PiecePos))
            PiecePos -= offset;
        ComputeProjection();
    }
    public void RotatePiece()
    {
        CurrPiece.RotateCW();
        if (!ValidBoard(PiecePos))
        {//maybe rotation is valid if we shift either left...
            PiecePos += Vector2Int.left;
            if (!ValidBoard(PiecePos)) //...or right
                PiecePos += 2 * Vector2Int.right;

            if (!ValidBoard(PiecePos))
            {//the rotation is invalid, so revert
                PiecePos += Vector2Int.left;
                CurrPiece.RotateCCW();
            }
        }
        ComputeProjection();
    }
    private void GeneratePiece()
    {
        CurrPiece.Reset(templatePieces[nextPieceTemplateIndex]);
        nextPieceTemplateIndex = Random.Range(0, templatePieces.Length);
        NextPiece.Reset(templatePieces[nextPieceTemplateIndex]);
        EventSystem<TetrisGameEvent, TemplatePiece>.TriggerEvent(TetrisGameEvent.NextPiece,
                        templatePieces[nextPieceTemplateIndex]);
        PiecePos = new Vector2Int(5, 20);
        ComputeProjection();
    }
    private bool ValidBoard(Vector2Int pos)
    {
        for (int y = 0; y < CurrPiece.bulkSize; y++)
            for (int x = 0; x < CurrPiece.bulkSize; x++)
                if (CurrPiece.shape[x, y])
                {
                    Vector2Int cellPos = pos + new Vector2Int(x, y);
                    if (cellPos.x < 0 || cellPos.y < 0 || cellPos.x >= TetrisWidth ||
                        cellPos.y < TetrisHeight && TetrisBoard[cellPos.x, cellPos.y])
                        return false;
                }
        return true;
    }
    private bool BakePieceOnBoard()
    {
        EventSystem<TetrisGameEvent>.TriggerEvent(TetrisGameEvent.BakePiece);
        for (int y = 0; y < CurrPiece.bulkSize; y++)
            for (int x = 0; x < CurrPiece.bulkSize; x++)
                if (CurrPiece.shape[x, y])
                {
                    var cellPos = PiecePos + new Vector2Int(x, y);
                    if (cellPos.y >= TetrisHeight)
                        return false;
                    TetrisBoard[cellPos.x, cellPos.y] = true;
                    ColourBoard[cellPos.x, cellPos.y] = CurrPiece.color;
                }
        return true;
    }
    private void ClearRows()
    {
        var startRow = Mathf.Min(Mathf.Max(0, PiecePos.y),     TetrisHeight - 1);
        var endRow = Mathf.Min(startRow + CurrPiece.bulkSize,  TetrisHeight - 1);
        for (int row = startRow; row < endRow; row++)
            while (IsRowFull(row))
                ClearRow(row);
    }
    private void ClearRow(int row)
    {
        for (int y = row; y < TetrisHeight - 1; y++)
            for (int x = 0; x < TetrisWidth; x++)
            {
                TetrisBoard[x, y] = TetrisBoard[x, y + 1];
                ColourBoard[x, y] = ColourBoard[x, y + 1];
            }
    }
    private bool IsRowFull(int row)
    {
        int cellCountOnRow = 0;
        for (int x = 0; x < TetrisWidth; x++)
            cellCountOnRow += TetrisBoard[x, row] ? 1 : 0;
        return (cellCountOnRow == TetrisWidth);
    }
};