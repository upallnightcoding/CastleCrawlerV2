using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;

public class UiCntrl : MonoBehaviour
{
    [SerializeField] private Transform dirBtnContainer;
    [SerializeField] private GameObject dirBtnPreFab;
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject winBanner;
    [SerializeField] private GameObject loseBanner;
    [SerializeField] private GameObject illegalMoveBanner;
    [SerializeField] private TMP_Text levelCntText;
    [SerializeField] private Image star1On;
    [SerializeField] private Image star2On;
    [SerializeField] private Image star3On;
    [SerializeField] private Image heart0;
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;

    private Dictionary<string, DirBtnCntrl> dirBtnDict;

    private List<GameObject> listOfDirBtns = null;

    private Image[] hearts = new Image[3];
    private int heartCnt = 2;

    private int starCnt = 0;
    private int levelCnt = 0;

    void Start()
    {
        winBanner.SetActive(false);
        loseBanner.SetActive(false);
        levelCntText.text = gameData.level.ToString();
        levelCnt = gameData.level;

        hearts[0] = heart0;
        hearts[1] = heart1;
        hearts[2] = heart2;

        listOfDirBtns = new List<GameObject>();
    }

    // Update is called once per frame
    public void StartNewGame(Stack<Move> moves)
    {
        Dictionary<string, int> moveCntDict = new Dictionary<string, int>();
        dirBtnDict = new Dictionary<string, DirBtnCntrl>();

        foreach(GameObject go in listOfDirBtns)
        {
            Destroy(go);
        }

        foreach(Move move in moves)
        {
            if (moveCntDict.TryGetValue(move.moveName, out int count))
            {
                moveCntDict[move.moveName] = ++count;
            } else {
                moveCntDict[move.moveName] = 1;
            }
        }

        int colorIndex = 0;

        foreach (string moveName in moveCntDict.Keys)
        {
            int count = moveCntDict[moveName];

            GameObject button = Object.Instantiate(dirBtnPreFab, dirBtnContainer);
            DirBtnCntrl dirBtnCntrl = button.GetComponent<DirBtnCntrl>();
            dirBtnCntrl.Initialize(moveName, colorIndex++, count);

            dirBtnDict[moveName] = dirBtnCntrl;

            listOfDirBtns.Add(button);
        }
    }

    public bool TotalPointsIsZero()
    {
        int total = 0;

        foreach (string move in dirBtnDict.Keys)
        {
            DirBtnCntrl dirBtnCntrl = dirBtnDict[move];

            total += dirBtnCntrl.GetCount();
        }

        return (total == 0);
    }

    public void ReduceHealth()
    {
        if (heartCnt >= 0)
        {
            hearts[heartCnt--].enabled = false;

            if (heartCnt < 0)
            {
                DisplayLooseBanner();
            }
        } 
    }

    public void OnPlayerMove(string moveName)
    {
        dirBtnDict[moveName].OnDirectionClick();
    }

    public bool IsDirBtnEnabled(string moveName)
    {
        return (dirBtnDict[moveName].IsDirBtnEnabled());
    }

    public void UndoPlayerMove(string moveName)
    {
        dirBtnDict[moveName].UndoPlayerMove();
    }

    /******************************/
    /*** Render Banner Routines ***/
    /******************************/

    public void DisplayWinBanner()
    {
        StartCoroutine(DisplayBanner(Banners.WIN));

        UpdateLevel();
    }

    public void DisplayLooseBanner()
    {
        StartCoroutine(DisplayBanner(Banners.LOOSE));
    }

    public void DisplayIllegalMoveBanner()
    {
        StartCoroutine(DisplayBanner(Banners.ILLEGAL));
    }

    /*************************/
    /*** Private Functions ***/
    /*************************/

    /*private IEnumerator ShowBanner()
    {
        winBanner.SetActive(true);

        yield return new WaitForSeconds(3);

        winBanner.SetActive(false);
    }*/

    private IEnumerator DisplayBanner(Banners banner)
    {
        DisplayBanner(banner, true);

        yield return new WaitForSeconds(3);

        DisplayBanner(banner, false);
    }

    private void DisplayBanner(Banners banner, bool offon)
    {
        GameObject bannerGameObject = null;

        switch(banner)
        {
            case Banners.LOOSE:
                bannerGameObject = loseBanner;
                break;
            case Banners.WIN:
                bannerGameObject = winBanner;
                break;
            case Banners.ILLEGAL:
                bannerGameObject = illegalMoveBanner;
                break;
        }

        bannerGameObject.SetActive(offon);
    }

    private void UpdateLevel()
    {
        if (++starCnt > 3)
        {
            starCnt = 0;
            levelCnt++;

            star1On.enabled = false;
            star2On.enabled = false;
            star3On.enabled = false;

            gameData.level = levelCnt;
        }

        levelCntText.text = levelCnt.ToString();

        switch(starCnt)
        {
            case 1:
                star1On.enabled = true;
                break;
            case 2:
                star2On.enabled = true;
                break;
            case 3:
                star3On.enabled = true;
                break;
        }
    }
}

public enum Banners
{
    LOOSE,
    WIN,
    ILLEGAL
}
