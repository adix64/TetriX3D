using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    private TemplatePiece[] templatePieces;
    private bool gamePaused = false;
    float gameSpeed = 1f;
    void Start()
    {
        RegisterControlEvents();
        InitializeTemplatePiecesFromPrefabs();
        tetrisCore = new TetrisCore(templatePieces);
        InitializeBoard();
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
        timeSinceLastStep += Time.deltaTime
                           * ComputeGameTimeScale();
        AnimateBoard();
    }
    public void MovePiece(Vector2Int offset)
    {
        tetrisCore.MovePiece(offset);
        BoardDisplayStepUpdate();
    }
    public void Rotate() { tetrisCore.RotatePiece(); BoardDisplayStepUpdate(); }
    
    private float ComputeGameTimeScale()
    {
        if (gamePaused)
            return 0f;
        float gameTimeScale = (dropPieceButtonNFO.timePressed > 0.5f
                            ? Mathf.Pow(4f * dropPieceButtonNFO.timePressed, 2f)
                            : 1f);
        gameTimeScale *= gameSpeed;
        return gameTimeScale;
    }
    private void RegisterControlEvents()
    {
        EventSystem<TetrisControlEvent>.Subscribe(TetrisControlEvent.Pause, PauseGame);
        EventSystem<TetrisControlEvent>.Subscribe(TetrisControlEvent.Resume, ResumeGame);
        EventSystem<TetrisControlEvent>.Subscribe(TetrisControlEvent.Restart, RestartGame);
        EventSystem<TetrisGameEvent, float>.Subscribe(TetrisGameEvent.SetGamePace, SetGamePace);
        RegisterButtonActions();
    }
    private void SetGamePace(float speed) { gameSpeed = speed; }
    private void PauseGame() { gamePaused = true; }
    private void ResumeGame() { gamePaused = false; }
    void RestartGame() { gamePaused = false; gameSpeed = 1f; }
}