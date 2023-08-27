using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TileCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private TMP_Text tileLabel;
    [SerializeField] private Image image;

    private TileState state = TileState.OPEN;

    private TileState ressetTileState;
    private Material resetMaterial;
    private string resetText;

    public void SetStartingTile()
    {
        SetTile(TileState.START, gameData.StartEndTileColor);
        image.sprite = gameData.crownSprite;
        image.gameObject.SetActive(true);
    }

    public void SetEndingTile()
    {
        SetTile(TileState.END, gameData.StartEndTileColor);
        image.sprite = gameData.castleSprite;
        image.gameObject.SetActive(true);
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
        ResetTile();
    }

    public bool IsValidTile()
    {
        bool valid = true;

        switch(state)
        {
            case TileState.BOMB:
                GetComponent<Renderer>().material = gameData.bombMaterial;
                GameManagerCntrl.Instance.FxMovedInPathOfBomb(gameObject.transform.position);
                valid = false;
                break;
            case TileState.START:
            case TileState.PATH:
            //case TileState.END:
            case TileState.MARK:
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
