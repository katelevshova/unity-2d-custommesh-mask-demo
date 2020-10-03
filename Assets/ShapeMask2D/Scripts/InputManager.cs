using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                //TODO: here we can use Interface if we will have lots of different shapes in future
                HexagonRenderer hexagon = hitInfo.collider.GetComponent<HexagonRenderer>();

                if (hexagon != null)
                {
                    Debug.Log("InputManager, clicked!");
                }
            }
        }
    }
}
