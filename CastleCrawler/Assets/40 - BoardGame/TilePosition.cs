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

    public void MoveToNextTile(Step step, bool direction = true)
    {
        col += step.col * (direction ? 1 : -1);
        row += step.row * (direction ? 1 : -1);
    }

    public bool IsValid()
    {
        bool colPos = (col >= 0) && (col < GameData.width);
        bool rowPos = (row >= 0) && (row < GameData.height);

        return (colPos && rowPos);
    }

    public bool IsEqual(TilePosition tile)
    {
        return ((tile.col == col) && (tile.row == row));
    }

    public void Log(string text)
    {
        Debug.Log($"{text}: {col}/{row}");
    }
}
