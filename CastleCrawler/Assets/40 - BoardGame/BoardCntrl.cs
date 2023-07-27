using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCntrl : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject tilePreFab;
    [SerializeField] Transform parent;

    void Start()
    {
        
    }

    public void CreateBoard()
    {
        int width = gameData.width;
        int height = gameData.hieght;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = gameData.GetTilePos(x, z);
                Instantiate(tilePreFab, position, Quaternion.identity, parent);
            }
        }

    }
}
