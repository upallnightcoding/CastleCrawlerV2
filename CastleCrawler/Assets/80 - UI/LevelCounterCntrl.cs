using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounterCntrl : MonoBehaviour
{
    [SerializeField] private TMP_Text levelCnt;
    [SerializeField] private GameData gameData;

    public void Start()
    {
        levelCnt.text = gameData.level.ToString();
    }
}
