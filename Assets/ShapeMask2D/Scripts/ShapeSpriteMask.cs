using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpriteMask : MonoBehaviour
{
    private SpriteMask _spriteMask; 


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
        string shape_sprite_path = "Sprites/" + "Heart";
        _spriteMask.sprite = Resources.Load<Sprite>(shape_sprite_path);
    }
}
