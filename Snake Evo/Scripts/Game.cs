using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.IO;

public enum EDirection { Up, Down, Right, Left };
public class Game : MonoBehaviour
{
    [SerializeField]
    private Level[] levels;

    [SerializeField]
    private Fisheye fishEyeCam;

    [SerializeField]
    private Vortex vortexCam;

    [SerializeField]
    private Material boardMaterial;

    [SerializeField]
    private GameObject gameOverWindow;

    [SerializeField]
    private float tileSize; //tamanho de cada tile

    [SerializeField]
    private GameObject tilePrefab; // a prefab dos tiles

    private TileBase[,] tileArray; // o array de tiles

    [SerializeField]
    private Snake snake;

    [SerializeField]
    private GameColors colors;

    [SerializeField]
    private Text scoreUI;

    [SerializeField]
    private Text bestScoreUI;

    [SerializeField]
    private Text levelUI;

    [SerializeField]
    private Text ReadyUI;

    [SerializeField]
    private float gameSpeed;

    [SerializeField]
    private float gameSpeedMode;

    [SerializeField]
    private int foodBodyIncreaser = 1;



    [SerializeField]
    private float actualGameSpeed;

    private float gameTimer;
    private float timer;
    private int score;
    private int BestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("bestScore"))
            {
                return PlayerPrefs.GetInt("bestScore");
            }
            else
                return 0;
        }

        set
        {
            PlayerPrefs.SetInt("bestScore",value);
        }
    }
    private int level = 1;

    private List<EDirection> inputDirectionBuffer;
    private int inputBufferMaxSize = 3;
    private EDirection lastDirection;

    private List<TileBase> foodList;

    private bool paused = true;

    void Awake()
    {
        foodList = new List<TileBase>();
        inputDirectionBuffer = new List<EDirection>(inputBufferMaxSize);
        lastDirection = EDirection.Up;
     //   inputDirectionBuffer.Add(lastDirection);
    }

    void Start()
    {
       

        gameTimer = (1 / gameSpeed);
        actualGameSpeed = gameSpeed;

        int xLength = 0, yLength = 0;



      //  char[,] charsLevel = levels[0].GetLevelBoard();

         char[,] charsLevel = levels[0].GetLevelData();

        xLength = charsLevel.GetLength(0);
        yLength = charsLevel.GetLength(1);

        tileArray = new TileBase[xLength, yLength];

        float xBordSize = xLength * tileSize;
        float yBoardSize = yLength * tileSize;

        for (int x = 0; x < xLength; x++)
        {
            for (int y = yLength-1; y >=0 ; y--)
            {
                Vector3 position = new Vector3(x * tileSize, y * -tileSize, 0) - new Vector3(xBordSize / 2, -yBoardSize / 2);
                tileArray[x, y] = ((GameObject)Instantiate(tilePrefab, position, Quaternion.identity)).GetComponent<TileBase>();

                  tileArray[x, y].SetType(ETileType.Ground);
                        ColorizeBoard(x, y);

           /*     switch(charsLevel[x,y])
                {
                    case 'G':
                        tileArray[x, y].SetType(ETileType.Ground);
                        ColorizeBoard(x, y);
                        break;

                    case 'W':
                        tileArray[x, y].SetType(ETileType.Wall);
                        break;

                    case 'F':
                        tileArray[x, y].SetType(ETileType.Food);
                        foodList.Add(tileArray[x, y]);
                        break;
                }*/
            }
        }

        SetRandomFood();
        UpdateScore();
        StartCoroutine(StartGameRoutine());
    }

    void FixedUpdate()
    {
        if (paused)
            return;

       
    }

    void Update()
    {

        if (paused)
            return;

        SetDirection();

        gameTimer = (1 / actualGameSpeed);
     
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            MoveRotine();
        }



    }

    private void MoveRotine()
    {

        timer = gameTimer;

        if (!MoveSnake(lastDirection))
        {
            paused = true;
            StartCoroutine(GameOverRoutine());
        }


        switch (ActualHeadTileType())
        {
            case ETileType.Wall:
                break;
            case ETileType.Ground:
                break;
            case ETileType.Food:

                OnEatFood();
                snake.AddTile(2);

                if (snake.bodyTile.Count > 30)
                {
                    for (int i = 0; i < foodList.Count; i++)
                    {
                        if (foodList[i].GetType() == ETileType.SpeedFood)
                            return;
                    }

                    if (Random.Range(0, 10) == 1)
                    {
                        SetSpeedFood();
                    }
                }

                break;
            case ETileType.EspecialFood:

            
                break;
            case ETileType.SpeedFood:

                StartCoroutine(SpeedModeRoutine(5));
                int quant = (snake.bodyTile.Count / 2) - 2;
                StartCoroutine(RemoveSnakeBodyTileNormal(quant));

                OnEatFood();

                break;
            case ETileType.Snake:
                break;
            default:
                break;  
        }
    }

    private void OnEatFood()
    {
        tileArray[snake.headXIndex, snake.headYIndex].SetType(ETileType.Ground);
        ColorizeBoard(snake.headXIndex, snake.headYIndex);
        foodList.Remove(tileArray[snake.headXIndex, snake.headYIndex]);
        score += 15;
        UpdateScore();
        Instanciador.instancia.PlaySfx(1, 1, 1);

        SetRandomFood();   
    }

    private void SnakeSpeedCalculation()
    {
       // actualGameSpeed += level / 4;
    }

    private void ColorizeBoard(int x, int y)
    {
        if (x % 2 == 0 && y % 2 == 0)
        {
            tileArray[x, y].spriteRenderer.color = colors.groundColor[0];
            tileArray[x, y].originalColor = colors.groundColor[0];
        }
        else
        {
            tileArray[x, y].spriteRenderer.color = colors.groundColor[1];
            tileArray[x, y].originalColor = colors.groundColor[1];
        }
    }

    private void ColorizeBoardMaterial(Color color)
    {

        GoEaseType type = GoEaseType.CubicOut;
        float delay = 0.1f;
        float duration = 0.1f;


     
    }

    private void UpdateScore()
    {
        scoreUI.text = string.Format("SCORE:{0}", score);
    }

    private void SetRandomFood()
    {
        int xTile = 0;
        int yTile = 0;

        bool snakeTile = false;

        do
        {
            snakeTile = false;

            xTile = Random.Range(0, tileArray.GetLength(0));
            yTile = Random.Range(0, tileArray.GetLength(1));

            for (int i = 0; i < snake.bodyTile.Count; i++)
            {
                if (snake.bodyTile[i].xIndex == xTile && snake.bodyTile[i].yIndex == yTile)
                {
                    snakeTile = true;
                }
            }


        } while (tileArray[xTile, yTile].GetType() != ETileType.Ground || snakeTile);

        tileArray[xTile, yTile].SetType(ETileType.Food);
        foodList.Add(tileArray[xTile, yTile]);
    }

    private void SetRandomEspecialFood()
    {
        int xTile = 0;
        int yTile = 0;

        bool snakeTile = false;

        do
        {
            snakeTile = false;

            xTile = Random.Range(0, tileArray.GetLength(0));
            yTile = Random.Range(0, tileArray.GetLength(1));

            for (int i = 0; i < snake.bodyTile.Count; i++)
            {
                if (snake.bodyTile[i].xIndex == xTile && snake.bodyTile[i].yIndex == yTile)
                {
                    snakeTile = true;
                }
            }


        } while (tileArray[xTile, yTile].GetType() != ETileType.Ground || snakeTile);

        tileArray[xTile, yTile].SetType(ETileType.EspecialFood);
        foodList.Add(tileArray[xTile, yTile]);
    }

    private void SetSpeedFood()
    {
        int xTile = 0;
        int yTile = 0;

        bool snakeTile = false;

        do
        {
            snakeTile = false;

            xTile = Random.Range(0, tileArray.GetLength(0));
            yTile = Random.Range(0, tileArray.GetLength(1));

            for (int i = 0; i < snake.bodyTile.Count; i++)
            {
                if (snake.bodyTile[i].xIndex == xTile && snake.bodyTile[i].yIndex == yTile)
                {
                    snakeTile = true;
                }
            }


        } while (tileArray[xTile, yTile].GetType() != ETileType.Ground || snakeTile);

        tileArray[xTile, yTile].SetType(ETileType.SpeedFood);
        foodList.Add(tileArray[xTile, yTile]);
    }

    private int EspecialFoodCount()
    {
        int count = 0;

            for (int y = 0; y < foodList.Count; y++)
            {
                if (foodList[y].GetType() == ETileType.EspecialFood)
                    count++;
            }

        return count;
    }

    private void CleanAllFoods()
    {
        for (int y = 0; y < foodList.Count; y++)
            {
                if (foodList[y].GetType() == ETileType.Food)
                {
                    foodList[y].SetType(ETileType.Ground);
                    ColorizeBoard(foodList[y].xIndex, foodList[y].yIndex);
                }
            }
    }

    private void SetAllFoodColor(Color color)
    {
            for (int y = 0; y < foodList.Count; y++)
            {
                if (foodList[y].GetType() == ETileType.Food)
                {
                    foodList[y].spriteRenderer.color = color;
                }
            }
    }  

    private void SetDirection()
    {


        if (Input.GetKeyDown(KeyCode.UpArrow) && lastDirection != EDirection.Down && lastDirection != EDirection.Up)
        {
            lastDirection = EDirection.Up;
            MoveRotine();

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && lastDirection != EDirection.Up && lastDirection != EDirection.Down)
        {
            lastDirection = EDirection.Down;
            MoveRotine();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && lastDirection != EDirection.Right && lastDirection != EDirection.Left)
        {
            lastDirection = EDirection.Left;
            MoveRotine();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastDirection != EDirection.Left && lastDirection != EDirection.Right)
        {
            lastDirection = EDirection.Right;
            MoveRotine();

        }
    }

    private bool MoveSnake(EDirection newDirection)
    {
        int xDir = 0;
        int yDir = 0;

        switch (newDirection)
        {
            case EDirection.Up:
                yDir = -1;
                break;
            case EDirection.Down:
                yDir = 1;
                break;
            case EDirection.Right:
                xDir = 1;
                break;
            case EDirection.Left:
                xDir = -1;
                break;


            default:
                break;
        }

        if (CanMove(xDir, yDir))
        {
            SetWrapPosition(xDir, yDir);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanMove(int xDir, int yDir)
    {
        if (snake.headXIndex <= tileArray.GetLength(0) -1 && snake.headXIndex >= 0 &&
            snake.headYIndex <= tileArray.GetLength(1) -1 && snake.headYIndex >= 0)
        {
            for (int i = 1; i < snake.bodyTile.Count; i++)
            {
                if (snake.headXIndex == snake.bodyTile[i].xIndex && snake.headYIndex == snake.bodyTile[i].yIndex)
                {
                    return false;
                }
            }

            if (tileArray[snake.headXIndex, snake.headYIndex].GetType() == ETileType.Wall)
            {
                return false;
            }

        }

        return true;
    }

    private void SetWrapPosition(int xDir, int yDir)
    {
        int newHeadX = (int)Mathf.Repeat(snake.headXIndex + xDir, tileArray.GetLength(0));
        int newHeadY = (int)Mathf.Repeat(snake.headYIndex + yDir, tileArray.GetLength(1));
        snake.SetPosition(newHeadX, newHeadY);
    }

    private TileBase GetWrapedTile(int x, int y)
    {
        int xIndex = (int)Mathf.Repeat(x, tileArray.GetLength(0));
        int yIndex = (int)Mathf.Repeat(y, tileArray.GetLength(1));

        return tileArray[xIndex,yIndex];
    }

    private ETileType ActualHeadTileType()
    {
        return tileArray[snake.headXIndex, snake.headYIndex].GetType();
    }

    public void PlayAgain()
    {
        Application.LoadLevel(0);
    }

    IEnumerator GameOverRoutine()
    {
        Instanciador.instancia.PlaySfx(3, 1, 1);

        if(score>BestScore)
        {
            BestScore = score;
        }

        bestScoreUI.text = string.Format("Best Score:{0}", BestScore);

        for (int i = 0; i < 5; i++)
        {
            snake.SnakeBodyColor(new Color(1, 1, 1, 0));
            yield return new WaitForSeconds(0.1f);
            snake.SnakeBodyColor(new Color(1, 1, 1, 1));
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.2f);
        gameOverWindow.SetActive(true);
    }

    IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 3; i >= 1; i--)
        {
            ReadyUI.text = i.ToString();
            ReadyUI.color = new Color(1, 1, 1, 0);
            ReadyUI.fontSize = 1;
            Go.to(ReadyUI, 0.2f, new GoTweenConfig().intProp("fontSize", 90, false));
            Go.to(ReadyUI, 0.2f, new GoTweenConfig().colorProp("color", Color.white, false).onComplete(o => { Instanciador.instancia.PlaySfx(5, 1, 1); }));
            yield return new WaitForSeconds(0.5f);
          
        }

        Instanciador.instancia.PlaySfx(4, 1, 1);
        ReadyUI.gameObject.SetActive(false);

/*
        for (int x = 0; x < xLength; x++)
        {

           yield return new WaitForSeconds(0.01f);
            ///for (int y = 0; y < yLength; y++)
            ///  {
            tileArray[x, 6].spriteRenderer.color = Color.white;
            Go.to(tileArray[x, 6].spriteRenderer, 2f, new GoTweenConfig().colorProp("color", tileArray[x, 6].originalColor, false).setEaseType(GoEaseType.ExpoOut));
            ///  }
        }*/

     
       
        snake.Initialize(tileArray);

        paused = false;
       
    }

    IEnumerator RemoveSnakeBodyTile(int number)
    {
        paused = true;
        float delay = 0.2f;
        float picth = 1.2f;

        for (int i = 0; i < number; i++)
        {
            snake.RemoveTile(1);
            Instanciador.instancia.PlaySfx(7, 1, picth);
            yield return new WaitForSeconds(delay);
            delay = delay / 2f;
            picth = picth * 1.01f;
            score += 5 * (i + 1);
            UpdateScore();
        }

        Instanciador.instancia.PlaySfx(8, 1, 1f);
        Go.to(fishEyeCam, 2f, new GoTweenConfig().floatProp("StrengthX", -0.5f, false).setEaseType(GoEaseType.Punch));
        Go.to(fishEyeCam, 2f, new GoTweenConfig().floatProp("StrengthY", 0.5f, false).setEaseType(GoEaseType.Punch).setDelay(0.05f));
   //     Go.to(vortexCam, 2f, new GoTweenConfig().floatProp("Angle", 50f, false).setEaseType(GoEaseType.Punch));
     //   Go.to(vortexCam, 4f, new GoTweenConfig().vector2Prop("Center", new Vector2(1,0.5f), false).setEaseType(GoEaseType.Punch));

        paused = false;
        SnakeSpeedCalculation();



        for (int i = 0; i < foodBodyIncreaser * 2; i++)
        {
            SetRandomFood();
        }

        SetSpeedFood();
        level++;
        levelUI.text = string.Format("LEVEL:{0}", level);


    }

    IEnumerator RemoveSnakeBodyTileNormal(int number)
    {
        float delay = 0.2f;
        float picth = 1.2f;

        for (int i = 0; i < number; i++)
        {
            snake.RemoveTile(1);
            Instanciador.instancia.PlaySfx(7, 1, picth);
            yield return new WaitForSeconds(delay);
            delay = delay / 2f;
            picth = picth * 1.01f;
            score += 5 * (i + 1);
            UpdateScore();
        }

        actualGameSpeed = gameSpeed;
        snake.SetSnakeSpeedMode(false);

    }

    IEnumerator SpeedModeRoutine(float time)
    {
        actualGameSpeed = gameSpeedMode;
        snake.SetSnakeSpeedMode(true);
        yield return new WaitForSeconds(time);

  //      actualGameSpeed = gameSpeed;
  //      snake.SetSnakeSpeedMode(false);

    }

    [System.Serializable]
    internal class GameColors
    {
        public Color[] groundColor;
    }

    [System.Serializable]
    public class Level
    {
       /* [SerializeField]
        private TextAsset levelTextAsset;
        private StringReader stringReader;
        public char [,] GetLevelBoard()
        {
            

            int xSize = 0;
            int ySize = 0;

            stringReader = new StringReader(levelTextAsset.text);
            List<string> lines = new List<string>();
            string actualLine;

            while ((actualLine = stringReader.ReadLine())!=null)
            {
                lines.Add(actualLine);
                ySize++;
            }

            xSize = lines[0].Length;

            char[,] boardChar = new char[xSize, ySize];

            for (int j = 0; j < ySize; j++)
            {
                for (int i = 0; i < xSize; i++)
                {
                    boardChar[i, j] = lines[j][i];
                }
            }
            return boardChar;
        }*/


        [SerializeField]
        private Texture2D LevelImage;

        [SerializeField]
        private Rect sourceRect;
      


        public char[,] GetLevelData()
        {
        
            int x = Mathf.FloorToInt(sourceRect.x);
            int y = Mathf.FloorToInt(sourceRect.x);

            int width = Mathf.FloorToInt(sourceRect.width);
            int height = Mathf.FloorToInt(sourceRect.height);

            char[,] arrayColor = new char[width, height];

            for (int j = height -1 ; j >= 0; j--)
            {
                for (int i = width -1 ; i >= 0 ; i--)
                {
                    if(LevelImage.GetPixel(i, j) == Color.red)
                    {
                        arrayColor[i, j] = 'G';
                    }
                    else if (LevelImage.GetPixel(i, j) == Color.black)
                    {
                        arrayColor[i, j] = 'W';
                    }
                    else if (LevelImage.GetPixel(i, j) == Color.green)
                    {
                        arrayColor[i, j] = 'F';
                    }
                }
            }
            return arrayColor;
        }

    }
}
