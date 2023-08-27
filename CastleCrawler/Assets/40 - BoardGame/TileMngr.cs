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

        tileCntrls[col, row].Initialize();
    }

    public void SetStartingTile(TilePosition position) =>
        tileCntrls[position.col, position.row].SetStartingTile();

    public void SetBombTile(TilePosition position) =>
        tileCntrls[position.col, position.row].SetBombTile();

    public bool IsOpen(TilePosition position) =>
        tileCntrls[position.col, position.row].IsTileOpen();

    public void SetTileAsVisted(TilePosition position) =>
        tileCntrls[position.col, position.row].SetTileAsVisted();

    public void Mark(TilePosition position, Material color) =>
        tileCntrls[position.col, position.row].Mark(color);

    public bool IsMoveValid(TilePosition position)
    {
        bool offTheBoard = IsOffTheBoard(position);
        bool valid = false;

        if (offTheBoard)
        {
            GameManagerCntrl.Instance.DisplayIllegalMoveBanner();
        } 
        else
        {
            valid = tileCntrls[position.col, position.row].IsValidTile();
        }

        return (!offTheBoard && valid);
    }

    public void ResetTile(TilePosition position) =>
        tileCntrls[position.col, position.row].ResetTile();

    public void UndoTile(TilePosition position) =>
       tileCntrls[position.col, position.row].UndoTile();

    public void SetEndingTile(TilePosition position) =>
        tileCntrls[position.col, position.row].SetEndingTile();

    /*************************/
    /*** Private Functions ***/
    /*************************/

    private bool IsOffTheBoard(TilePosition position)
    {
        bool colOutOfRange = (position.col >= GameData.height) || (position.col < 0);
        bool rowOutOfRange = (position.row >= GameData.width) || (position.row < 0);

        return (colOutOfRange || rowOutOfRange);
    }

}
