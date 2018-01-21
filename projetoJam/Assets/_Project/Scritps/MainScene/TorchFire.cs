using UnityEngine;
using System.Collections;

public class TorchFire : MonoBehaviour 
{

	void OnTriggerEnter(Collider p_collider)
	{
		if(p_collider.gameObject.tag != "heart")
			return;
		
		SceneBehaviour.GameLost();
	}
}
