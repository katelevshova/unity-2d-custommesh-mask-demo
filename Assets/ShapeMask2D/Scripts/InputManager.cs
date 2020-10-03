using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InputManager : MonoBehaviour, IPointerClickHandler
{
    public static InputManager Instance;
    [SerializeField]
    private float timeClickDelay = 1f;
    private float _lastTimeClick;
    public LayerMask whatIsClickable;
    public Vector2 mouseScreenPosition;    //Where the mouse is on the screen

    [HideInInspector] 
    public Vector3 mousePosition3D; //Location in 3D space of the mouse cursor	
    [HideInInspector] 
    public bool isValid;          //Is the mouse location valid?

    Ray mouseRay;                                   //A ray that will be used to find the mouse
    RaycastHit hitInfo;                                 //A RaycastHit which will store information about a raycast

    public delegate void ActionDoubleClick();
    public static event ActionDoubleClick OnDoubleClickHandler;

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
    }

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

        //Assume the mouse location isn't valid
        isValid = false;

        //This is platform specific code. Any code that isn't in the appropriate section
        //is effectively turned into a comment (essentialy doesn't exist when the project is built).
        //If this is a mobile platform (Android, iOS, or WP8)... 
        #if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
		    //...and if it isn't using touch aiming, leave
			if (!isTouchAiming)
			    return;
        #else
            //...otherwise, record the mouse's position to the screenPosition variable
            mouseScreenPosition = Input.mousePosition;
        #endif

        //Create a ray that extends from the main camera, through the mouse's position on the screen
        //into the scene
        mouseRay = Camera.main.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(mouseRay, out hitInfo, 100f, whatIsClickable))
        {
            //TODO: here we can use Interface if we will have lots of different shapes in future
            HexagonRenderer hexagon = hitInfo.collider.GetComponent<HexagonRenderer>();
            
            if (hexagon != null)
            {
                Debug.Log("[InputManager]->DoubleClickHandler: Hexagon!");
                //...the mouse position is valid...
                isValid = true;
                //...and record the point in 3D space that the ray hit the whatIsClickable
                mousePosition3D = hitInfo.point;

                OnDoubleClickHandler?.Invoke();
            }
        }
    }
}
