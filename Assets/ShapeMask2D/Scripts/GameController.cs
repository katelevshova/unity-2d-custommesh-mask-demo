using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
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
            || btnDrawHexagonMesh == null || shapeSpriteMask == null)
        {
            throw new Exception("Initialize GameController properties in the Editor, drag from Hierarchy window");
        }
        
        Init();
    }

    private void Init()
    {
        btnToggleSound.GetComponentInChildren<TextMeshProUGUI>().text = "Sound is ON"; 
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
}
