using UnityEngine;

public partial class TetrisGame : MonoBehaviour
{
    private void BoardDisplayStepUpdate()
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
                var _x = x + piecePos.x; var _y = y + piecePos.y;
                if (tetrisCore.CurrPiece.shape[x, y] && _y < TetrisHeight)
                {
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
                var _x = x + projPos.x; var _y = y + projPos.y;
                if (tetrisCore.CurrPiece.shape[x, y] && _y < TetrisHeight)
                    cells[_x, _y].transform.localScale = new Vector3(1, 1, 1) * 0.6f;
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
    private void AnimateBoard()
    {
        AnimateStaticPieces();
        AnimateProjection();
        UnAnimatePiece();
    }

    private void AnimateStaticPieces()
    {
        for (int y = 0; y < TetrisHeight; y++)
            for (int x = 0; x < TetrisWidth; x++)
                if (!tetrisCore.TetrisBoard[x, y])
                {
                    float yR;
                    yR = Mathf.Sin(Time.time * 5f + x + y) * 4f;
                    cellTransforms[x, y].rotation = Quaternion.Euler(0, 180 + yR, 0);
                }
                else
                {
                    cellTransforms[x, y].rotation = Quaternion.Euler(0, 180, 0);
                }
    }

    private void AnimateProjection()
    {
        Vector2Int projPos = tetrisCore.ProjectionPos;
        var n = tetrisCore.CurrPiece.bulkSize;
        for (int y = 0; y < n; y++)
            for (int x = 0; x < n; x++)
            {
                var _x = x + projPos.x; var _y = y + projPos.y;
                if (tetrisCore.CurrPiece.shape[x, y] && _y < TetrisHeight)
                {
                    cells[_x, _y].material.color = Color.white * 0.5f;
                    float xR = Mathf.Sin(Time.time * 50f + x + y) * 30f;
                    float yR = Mathf.Sin(Time.time * 20f + x + y ) * 30f;
                    cellTransforms[_x, _y].rotation = Quaternion.Euler(xR, 180 + yR, 0);
                }
            }
    }

    private void UnAnimatePiece()
    {
        Vector2Int piecePos = tetrisCore.PiecePos;
        var n = tetrisCore.CurrPiece.bulkSize;
        for (int y = 0; y < n; y++)
            for (int x = 0; x < n; x++)
            {
                var _x = x + piecePos.x; var _y = y + piecePos.y;
                if (tetrisCore.CurrPiece.shape[x, y] && _y < TetrisHeight)
                {
                    cells[_x, _y].material.color = tetrisCore.CurrPiece.color;
                    cellTransforms[_x, _y].rotation = Quaternion.Euler(0, 180, 0);
                }
            }
    }
}