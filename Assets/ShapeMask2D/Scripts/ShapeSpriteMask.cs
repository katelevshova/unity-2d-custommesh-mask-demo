using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpriteMask : MonoBehaviour
{
    private SpriteMask _spriteMask;
    private int _currentId = 0;


    // Start is called before the first frame update
    void Start()
    {
        _spriteMask = GetComponentInChildren<SpriteMask>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateShapeToNext()
    {
        _currentId++;
        var total = Enum.GetNames(typeof(EnumShapes)).Length;

        if (_currentId == total)
        {
            _currentId = 0;
        }

        string shape_sprite_path = "Sprites/" + (EnumShapes)_currentId;
        _spriteMask.sprite = Resources.Load<Sprite>(shape_sprite_path);
    }
}
