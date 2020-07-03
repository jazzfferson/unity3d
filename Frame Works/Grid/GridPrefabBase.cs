
using UnityEngine;

public abstract class GridPrefabBase<T> : GridBase<CellPrefabBase<T>> where T : MonoBehaviour
{
    public GridPrefabBase(T prefab, Transform parent, Vector2Int gridSize, float cellSize, Vector3 gridOffset, bool centralizeGrid = true, bool infinityEdges = true) : base(gridSize, cellSize, gridOffset, centralizeGrid, infinityEdges)
    {
        InitializePrefab(prefab, parent);
    }
    protected virtual void InitializePrefab(T prefab, Transform parent)
    {
        for (int i = 0; i < m_CellGridArray.Length; i++)
        {
            m_CellGridArray[i].monoBehaviour = GameObject.Instantiate<T>(prefab, parent);
            m_CellGridArray[i].monoBehaviour.transform.localPosition = m_CellGridArray[i].position;
        }
    }
}
