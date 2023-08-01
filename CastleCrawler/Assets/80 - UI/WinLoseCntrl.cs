using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseCntrl : MonoBehaviour
{
    [SerializeField] private GameObject winFlag;
    [SerializeField] private GameObject loseFlag;
    
    void Start()
    {
        winFlag.SetActive(false);
        loseFlag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
