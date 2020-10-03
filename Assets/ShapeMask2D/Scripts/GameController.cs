﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Button btnChangeSpriteMask;
    public ShapeSpriteMask shapeSpriteMask;

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

        if (btnChangeSpriteMask == null || shapeSpriteMask == null)
        {
            throw new Exception("Initialize GameController properties in the Editor, drag from Hierarchy window");
        }
    }

    void Start()
    {
        Debug.Log("[GameController] start");
    }

    public void OnBtnChangeSpriteMaskClickHandler()
    {
        Debug.Log("->OnBtnChangeSpriteMaskClickHandler");
        shapeSpriteMask.UpdateShapeToNext();
    }
}
