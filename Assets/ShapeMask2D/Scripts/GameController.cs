using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;


public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Button btnChangeSpriteMask;
    public Button btnDrawHexagonMesh;
    public ShapeSpriteMask shapeSpriteMask;
    public HexagonRenderer hexagonRenderer;
    public Toggle btnToggleSound;
    public SoundManager soundManager;
    public Slider sliderBgMusic;
    public Button btnPlayNarratorSound;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        if (btnToggleSound == null || btnChangeSpriteMask == null || soundManager == null 
            || btnPlayNarratorSound == null
            || btnDrawHexagonMesh == null || shapeSpriteMask == null || sliderBgMusic == null)
        {
            throw new Exception("Initialize GameController properties in the Editor, " +
                                "drag from Hierarchy window");
        }
        
        Init();
    }

    private void Init()
    {
        btnToggleSound.GetComponentInChildren<TextMeshProUGUI>().text = "Sound is ON"; 
        sliderBgMusic.value = PlayerPrefs.GetFloat(PlayerPrefsConstants.PLAYER_PREFS_BG_MUSIC, 100f);
    }

    void Start()
    {
        Debug.Log("[GameController] start");
    }

    public void OnBtnChangeSpriteMaskClickHandler()
    {
        shapeSpriteMask.UpdateShapeToNext();
    }

    public void OnBtnDrawHexagonMeshClickHandler()
    {
        hexagonRenderer.DrawHexagon();
    }

    public void OnBtnToggleSoundClickHandler()
    {
        string sound_status = "Sound is ";
        if (btnToggleSound.isOn)
        {
            soundManager.TransitionToSnapshot(SoundManager.UN_MUTE);
            sound_status += "ON";
        }
        else
        {
            soundManager.TransitionToSnapshot(SoundManager.MUTE);
            sound_status += "OFF";
        }

        btnToggleSound.GetComponentInChildren<TextMeshProUGUI>().text = sound_status;
    }
    
    public void SliderBgMusic_ValueChangeCheck()
    {
        soundManager.SetVolumeLevel(SoundManager.EXPOSED_PARAM_BG_MUSIC, sliderBgMusic.value);
        PlayerPrefs.SetFloat(PlayerPrefsConstants.PLAYER_PREFS_BG_MUSIC, sliderBgMusic.value);
    }
    
    public void OnBtnPlayNarratorSoundClickHandler()
    {
        soundManager.narratorSound.PlayNextSound();
    }
}
