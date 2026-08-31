using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeSC : MonoBehaviour
{
    //Score from this game mode not contribue to data score/coin
    //Gameplay concept: In a determined amount of time, player must complete objective, if not = loose
    [HideInInspector] GenMNSC genctr;
    [HideInInspector] DataSC data;
    [HideInInspector] PauseSC pauseCtr;
    [SerializeField] SunSC sun;
    [SerializeField] Text pScoreChallenge, objectiveTxt;
    [SerializeField] Image planetPrevieIMG, objectiveImg;
    [SerializeField] GameObject streakAnnoucePnl, winPanel;
    [SerializeField] List<Sprite> planetSprites = new List<Sprite>();
    [HideInInspector] SoundSC sfxCtr;

    [HideInInspector] List<GameObject> curItemsOnScreen = new List<GameObject>();

    public int deviceMode;
    private int objectiveID, targetAmountofObjective, curAmountObjective;
    private float challengeScore; //variable for win condition check
    private int tempScoreTarget, tempTimeRemain, coinToReward, gemToReward;
    public bool isEnablePlay, isContinuePlay;
    public bool isPauseGameplay;
    string targetPlanet;
    void Start()
    {
        genctr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        sfxCtr = GameObject.Find("OBJ_SoundMN").GetComponent<SoundSC>();
        data = GameObject.Find("GenMN").GetComponent<DataSC>();
        genctr.AssistObjectPreload(3);
        sun = Instantiate(sun, new Vector3(0, 4, 0), Quaternion.identity);
        curAmountObjective = 0;
        GenerateChallenge();

        isEnablePlay = false;
        isContinuePlay = false;
        tempScoreTarget = 0;
        tempTimeRemain = 0;
    }
    private void GenerateChallenge()
    {
        DetermindChallenge();
        SelectReward();
        GenerateGameplay();
        OnAssistObjectiveName();
    }

    public void SetPreviewImage(int imageOrder) => planetPrevieIMG.GetComponent<Image>().sprite = planetSprites[imageOrder];
    private void DetermindChallenge()
    {
        //Choose objecttive
        targetAmountofObjective = Random.Range(10, 100);
        objectiveID = Random.Range(0, 13);
        objectiveImg.GetComponent<Image>().sprite = planetSprites[objectiveID];
        objectiveTxt.text = curAmountObjective + "/" + targetAmountofObjective;
    }

    #region Challenge Setup
    private void SelectReward()
    {
        int tempRewardOder = Random.Range(1, 4);
        if (tempRewardOder == 1)
        {
            //Case of reward Coin
            coinToReward = Random.Range(10, 50);
            gemToReward = 0;
            //rewardTxt.text = "REWARD: " + coinToReward + " COIN ONCE WIN!";
        }
        else if (tempRewardOder == 2)
        {
            //Case of reward Free Gem
            gemToReward = Random.Range(1, 3);
            coinToReward = 0;
            //rewardTxt.text = "REWARD: " + gemToReward + " GEM ONCE WIN!";
        }
        else if (tempRewardOder == 3)
        {
            coinToReward = Random.Range(10, 50);
            gemToReward = Random.Range(1, 3);
            //rewardTxt.text = "REWARD: " + coinToReward + " COIN AND " + gemToReward + " GEM ONCE WIN!";
            //Case of Reward both Gem & Coin
        }
    }
    #endregion

    #region Objective Panel
    private IEnumerator WaitToStartGame()
    {
        yield return new WaitForSeconds(5f);
        isEnablePlay = true;
    }
    #endregion

    #region Gameplay Control
    private void GenerateGameplay()
    {
        SetIngamePlayerStat();
        StartCoroutine(WaitToStartGame());
    }
    private void SetIngamePlayerStat()
    {
        challengeScore = 0;
        pScoreChallenge.text = challengeScore.ToString();
    }
    #endregion
    private void OnWinChallenge()
    {
        isEnablePlay = false;
        winPanel.gameObject.SetActive(true);

        if (coinToReward != 0 && gemToReward == 0)
        {
            int tempScore;
            tempScore = coinToReward + data.pTotalScore;
            data.UpdateTotalScore(tempScore);
        }
        else if (coinToReward == 0 && gemToReward != 0)
        {
            int tempScore;
            tempScore = gemToReward + data.pGems;
            data.UpdateTotalGem(tempScore);
        }
        else if (coinToReward != 0 && gemToReward != 0)
        {
            int tempCoin, tempGems;
            tempCoin = coinToReward + data.pTotalScore;
            tempGems = gemToReward + data.pGems;
            data.UpdateTotalScore(tempCoin);
            data.UpdateTotalGem(tempGems);
        }

    }
    public void OnToHome()
    {
        genctr.OnLoadHome();
    }
    public void OnNextChallenge()
    {
        isContinuePlay = true;
        GenerateChallenge();
    }

    public void OnGameLose() => isEnablePlay = false;
    public void OnUpdatePlayerData()
    {
        //Place holder
    }
    public void OnPause()
    {
        genctr.OnShowPause();
        isPauseGameplay = true;
    }
    private void OnAssistObjectiveName()
    {
        if(objectiveID < 10)
        {
            targetPlanet = "OBJ0" + objectiveID + "(Clone)";
        }else if(objectiveID >= 10)
        {
            targetPlanet = "OBJ" + objectiveID + "(Clone)";
        }
    }
    public void OnCompareTarget(string planetMerged)
    {
        if (planetMerged == targetPlanet)
        {
            curAmountObjective++;
            objectiveTxt.text = curAmountObjective + "/" + targetAmountofObjective;
            if (curAmountObjective >= targetAmountofObjective)
            {
                //Win
                OnWinChallenge();
            }
        }
    }
    public void PlaySFX() => sfxCtr.PlaySFX();
    public void IncreaseScore(float score)
    {
        float tempScore;
        tempScore = challengeScore + score;
        challengeScore = tempScore;
        pScoreChallenge.text = challengeScore.ToString();
    }
    public void OnAddCurItemOnScreen(GameObject a)
    {
        curItemsOnScreen.Add(a);
    }
    public void OnEraseCurItemOnScreen()
    {
        for (int i = 0; i < curItemsOnScreen.Count / 2; i++)
        {
            Destroy(curItemsOnScreen[i]);
            curItemsOnScreen.Remove(curItemsOnScreen[i]);
        }
    }
}
