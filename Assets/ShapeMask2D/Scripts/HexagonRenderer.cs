using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]   // for working  OnPointerClick
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexagonRenderer : MonoBehaviour
{
    [SerializeField]
    private float _width = 2; // width of the square in which we draw the hexagon
    [SerializeField]
    private Material _material;
    private MeshCollider _meshCollider;

    private Vector3 _screenPoint;
    private Vector3 _offset;

    [HideInInspector]
    Vector3[] vertices = new Vector3[6];


    // Start is called before the first frame update
    void Start()
    {
        _meshCollider = GetComponent<MeshCollider>();
        InputManager.OnDoubleClickHandler += OnDoubleClickHandler;
        DrawHexagon();
    }

    void Update()
    {
        /*if (InputManager.Instance == null || !InputManager.Instance.isValid)
            return;

        Vector3 distance = _offset.normalized;
        float dotProd = Vector3.Dot(transform.right, distance);

        if (Mathf.Abs(1 - dotProd) < 1f)
        {
            Debug.Log("[HexagonRenderer]-> YAHOOOO!");
        }*/

        if (_meshCollider.bounds.Contains(Input.mousePosition))
        {
            Debug.Log("Inside");
        }


     
            

    }

    private void OnDoubleClickHandler()
    {
        Debug.Log("[HexagonRenderer]->OnDoubleClickHandler");
         /*_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

         Vector2 distance = (InputManager.Instance.mousePosition3D - _screenPoint).normalized;
         Debug.Log("distance=" + distance);

         float dotProd = Vector3.Dot(transform.right, distance);
         Debug.Log("dotProd=" + dotProd);
         float dotProdModulo = Mathf.Abs(dotProd);
         Debug.Log("dotProdModulo=" + dotProdModulo);*/

         Boolean isInside = ContainsPoint(vertices, InputManager.Instance.mouseScreenPosition);
         Debug.Log("1 isInside=" + isInside);

        if (_meshCollider.bounds.Contains(InputManager.Instance.mousePosition3D))
        {
            Debug.Log("Inside 2");
        }
    }

    public Boolean ContainsPoint(Vector3[] polyPoints, Vector3 p ) 
    { 
        var j = polyPoints.Length - 1;
        var inside = false; 
        for (int i = 0; i < polyPoints.Length; j = i++) 
        { 
            if (((polyPoints[i].y <= p.y && p.y<polyPoints[j].y) || (polyPoints[j].y <= p.y && p.y<polyPoints[i].y)) && 
                (p.x<(polyPoints[j].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) + polyPoints[i].x)) 
                inside = !inside; 
        }
        return inside; 
    }

    public void DrawHexagon()
    {
        Mesh mesh = new Mesh();

       
        vertices[0] = new Vector3(-_width / 2, Mathf.Sqrt(_width * 1.5f));
        vertices[1] = new Vector3(_width / 2, Mathf.Sqrt(_width * 1.5f));
        vertices[2] = new Vector3(_width, 0);
        vertices[3] = new Vector3(_width / 2, -Mathf.Sqrt(_width * 1.5f));
        vertices[4] = new Vector3(-_width / 2, -Mathf.Sqrt(_width * 1.5f));
        vertices[5] = new Vector3(-_width, 0);

        mesh.vertices = vertices;


        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
        mesh.uv = uvs;


        mesh.triangles = new int[] { 0, 4, 5, 0, 3, 4, 0, 1, 3, 1, 2, 3 };
        GetComponent<MeshRenderer>().material = _material;

       

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        GetComponent<MeshFilter>().mesh = mesh;
        _meshCollider.sharedMesh = null;
        _meshCollider.sharedMesh = mesh;

        
    }

    private void OnDisable()
    {
        InputManager.OnDoubleClickHandler -= OnDoubleClickHandler;
    }

    public void OnMouseDown()
    {
        if (_meshCollider.sharedMesh.bounds.Contains(InputManager.Instance.mousePosition3D))
        {
            Debug.Log("Inside 3");
        }
    }
}
