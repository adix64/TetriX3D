using UnityEngine;
struct TemplatePiece
{
    public Color color;
    public bool[,] shape;
};
class TetrisPiece
{
    public int bulkSize = 3;
    const int maxBulkSize = 5;
    public Color color = Color.white;
    public bool[,] shape;
    public TetrisPiece()
    {
        shape = new bool[maxBulkSize, maxBulkSize];
        shape[0, 0] = shape[0, 1] = shape[1, 1] = true;
    }
    public void Reset(TemplatePiece template)
    {
        color = template.color;
        bulkSize = (int)Mathf.Sqrt(template.shape.Length);
        for (int y = 0; y < bulkSize; y++)
            for (int x = 0; x < bulkSize; x++)
                shape[x, y] = template.shape[x, y];
    }
    private void RotateCCWcycle(int x, int y)
    {
        bool temp = shape[x, y];
        shape[x, y] = shape[y, bulkSize - 1 - x];
        shape[y, bulkSize - 1 - x] = shape[bulkSize - 1 - x, bulkSize - 1 - y];
        shape[bulkSize - 1 - x, bulkSize - 1 - y] = shape[bulkSize - 1 - y, x];
        shape[bulkSize - 1 - y, x] = temp;
    }
    private void RotateCWcycle(int x, int y)
    {
        bool temp = shape[bulkSize - 1 - y, x];
        shape[bulkSize - 1 - y, x] = shape[bulkSize - 1 - x, bulkSize - 1 - y];
        shape[bulkSize - 1 - x, bulkSize - 1 - y] = shape[y, bulkSize - 1 - x];
        shape[y, bulkSize - 1 - x] = shape[x, y];
        shape[x, y] = temp;
    }
    public void RotateCW()
    {
        for (int x = 0; x < bulkSize / 2; x++)
            for (int y = x; y < bulkSize - x - 1; y++)
                RotateCWcycle(x, y);
    }
    public void RotateCCW()
    {
        for (int x = 0; x < bulkSize / 2; x++)
            for (int y = x; y < bulkSize - x - 1; y++)
                RotateCCWcycle(x, y);
    }
};