using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private TMP_Text tileLabel;
    [SerializeField] private GameObject bombFx;

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
        SetTile(TileState.START, gameData.StartEndTileColor);
    }

    public void SetEndingTile()
    {
        SetTile(TileState.END, gameData.StartEndTileColor);
    }

    public void SetBombTile()
    {
        if (IsTileOpen())
        {
            SetTile(TileState.BOMB, gameData.BombTileColor);
        }
    }

    public void SetTileAsVisted()
    {
        SetTile(TileState.VISITED, GameManagerCntrl.Instance.DisplayTileMaterial());
    }

    public void ResetTile()
    {
        SetTile(TileState.OPEN, gameData.TileGray, "");
    }

    public void UndoTile()
    {
        state = TileState.OPEN;
        GetComponent<Renderer>().material = gameData.TileGray;
    }

    public bool SetMove(Material color)
    {
        bool valid = false;

        switch(state)
        {
            case TileState.BOMB:
                Instantiate(bombFx, gameObject.transform.position, Quaternion.identity);
                GetComponent<Renderer>().material = gameData.TileBlack;
                break;
            case TileState.OPEN:
            case TileState.VISITED:
                SetTile(TileState.PATH, color);
                valid = true;
                break;
            case TileState.END:
                valid = true;
                break;
        }

        return (valid);
    }

    public bool IsTileOpen()
    {
        return (state == TileState.OPEN);
    }

    private void SetTile(TileState tileState, Material material)
    {
        SetTile(tileState, material, tileState.ToString());
    }

    private void SetTile(TileState tileState, Material material, string text)
    {
        state = tileState;
        GetComponent<Renderer>().material = material;
        tileLabel.text = text;
    }

    private enum TileState
    {
        OPEN,
        START,
        END,
        PATH,
        VISITED,
        BOMB
    }
}
