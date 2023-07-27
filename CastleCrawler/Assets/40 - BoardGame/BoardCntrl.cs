using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCntrl : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject tilePreFab;
    [SerializeField] Transform parent;

    private TileCntrl[,] tileCntrls;

    private TileCntrl startPosition;

    void Start()
    {
        
    }

    public void StartNewGame()
    {
        Initialize();
        RenderBoard();
        SelectStartingPoint();
        //CreateAPath();
    }

    private bool CreateAPath()
    {
        while(!IsLevelReached())
        {
            Move move = SelectAMove();

            if (MoveIsLegal(move))
            {
                Track(move);
            }
        }

        return (IsLevelReached());
    }

    private void Track(Move move)
    {
        
    }

    private bool IsLevelReached()
    {
        return (false);
    }

    private bool MoveIsLegal(Move move)
    {
        return (false);
    }

    private Move SelectAMove()
    {
        return (null);
    }

    private void Initialize()
    {
        tileCntrls = new TileCntrl[gameData.width, gameData.height];
    }

    private void RenderBoard()
    {
        int width = gameData.width;
        int height = gameData.height;

        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                Vector3 position = gameData.GetTilePos(col, row);
                GameObject tile = Instantiate(tilePreFab, position, Quaternion.identity, parent);
                tileCntrls[col, row] = tile.GetComponent<TileCntrl>();
            }
        }
    }

    private void SelectStartingPoint()
    {
        int startCol = Random.Range(0, gameData.width);
        int startRow = Random.Range(0, gameData.height);

        startPosition = tileCntrls[startCol, startRow];
        startPosition.SetStartingTile();
    }
}
