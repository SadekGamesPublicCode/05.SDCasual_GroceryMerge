using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSC : MonoBehaviour
{
    [SerializeField] GenMNSC genCtr;
    [SerializeField] Text versionTxt, tipTxt;
    [SerializeField] Slider loadSlide;
    private float loadSpd2;
    private int curLoadCOunt;
    void Start()
    {
        versionTxt.text = Application.version;
        curLoadCOunt = 0;
        loadSlide.maxValue = 1;
        loadSlide.minValue = loadSlide.value = 0;
        loadSpd2 = 0;
        StartCoroutine(RunLoadGameLogo());
        InvokeRepeating(nameof(OnUpdateTips), 0f, 3f);
    }

    IEnumerator RunLoadGameLogo()
    {
        loadSpd2 = Random.Range(0.01f, 0.5f);
        loadSlide.value += loadSpd2;
        if (loadSlide.value >= 1)
        {
            StopCoroutine(RunLoadGameLogo());
            SceneManager.LoadScene("HomeScene");
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RunLoadGameLogo());
    }
    void OnUpdateTips()
    {
        curLoadCOunt++;
        switch (curLoadCOunt)
        {
            case 0:
                tipTxt.text = "Beware the redline";
                break;
            case 1:
                tipTxt.text = "Same planet - matching!!";
                break;
            case 2:
                tipTxt.text = "Don't stack too much";
                break;
            case 3:
                tipTxt.text = "Careful yuor drops";
                break;
            case 4:
                tipTxt.text = "Bring power ups!!";
                break;
            case 5:
                tipTxt.text = "YOLO!!!";
                curLoadCOunt = 0;
                break;
        }
    }
}
