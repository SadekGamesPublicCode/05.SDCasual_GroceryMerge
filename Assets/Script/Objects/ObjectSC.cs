using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSC : MonoBehaviour
{
    [HideInInspector] ArcadeSC arcadeCtrl;
    [HideInInspector] ChallengeSC challengeCtr;
    [HideInInspector] GenMNSC genCtr;

    internal float collisionStart = -1f;
    internal float selfScore;
    internal int gameMode;
    internal bool isCheckDead;
    internal string objectName;
    internal virtual void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        selfScore = 0.5f;
        isCheckDead = false;
        Invoke(nameof(EnableCheckLoose), 3f);
        CheckGameomde();
    }
    internal void Update() => CheckLoose();
    internal void OnCollisionEnter2D(Collision2D collision)
    {
        string colName = collision.gameObject.name;
        if(objectName == colName)
        {
            AddScoring();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
    internal void CheckGameomde()
    {
        if (genCtr.curGameMode == 2)
        {
            gameMode = 2;
            arcadeCtrl = GameObject.Find("ArcadeMN").GetComponent<ArcadeSC>();
        }
        else if (genCtr.curGameMode == 3)
        {
            gameMode = 3;
            challengeCtr = GameObject.Find("ChallengeMN").GetComponent<ChallengeSC>();
        }
    }
    internal void AddScoring()
    {
        if (gameMode == 2)
        {
            arcadeCtrl.IncreaseScore(selfScore);
            arcadeCtrl.OnStartCountStreak();
        }
        else if (gameMode == 3) { } //Add score Challenge
    }
    internal void CheckLoose()
    {
        if (isCheckDead == true)
        {
            if (gameObject.transform.position.y >= 2.5f)
            {
                genCtr.OnShowLose();
            }
        }
    }
    internal void EnableCheckLoose() => isCheckDead = true;
}
