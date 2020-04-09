using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Level : Singleton<Level>
{
    public CanvasGroup endGameCanvasGroup;
    public AnimationCurve picthCurve;
    public int initialTime=120;
    public Text scoreUI,timerUI;
    public Image timerImage;
    public SpriteManagerScript spriteManager;
    public static int NumColumns = 0, NumRows = 0;
    public Camera cameraGame;
   

    public GameObject baseCandy;
    public GameObject baseTile;
    public Transform canvasTransform;

    public const float TileWidth = 0.92f;
    public const float TileHeight = 0.92f;
    public int TotalScore { get; set; }
    private int uiTotalScore;
    private int counter;

    //Level from json file
    public TextAsset levelDesign;
    private Candy[,] candysGrid ;
    private Tile[,] tileGrid;
    private bool userCanInteractWithGrid = false;
    private int swipeFromColumn = -1;
    private int swipeFromRow = -1;
    private Candy candyTouched;
    private List<Swaper> possiblesSwaps;

    private float actualTime=0;
    private float timerImageWidth;
    private Color timerBarOriginalColor;
    private float audioPicth;
    private AudioSource audioSouceMusic;
    private bool endGame = false;
    

    //Game rules
    const int timeEarned = 2;
    const int lostedTime = 3;



    void Awake()
    {
        base.Awake();
        timerImageWidth = timerImage.rectTransform.rect.width;
        timerBarOriginalColor = timerImage.color;
        audioSouceMusic = GetComponent<AudioSource>();
    }

    void Start()
    {
        InitWithFile(levelDesign.text);
        NewGame();
    }

    public void NewGame()
    {
        DestroyAllCandys();
        Initialize();
        actualTime = initialTime;
        TotalScore = 0;
        SetTimerBar(1);
        Go.killAllTweensWithTarget(audioSouceMusic);
        audioSouceMusic.pitch = 1;
        audioSouceMusic.volume = 0.23f;
        audioSouceMusic.Play();
        endGame = false;
        StartCoroutine("ShowPossiblesMovesAnimation");
        if (endGameCanvasGroup.alpha > 0)
        {
            Go.killAllTweensWithTarget(endGameCanvasGroup);
            endGameCanvasGroup.alpha = 0;
            endGameCanvasGroup.blocksRaycasts = false;
        }
    }

    void Initialize()
    {

        List<Candy> listCandy = Shuffle();
        SetCandyInitialPosition(listCandy);
        SetCandySprite(listCandy);
        SetTilePosition(listCandy);
        BeginAnimation(listCandy);
    }

    List<Candy> Shuffle()
    {
        List<Candy> listCandy = null;
        do
        {
            listCandy = CreatInitialCandys();
            DetectPossibleSwaps();
        } while (possiblesSwaps.Count == 0);

        return listCandy;
    }

    List<Candy> CreatInitialCandys()
    {
        List<Candy> listaCandy = new List<Candy>();

        for (int row = 0; row < NumRows; row++)
        {
            for (int column = 0; column < NumColumns; column++)
            {
                candysGrid[column, row] = new Candy();
            }
        }

        for (int row = 0; row < NumRows; row++)
        {
            for (int column = 0; column < NumColumns; column++)
            {
                if (GetTileAt(column, row) != null)
                {
                    ECandyType type;
                    do
                    {
                        type = GetRandomCandyType();
                    }
                    while ((column >= 2 &&
                    candysGrid[column - 1, row].type == type &&
                    candysGrid[column - 2, row].type == type)
                  ||
                   (row >= 2 &&
                    candysGrid[column, row - 1].type == type &&
                    candysGrid[column, row - 2].type == type));



                    Candy newCandy = SetCandyItem(column, row, type);
                    newCandy.SetAlpha(1);
                    listaCandy.Add(newCandy);
                }
            }
        }

        return listaCandy;

    }

    Candy GetCandyAt(int coluna, int linha)
    {
        if ((coluna >= 0 && coluna <= NumColumns) && (linha >= 0 && linha <= NumRows))
            return candysGrid[coluna, linha];
        else
        {
            Debug.LogError("Linha e/ou coluna invalida");
            return null;
        }
    }

    Candy SetCandyItem(int coluna, int linha, ECandyType tipo)
    {

        GameObject gOCandy = (GameObject)Instantiate(baseCandy, Vector3.zero, Quaternion.identity);
        Candy newCandy = gOCandy.GetComponent<Candy>();
        newCandy.name = string.Format("Candy_Column[{0}]_Row[{1}]", coluna, linha);
        newCandy.type = tipo;
        newCandy.Column = coluna;
        newCandy.Row = linha;
        candysGrid[coluna, linha] = newCandy;
        return newCandy;
    }

    ECandyType GetRandomCandyType()
    {
        int tipoNum = Random.Range(1, Candy.NumCandyTypes + 1);
        return (ECandyType)tipoNum;
    }

    void SetCandySprite(List<Candy> listCandy)
    {
        foreach (Candy candy in listCandy)
        {
            candy.LoadSprite(spriteManager.GetCandySprite(candy.type, false), spriteManager.GetCandySprite(candy.type, true));
        }
    }

    void InitWithFile(string textFile)
    {
        int x = 0;
        int y = 0;


        bool endLine = false;

        foreach (char character in textFile)
        {
            if ((character == '1' || character == '0') && !endLine)
            {
                x++;
            }
            else if (character == '\n')
            {
                endLine = true;
                y++;
            }
        }

        NumColumns = x;
        NumRows = y;
        tileGrid = new Tile[x, y];
        candysGrid = new Candy[x, y];


        x = 0;
        y = 0;

        GameObject instanciaGO;
        Tile instanciaTile;

        foreach (char charac in textFile)
        {
            if (charac == '1')
            {
                instanciaGO = (GameObject)Instantiate(baseTile, Vector3.zero, Quaternion.identity);
                instanciaGO.transform.parent = transform;
                instanciaTile = instanciaGO.GetComponent<Tile>();
                tileGrid[x, y] = instanciaTile;
                x++;
            }
            else if (charac == '0')
            {
                tileGrid[x, y] = null;
                x++;
            }
            else if (charac == '\n')
            {
                x = 0;
                y++;
            }

        }
    }

    void SetTilePosition(List<Candy> listCandy)
    {
        for (int linha = 0; linha < NumRows; linha++)
        {
            for (int coluna = 0; coluna < NumColumns; coluna++)
            {
                if (tileGrid[coluna, linha] != null)
                    tileGrid[coluna, linha].transform.localPosition = candysGrid[coluna, linha].transform.localPosition;
            }
        }
    }

    Tile GetTileAt(int coluna, int linha)
    {


        if ((coluna >= 0 && coluna <= NumColumns) && (linha >= 0 && linha <= NumRows))
            return tileGrid[coluna, linha];
        else
        {
            Debug.LogError("Linha e/ou coluna invalida");
            return null;
        }
    }

    void Update()
    {
        UpdateTouch();
        MainTimer();

        if (uiTotalScore < TotalScore)
        {
            if (counter % 2 == 0)
            {
                uiTotalScore++;
                scoreUI.text = uiTotalScore.ToString();
            }

            counter+=Mathf.RoundToInt(Time.deltaTime);
        }
        else
        {
            counter = 0;
        }
    }

    void TrySwapHorizontalVertical(int horzDelta, int vertDelta)
    {

        int toColumn = swipeFromColumn + horzDelta;
        int toRow = swipeFromRow + vertDelta;


        if (toColumn < 0 || toColumn >= NumColumns)
            return;
        if (toRow < 0 || toRow >= NumRows)
            return;

        Candy toCandy = GetCandyAt(toColumn, toRow);
        if (toCandy == null)
            return;

        Candy fromCandy = GetCandyAt(swipeFromColumn, swipeFromRow);

        //  print("Tile de origem [Coluna] = " + fromCandy.Column + "[Linha] = " + fromCandy.Row);
        //   print("Tile de destino [Coluna] = " + toCandy.Column + "[Linha] = " + toCandy.Row);


        Swaper candySwap = new Swaper();
        candySwap.candyA = fromCandy;
        candySwap.candyB = toCandy;

        if (IsPossibleSwap(candySwap))
        {
            PerformSwap(candySwap);
            AnimateSwap(candySwap, true);
        }
        else
        {
            AnimateSwap(candySwap, false);
            Instanciador.instancia.PlaySfx(2, 1, 1);
        }

    }

    void PerformSwap(Swaper swap)
    {

        int columnA = swap.candyA.Column;
        int rowA = swap.candyA.Row;
        int columnB = swap.candyB.Column;
        int rowB = swap.candyB.Row;

        candysGrid[columnA, rowA] = swap.candyB;
        swap.candyB.Column = columnA;
        swap.candyB.Row = rowA;

        candysGrid[columnB, rowB] = swap.candyA;
        swap.candyA.Column = columnB;
        swap.candyA.Row = rowB;
    }

    #region Animation
    //Animations

    void BeginAnimation(List<Candy> allCandys)
    {
        float speed = 0.15f;
        float delay = 0;
        float longDelay = 0;

        GoEaseType easeType = GoEaseType.BackOut;

        for (int i = 0; i < allCandys.Count; i++)
        {
            allCandys[i].SetAlpha(0);
            allCandys[i].transform.localScale = Vector3.zero;
            delay = Random.Range(0.07f, 0.5f);
            if (delay > longDelay)
                longDelay = delay;

            Go.to(allCandys[i].transform, speed, new GoTweenConfig().scale(1, false).setDelay(delay).setEaseType(easeType));
            Go.to(allCandys[i], speed, new GoTweenConfig().floatProp("Alpha", 1, false).setDelay(delay));
        }

        StartCoroutine(BeginAnimationCompleted(longDelay));

    }

    IEnumerator BeginAnimationCompleted(float delay)
    {
        yield return new WaitForSeconds(delay);
        userCanInteractWithGrid = true;
    }

    void AnimateSwap(Swaper swap, bool validSwap)
    {

        userCanInteractWithGrid = false;

        // Put the cookie you started with on top.
        swap.candyA.GetComponent<SpriteRenderer>().sortingOrder = 2;
        swap.candyB.GetComponent<SpriteRenderer>().sortingOrder = 1;

        const float Duration = 0.3f;




        Vector3 aPosition = swap.candyA.transform.position;
        Vector3 bPosition = swap.candyB.transform.position;

        if (validSwap)
        {
            GoEaseType easyType = GoEaseType.CubicOut;

            Go.to(swap.candyA.transform, Duration, new GoTweenConfig().position(bPosition, false).setEaseType(easyType));
            Go.to(swap.candyB.transform, Duration, new GoTweenConfig().position(aPosition, false).setEaseType(easyType).onComplete(o =>
            {
                AnimationCompleted(true);
            }));
        }
        else
        {
            GoEaseType easyType = GoEaseType.CubicInOut;

            Go.to(swap.candyA.transform, Duration / 1.5f, new GoTweenConfig().position(bPosition, false).setEaseType(easyType).setIterations(2, GoLoopType.PingPong));
            Go.to(swap.candyB.transform, Duration / 1.5f, new GoTweenConfig().position(aPosition, false).setEaseType(easyType).setIterations(2, GoLoopType.PingPong).onComplete(o =>
            {
                AnimationCompleted(false);
            }));
        }
    }

    void AnimationCompleted(bool validSwap)
    {

        if (validSwap)
        {
            HandleMatches();
        }
        else
        {
            userCanInteractWithGrid = true;
        }
    }

    void AnimateMatchedCandys(List<Chain> listChain)
    {
        const float Duration = 0.2f;
        GoEaseType easyType = GoEaseType.Linear;


        BarTimerAnimation(true);
        actualTime += timeEarned;

        foreach (Chain chain in listChain)
        {
            AnimateScoreForChain(chain);
            for (int i = 0; i < chain.arrayCandy.Count; i++)
            {
                Go.to(chain.arrayCandy[i].transform, Duration, new GoTweenConfig().scale(0, false).setEaseType(easyType));
            }

            TotalScore += chain.score;
          
        }
        StopPossibleSwapesAnimation();
        StartCoroutine(AnimateMatchedCompleted(listChain, Duration));
        Instanciador.instancia.PlaySfx(0, 1, 1);
    }

    IEnumerator AnimateMatchedCompleted(List<Chain> listChain, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Chain chain in listChain)
        {
            foreach (Candy candy in chain.arrayCandy)
            {
                Destroy(candy);
            }
        }

        AnimateFallingCookies(FillHoles());
    }

    void AnimateFallingCookies(List<List<Candy>> listaListaCandy)
    {

      //  print("Animating Falling Initialized");

        GoEaseType easyType = GoEaseType.BackOut;

        float totalAnimationTime = 0;
        float time = 0;
        float delay = 0;
        Vector3 targetPosition = Vector3.zero;
        GroupTween groupTween = new GroupTween();
       
         
        foreach (List<Candy> listCandy in listaListaCandy)
        {
            for (int i = 0; i < listCandy.Count; i++)
            {
                time = 0.15f;
              //  delay = 0.05f + (time * i);
                totalAnimationTime = time + delay;
                groupTween.AddTransform(listCandy[i].transform);

            }

            targetPosition = ColumRowToPosition(listCandy[0].Column, listCandy[0].Row ) - listCandy[0].transform.position;
        }
        
        groupTween.Initialize(Go.to(groupTween, time, new GoTweenConfig().vector3Prop("Position",new Vector3(0,targetPosition.y,0), false).setEaseType(easyType).setDelay(delay)));

        StartCoroutine(AnimateFallingCompleted(totalAnimationTime));

    }

    IEnumerator AnimateFallingCompleted(float delay)
    {
        yield return new WaitForSeconds(delay);
        //print("Animating Falling Completed");
        AnimateNewCandys(TopUpCookies());
    }

    void AnimateNewCandys(List<List<Candy>> listNewCandys)
    {
        float totalAnimationTime = 0;
       // int counter = 1;
        foreach (List<Candy> array in listNewCandys)
        {
            int startRow = array[0].Row + 1;

            for (int i = 0; i < array.Count; i++)
            {
                Vector3 targetPosition = ColumRowToPosition(array[i].Column, array[i].Row);
                Vector3 startPosition = ColumRowToPosition(array[i].Column, startRow);
                array[i].transform.position = startPosition;
                array[i].Alpha = 0;

                GoEaseType type = GoEaseType.BackOut;
                float delay = (array.Count - i) * 0.1f;
                float duration = (startRow - array[i].Row) * 0.1f;
                totalAnimationTime = duration + delay;

                Go.to(array[i].transform, duration, new GoTweenConfig().position(targetPosition, false).setDelay(delay).setEaseType(type));

                Go.to(array[i], 0.1f, new GoTweenConfig().floatProp("Alpha", 1, false).setDelay(delay));

            }
        }

        StartCoroutine(AnimateNewCandyCompleted(totalAnimationTime));
        // 2
    }

    IEnumerator AnimateNewCandyCompleted(float delay)
    {
        yield return new WaitForSeconds(delay);
        HandleMatches();
        System.GC.Collect();
    }

    void AnimateScoreForChain(Chain chain)
    {
        // Figure out what the midpoint of the chain is.
        Candy firstCandy = chain.arrayCandy[0];
        Candy lastCandy = chain.arrayCandy[chain.arrayCandy.Count - 1];

        Vector3 centerPosition = new Vector3(
          (firstCandy.transform.localPosition.x + lastCandy.transform.localPosition.x) / 2,
          (firstCandy.transform.localPosition.y + lastCandy.transform.localPosition.y) / 2);

        GameObject comboObj = Instanciador.instancia.InstanciarLocal(0, centerPosition, Quaternion.identity, canvasTransform);
        comboObj.GetComponent<Text>().text = chain.score.ToString();
        comboObj.GetComponent<RectTransform>().localScale = Vector3.one;
        comboObj.transform.positionTo(1, new Vector3(0, 0.8f, 0), true).setOnCompleteHandler(o => { Destroy(comboObj); });

    }
    
    void BarTimerAnimation(bool positiveFeedBack)
    {
        if (positiveFeedBack)
        {
            timerImage.color = Color.white;
            Go.to(timerImage, 0.3f, new GoTweenConfig().colorProp("color", timerBarOriginalColor, false).setDelay(0.1f));
        }
        else
        {
            timerImage.color = Color.red;
            Go.to(timerImage, 0.8f, new GoTweenConfig().colorProp("color", timerBarOriginalColor, false).setDelay(0.2f));
        }
    }

    #endregion
    //
    #region Touch

    bool TouchDownState()
    {

#if UNITY_STANDALONE || UNITY_EDITOR
        return Input.GetMouseButtonDown(0);
#else
         if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            return true;
        else
            return false;
#endif

    }

    bool TouchUpState()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        return Input.GetMouseButtonUp(0);
#else
         if(Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended))
            return true;
        else
            return false;
#endif
    }

    bool TouchMovingState()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        return Input.GetMouseButton(0);
#else
         if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
            return true;
        else
            return false;
#endif
    }

    void UpdateTouch()
    {

        if (!userCanInteractWithGrid) return;

        Candy firstPickedCandy = null;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if (hit != null && hit.collider != null)
        {
            candyTouched = hit.collider.GetComponent<Candy>();
            firstPickedCandy = candyTouched;
            //print(hit.collider.tag);

            // Touch BEGAN
            if (TouchDownState())
            {

                swipeFromColumn = candyTouched.Column;
                swipeFromRow = candyTouched.Row;
            }
            if (TouchMovingState())
            {
                firstPickedCandy.HighLighted = 1;
            }

            if (swipeFromColumn == -1 || swipeFromRow == -1)
                return;

            int horzDelta = 0, vertDelta = 0;
            // Touch MOVED
            if (candyTouched.Column < swipeFromColumn)
            {          // swipe left
                horzDelta = -1;
            }
            else if (candyTouched.Column > swipeFromColumn)
            {   // swipe right
                horzDelta = 1;
            }
            else if (candyTouched.Row < swipeFromRow)
            {         // swipe down
                vertDelta = -1;
            }
            else if (candyTouched.Row > swipeFromRow)
            {         // swipe up
                vertDelta = 1;
            }

            if (horzDelta != 0 || vertDelta != 0)
            {
                TrySwapHorizontalVertical(horzDelta, vertDelta);
                swipeFromColumn = -1;
                swipeFromRow = -1;
            }

        }

    // Touch ENDED
        else if (TouchUpState())
        {

            swipeFromColumn = -1;
            swipeFromRow = -1;
        }

    }

    #endregion

    #region Positions

    Vector3 GetCandPosition(int coluna, int linha)
    {

        if ((coluna >= 0 && coluna <= NumColumns) && (linha >= 0 && linha <= NumRows))
            return candysGrid[coluna, linha].transform.localPosition;
        else
        {
            Debug.LogError("Linha e/ou coluna invalida");
            return Vector3.zero;
        }
    }
    void SetCandyInitialPosition(List<Candy> listCandy)
    {
        foreach (Candy candy in listCandy)
        {
            candy.transform.localPosition = ColumRowToPosition(candy.Column, candy.Row);
            candy.transform.parent = transform;
        }
    }
    Vector3 ColumRowToPosition(int coluna, int linha)
    {
        return new Vector3(TileWidth * (float)coluna / 2f, TileHeight * (float)linha / 2f) + RootPosition();
    }
    Vector3 RootPosition()
    {
        return new Vector3(-((TileWidth / 2) * NumColumns) / 2f, -((TileHeight / 2) * NumRows) / 2f) + new Vector3(TileWidth / 4, TileHeight / 4);
        //return Vector3.zero;
    }

    #endregion


    bool HasChainAtColumn(int column, int row)
    {
        ECandyType candyType = candysGrid[column, row].type;

        int horzLength = 1;
        for (int i = column - 1; i >= 0 && candysGrid[i, row].type == candyType; i--, horzLength++) ;
        for (int i = column + 1; i < NumColumns && candysGrid[i, row].type == candyType; i++, horzLength++) ;
        if (horzLength >= 3) return true;

        int vertLength = 1;
        for (int i = row - 1; i >= 0 && candysGrid[column, i].type == candyType; i--, vertLength++) ;
        for (int i = row + 1; i < NumRows && candysGrid[column, i].type == candyType; i++, vertLength++) ;
        return (vertLength >= 3);
    }

    void DetectPossibleSwaps()
    {

        List<Swaper> listSwaps = new List<Swaper>();

        for (int row = 0; row < NumRows; row++)
        {
            for (int column = 0; column < NumColumns; column++)
            {

                Candy candy = candysGrid[column, row];

                if (candy != null)
                {

                    // Is it possible to swap this cookie with the one on the right?
                    if (column < NumColumns - 1)
                    {
                        // Have a cookie in this spot? If there is no tile, there is no cookie.
                        Candy candyOther = candysGrid[column + 1, row];
                        if (candyOther != null)
                        {
                            // Swap them
                            candysGrid[column, row] = candyOther;
                            candysGrid[column + 1, row] = candy;

                            // Is either cookie now part of a chain?
                            if (HasChainAtColumn(column + 1, row) ||
                               HasChainAtColumn(column, row))
                            {

                                Swaper swap = new Swaper();
                                swap.candyA = candy;
                                swap.candyB = candyOther;
                                listSwaps.Add(swap);
                            }

                            // Swap them back
                            candysGrid[column, row] = candy;
                            candysGrid[column + 1, row] = candyOther;
                        }
                    }

                    if (row < NumRows - 1)
                    {

                        Candy candyOther = candysGrid[column, row + 1];
                        if (candyOther != null)
                        {
                            // Swap them
                            candysGrid[column, row] = candyOther;
                            candysGrid[column, row + 1] = candy;

                            if (HasChainAtColumn(column, row + 1) ||
                                HasChainAtColumn(column, row))
                            {

                                Swaper swap = new Swaper();
                                swap.candyA = candy;
                                swap.candyB = candyOther;
                                listSwaps.Add(swap);
                            }

                            candysGrid[column, row] = candy;
                            candysGrid[column, row + 1] = candyOther;
                        }
                    }


                }
            }
        }

        possiblesSwaps = listSwaps;
    }

    bool IsPossibleSwap(Swaper swap)
    {
        return possiblesSwaps.Contains(swap);
    }

    List<Chain> RemoveMatches()
    {
        List<Chain> horizontalChains = DetectHorizontalMatches();
        List<Chain> verticalChains = DetectVerticalMatches();

        RemoveCandys(horizontalChains);
        RemoveCandys(verticalChains);
        List<Chain> finalChain = new List<Chain>(horizontalChains.Union(verticalChains));
        CalculateScores(finalChain);    
        return finalChain;
    }

    List<Chain> DetectHorizontalMatches()
    {
        // 1
        List<Chain> set = new List<Chain>();

        // 2
        for (int row = 0; row < NumRows; row++)
        {
            for (int column = 0; column < NumColumns - 2; )
            {

                // 3
                if (candysGrid[column, row] != null)
                {
                    // print(candysGrid[column, row].type);

                    ECandyType matchType = candysGrid[column, row].type;

                    // 4

                    if (candysGrid[column + 1, row].type == matchType
                     && candysGrid[column + 2, row].type == matchType)
                    {
                        // 5
                        Chain chain = new Chain();
                        chain.chainType = EChainType.ChainTypeHorizontal;
                        do
                        {
                            chain.AddCandy(candysGrid[column, row]);
                            column += 1;
                        }
                        while (column < NumColumns && candysGrid[column, row].type == matchType);

                        set.Add(chain);

                        continue;
                    }
                }

                // 6
                column += 1;
            }
        }
        return set;
    }

    List<Chain> DetectVerticalMatches()
    {
        List<Chain> set = new List<Chain>();

        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows - 2; )
            {
                if (candysGrid[column, row] != null)
                {

                    ECandyType matchType = candysGrid[column, row].type;

                    if (candysGrid[column, row + 1].type == matchType
                     && candysGrid[column, row + 2].type == matchType)
                    {

                        Chain chain = new Chain();
                        chain.chainType = EChainType.ChainTypeVertical;
                        do
                        {
                            chain.AddCandy(candysGrid[column, row]);
                            row += 1;
                        }
                        while (row < NumRows && candysGrid[column, row].type == matchType);

                        set.Add(chain);
                        continue;
                    }
                }
                row += 1;
            }
        }
        return set;
    }

    void RemoveCandys(List<Chain> chains)
    {
        foreach (Chain chain in chains)
        {
            foreach (Candy candy in chain.arrayCandy)
            {
                candysGrid[candy.Column, candy.Row] = null;
            }
        }
    }

    void HandleMatches()
    {
        List<Chain> listChain = RemoveMatches();

        if (listChain.Count == 0)
        {
            BeginNextTurn();
            return;
        }


        AnimateMatchedCandys(listChain);

    }

    void BeginNextTurn()
    {
        userCanInteractWithGrid = true;
        DetectPossibleSwaps();
        StartCoroutine("ShowPossiblesMovesAnimation");
    }

    List<List<Candy>> FillHoles()
    {

        List<List<Candy>> columns = new List<List<Candy>>();

        // 1
        for (int column = 0; column < NumColumns; column++)
        {

            List<Candy> array = null;

            for (int row = 0; row < NumRows; row++)
            {

                // 2
                if (tileGrid[column, row] != null && candysGrid[column, row] == null)
                {

                    // 3
                    for (int lookup = row + 1; lookup < NumRows; lookup++)
                    {
                        Candy candy = candysGrid[column, lookup];
                        if (candy != null)
                        {
                            // 4
                            candysGrid[column, lookup] = null;
                            candysGrid[column, row] = candy;
                            candy.Row = row;

                            // 5
                            if (array == null)
                            {
                                array = new List<Candy>();
                                columns.Add(array);
                            }
                            array.Add(candy);

                            // 6
                            break;
                        }
                    }
                }
            }
        }
        return columns;
    }

    List<List<Candy>> TopUpCookies()
    {
        List<List<Candy>> columns = new List<List<Candy>>();

        ECandyType cookieType = ECandyType.Null;

        for (int column = 0; column < NumColumns; column++)
        {

            List<Candy> array = null;

            // 1
            for (int row = NumRows - 1; row >= 0 && candysGrid[column, row] == null; row--)
            {

                // 2
                if (tileGrid[column, row] != null)
                {

                    // 3
                    ECandyType newCookieType;
                    do
                    {
                        newCookieType = GetRandomCandyType();
                    } while (newCookieType == cookieType);
                    cookieType = newCookieType;

                    // 4
                    Candy candy = SetCandyItem(column, row, cookieType);
                    candy.LoadSprite(spriteManager.GetCandySprite(candy.type, false), spriteManager.GetCandySprite(candy.type, true));
                    candy.transform.parent = transform;
                    candy.SetAlpha(0);

                    // 5
                    if (array == null)
                    {
                        array = new List<Candy>();
                        columns.Add(array);
                    }
                    array.Add(candy);
                }
            }
        }
        //  ListMessager(columns, "[TOP UP COOKIES]");
        return columns;
    }

    public void ResetGame()
    {
        if (!userCanInteractWithGrid)
            return;

        actualTime -= lostedTime;

        BarTimerAnimation(false);

        userCanInteractWithGrid = false;

        List<Transform> listCandyTransform = new List<Transform>();
        List<Candy> listCandy = new List<Candy>();
        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {
                if (candysGrid[column, row] != null)
                {
                    
                    listCandyTransform.Add(candysGrid[column, row].transform);
                    listCandy.Add(candysGrid[column, row]);
                    candysGrid[column, row] = null;
                    
                }
            }
        }

        float scale = 0f;
        float time = 0.6f;
        GoEaseType type = GoEaseType.CubicOut;
        GroupTween groupTween = new GroupTween();
       // GoTween tween = Go.to(groupTween, time, new GoTweenConfig().vector3Prop("position", new Vector3(scale, scale, scale), false).setEaseType(type).onComplete(o => { listCandy.ForEach(candy => Destroy(candy.gameObject)); }));
      //  groupTween.Initialize(listCandyTransform.ToArray(), tween, EAxis.ALL);

        Invoke("Initialize", time);
    }

    void DestroyAllCandys()
    {

        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {
                if (candysGrid[column, row] != null)
                {
                    Destroy(candysGrid[column, row].gameObject);
                    candysGrid[column, row] = null;
                }
            }
        }
    }

    void CalculateScores(List<Chain> listChain)
    {
        foreach (Chain chain in listChain)
        {
            chain.score = 60 * (chain.arrayCandy.Count - 2);        
        }
    }

    void MainTimer()
    {
        if (actualTime > 0)
        {
            actualTime -= Time.deltaTime;
            timerUI.text = TimerFormat((int)actualTime);
            SetTimerBar(actualTime / initialTime);
            audioSouceMusic.pitch = picthCurve.Evaluate(actualTime);
        }
        else if(!endGame)
        {
            EndGame();
        }

        
       
    }

    void SetTimerBar(float amount)
    {
        amount = (amount * timerImageWidth);
        timerImage.rectTransform.sizeDelta = new Vector2(amount
            , timerImage.rectTransform.sizeDelta.y);
    }

    void EndGame()
    {
        endGame = true;
        GoEaseType easeType = GoEaseType.ExpoOut;
        float time = 1.3f;
        Go.to(audioSouceMusic, time, new GoTweenConfig().floatProp("pitch", 1, false).setEaseType(easeType));
        Go.to(audioSouceMusic, time*2, new GoTweenConfig().floatProp("volume", 0, false).setEaseType(easeType).onComplete(o => 
        { 
            audioSouceMusic.Stop();  
        }));

        Go.to(endGameCanvasGroup, 0.2f, new GoTweenConfig().floatProp("alpha", 1, false));
        endGameCanvasGroup.blocksRaycasts = true;
        userCanInteractWithGrid = false;
    }

    GoTween tweenPossibleSwap = null;

    IEnumerator ShowPossiblesMovesAnimation()
    {
        if(tweenPossibleSwap!=null)
            yield return null;
        
        yield return new WaitForSeconds(5);

        Swaper swaper = possiblesSwaps[Random.Range(0, possiblesSwaps.Count - 1)];
        tweenPossibleSwap = Go.to(swaper.candyB.normalSpriteRenderer, 0.5f, new GoTweenConfig().colorProp("color", new Color(1, 1, 1, 0), false).setIterations(6, GoLoopType.PingPong).onComplete(completed =>
            {
                    StopPossibleSwapesAnimation();

                    if(tweenPossibleSwap!=null)
                    StartCoroutine("ShowPossiblesMovesAnimation");
            }));
  
    }

    void StopPossibleSwapesAnimation()
    { 
        if(tweenPossibleSwap!=null)
        tweenPossibleSwap.destroy();
        StopCoroutine("ShowPossiblesMovesAnimation");
    }

    string TimerFormat(int totalSeconds)
    {
       /* int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        return minutes + ":" + seconds;*/

        var span = new System.TimeSpan(0, 0, totalSeconds); //Or TimeSpan.FromSeconds(seconds); (see Jakob C´s answer)
        return string.Format("{0}:{1:00}",(int)span.TotalMinutes,span.Seconds);
    }

    struct Point
    {

        public int X, Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

