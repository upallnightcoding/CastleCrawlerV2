using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCntrl : MonoBehaviour
{
    [SerializeField] BoardCntrl boardCntrl;
    [SerializeField] UiCntrl uiCntrl;

    public static GameManagerCntrl Instance = null;

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

    public void OnPlayerMove(string move, Sprite color)
    {
        if (uiCntrl.IsDirBtnEnabled(move))
        {
            boardCntrl.OnPlayerMove(move, color);
            uiCntrl.OnPlayerMove(move);
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
