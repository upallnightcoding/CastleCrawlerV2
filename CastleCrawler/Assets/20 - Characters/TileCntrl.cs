using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCntrl : MonoBehaviour
{
    [SerializeField] GameData gameData;

    private TileState state = TileState.OPEN;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStartingTile()
    {
        GetComponent<Renderer>().material = gameData.TileGreen;
        state = TileState.VISTED;
    }

    public void SetEndingTile()
    {
        GetComponent<Renderer>().material = gameData.TileRed;
        state = TileState.VISTED;
    }

    public void SetTileAsVisted()
    {
        GetComponent<Renderer>().material = gameData.TileWhite;
        state = TileState.VISTED;
    }

    public void ResetTile()
    {
        state = TileState.OPEN;
        GetComponent<Renderer>().material = gameData.TileGray;
    }

    public bool IsOpen()
    {
        return (state == TileState.OPEN);
    }

    private enum TileState
    {
        OPEN,
        VISTED
    }
}
