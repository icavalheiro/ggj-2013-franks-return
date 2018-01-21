using UnityEngine;
using System.Collections;

public class WallColliderBehaviour : MonoBehaviour 
{
	void OnTriggerEnter(Collider p_collider)
	{
		if(p_collider.gameObject.tag != "heart")
			return;
		
		SceneBehaviour.GameLost();
	}
}
