
public enum ECellEdge { Top, Bottom, Left, Right, None };


public struct Edges
{
    public Cell up;
    public Cell down;
    public Cell right;
    public Cell left;

    public static ECellEdge GetOppositeEdge(ECellEdge edge)
    {
        switch (edge)
        {
            case ECellEdge.Top:
                return ECellEdge.Bottom;
            case ECellEdge.Bottom:
                return ECellEdge.Top;
            case ECellEdge.Left:
                return ECellEdge.Right;
            case ECellEdge.Right:
                return ECellEdge.Left;
            default:
                return ECellEdge.None;
        }
    }
}