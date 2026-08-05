using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSC : MonoBehaviour
{
    [SerializeField] GenMNSC genCtr;
    [SerializeField] Text curVersion;
    [SerializeField] GameObject logoIMG;
    private float loadSpd2;
    public float targetAlpha = 1f;
    void Start()
    {
        curVersion.text = Application.version;
        logoIMG.GetComponent<Image>().fillAmount = 0;
        StartCoroutine(RunLoadGameLogo());
    }

    IEnumerator RunLoadGameLogo()
    {
        loadSpd2 = Random.Range(0.01f, 0.5f);
        if (logoIMG.GetComponent<Image>().fillAmount >= 1)
        {
            logoIMG.gameObject.SetActive(true);
            StopCoroutine(RunLoadGameLogo());
            genCtr.OnToHome();
        }
        yield return new WaitForSeconds(0.1f);
        logoIMG.GetComponent<Image>().fillAmount += loadSpd2 * Time.deltaTime * 10;
        StartCoroutine(RunLoadGameLogo());
    }
}
