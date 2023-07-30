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

    void Start()
    {
        
    }

    public void OnPlayerMove()
    {
        Debug.Log($"Move Name: {directionTxt.text}");
    }

    public void Initialize(string direction, Sprite sprite, int count)
    {
        // Set the button text
        directionTxt.text = direction;

        // Set the sprite of the button image
        image.sprite = sprite;

        // Cash the original sprite
        //originalSprite = sprite;

        // Initialize the count of this direction
        countTxt.text = count.ToString();
        //selectCount = count;

    }
}
