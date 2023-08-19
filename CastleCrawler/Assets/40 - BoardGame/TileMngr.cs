using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMngr : MonoBehaviour
{
    private TileCntrl[,] tileCntrls = null;

    private List<GameObject> tileObjects;

    public void Start()
    {
        tileObjects = new List<GameObject>();
        tileCntrls = new TileCntrl[GameData.width, GameData.height];
    }

    public void Initialize()
    {
        if ((tileObjects != null) && (tileObjects.Count > 0))
        {
            foreach(GameObject tile in tileObjects)
            {
                Destroy(tile); 
            }
        }
    }

    public void Set(int col, int row, GameObject tile)
    {
        tileCntrls[col, row] = tile.GetComponent<TileCntrl>();

        tileObjects.Add(tile);

        ResetTile(new TilePosition(col, row));
    }

    public void SetStartingTile(TilePosition position)
    {
        tileCntrls[position.col, position.row].SetStartingTile();
    }

    public void SetBombTile(TilePosition position)
    {
        tileCntrls[position.col, position.row].SetBombTile();
    }

    public bool IsOpen(TilePosition position)
    {
        return (tileCntrls[position.col, position.row].IsTileOpen());
    }

    public void SetTileAsVisted(TilePosition position)
    {
        tileCntrls[position.col, position.row].SetTileAsVisted();
    }

    public bool SetMove(TilePosition position, Material color)
    {
        return(tileCntrls[position.col, position.row].SetMove(color));
    }

    public void ResetTile(TilePosition position) =>
        tileCntrls[position.col, position.row].ResetTile();

    public void UndoTile(TilePosition position) =>
       tileCntrls[position.col, position.row].UndoTile();

    public void SetEndingTile(TilePosition position) =>
        tileCntrls[position.col, position.row].SetEndingTile();
}
