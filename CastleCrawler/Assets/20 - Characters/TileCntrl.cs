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
        state = TileState.VISTED;
        GetComponent<Renderer>().material = gameData.TileGreen;
    }

    public void SetEndingTile()
    {
        state = TileState.VISTED;
        GetComponent<Renderer>().material = gameData.TileRed;
    }

    public void SetTileAsVisted()
    {
        state = TileState.VISTED;
        GetComponent<Renderer>().material = gameData.TileWhite;
    }

    public void ResetTile()
    {
        state = TileState.OPEN;
        GetComponent<Renderer>().material = gameData.TileGray;
    }

    public void SetMove(Sprite color)
    {
        state = TileState.MOVE;
        //GetComponent<Renderer>().material.color = Color.cyan;
        GetComponent<Renderer>().material.mainTexture = color.texture;
    }

    public bool IsOpen()
    {
        return (state == TileState.OPEN);
    }

    private enum TileState
    {
        OPEN,
        VISTED,
        MOVE
    }
}
