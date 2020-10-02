using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocator : MonoBehaviour
{
	public static MouseLocator Instance;           //A reference to a MouseLacation object. This allows the class to have a public reference to itself to other scripts can 
													//access it without having a reference to it. 

	[HideInInspector] public Vector3 MousePosition; //Location in 3D space of the mouse cursor	
	[HideInInspector] public bool IsValid;          //Is the mouse location valid?

	[SerializeField] LayerMask whatIsClickableTarget;        

	Ray mouseRay;                                   //A ray that will be used to find the mouse
	RaycastHit hit;                                 //A RaycastHit which will store information about a raycast
	Vector2 screenPosition;                         //Where the mouse is on the screen
	bool isTouchAiming;                             //Are we using touch to aim? This will be used if we are on a mobile device

	void Awake()
	{
		Debug.Log("MouseLocation");
		//This is a common approach to handling a class with a reference to itself.
		//If instance variable doesn't exist, assign this object to it
		if (Instance == null)
			Instance = this;
		//Otherwise, if the instance variable does exist, but it isn't this object, destroy this object.
		//This is useful so that we cannot have more than one MouseLocation object in a scene at a time.
		else if (Instance != this)
			Destroy(this);
	}

	void Update()
	{
		//Assume the mouse location isn't valid
		IsValid = false;

		//This is platform specific code. Any code that isn't in the appropriate section
		//is effectively turned into a comment (essentialy doesn't exist when the project is built).
		//If this is a mobile platform (Android, iOS, or WP8)... 
#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
				//...and if it isn't using touch aiming, leave
				//...and if it isn't using touch aiming, leave
				if (!isTouchAiming)
					return;
#else
		//...otherwise, record the mouse's position to the screenPosition variable
		screenPosition = Input.mousePosition;
#endif

		//Create a ray that extends from the main camera, through the mouse's position on the screen
		//into the scene
		mouseRay = Camera.main.ScreenPointToRay(screenPosition);

		//If the ray from our camera hits something that is ground...
		if (Physics.Raycast(mouseRay, out hit, 100f, whatIsClickableTarget))
		{
			//...the mouse position is valid...
			IsValid = true;
			//...and record the point in 3D space that the ray hit the ground
			MousePosition = hit.point;
			Debug.Log("MouseLocation");
		}
	}


	/**
	 *  if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(rayOrigin, out hitInfo))
            {
                AvatarHead avatarHead = hitInfo.collider.GetComponent<AvatarHead>();

                if(avatarHead != null)
                {
                    Debug.Log("MouseLocation, clicked!");

                    CmdClientUpdateAvatar cmd = new CmdClientUpdateAvatar();
                    cmd.Execute<Player>(avatarHead.GetComponentInParent<Player>());
                }
            }**/
}
