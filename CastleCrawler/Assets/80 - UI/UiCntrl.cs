using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] Transform directionButtons;
    [SerializeField] GameObject dirBtnPreFab;
    [SerializeField] GameData gameData;

    private Dictionary<string, DirBtnCntrl> durBtnDict;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartNewGame(Stack<Move> moves)
    {
        Dictionary<string, int> moveCntDict = new Dictionary<string, int>();
        durBtnDict = new Dictionary<string, DirBtnCntrl>();

        foreach(Move move in moves)
        {
            if (moveCntDict.TryGetValue(move.moveName, out int count))
            {
                moveCntDict[move.moveName] = ++count;
            } else {
                moveCntDict[move.moveName] = 1;
            }
        }

        int colorIndex = 0;

        foreach (string moveName in moveCntDict.Keys)
        {
            int count = moveCntDict[moveName];

            GameObject button = Object.Instantiate(dirBtnPreFab, directionButtons);
            DirBtnCntrl dirBtnCntrl = button.GetComponent<DirBtnCntrl>();
            dirBtnCntrl.Initialize(moveName, gameData.btnColors[colorIndex++], count);

            durBtnDict[moveName] = dirBtnCntrl;
        }
    }

    public void OnPlayerMove(string moveName)
    {
        durBtnDict[moveName].OnDirectionClick();
    }

    public bool IsDirBtnEnabled(string moveName)
    {
        return (durBtnDict[moveName].IsDirBtnEnabled());
    }

    public void UndoPlayerMove(string moveName)
    {
        durBtnDict[moveName].UndoPlayerMove();
    }
}
