using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdBtnCntrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartNewGame()
    {
        GameManagerCntrl.Instance.StartNewGame();
    }

    public void TogglePath()
    {
        GameManagerCntrl.Instance.TogglePath();
    }
}
