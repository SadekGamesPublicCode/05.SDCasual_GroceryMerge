using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseSC : MonoBehaviour
{
    [HideInInspector] GenMNSC genCtr;
    void Start() 
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
    }
    public void OnReplay()
    { 
        genCtr.OnReplay();
        genCtr.OnHideLose();
    }
    public void OnContinueByAds()
    {
        genCtr.OnCallbackShowAdsReward();
        genCtr.OnClearGameScreenToContinue();
        genCtr.OnHideLose();
    }
    public void OnHome()
    {
        genCtr.OnLoadHome();
        genCtr.OnHideLose();
    }
}
