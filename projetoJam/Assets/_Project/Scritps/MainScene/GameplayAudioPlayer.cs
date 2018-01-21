using UnityEngine;
using System.Collections;

public class GameplayAudioPlayer : MonoBehaviour 
{
	public AudioClip clip;
	
	public static GameplayAudioPlayer instance;
	
	void Start()
	{
		instance = this;
		AudioSource __source = gameObject.AddComponent<AudioSource>();
		__source.clip = clip;
		__source.loop = true;
		__source.Play();
		
		GameObject.DontDestroyOnLoad(this);
	}
	
	void OnLevelWasLoaded (int level)
	{
		if(level == 13 || level == 0)
			Destroy(this.gameObject);
	}
	
	void OnDestroy()
	{
		if(instance == this)
			instance = null;
	}
}
