using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] Transform directionButtons;
    [SerializeField] GameObject dirBtnPreFab;
    [SerializeField] GameData gameData;

    private Dictionary<string, int> moveCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartNewGame(Stack<Move> moves)
    {
        moveCount = new Dictionary<string, int>();

        foreach(Move move in moves)
        {
            if (moveCount.TryGetValue(move.moveName, out int count))
            {
                moveCount[move.moveName] = ++count;
            } else {
                moveCount[move.moveName] = 1;
            }
        }

        int colorIndex = 0;

        foreach (string moveName in moveCount.Keys)
        {
            int count = moveCount[moveName];

            GameObject button = Object.Instantiate(dirBtnPreFab, directionButtons);
            DirBtnCntrl dirBtnCntrl = button.GetComponent<DirBtnCntrl>();
            dirBtnCntrl.Initialize(moveName, gameData.btnColors[colorIndex++], count);
        }
    }
}
