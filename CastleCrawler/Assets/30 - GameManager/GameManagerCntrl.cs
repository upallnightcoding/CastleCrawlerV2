using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerCntrl : MonoBehaviour
{
    [SerializeField] BoardCntrl boardCntrl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartNewGame()
    {
        boardCntrl.StartNewGame();
    }
}
