using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCntrl : MonoBehaviour
{
    [SerializeField] BoardCntrl boardCntrl;
    [SerializeField] UiCntrl uiCntrl;

    // Start is called before the first frame update
    void Start()
    {
        boardCntrl.Initialize();
    }

    public void StartNewGame()
    {
        Stack<Move> moves = boardCntrl.StartNewGame();

        uiCntrl.StartNewGame(moves);
    }
}
