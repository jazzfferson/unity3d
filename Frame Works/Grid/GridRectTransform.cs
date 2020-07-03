using UnityEngine;

public class GridRectTransform<T> : GridPrefabBase<T> where T : MonoBehaviour
{
    public GridRectTransform(T prefab, Transform parent, Vector2Int gridSize, float cellSize, Vector3 gridOffset, bool centralizeGrid = true, bool infinityEdges = true) : base(prefab, parent, gridSize, cellSize, gridOffset, centralizeGrid, infinityEdges)
    {

    }
    protected override void InitializePrefab(T prefab, Transform parent)
    {

        for (int i = 0; i < m_CellGridArray.Length; i++)
        {
            m_CellGridArray[i].monoBehaviour = GameObject.Instantiate<T>(prefab, parent);
            m_CellGridArray[i].monoBehaviour.GetComponent<RectTransform>().anchoredPosition3D = m_CellGridArray[i].position;
        }
    }
}
