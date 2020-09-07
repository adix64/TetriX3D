using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppEvent;
public class NextPieceDisplay : MonoBehaviour
{
    const int maxBulkSize = 5;
    private MeshRenderer[,] slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = new MeshRenderer[maxBulkSize, maxBulkSize];
        for (int y = 0; y < maxBulkSize; y++)
            for (int x = 0; x < maxBulkSize; x++)
                    slots[x, y] = transform.GetChild(y).GetChild(x).GetComponent<MeshRenderer>();

        EventSystem<TetrisGameEvent, TemplatePiece>.Subscribe(TetrisGameEvent.NextPiece, DisplayNextPiece);
    }

    private void DisplayNextPiece(TemplatePiece nextPiece)
    {
        for (int y = 0; y < maxBulkSize; y++)
            for (int x = 0; x < maxBulkSize; x++)
                slots[x, y].enabled = false;

        int N = (int)Mathf.Sqrt(nextPiece.shape.Length);
        for (int y = 0; y < N; y++)
            for (int x = 0; x < N; x++)
                if (nextPiece.shape[x, y])
                {
                    slots[x, y].enabled = true;
                    slots[x, y].material.color = nextPiece.color;
                }
    }
}
