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
    }

    private enum TileState
    {
        OPEN,
        CLOSED
    }
}
