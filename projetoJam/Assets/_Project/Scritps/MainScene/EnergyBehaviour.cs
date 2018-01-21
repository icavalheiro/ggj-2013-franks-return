using UnityEngine;
using System.Collections;

public class EnergyBehaviour : MonoBehaviour 
{
	void OnTriggerEnter(Collider p_who)
	{
		if(p_who.gameObject.tag != "heart")
			return;
		SceneBehaviour.EnergyFound();
		Destroy(this.transform.parent.gameObject);
	}
	
}
