using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeSC : MonoBehaviour
{
    //Aecade mode
    //Playr control Sun to spawn planets
    [HideInInspector] GenMNSC genCtr;
    [HideInInspector] PauseSC pauseCtr;
    [HideInInspector] DataSC data;
    [SerializeField] SunSC sun;
    [SerializeField] Image planetPrevieIMG;
    [SerializeField] Text pScoreTxt, pLevelTxt;
    [SerializeField] List<Sprite> previewPlanet = new List<Sprite>();
    [SerializeField] GameObject streakAnnoucePnl;
    public int deviceMode, gameMode;
    public int  arcadeLv, baseTargetLv;
    private float arcadeScore;
    public bool isPauseGameplay;
    public float bonusStreakCount;
    public int bonusCountdown;
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        data = GameObject.Find("GenMN").GetComponent<DataSC>();
        genCtr.AssistObjectPreload(2);
        sun = Instantiate(sun, new Vector3(0, 4, 0), Quaternion.identity);

        GenerateGameplay();
        SettingSun();
    }

    void Update() { }

    public void SetDeviceMode(int mode)
    {
        deviceMode = mode;
    }
    private void SettingSun()
    {
        sun.SetGameMode(2);
        sun.SetDeviceType(deviceMode);
    }

    public void SetPreviewImage(int imageOrder)
    {
        planetPrevieIMG.GetComponent<Image>().sprite = previewPlanet[imageOrder];
    }
    public void OnPause()
    {
        genCtr.OnShowPause();
        isPauseGameplay = true;
    }
    private void SettingUI()
    {
        pScoreTxt.text = "0";
        pLevelTxt.text = "0";
    }
    public void IncreaseScore(float score)
    {
        float tempScore;
        tempScore = arcadeScore + score;
        arcadeScore = tempScore;
        pScoreTxt.text = arcadeScore.ToString();
        if(arcadeScore == baseTargetLv)
        {
            arcadeLv++;
            pLevelTxt.text = arcadeLv.ToString();
            DetermineNextLevelTarget();
        }
    }
    private void DetermineNextLevelTarget()
    {
        int newBaseLv;
        newBaseLv = baseTargetLv * arcadeLv * 2;
        baseTargetLv = newBaseLv;
    }
    public void GenerateGameplay()
    {
        baseTargetLv = 10;
        arcadeLv = 0;
        arcadeScore = 0;
        isPauseGameplay = false;

        SettingUI();
    }
    public void UpdatePlayerData()
    {
        int tempScore;
        tempScore = ((int)arcadeScore);
        data.UpdateTotalScore(tempScore);
    }
    #region bonus streaks
    public void OnShowStreak()
    {
        streakAnnoucePnl.SetActive(true);
        Invoke(nameof(OnUnShowStreak), 2f);
        CancelInvoke(nameof(CountdownBonus));
    }
    private void OnUnShowStreak()
    {
        //Case of complet streak
        streakAnnoucePnl.SetActive(false);
        bonusCountdown = 15;
        bonusStreakCount = 0;
    }
    private void CountdownBonus()
    {
        bonusCountdown--;
        //case of new streak inside counting streak
        if (bonusCountdown <= 0)
        {
            bonusCountdown = 15; //Reset StreakCount
            bonusStreakCount = 0; //Reset StreakCount
            CancelInvoke(nameof(CountdownBonus)); //Stop all bonus countdown
        }
    }
    public void OnStartCountStreak()
    {
        bonusStreakCount += 1f;
        if (bonusStreakCount >= 5)
        {
            OnShowStreak();
        }

        if (bonusCountdown < 15)
        {
            CancelInvoke(nameof(CountdownBonus)); //Stop previous counting
            InvokeRepeating(nameof(CountdownBonus), 0f, 1f); //Init new countdown

        }
        else if (bonusCountdown == 15)
        {
            InvokeRepeating(nameof(CountdownBonus), 0f, 1f); //Init new countdown
        }

    }
    #endregion
}
