using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GridBase<T> where T : Cell, new()
{
    protected T[] m_CellGridArray;
    public T[] cellGridArray => m_CellGridArray;
    private float m_CellSize;
    public float cellSize => m_CellSize;
    private Vector2Int m_gridSize;
    public Vector2Int gridSize => m_gridSize;
    private Vector3 m_GridOffset;
    public Vector3 gridOffset => m_GridOffset;
    private bool m_CentralizeGrid = true;

    public GridBase(Vector2Int gridSize, float cellSize, Vector3 gridOffset, bool centralizeGrid = true, bool infinityEdges = true)
    {
        this.m_CentralizeGrid = centralizeGrid;
        this.m_CellSize = cellSize;
        this.m_gridSize = gridSize;
        this.m_GridOffset = gridOffset;
        this.m_CellGridArray = new T[gridSize.x * gridSize.y];

        //grid cell
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                //coordinate, position
                Vector2Int index = new Vector2Int(x, y);
                Vector3 position = GridToPosition(gridSize, index);

                T cell = new T();
                cell.index = index;
                cell.position = position;
                m_CellGridArray[BiToOneDimensionalIterator(x, y)] = cell;
            }
        }

        //cell edges
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                T cell = m_CellGridArray[BiToOneDimensionalIterator(x, y)];

                var upCoord = new Vector2Int(x, y - 1);
                var downCoord = new Vector2Int(x, y + 1);
                var leftCoord = new Vector2Int(x - 1, y);
                var rightCoord = new Vector2Int(x + 1, y);
                Vector2Int sharedCellIndex;

                if (infinityEdges)
                {
                    sharedCellIndex = GetWrappedCoordinate(upCoord);
                    cell.edges.up = m_CellGridArray[BiToOneDimensionalIterator(sharedCellIndex.x, sharedCellIndex.y)];

                    sharedCellIndex = GetWrappedCoordinate(downCoord);
                    cell.edges.down = m_CellGridArray[BiToOneDimensionalIterator(sharedCellIndex.x, sharedCellIndex.y)];

                    sharedCellIndex = GetWrappedCoordinate(leftCoord);
                    cell.edges.left = m_CellGridArray[BiToOneDimensionalIterator(sharedCellIndex.x, sharedCellIndex.y)];

                    sharedCellIndex = GetWrappedCoordinate(rightCoord);
                    cell.edges.right = m_CellGridArray[BiToOneDimensionalIterator(sharedCellIndex.x, sharedCellIndex.y)];
                }
                else
                {
                    if (IsInsideRangeCoordinate(upCoord))
                        cell.edges.up = m_CellGridArray[BiToOneDimensionalIterator(upCoord.x, upCoord.y)];
                    else
                        cell.edges.up = null;

                    if (IsInsideRangeCoordinate(downCoord))
                        cell.edges.down = m_CellGridArray[BiToOneDimensionalIterator(downCoord.x, downCoord.y)];
                    else
                        cell.edges.down = null;

                    if (IsInsideRangeCoordinate(leftCoord))
                        cell.edges.left = m_CellGridArray[BiToOneDimensionalIterator(leftCoord.x, leftCoord.y)];
                    else
                        cell.edges.left = null;

                    if (IsInsideRangeCoordinate(rightCoord))
                        cell.edges.right = m_CellGridArray[BiToOneDimensionalIterator(rightCoord.x, rightCoord.y)];
                    else
                        cell.edges.right = null;
                }

            }
        }
    }

    private bool IsInsideRangeCoordinate(Vector2Int coordinate)
    {
        if (coordinate.x < 0 || coordinate.x >= m_gridSize.x || coordinate.y < 0 || coordinate.y >= m_gridSize.y)
            return false;
        else
            return true;
    }

    public Vector3 GridToPosition(Vector2Int boardLenght, Vector2Int coordinate)
    {
        Vector3 boardSize = new Vector3(boardLenght.x * m_CellSize, boardLenght.y * m_CellSize);
        Vector3 position = new Vector3(coordinate.x * m_CellSize, coordinate.y * m_CellSize);

        if (m_CentralizeGrid)
        {
            return (position - new Vector3(boardSize.x / 2, boardSize.y / 2) + (new Vector3(m_CellSize, m_CellSize) * 0.5f)) + m_GridOffset;
        }
        else { return position + m_GridOffset; }
    }

    public T GetCell(Vector2Int coord)
    {
        //Debug.Log($"GetTile Index: {index}");
        return m_CellGridArray[BiToOneDimensionalIterator(coord.x, coord.y)];
    }

    public T GetWrappedCell(Vector2Int coord)
    {
        // Debug.Log($"GetWrappedTile Index: {index}");
        return GetCell(GetWrappedCoordinate(coord));
    }

    public Vector2Int GetWrappedCoordinate(Vector2Int index)
    {
        Vector2Int newIndex = new Vector2Int
        {
            x = (int)Mathf.Repeat(index.x, m_gridSize.x),
            y = (int)Mathf.Repeat(index.y, m_gridSize.y)
        };
        //Debug.Log($"GetWrappedBoardCoordinate Index: {newIndex}");
        return newIndex;
    }

    public virtual Bounds GetBounds(float sizeOffset)
    {
        float xSize = (m_gridSize.x * m_CellSize) + sizeOffset;
        float ySize = (m_gridSize.y * m_CellSize) + sizeOffset;
        return new Bounds(new Vector3(-m_CellSize * .5f, -m_CellSize * .5f), new Vector3(xSize, ySize));
    }

    public int BiToOneDimensionalIterator(int x, int y)
    {
        return BiToOneDimensionalIterator(new Vector2Int(x, y));
    }
    public int BiToOneDimensionalIterator(Vector2Int index)
    {
        return index.y * m_gridSize.x + index.x;
    }
}
