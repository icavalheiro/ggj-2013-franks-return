using UnityEngine;
using System.Collections;

public class CutSceneBehaviour : MonoBehaviour 
{
	public Texture2D[] frames;
	public float[] framesTime;
	
 	private float _counter = 0;
	private int _currentFrame = 0;
	
	void Start()
	{
		AudioClip __clip = (AudioClip)Resources.Load("Sounds/bgmAbertura");
		AudioSource __source = gameObject.AddComponent<AudioSource>();
		__source.clip = __clip;
		__source.loop = false;
		__source.Play();
	}
	
	public void Update()
	{
		_counter += Time.deltaTime;
		if(_counter >= framesTime[_currentFrame])
		{
			_currentFrame++;
			_counter = 0;
			if(_currentFrame >= frames.Length)
			{
				Application.LoadLevel("Stage0");
			}
		}
	}
	
	public void OnGUI()
	{
		if(_currentFrame >= frames.Length)
			return;
		
		Rect __frameRect = new Rect(
			Screen.width * 0.5f - frames[_currentFrame].width * 0.5f,
			Screen.height * 0.5f - frames[_currentFrame].height * 0.5f,
			frames[_currentFrame].width,
			frames[_currentFrame].height
			);
		
		GUI.DrawTexture(__frameRect, frames[_currentFrame]);
	}
}
