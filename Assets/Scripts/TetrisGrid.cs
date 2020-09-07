using UnityEngine;

public class TetrisGrid : MonoBehaviour
{
    const int TetrisWidth = 10, TetrisHeight = 20;
    public GameObject cellBlockPrefab;
    GameObject[,] cellsGOs;
    MeshRenderer[,] cells;
    float timeSinceLastStep = 0f;
    public Transform[] templatePrefabs;
    TetrisCore tetrisCore;
    TemplatePiece[] templatePieces;
    public DropPieceButton dropPieceButton;
    void Start()
    {
        InitializePrefabTemplatePieces();
        tetrisCore = new TetrisCore(templatePieces);
        InitializeBoard();
    }
    public void MovePiece(Vector2Int offset)
    {
        tetrisCore.MovePiece(offset);
        BoardDisplayUpdate();
    }
    public void MoveRight() { MovePiece(Vector2Int.right); }
    public void MoveLeft() { MovePiece(Vector2Int.left); }
    public void MoveDown() { MovePiece(Vector2Int.down); }
    public void Rotate()
    {
        tetrisCore.RotatePiece();
        BoardDisplayUpdate();
    }
    void Update()
    {
        if (timeSinceLastStep > 1f)
        {
            tetrisCore.StepGame();
            BoardDisplayUpdate();
            timeSinceLastStep = 0f;
        }
        timeSinceLastStep += Time.deltaTime * ComputeGameTimeScale();
    }
    private float ComputeGameTimeScale()
    {

        float gameTimeScale = (dropPieceButton.timePressed > 0.5f ?
                               Mathf.Pow(2f * dropPieceButton.timePressed, 2f) :
                               1f);
        return gameTimeScale;
    }
    private void InitializePrefabTemplatePieces()
    {
        templatePieces = new TemplatePiece[templatePrefabs.Length];
        for (int i = 0; i < templatePieces.Length; i++)
        {
            var template = templatePrefabs[i];
            int bulkSz = template.childCount;
            templatePieces[i].shape = new bool[bulkSz, bulkSz];
            for (int y = 0; y < bulkSz; y++)
                for (int x = 0; x < bulkSz; x++)
                {
                    Transform cell = template.GetChild(x).GetChild(y);
                    
                    if (cell.localScale.x > .95f)
                    {
                        templatePieces[i].shape[x, y] = true;
                        templatePieces[i].color = cell.GetComponent<MeshRenderer>().sharedMaterial.color;
                    }
                }
        }
    }
    private void InitializeBoard()
    {
        cellsGOs = new GameObject[TetrisWidth, TetrisHeight];
        cells = new MeshRenderer[TetrisWidth, TetrisHeight];
        for (int y = 0; y < TetrisHeight; y++)
            for (int x = 0; x < TetrisWidth; x++)
            {
                cellsGOs[x, y] = Instantiate(cellBlockPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                cells[x, y] = cellsGOs[x, y].GetComponent<MeshRenderer>();
                cells[x, y].material = new Material(cells[x, y].material);
            }
    }
    private void BoardDisplayUpdate()
    {
        BoardDisplayClearCells();
        BoardDisplayCurrentPieceProjection();
        BoardDisplayCurrentPiece();
    }
    private void BoardDisplayCurrentPiece()
    {
        Vector2Int piecePos = tetrisCore.PiecePos;
        var n = tetrisCore.CurrPiece.bulkSize;
        for (int y = 0; y < n; y++)
            for (int x = 0; x < n; x++)
            {
                if (tetrisCore.CurrPiece.shape[x, y] && y + piecePos.y < TetrisHeight)
                {
                    var _x = x + piecePos.x; var _y = y + piecePos.y;
                    cells[_x, _y].material.color = tetrisCore.CurrPiece.color;
                    cells[_x, _y].transform.localScale = new Vector3(1, 1, 1) * 1.1f;
                }
            }
    }
    private void BoardDisplayCurrentPieceProjection()
    {
        Vector2Int projPos = tetrisCore.ProjectionPos;
        var n = tetrisCore.CurrPiece.bulkSize;
        for (int y = 0; y < n; y++)
            for (int x = 0; x < n; x++)
            {
                if (tetrisCore.CurrPiece.shape[x, y] && y + projPos.y < TetrisHeight)
                {
                    var _x = x + projPos.x; var _y = y + projPos.y;
                    cells[_x, _y].transform.localScale = new Vector3(1, 1, 1) * 0.6f;
                }
            }
    }
    private void BoardDisplayClearCells()
    {
        for (int y = 0; y < TetrisHeight; y++)
            for (int x = 0; x < TetrisWidth; x++)
            {
                cells[x, y].material.color = tetrisCore.ColourBoard[x, y];
                float scaleFact = tetrisCore.TetrisBoard[x, y] ? 1f : 0.9f;
                cells[x, y].transform.localScale = new Vector3(1, 1, 1) * scaleFact;
            }
    }
}