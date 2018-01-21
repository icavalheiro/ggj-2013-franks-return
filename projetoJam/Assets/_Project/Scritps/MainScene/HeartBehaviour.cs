using UnityEngine;
using System.Collections;

public class HeartBehaviour : MonoBehaviour 
{
	public MeshRenderer heartImage;
	
	#region Private data
	private float _baseForce = 9.86f;
	#endregion
	
	public void FixedUpdate()
	{
		//Get the force to add in the heart
		if(Input.acceleration == Vector3.zero)
		{
			float __inputAccel = Mathf.Clamp(-Input.GetAxis("Horizontal"), -0.5f, 0.5f);
			float __forceToAdd = _baseForce * __inputAccel;
			Vector3 __lateralForce = Vector3.right * (-__forceToAdd);
			Vector3 __downForce = (new Vector3(0, (-_baseForce) + Mathf.Abs(__forceToAdd), 0));
			
			Physics.gravity = __downForce + __lateralForce;
		}
		else
		{
			float __inputAccel = Mathf.Clamp(Input.acceleration.y, -0.5f, 0.5f);
			float __forceToAdd = _baseForce * __inputAccel;
			Vector3 __lateralForce = Vector3.right * (-__forceToAdd);
			Vector3 __downForce = (new Vector3(0, (-_baseForce) + Mathf.Abs(__forceToAdd), 0));
			
			Physics.gravity = __downForce + __lateralForce;
		}
		
		float __maxSpeed = -2f;
		if(GetComponent<Rigidbody>().velocity.y < __maxSpeed)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, __maxSpeed, GetComponent<Rigidbody>().velocity.z);
		}
	}
	
	void Update()
	{
		if(GetComponent<Rigidbody>().velocity == Vector3.zero)
		{
			GetComponent<Rigidbody>().velocity = Vector3.right * 0.3f;
		}
		
	}
	
	public void OnCollisionEnter(Collision p_collider)
	{
		if(p_collider.impactForceSum.magnitude > 2)
		{
			
		}
	}
	public void  OnTriggerEnter(Collider p_collider)
	{
		if(p_collider.transform.parent.name == "Energy")
		{
			GetComponent<AudioSource>().Play();
		}
	}
}
