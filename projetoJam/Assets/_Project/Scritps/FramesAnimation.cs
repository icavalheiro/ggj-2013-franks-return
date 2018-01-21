using UnityEngine;
using System.Collections;

public class FramesAnimation : MonoBehaviour 
{
	public float frameRate = 10;
	public Material material;
	public Texture[] frames;
	
	
	private int _currentFrame = 0;
	private float _counter = 0;
	
	public void Update()
	{
		_counter += Time.deltaTime;
		if(_counter >= frameRate)
		{
			_counter = 0;
			_currentFrame ++;
			if(_currentFrame >= frames.Length)
				_currentFrame = 0;
		}
		
		if(material.mainTexture != frames[_currentFrame])
			material.mainTexture = frames[_currentFrame];
	}
}
