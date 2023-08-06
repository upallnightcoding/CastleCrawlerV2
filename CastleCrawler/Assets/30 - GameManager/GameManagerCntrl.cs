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
        boardCntrl.Initialize();
    }

    public void StartNewGame()
    {
        Stack<Move> moves = boardCntrl.StartNewGame();

        uiCntrl.StartNewGame(moves);
    }

    public void TogglePath()
    {
        displayPath = !displayPath;
    }

    public Material DisplayTileMaterial()
    {
        return (displayPath ? gameData.TileWhite : gameData.TileGray);
    }

    public void OnPlayerMove(string move, Material color)
    {
        if (uiCntrl.IsDirBtnEnabled(move))
        {
            boardCntrl.OnPlayerMove(move, color);
            uiCntrl.OnPlayerMove(move);

            if (boardCntrl.IsFinished() && uiCntrl.TotalPointsIsZero())
            {
                uiCntrl.TriggerWin();
            }
        }
    }

    public void UndoPlayerMove()
    {
        string moveName = boardCntrl.UndoPlayerMove();

        if (moveName != null)
        {
            uiCntrl.UndoPlayerMove(moveName);
        }
    }
}
