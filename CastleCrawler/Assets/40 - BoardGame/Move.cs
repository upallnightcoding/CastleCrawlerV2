using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    private Step[] move = null;
    public string moveName = null;

    public Move(string moveName)
    {
        this.moveName = moveName;

        move = new Step[moveName.Length];

        for (int character = 0; character < moveName.Length; character++)
        {
            switch(moveName.Substring(character, 1))
            {
                case "N":
                    move[character] = GameData.NORTH_STEP;
                    break;
                case "S":
                    move[character] = GameData.SOUTH_STEP;
                    break;
                case "E":
                    move[character] = GameData.EAST_STEP;
                    break;
                case "W":
                    move[character] = GameData.WEST_STEP;
                    break;
            }
        }
    }

    public void Log(string text)
    {
        Debug.Log($"{text}: {moveName}");
    }

    public TilePosition IsValid(TilePosition tile, TileMngr tileMgr)
    {
        bool valid = true;
        TilePosition nextTile = new TilePosition(tile);
        Stack<TilePosition> tracking = new Stack<TilePosition>();

        nextTile.Log("Start of Segment");

        for (int i = 0; (i < move.Length) && valid; i++)
        {
            nextTile = nextTile.MoveToNextTile(move[i]);
            nextTile.Log("Test Next Tile");

            valid = (nextTile.IsValid() && tileMgr.IsOpen(nextTile));

            if (valid)
            {
                tileMgr.SetTileAsVisted(nextTile);
                tracking.Push(nextTile);
            } 
        }

        if (!valid)
        {
            tile.Log("InValid Tile");

            foreach(TilePosition position in tracking)
            {
                position.Log("Reset Position");
                tileMgr.ResetTile(position);
            }

            nextTile = null;
        }

        return (nextTile);
    }
}
