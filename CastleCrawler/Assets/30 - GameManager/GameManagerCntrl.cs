using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCntrl : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] BoardCntrl boardCntrl;
    [SerializeField] UiCntrl uiCntrl;

    public static GameManagerCntrl Instance = null;

    private bool displayPath = false;

    private FxCntrl fxCntrl = null;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        fxCntrl = GetComponent<FxCntrl>();

        boardCntrl.Initialize();
    }

    public void TogglePath()
    {
        displayPath = !displayPath;
    }

    public Material DisplayTileMaterial()
    {
        return (displayPath ? gameData.StartEndTileColor : gameData.TileGray);
    }

    public void DisplayIllegalMoveBanner()
    {
        uiCntrl.DisplayIllegalMoveBanner();
    }

    /***********************************/
    /*** Fx Particle Systems & Sound ***/
    /***********************************/

    public void FxMovedInPathOfBomb(Vector3 position)
    {
        fxCntrl.Bomb(position);

        ReduceHealth();
    }

    public void ReduceHealth()
    {
        uiCntrl.ReduceHealth();
    }

    /*********************/
    /*** Button Events ***/
    /*********************/

    public void OnStartNewGame()
    {
        Stack<Move> moves = boardCntrl.StartNewGame();

        uiCntrl.StartNewGame(moves);
    }

    public void OnPlayerMove(string move, Material color)
    {
        if (uiCntrl.IsDirBtnEnabled(move))
        {
            if (boardCntrl.OnPlayerMove(move, color))
            {
                uiCntrl.OnPlayerMove(move);

                if (boardCntrl.IsFinished() && uiCntrl.TotalPointsIsZero())
                {
                    uiCntrl.DisplayWinBanner();
                }
            }
        }
    }

    public void OnUndoPlayerMove()
    {
        string moveName = boardCntrl.UndoPlayerMove();

        if (moveName != null)
        {
            uiCntrl.UndoPlayerMove(moveName);
        }
    }
}
