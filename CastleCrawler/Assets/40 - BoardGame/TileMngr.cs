using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMngr : MonoBehaviour
{
    private TileCntrl[,] tileCntrls = null;

    private Stack<GameObject> tileObjects;

    public void Initialize()
    {
        if ((tileObjects != null) && (tileObjects.Count > 0))
        {
            foreach(GameObject tile in tileObjects)
            {
                Destroy(tile); 
            }
        }

        tileObjects = new Stack<GameObject>();
        tileCntrls = new TileCntrl[GameData.width, GameData.height];
    }

    public void Set(int col, int row, GameObject tile)
    {
        tileCntrls[col, row] = tile.GetComponent<TileCntrl>();

        ResetTile(new TilePosition(col, row));
    }

    public void SetStartingTile(TilePosition position)
    {
        tileCntrls[position.col, position.row].SetStartingTile();
    }

    public bool IsOpen(TilePosition position)
    {
        return (tileCntrls[position.col, position.row].IsOpen());
    }

    public void SetTileAsVisted(TilePosition position)
    {
        tileCntrls[position.col, position.row].SetTileAsVisted();
    }

    public void ResetTile(TilePosition position)
    {
        tileCntrls[position.col, position.row].ResetTile();
    }

    public void SetEndingTile(TilePosition position)
    {
        tileCntrls[position.col, position.row].SetEndingTile();
    }
}
