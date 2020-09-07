using UnityEngine;
using AppEvent;
public partial class TetrisGame : MonoBehaviour
{
    const int TetrisWidth = 10, TetrisHeight = 20;
    public GameObject cellBlockPrefab;
    Transform[,] cellTransforms;
    MeshRenderer[,] cells;
    float timeSinceLastStep = 0f;
    public Transform[] templatePrefabs;
    TetrisCore tetrisCore;
    TemplatePiece[] templatePieces;
    public DropPieceButton dropPieceButton;
    bool gamePaused = false;
    float gameSpeed = 1f;
    void Start()
    {
        RegisterControlEvents();
        InitializeTemplatePiecesFromPrefabs();
        tetrisCore = new TetrisCore(templatePieces);
        InitializeBoard();
        BoardDisplayStepUpdate();
    }
    
    public void MovePiece(Vector2Int offset)
    {
        tetrisCore.MovePiece(offset);
        BoardDisplayStepUpdate();
    }
    public void MoveRight() { MovePiece(Vector2Int.right); }
    public void MoveLeft() { MovePiece(Vector2Int.left); }
    public void MoveDown() { MovePiece(Vector2Int.down); }
    public void Rotate()
    {
        tetrisCore.RotatePiece();
        BoardDisplayStepUpdate();
    }
    void Update()
    {
        if (timeSinceLastStep > 1f)
        {
            tetrisCore.StepGame();
            BoardDisplayStepUpdate();
            timeSinceLastStep = 0f;
        }
        timeSinceLastStep += Time.deltaTime * ComputeGameTimeScale();
        AnimateBoard();
    }
    private float ComputeGameTimeScale()
    {
        if (gamePaused)
            return 0f;
        float gameTimeScale = (dropPieceButton.timePressed > 0.5f ?
                               Mathf.Pow(4f * dropPieceButton.timePressed, 2f) :
                               1f);
        gameTimeScale *= gameSpeed;
        return gameTimeScale;
    }
    private void RegisterControlEvents()
    {
        EventSystem<TetrisControlEvent>.Subscribe(TetrisControlEvent.Pause, PauseGame);
        EventSystem<TetrisControlEvent>.Subscribe(TetrisControlEvent.Resume, ResumeGame);
    }

    private void PauseGame()
    {
        gamePaused = true;
    }
    private void ResumeGame()
    {
        gamePaused = false;
    }
    private void InitializeTemplatePiecesFromPrefabs()
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
                    if (cell.localScale.x > .95f) //smaller cells represent empty space within the bulk
                    {
                        templatePieces[i].shape[x, y] = true;
                        templatePieces[i].color = cell.GetComponent<MeshRenderer>().sharedMaterial.color;
                    }
                }
        }
    }
    private void InitializeBoard()
    {
        cellTransforms = new Transform[TetrisWidth, TetrisHeight];
        cells = new MeshRenderer[TetrisWidth, TetrisHeight];
        for (int y = 0; y < TetrisHeight; y++)
            for (int x = 0; x < TetrisWidth; x++)
            {
                cellTransforms[x, y] = Instantiate(cellBlockPrefab, new Vector3(x, y, 0),
                                        Quaternion.identity, transform).transform;
                cells[x, y] = cellTransforms[x, y].GetComponent<MeshRenderer>();
                cells[x, y].material = new Material(cells[x, y].material);
            }
    }
}