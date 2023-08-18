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

    public void SetOffBomb()
    {
        Instantiate(bombFx, gameObject.transform.position, Quaternion.identity);
    }

    public void SetStartingTile()
    {
        state = TileState.VISTED;
        GetComponent<Renderer>().material = gameData.TileGreen;
        tileLabel.text = "Start";
    }

    public void SetBombTile()
    {
        if (IsTileOpen())
        {
            state = TileState.BOMB;
            GetComponent<Renderer>().material = gameData.TileGreen;
            tileLabel.text = "Bomb";
        }
    }

    public void SetEndingTile()
    {
        state = TileState.VISTED;
        GetComponent<Renderer>().material = gameData.TileRed;
        tileLabel.text = "Ending";
    }

    public void SetTileAsVisted()
    {
        state = TileState.VISTED;
        GetComponent<Renderer>().material = GameManagerCntrl.Instance.DisplayTileMaterial();
        tileLabel.text = "Visited";
    }

    public void ResetTile()
    {
        state = TileState.OPEN;
        GetComponent<Renderer>().material = gameData.TileGray;
        tileLabel.text = "";
    }

    public void UndoTile()
    {
        state = TileState.OPEN;
        GetComponent<Renderer>().material = gameData.TileGray;
    }

    public void SetMove(Material color)
    {
        GetComponent<Renderer>().material = color;

        if (state == TileState.BOMB)
        {
            bombFx.SetActive(true);
        }
    }

    public bool IsTileOpen()
    {
        return (state == TileState.OPEN);
    }

    private enum TileState
    {
        OPEN,
        VISTED,
        BOMB
    }
}
