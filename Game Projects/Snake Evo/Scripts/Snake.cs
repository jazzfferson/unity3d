using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{

    [SerializeField]
    private Gradient gradientSpeedSnake;

    [SerializeField]
    private int bodySize;

    [SerializeField]
    private GameObject snakeTilePrefab;

    public List<TileSnake> bodyTile;
    private TileBase[,] _board;



    public int headXIndex;
    public int headYIndex;

    private bool _isSpeedMode = false;

    public void Initialize(TileBase[,] board)
    {

        _board = board;

        bodyTile = new List<TileSnake>();

        for (int i = 0; i < bodySize; i++)
        {
            bodyTile.Add(((GameObject)Instantiate(snakeTilePrefab, board[headXIndex, headYIndex + i].transform.position, Quaternion.identity)).GetComponent<TileSnake>());
            bodyTile[i].xIndex = headXIndex;
            bodyTile[i].yIndex = headYIndex + i;
        }
    }

    public void SetPosition(int newHeadXIndex, int newHeadYIndex)
    {

        headXIndex = newHeadXIndex;
        headYIndex = newHeadYIndex;

        for (int i = bodyTile.Count - 1; i > 0; i--)
        {
            bodyTile[i].transform.position = bodyTile[i - 1].transform.position;
            bodyTile[i].xIndex = bodyTile[i - 1].xIndex;
            bodyTile[i].yIndex = bodyTile[i - 1].yIndex;

        }

        bodyTile[0].transform.position = _board[headXIndex, headYIndex].transform.position;
        bodyTile[0].xIndex = headXIndex;
        bodyTile[0].yIndex = headYIndex;
    }

    public void AddTile(int number)
    {
        for (int i = 0; i < number; i++)
        {
            bodyTile.Add(((GameObject)Instantiate(snakeTilePrefab, bodyTile[bodyTile.Count - 1].transform.position, Quaternion.identity)).GetComponent<TileSnake>());
        }

    }

    public void RemoveTile(int number)
    {
        int quant = (bodyTile.Count - 1) - number;

        for (int i = bodyTile.Count - 1; i > quant; i--)
        {
            GameObject tile = bodyTile[i].gameObject;
            bodyTile.RemoveAt(i);
            Destroy(tile);
        }
    }

    public void SnakeBodyColor(Color color)
    {
        for (int i = 0; i < bodyTile.Count; i++)
        {
            bodyTile[i].spriteRenderer.color = color;
        }
    }

    void Update()
    {
        if (_isSpeedMode)
        {
            for (int i = 1; i < bodyTile.Count; i++)
            {
                bodyTile[i].spriteRenderer.color = gradientSpeedSnake.Evaluate(Mathf.Repeat((-Time.time + (i * 0.025f)) * 2, 1));
            }
        }
    }

    public void SetSnakeSpeedMode(bool isSpeedMode)
    {
        _isSpeedMode = isSpeedMode;

        if(!isSpeedMode)
        {
            SnakeBodyColor(Color.white);
        }
    }
}
