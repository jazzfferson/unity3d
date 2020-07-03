public class Cell
{
    public Vector2Int index;
    public Vector3 position;
    public Edges edges;
    public Cell GetEdgeCell(ECellEdge edge)
    {
        switch (edge)
        {
            case ECellEdge.Top:
                return edges.up;
            case ECellEdge.Bottom:
                return edges.down;
            case ECellEdge.Left:
                return edges.left;
            case ECellEdge.Right:
                return edges.right;
            default:
                return null;
        }
    }
}