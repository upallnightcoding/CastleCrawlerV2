using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCntrl : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject tilePreFab;
    [SerializeField] Transform parent;

    private Dictionary<string, Move> moveDictionary = null;

    //private TileCntrl[,] tileCntrls = null;
    private TileMngr tileMngr = null;

    private TilePosition startPosition;

    private int width = 0;
    private int height = 0;

    public void Initialize()
    {
        width = GameData.width;
        height = GameData.height;

        //tileCntrls = new TileCntrl[width, height];
        moveDictionary = new Dictionary<string, Move>();
        tileMngr = new TileMngr(gameData);

        foreach (string moveName in gameData.listOfMoves)
        {
            moveDictionary.Add(moveName, new Move(moveName));
        }
    }

    public void StartNewGame()
    {
        RenderBoard();
        SelectStartingPoint();
        CreateAPath();
    }

    private void CreateAPath()
    {
        int level = 0;
        int count = 0;
        TilePosition finalPosition = null;
        TilePosition tile = new TilePosition(startPosition);

        while (BuildingPath(level) && SafeGuard(count))
        {
            int[] moves = ShuffleMoves();
            Move moveFound = null;

            for (int i = 0; (i < gameData.listOfMoves.Length) && (moveFound == null); i++)
            {
                if (moveDictionary.TryGetValue(gameData.listOfMoves[moves[i]], out Move move))
                {
                    finalPosition = move.IsValid(tile, tileMngr);

                    if (finalPosition != null)
                    {
                        move.Log("*** Selected Move");
                        moveFound = move;
                        tile = new TilePosition(finalPosition);
                        level++;
                    }
                }
            }

            count++;
        }

        tileMngr.SetEndingTile(finalPosition);
    }

    private bool SafeGuard(int count)
    {
        return (count < 500);
    }

    private int[] ShuffleMoves()
    {
        int[] moves = new int[gameData.listOfMoves.Length];

        for (int i = 0; i < gameData.listOfMoves.Length; i++)
        {
            moves[i] = i;
        }

        for (int i = 0; i < gameData.listOfMoves.Length / 2; i++)
        {
            int indexA = Random.Range(0, gameData.listOfMoves.Length);
            int indexB = Random.Range(0, gameData.listOfMoves.Length);

            int value = moves[indexA];
            moves[indexA] = moves[indexB];
            moves[indexB] = value;
        }

        return (moves);
    }

    private bool BuildingPath(int level)
    {
        return (level < gameData.level);
    }

    private void RenderBoard()
    {
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                Vector3 position = gameData.GetTilePos(col, row);
                GameObject tile = Instantiate(tilePreFab, position, Quaternion.identity, parent);
                tileMngr.Set(col, row, tile);
            }
        }
    }

    private void SelectStartingPoint()
    {
        int col = Random.Range(0, width);
        int row = Random.Range(0, height);

        startPosition = new TilePosition(col, row);

        tileMngr.SetStartingTile(startPosition);
    }
}
