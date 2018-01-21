using UnityEngine;
using System.Collections;

public class FranksChest : MonoBehaviour 
{
	public MeshRenderer mesh;
	public Texture[] frames;
	
	void OnTriggerEnter(Collider p_who)
	{
		if(p_who.gameObject.tag != "heart")
			return;
		
		Destroy(p_who.gameObject.transform.parent.gameObject);
		
		mesh.material.mainTexture = frames[1];
		Invoke("Frame2", 1);
		
		
	}
	
	void Frame2()
	{
		mesh.material.mainTexture = frames[2];
		Invoke("CallWon", 1);
	}
	
	void CallWon()
	{
		SceneBehaviour.GameWon();
	}
}
