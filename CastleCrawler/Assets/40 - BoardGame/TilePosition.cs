using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosition 
{
    public int col = 0;
    public int row = 0;

    public TilePosition(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    public TilePosition(TilePosition tile) : this(tile.col, tile.row)
    {

    }

    public TilePosition MoveToNextTile(Step step)
    {
        return (new TilePosition(col + step.col, row + step.row));
    }

    public bool IsValid()
    {
        bool colPos = (col >= 0) && (col < GameData.width);
        bool rowPos = (row >= 0) && (row < GameData.height);

        return (colPos && rowPos);
    }

    public void Log(string text)
    {
        Debug.Log($"{text}: {col}/{row}");
    }
}
