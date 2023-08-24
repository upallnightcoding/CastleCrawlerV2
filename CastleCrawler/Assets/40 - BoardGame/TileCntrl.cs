using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TileCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private TMP_Text tileLabel;
    [SerializeField] private GameObject bombFx;
    [SerializeField] private Image image;

    private TileState state = TileState.OPEN;

    private TileState ressetTileState;
    private Material resetMaterial;
    private string resetText;

    public void SetStartingTile()
    {
        SetTile(TileState.START, gameData.StartEndTileColor);
    }

    public void SetEndingTile()
    {
        SetTile(TileState.END, gameData.StartEndTileColor);
        //image.enabled = true;
        image.gameObject.SetActive(true);
        //image.sprite = gameData.castelMaterial.mainTexture;
        //GetComponent<Renderer>().material = gameData.castelMaterial;
    }

    public void SetBombTile()
    {
        if (IsTileOpen())
        {
            Material material = gameData.debugSw ? gameData.BombTileColor : gameData.TileGray;
            SetTile(TileState.BOMB, material);
        }
    }

    public void SetTileAsVisted()
    {
        SetTile(TileState.VISITED, GameManagerCntrl.Instance.DisplayTileMaterial());
    }    

    public void ResetTile()
    {
        SetTile(ressetTileState, resetMaterial, resetText);
    }

    public void UndoTile()
    {
        //state = TileState.OPEN;
        //GetComponent<Renderer>().material = gameData.TileGray;
        //SetTile(TileState.OPEN, gameData.TileGray);
        ResetTile();
    }

    public bool TestValid()
    {
        bool valid = true;

        switch(state)
        {
            case TileState.BOMB:
                Instantiate(bombFx, gameObject.transform.position, Quaternion.identity);
                GetComponent<Renderer>().material = gameData.bombMaterial;
                GameManagerCntrl.Instance.ReduceHealth();
                valid = false;
                break;
            case TileState.START:
            case TileState.END:
            case TileState.PATH:
                valid = false;
                break;
        }

        return (valid);
    }

    public void Mark(Material color)
    {
        SetTile(TileState.MARK, color);
    }

    public bool IsTileOpen()
    {
        return (state == TileState.OPEN);
    }

    public void Initialize()
    {
        SetTile(TileState.OPEN, gameData.TileGray, "");
    }

    private void SetTile(TileState tileState, Material material)
    {
        SetTile(tileState, material, tileState.ToString());
    }

    private void SetTile(TileState tileState, Material material, string text)
    {
        ressetTileState = state;
        resetMaterial = GetComponent<Renderer>().material;
        resetText = tileLabel.text;

        state = tileState;
        GetComponent<Renderer>().material = material;

        if (gameData.debugSw)
        {
            tileLabel.text = text;
        }
    }

    private enum TileState
    {
        OPEN,
        START,
        END,
        PATH,
        MARK,
        VISITED,
        BOMB
    }
}
