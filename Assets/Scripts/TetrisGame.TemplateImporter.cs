using UnityEngine;
using AppEvent;
public partial class TetrisGame : MonoBehaviour
{
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

}