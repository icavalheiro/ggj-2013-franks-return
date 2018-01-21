using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotatiableBase : MonoBehaviour 
{
	private class CustomTouch
	{
		public Vector2 position;
		public int fingerId;
		public CustomTouch(Vector2 p_position, int p_fingerID)
		{
			position = p_position;
			fingerId = p_fingerID;
		}
		
		public CustomTouch(Touch p_touch)
		{
			position = p_touch.position;
			fingerId = p_touch.fingerId;
		}
		
		public CustomTouch()
		{
		}
		
		public static implicit operator CustomTouch(Touch p_touch)
		{
			return new CustomTouch(p_touch);
		}
	}
	
	public float minRot = 0;
	public float maxRot = 360;
	
	private Vector3 _initialRotation;
	private int _fingerID = -2;
	
	void Start()
	{
		_initialRotation = transform.eulerAngles;
	}
	
	void Update()
	{
		if(SceneBehaviour.inEditMode == false)
			return;
		
		if(SceneBehaviour.activeInEdition != this.gameObject && SceneBehaviour.activeInEdition != null)
			return;
		
		List<CustomTouch> __touchList = new List<CustomTouch>();
		foreach(var touch in Input.touches)
		{
			__touchList.Add(touch);
		}
		CustomTouch _mouseTouch = new CustomTouch(Input.mousePosition, -1);
		if(Input.GetMouseButton(0))
		{
			__touchList.Add(_mouseTouch);
		}
		
		//Check if theres a finger clicking in it if its not already being clicked
		if(_fingerID == -2)
		{
			foreach(var touch in __touchList)
			{
				Ray __ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit __toReturn;
				if(Physics.Raycast(__ray, out __toReturn))
				{
					//If the raycast hited us
					if(__toReturn.collider.gameObject == this.gameObject)
					{
						_fingerID = touch.fingerId;
						SceneBehaviour.activeInEdition = this.gameObject;
					}
				}
			}
		}
		else //If we have a finger clicking in me
		{
			bool __found = false;
			CustomTouch __finder = new CustomTouch();
		
			foreach(var touch in __touchList)
			{
				if(touch.fingerId == _fingerID)
				{
					__found = true;
					__finder = touch;
				}
			}
			
			//PODE DAR MERDA
			if(__found == false)
			{
				_fingerID = -2;
				SceneBehaviour.activeInEdition = null;
				return;
			}
			
			CustomTouch __touch = __finder;
			Vector2 __touchPosition = __touch.position;
			Vector2 __thisObjectPositonInScreen = Camera.main.WorldToScreenPoint(transform.position);
			Vector2 __vector = __touchPosition - __thisObjectPositonInScreen;
			float __angle = Mathf.Rad2Deg * Mathf.Atan2(__vector.y, __vector.x);
			if (!(minRot == 0 && maxRot == 360))
			{
				Mathf.Clamp( __angle, minRot, maxRot);
			}
			
			//Vector3 _newRotation = new Vector3(_initialRotation.x, _initialRotation.y, Mathf.Clamp( __angle, minRot, maxRot));
			Vector3 _newRotation = new Vector3(_initialRotation.x, _initialRotation.y, __angle);
			transform.eulerAngles = _newRotation;
		}
	}
	
	
	void OnDestroy()
	{
		if(SceneBehaviour.activeInEdition == this.gameObject)
			SceneBehaviour.activeInEdition = null;
	}
	
}
