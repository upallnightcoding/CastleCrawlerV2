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

    //private Sprite originalColor;
    private int count;
    private bool enabledBtn = true;
    private Sprite originalColor = null;

    void Start()
    {
        
    }

    public void OnPlayerMove()
    {
        GameManagerCntrl.Instance.OnPlayerMove(directionTxt.text, image.sprite);
    }

    public void OnDirectionClick()
    {
        if (enabledBtn)
        {
            countTxt.text = (--count).ToString();
            
            if (count == 0)
            {
                enabledBtn = false;
                image.sprite = buttonDisabled;
            }
        }
    }

    public bool IsDirBtnEnabled()
    {
        return (enabledBtn);
    }

    public void UndoPlayerMove()
    {
        if (count == 0)
        {
            image.sprite = originalColor;
            enabledBtn = true;
        }

        countTxt.text = (++count).ToString();
    }

    public void Initialize(string direction, Sprite color, int count)
    {
        // Set the button text
        directionTxt.text = direction;

        // Set the sprite of the button image
        image.sprite = color;

        // Cash the original sprite
        originalColor = color;

        // Initialize the count of this direction
        countTxt.text = count.ToString();
        this.count = count;

    }
}
