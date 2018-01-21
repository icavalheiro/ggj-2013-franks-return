using UnityEngine;
using System.Collections;

public enum GameStatus
{
	WON,
	LOST,
	RUNNING
}

public class SceneBehaviour : MonoBehaviour 
{
	public static bool inEditMode = false;
	public static GameObject activeInEdition = null;
	public AudioSource clique;
	private static GameStatus _gameStatus = GameStatus.RUNNING;
	private int _energysFound = 0;
	private static SceneBehaviour _instance;
	
	private GUIStyle _startBtn;
	private GUIStyle _replay;
	private GUIStyle _back;
	private GUIStyle _next;
	private Font _fonte;
	private Texture2D _imgTrovao;
	
	void Start()
	{
		_instance = this;
		_gameStatus = GameStatus.RUNNING;
		
		_startBtn = new GUIStyle();
		_startBtn.normal.background = (Texture2D)Resources.Load("Images/botao play");
		_replay = new GUIStyle();
		_replay.normal.background = (Texture2D)Resources.Load("Images/botao replay");
		_back = new GUIStyle();
		_back.normal.background = (Texture2D)Resources.Load("Images/voltar");
		_next = new GUIStyle();
		_next.normal.background = (Texture2D)Resources.Load("Images/botao next");
		_fonte = (Font)Resources.Load("Font/EvilofFrankenstein50");
		_imgTrovao = (Texture2D)Resources.Load("Images/trovao");
		
		//Play sound
		if(GameplayAudioPlayer.instance == null)
		{
			GameObject __obj = (GameObject)Resources.Load("Prefabs/GameplaySoundPlayer");
			Instantiate(__obj);
		}
			
		//Stop the time to let the player modify the moving parts
		EnterEditMode();
	}
	
	public static void EnergyFound()
	{
		_instance._energysFound ++;
	}
	
	private void EnterEditMode()
	{
		Time.timeScale = 0;
		inEditMode = true;
	}
	
	private void LeaveEditMode()
	{
		Time.timeScale = 1;
		inEditMode = false;
	}
	
	void OnGUI()
	{
		//If it is in edit mode show the button to start the game
		if(inEditMode)
		{
			float __btnWidth = 250;
			float __btnHeight = 50;
			Rect __leaveEditModeBtnRect = new Rect(
				Screen.width* 0.5f - (__btnWidth * 0.5f + (15)) ,
				Screen.height - (__btnHeight + (__btnHeight * 0.1f)),
				__btnWidth,
				__btnHeight
				);
			
			if(GUI.Button(__leaveEditModeBtnRect, "", _startBtn))
			{
				clique.Play();
				LeaveEditMode();
			}
		}
		else if(_gameStatus == GameStatus.WON)
		{
			int __currentLevel = Application.loadedLevel;
			int __nextLevel = __currentLevel + 1;
			
			Rect __laberRect = new Rect(
				Screen.width * 0.5f - 300,
				80,
				600,
				70
				);
			
			GUIStyle __cacheStyle = new GUIStyle();
			__cacheStyle.font = _fonte;
			__cacheStyle.alignment = TextAnchor.MiddleCenter;
			
			__cacheStyle.normal.textColor = Color.black;
			GUI.Label(new Rect(__laberRect.x + 5, __laberRect.y +5, __laberRect.width, __laberRect.height), "COMPLETED", __cacheStyle);
			__cacheStyle.normal.textColor = new Color(11f/255f,100f/255f,11f/255f,1);
			GUI.Label(__laberRect, "COMPLETED", __cacheStyle);
			
			Rect __btnReturnRect = new Rect(
				Screen.width * 0.5f - 150,
				Screen.height * 0.5f - 25 - 100,
				300,
				50
				);
			
			//DRAW TROVAO
			Rect __bgTrovao = new Rect(
				Screen.width * 0.5f - 150,
				__btnReturnRect.y - 180,
				300,
				160
				);
			
			GUI.Box(__bgTrovao, "");
			
			GUI.color = new Color(1,1,1,0.3f);
			for(int i = 0; i < 3; i ++)
			{
				Rect __cache = new Rect(
					__bgTrovao.x + (30 * (i +1)) + ((_imgTrovao.width + 25) * i),
					__bgTrovao.y + 30,
					_imgTrovao.width  + 25,
					_imgTrovao.height
					);
				
				GUI.DrawTexture(__cache, _imgTrovao);
			}
			GUI.color = Color.white;
			for(int i = 0; i < _energysFound; i ++)
			{
				Rect __cache = new Rect(
					__bgTrovao.x + (30 * (i +1)) + ((_imgTrovao.width + 25) * i),
					__bgTrovao.y + 30,
					_imgTrovao.width  + 25,
					_imgTrovao.height
					);
				
				GUI.DrawTexture(__cache, _imgTrovao);
			}
			
			//DRAWTROVAO
			
			if(GUI.Button(__btnReturnRect, "", _back))
			{
				Time.timeScale = 1;
				clique.Play();
				Application.LoadLevel("MenuScene");
			}
			
			Rect __btnNextLevelRect = new Rect(
				__btnReturnRect.x,
				__btnReturnRect.y + __btnReturnRect.height + 30,
				__btnReturnRect.width,
				__btnReturnRect.height
				);
			
			if(GUI.Button(__btnNextLevelRect, "", _next))
			{
				Time.timeScale = 1;
				clique.Play();
				Application.LoadLevel(__nextLevel);
			}
			
			Rect __btnReplay = new Rect(
				__btnNextLevelRect.x,
				__btnNextLevelRect.y + __btnNextLevelRect.height + 30,
				__btnNextLevelRect.width,
				__btnNextLevelRect.height
				);
			
			if(GUI.Button(__btnReplay, "", _replay))
			{
				Time.timeScale = 1;
				clique.Play();
				Application.LoadLevel(Application.loadedLevel);
			}
			
		}
		else if(_gameStatus == GameStatus.LOST)
		{	
			Rect __laberRect = new Rect(
				Screen.width * 0.5f - 300,
				80,
				600,
				70
				);
			
			GUIStyle __cacheStyle = new GUIStyle();
			__cacheStyle.font = _fonte;
			__cacheStyle.alignment = TextAnchor.MiddleCenter;
			__cacheStyle.normal.textColor = Color.black;
			GUI.Label(new Rect(__laberRect.x + 5, __laberRect.y +5, __laberRect.width, __laberRect.height), "Try again!", __cacheStyle);
			__cacheStyle.normal.textColor = new Color(11f/255f,100f/255f,11f/255f,1);
			GUI.Label(__laberRect, "Try again!", __cacheStyle);
			
			
			Rect __btnReturnRect = new Rect(
				Screen.width * 0.5f - 150,
				Screen.height * 0.5f - 25 - 100,
				300,
				50
				);
			
			if(GUI.Button(__btnReturnRect, "", _back))
			{
				Time.timeScale = 1;
				clique.Play();
				Application.LoadLevel("MenuScene");
			}
			
			Rect __btnReplay = new Rect(
				__btnReturnRect.x,
				__btnReturnRect.y + __btnReturnRect.height + 30,
				__btnReturnRect.width,
				__btnReturnRect.height
				);
			
			if(GUI.Button(__btnReplay, "", _replay))
			{
				Time.timeScale = 1;
				clique.Play();
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		else if(_gameStatus == GameStatus.RUNNING)
		{
			Rect __btnResetRect = new Rect(
				Screen.width * 0.5f - 90,
				5,
				180,
				35
				);
			
			if(GUI.Button(__btnResetRect, "", _replay))
			{
				clique.Play();
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
	
	public static void GameWon()
	{
		Debug.Log("Game Won");
		Time.timeScale = 0;
		_gameStatus = GameStatus.WON;
		
	}
	
	public static void GameLost()
	{
		Debug.Log("Game Lost");
		Time.timeScale = 0;
		_gameStatus = GameStatus.LOST;
	}
}
