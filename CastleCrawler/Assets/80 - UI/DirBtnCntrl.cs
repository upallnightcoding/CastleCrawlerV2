using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DirBtnCntrl : MonoBehaviour
{
    [SerializeField] private TMP_Text directionTxt;
    [SerializeField] private TMP_Text countTxt;
    [SerializeField] private Sprite buttonDisabled;
    [SerializeField] private Image image;
    [SerializeField] private GameData gameData;

    //private Sprite originalColor;
    private int count;
    private bool enabledBtn = true;
    private Material originalColor = null;
    private int colorIndex;

    void Start()
    {
        
    }

    public bool IsDirBtnEnabled() => enabledBtn;

    public int GetCount() => count;

    public void OnPlayerMove()
    {
        GameManagerCntrl.Instance.OnPlayerMove(directionTxt.text, gameData.tileMaterial[colorIndex]);
    }

    public void OnDirectionClick()
    {
        if (enabledBtn)
        {
            countTxt.text = (--count).ToString();
            
            if (count == 0)
            {
                enabledBtn = false;
                //image.sprite = buttonDisabled;
            }
        }
    }

    public void UndoPlayerMove()
    {
        if (count == 0)
        {
            //image.sprite = originalColor;
            enabledBtn = true;
        }

        countTxt.text = (++count).ToString();
    }

    public void Initialize(string direction, int colorIndex, int count)
    {
        // Set the button text
        directionTxt.text = direction;

        this.colorIndex = colorIndex;

        // Set the sprite of the button image
        image.sprite = gameData.btnSprite[colorIndex];

        // Cash the original sprite
        originalColor = gameData.tileMaterial[colorIndex];

        // Initialize the count of this direction
        countTxt.text = count.ToString();
        this.count = count;

    }
}
