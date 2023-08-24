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
    private TilePosition finalPosition;

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

        foreach (string moveName in gameData.listOfMoves)
        {
            moveDictionary.Add(moveName, new Move(moveName));
        }
    }

    public Stack<Move> StartNewGame()
    {
        RenderBoard();
        SelectStartingPoint();
        Stack<Move> moveStack = CreateAPath();
        PlaceBombs();
        return (moveStack);
    }

    private void PlaceBombs()
    {
        int nBombs = 30;

        for (int i = 0; i < nBombs; i++)
        {
            TilePosition bombPosition = SelectRandomPoint();

            tileMngr.SetBombTile(bombPosition);
        }
    }

    public bool OnPlayerMove(string moveName, Material color)
    {
        bool valid = true;
        Stack<TilePosition> tracking = new Stack<TilePosition>();
        TilePosition startingTile = new TilePosition(currentPlayPos);

        for (int move = 0; (move < moveName.Length) && valid; move++)
        {
            switch (moveName.Substring(move, 1))
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

            valid = tileMngr.TestValid(currentPlayPos);

            if (valid)
            {
                tracking.Push(new TilePosition(currentPlayPos));
            }
        }

        if (valid)
        {
            moveStack.Push(moveName);

            foreach (TilePosition position in tracking)
            {
                tileMngr.Mark(position, color);
            }
        } 
        else
        {
            currentPlayPos = new TilePosition(startingTile);
        }

        return (valid);
    }

    public string UndoPlayerMove()
    {
        string moveName = null;

        if (moveStack.Count > 0)
        {
            moveName = moveStack.Pop();

            for (int character = moveName.Length - 1; character >= 0; character--)
            {
                tileMngr.UndoTile(currentPlayPos);

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

    public bool IsFinished()
    {
        return (currentPlayPos.IsEqual(finalPosition));
    }

    private Stack<Move> CreateAPath()
    {
        int level = 0;
        int count = 0;
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
        moveStack = new Stack<string>();

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

    private TilePosition SelectRandomPoint()
    {
        int col = Random.Range(0, width);
        int row = Random.Range(0, height);

        return (new TilePosition(col, row));
    }

    private void SelectStartingPoint()
    {
        startPosition = SelectRandomPoint();

        tileMngr.SetStartingTile(startPosition);
    }
}
