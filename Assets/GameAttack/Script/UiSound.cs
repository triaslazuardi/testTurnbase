using AttackTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSound : MonoBehaviour
{
    public Toggle toggleSoundBgm;
    public Toggle toggleSoundSfx;

    private void Start()
    {

        //CheckSound();
        OperateBgm(PlayerPrefs.GetInt("bgmSound", 1) >= 1 ? true : false);
        OperateSfx(PlayerPrefs.GetInt("sfxSound", 1) >= 1 ? true : false);
    }

    public void OnBGmSound()
    {
        OperateBgm(toggleSoundBgm.isOn);
    }

    public void OnSfxSound()
    {
        OperateSfx(toggleSoundSfx.isOn);
    }

    public void OperateBgm(bool isActive)
    {
        toggleSoundBgm.isOn = isActive;
        SoundManager.instance.MuteSoundBGM(isActive);
    }

    public void OperateSfx(bool isActive)
    {
        toggleSoundSfx.isOn = isActive;
        SoundManager.instance.MuteSoundSFX(isActive);
    }
}
