using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }

    /**
* #if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
           //...and if it isn't using touch aiming, leave
           //...and if it isn't using touch aiming, leave
           if (!isTouchAiming)
               return;
#else
   //...otherwise, record the mouse's position to the screenPosition variable
   screenPosition = Input.mousePosition;
#endif*/


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
