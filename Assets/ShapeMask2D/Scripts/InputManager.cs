using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]   // for working  OnPointerClick
public class InputManager : MonoBehaviour, IPointerClickHandler
{
    public static InputManager Instance;
    [SerializeField]
    private float timeClickDelay = 1f;
    private float _lastTimeClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        // MOBILE
        if (eventData.clickCount == 1)
        {
            float currentTimeClick = eventData.clickTime;

            if (Mathf.Abs(currentTimeClick - _lastTimeClick) < timeClickDelay)
            {
                //Debug.Log("[InputManager]->OnPointerClick:MOBILE double click");
                DoubleClickHandler();
            }
            _lastTimeClick = currentTimeClick;
        }

        // PC
        if (eventData.clickCount == 2)
        {
            //Debug.Log("[InputManager]->OnPointerClick: PC double click");
            DoubleClickHandler();
        }
    }

    private void DoubleClickHandler()
    {
        //Debug.Log("[InputManager]->DoubleClickHandler");
        
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            //TODO: here we can use Interface if we will have lots of different shapes in future
            HexagonRenderer hexagon = hitInfo.collider.GetComponent<HexagonRenderer>();

            if (hexagon != null)
            {
                Debug.Log("[InputManager]->DoubleClickHandler: Hexagon!");
            }
        }
    }
}
