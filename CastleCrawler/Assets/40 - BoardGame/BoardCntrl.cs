using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCntrl : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject tilePreFab;
    [SerializeField] Transform parent;
    [SerializeField] TileMngr tileMngr;

    private Dictionary<string, Move> moveDictionary = null;

    private TilePosition startPosition;

    private TilePosition currentPlayPos;

    private int width = 0;
    private int height = 0;

    private Stack<string> moveStack;

    private bool SafeGuard(int count) => count < gameData.safeGuardLimit;
    private bool BuildingPath(int level) => level < gameData.level;

    public void Initialize()
    {
        width = GameData.width;
        height = GameData.height;

        moveDictionary = new Dictionary<string, Move>();
        moveStack = new Stack<string>();

        foreach (string moveName in gameData.listOfMoves)
        {
            moveDictionary.Add(moveName, new Move(moveName));
        }
    }

    public Stack<Move> StartNewGame()
    {
        RenderBoard();
        SelectStartingPoint();
        return(CreateAPath());
    }

    public void OnPlayerMove(string moveName, Sprite color)
    {
        for (int character = 0; character < moveName.Length; character++)
        {
            switch (moveName.Substring(character, 1))
            {
                case "N":
                    currentPlayPos.MoveToNextTile(GameData.NORTH_STEP);
                    break;
                case "S":
                    currentPlayPos.MoveToNextTile(GameData.SOUTH_STEP);
                    break;
                case "E":
                    currentPlayPos.MoveToNextTile(GameData.EAST_STEP);
                    break;
                case "W":
                    currentPlayPos.MoveToNextTile(GameData.WEST_STEP);
                    break;
            }

            tileMngr.SetMove(currentPlayPos, color);
        }

        moveStack.Push(moveName);
    }

    public string UndoPlayerMove()
    {
        string moveName = null;

        if (moveStack.Count > 0)
        {
            moveName = moveStack.Pop();

            for (int character = moveName.Length - 1; character >= 0; character--)
            {
                tileMngr.ResetTile(currentPlayPos);

                switch (moveName.Substring(character, 1))
                {
                    case "N":
                        currentPlayPos.MoveToNextTile(GameData.NORTH_STEP, false);
                        break;
                    case "S":
                        currentPlayPos.MoveToNextTile(GameData.SOUTH_STEP, false);
                        break;
                    case "E":
                        currentPlayPos.MoveToNextTile(GameData.EAST_STEP, false);
                        break;
                    case "W":
                        currentPlayPos.MoveToNextTile(GameData.WEST_STEP, false);
                        break;
                }
            }
        } 

        return (moveName);
    }

    private Stack<Move> CreateAPath()
    {
        int level = 0;
        int count = 0;
        TilePosition finalPosition = null;
        TilePosition tile = new TilePosition(startPosition);
        Stack<Move> moves = new Stack<Move>();

        while (BuildingPath(level) && SafeGuard(count))
        {
            int[] moveIndex = ShuffleMoves();
            Move moveFound = null;

            for (int i = 0; (i < gameData.listOfMoves.Length) && (moveFound == null); i++)
            {
                if (moveDictionary.TryGetValue(gameData.listOfMoves[moveIndex[i]], out Move move))
                {
                    finalPosition = move.IsValid(tile, tileMngr);

                    if (finalPosition != null)
                    {
                        move.Log("*** Selected Move");
                        moveFound = move;
                        moves.Push(move);
                        tile = new TilePosition(finalPosition);
                        level++;
                    }
                }
            }

            count++;
        }

        tileMngr.SetEndingTile(finalPosition);

        currentPlayPos = new TilePosition(startPosition);

        return (moves);
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

    private void RenderBoard()
    {
        tileMngr.Initialize();

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
