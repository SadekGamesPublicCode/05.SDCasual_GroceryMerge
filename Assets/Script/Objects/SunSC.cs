using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SunSC : MonoBehaviour
{
    [HideInInspector] ArcadeSC arcadeCtr;
    [HideInInspector] ChallengeSC challengeCtr;
    [SerializeField] GenMNSC genCtr;
    [SerializeField] List<GameObject> planetList = new List<GameObject>();
    private int deviceType, gamemode;
    private float moveSpd = 5f;
    private int randPlanetToSpawn;
    private int curPlayerLevel;
    void Start()
    {
        genCtr = GameObject.Find("GenMN").GetComponent<GenMNSC>();
        SetSun();
    }
    public void SetDeviceType(int type) => deviceType = type;
    private void SetSun()
    {
        deviceType = genCtr.deviceType;
        if (genCtr.curGameMode == 2)
        {
            arcadeCtr = GameObject.Find("ArcadeMN").GetComponent<ArcadeSC>();
            curPlayerLevel = arcadeCtr.arcadeLv;
            gamemode = 2;
        }
        else if (genCtr.curGameMode == 3)
        {
            challengeCtr = GameObject.Find("ChallengeMN").GetComponent<ChallengeSC>();
            gamemode = 3;
        }
        SelectNextPlanet();
    }

    void Update()
    {
        if(deviceType == 1)
        {
            OnSpawnPlanetByTouch();
            OnMoveTouch();
        }else if(deviceType == 2)
        {
            OnSpawnPlanetByKey();
            OnMoveByKey();
        }
    }

    private void OnSpawnPlanetByKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randPlanet = randPlanetToSpawn;
            Vector3 curPos = transform.position;
            float tempY = curPos.y - 0.5f;
            GameObject spawnedPlanet = Instantiate(planetList[randPlanet], new Vector3(curPos.x, tempY, 0), Quaternion.identity) as GameObject;
            if (gamemode == 2)
            {
                arcadeCtr.OnAddCurItemOnScreen(spawnedPlanet);
            }
            else if (gamemode == 3) 
            {
                challengeCtr.OnAddCurItemOnScreen(spawnedPlanet);
            }
            SelectNextPlanet();
        }
    }
    private void OnSpawnPlanetByTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Ended)
            {
                float screenMidVertical = (Screen.height / 2) + (Screen.height / 4);
                if(t.position.y < screenMidVertical)
                {
                    int randPlanet = randPlanetToSpawn;
                    Vector3 curPos = transform.position;
                    float tempY = curPos.y - 0.5f;
                    GameObject spawnedPlanet = Instantiate(planetList[randPlanet], new Vector3(curPos.x, tempY, 0), Quaternion.identity) as GameObject;
                    if (gamemode == 2)
                    {
                        arcadeCtr.OnAddCurItemOnScreen(spawnedPlanet);
                    }
                    else if (gamemode == 3)
                    {
                        challengeCtr.OnAddCurItemOnScreen(spawnedPlanet);
                    }
                    SelectNextPlanet();
                }
            }
        }
    }

    private void OnMoveTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Stationary || t.phase == TouchPhase.Moved)
            {
                float screenMid = Screen.width / 2;
                if (transform.position.x >= -3 && transform.position.x <= 3)
                {
                    if (t.position.x > screenMid)
                    {
                        gameObject.transform.position += Vector3.right * Time.deltaTime * moveSpd;
                    }
                    else if (t.position.x < screenMid)
                    {
                        gameObject.transform.position += Vector3.left * Time.deltaTime * moveSpd;
                    }
                }
                else
                {
                    if (gameObject.transform.position.x > 3) gameObject.transform.position = new Vector3(3, 4, 0);
                    else if (gameObject.transform.position.x < -3) gameObject.transform.position = new Vector3(-3, 4, 0);
                }
                
            }
        }
        
    }
    private void OnMoveByKey()
    {
        if(transform.position.x >= -3 && transform.position.x <= 3)
        {
            if (Input.GetKey(KeyCode.A))
            {
                //Move left
                transform.position += Vector3.left * Time.deltaTime * moveSpd;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //Move Right
                transform.position += Vector3.right * Time.deltaTime * moveSpd;
            }
        }
        else
        {
            if (gameObject.transform.position.x > 3) gameObject.transform.position = new Vector3(3, 4, 0);
            else if (gameObject.transform.position.x < -3) gameObject.transform.position = new Vector3(-3, 4, 0);
        }
    }
    private void SelectNextPlanet()
    {
        if (gamemode == 2)
        {
            if (curPlayerLevel <= 10)
            {
                randPlanetToSpawn = Random.Range(0, planetList.Count / 2);
                arcadeCtr.SetPreviewImage(randPlanetToSpawn);
              //  gameObject.GetComponent<SpriteRenderer>().sprite = normalApparance;

            }
            else if (curPlayerLevel > 10 && curPlayerLevel <= 30)
            {
                randPlanetToSpawn = Random.Range(0, planetList.Count - 2);
                arcadeCtr.SetPreviewImage(randPlanetToSpawn);
              //  gameObject.GetComponent<SpriteRenderer>().sprite = normalApparance;
            }
            else if (curPlayerLevel > 30)
            {
                randPlanetToSpawn = Random.Range(0, planetList.Count);
                arcadeCtr.SetPreviewImage(randPlanetToSpawn);
              //  gameObject.GetComponent<SpriteRenderer>().sprite = normalApparance;
            }
        }
        else if (gamemode == 3)
        {
            randPlanetToSpawn = Random.Range(0, planetList.Count);
            challengeCtr.SetPreviewImage(randPlanetToSpawn);
           // gameObject.GetComponent<SpriteRenderer>().sprite = normalApparance;
        }
    }
}
