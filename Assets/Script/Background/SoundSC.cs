using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundSC : Singleton<SoundSC>
{
    [SerializeField] AudioSource sfxSource;
    [SerializeField] DataSC data;
    private int pSFX; //This variable handle communicate with PlayerPrefs

    private void Start()
    {
        pSFX = data.pSFX;
        CheckPlayerSFX();
    }
    private void CheckPlayerSFX()
    {
        //Decide on init
        if (pSFX == 0)
        {
            //Mute
            //MuteSFX();
        }
        else if (pSFX == 1)
        {
            PlaySFX();
        }
    }
    public void UpdateSFX(bool isAllow)
    {
        if (isAllow == false)
        {
            //MuteSFX();
            sfxSource.volume = 0;
            data.UpdateSFXState(0);
        }
        else if (isAllow == true)
        {
            PlaySFX();
            sfxSource.volume = 1;
            data.UpdateSFXState(1);
        }
    }
    public void PlaySFX()
    {
        sfxSource.Play();
    }
    public void MuteSFX()
    {

    }
}
