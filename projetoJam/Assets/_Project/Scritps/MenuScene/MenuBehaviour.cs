using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MenuState
{
	INITIAL,
	SELECTING_LEVEL,
	CREDITS
}

public class MenuBehaviour : MonoBehaviour 
{
	private MenuState _currentState = MenuState.INITIAL;
	
	public List<Texture2D> images;
	public Font fonte;
	public AudioSource clique;
	
	private Texture2D _bgInicial;
	private GUIStyle _startBtn;
	private GUIStyle _selectBtn;
	private GUIStyle _backBtn;
	private GUIStyle _selectNumBtn;
	private Texture2D _bgCredits;
	private GUIStyle _creditsBtn;
	
	void Start()
	{
		AudioClip __clip = (AudioClip)Resources.Load("Sounds/bgmMenu");
		AudioSource __source = gameObject.AddComponent<AudioSource>();
		__source.clip = __clip;
		__source.loop = true;
		__source.Play();
		
		_bgInicial = images.Find(img => img.name == "background tela principal");
		_startBtn = new GUIStyle();
		_startBtn.normal.background = images.Find(img => img.name == "botao play");
		_selectBtn = new GUIStyle();
		_selectBtn.normal.background = images.Find(img => img.name == "botao select");
		_backBtn = new GUIStyle();
		_backBtn.normal.background = images.Find(img => img.name == "voltar");
		_selectNumBtn = new GUIStyle();
		_selectNumBtn.normal.background = images.Find(img => img.name == "botao selecao");
		_selectNumBtn.active.background = images.Find(img => img.name == "botao selecao clique");
		_selectNumBtn.font = fonte;
		_selectNumBtn.alignment = TextAnchor.MiddleCenter;
		_bgCredits = images.Find(img => img.name == "background credits");
		_creditsBtn = new GUIStyle();
		_creditsBtn.normal.background = images.Find(img => img.name == "botao credits");
	}
	
	void OnGUI()
	{
		if(_currentState == MenuState.INITIAL)
		{
			//DrawBG
			float __bgMp = Screen.width/1024f;
			Rect __bgRect = new Rect(
				Screen.width * 0.5f - (1024 * __bgMp) * 0.5f,
				Screen.height * 0.5f - (1024 * __bgMp) * 0.5f,
				(1024 * __bgMp),
				(1024 * __bgMp)
				);
			
			GUI.DrawTexture(__bgRect, _bgInicial);
			
			//Start btn
			Rect __startBtnRect = new Rect(
				(Screen.width * 0.5f) - 150,
				(Screen.height * 0.5f) - 25,
				300,
				50
				);
			
			if(GUI.Button(__startBtnRect, "", _startBtn))
			{
				clique.Play();
				Application.LoadLevel("CutScene");
			}
			
			//Select fase
			Rect __selectFaseBtnRect = new Rect(
				__startBtnRect.x,
				__startBtnRect.y + __startBtnRect.height + 15,
				__startBtnRect.width,
				__startBtnRect.height
				);
			
			if(GUI.Button(__selectFaseBtnRect, "", _selectBtn))
			{
				clique.Play();
				_currentState = MenuState.SELECTING_LEVEL;
			}
			
			//credits
			Rect __creditsBtnRect = new Rect(
				__selectFaseBtnRect.x,
				__selectFaseBtnRect.y + __selectFaseBtnRect.height + 15,
				__selectFaseBtnRect.width,
				__selectFaseBtnRect.height
				);
			
			if(GUI.Button(__creditsBtnRect, "", _creditsBtn))
			{
				clique.Play();
				_currentState = MenuState.CREDITS;
			}
		}
		else if(_currentState == MenuState.SELECTING_LEVEL)
		{
			int __btnColuns = 3;
			int __btnLines = 4;
			float __widthMargin = Screen.height/20.0f;
			float __marginBtweenBtns = 18;
			//float __btnWidth = 85;//((Screen.height - 30) - (__widthMargin * 2) - (__marginBtweenBtns * __btnColuns))/__btnColuns ;
			//float __btnWidth = ((Screen.height) - (__widthMargin * 2) - (__marginBtweenBtns * __btnLines)) ;
			float __btnWidth = (Screen.height/__btnLines)-(__widthMargin*2);
			float __btnHeight = __btnWidth;
			
			for(int i = 0; i < __btnColuns; i ++)
			{
				for(int j = 0; j < __btnLines; j ++)
				{
					Rect __btnRect = new Rect(
						((Screen.width*0.5f)+((__btnWidth+__marginBtweenBtns)*(i-(__btnColuns*0.5f)))) + __marginBtweenBtns * 0.5f, //x
						((Screen.height*0.5f)+((__btnHeight+__marginBtweenBtns)*(j-(__btnLines*0.5f)))) + __marginBtweenBtns * 0.5f, //y
						__btnWidth, //width
						__btnHeight //height
						);
					
					if(GUI.Button(__btnRect, "" + (j * __btnColuns + (i+1)), _selectNumBtn))
					{
						clique.Play();
						Application.LoadLevel("Stage"+(j * __btnColuns + (i)));
					}
				}
			}
			
			//Btn to return to menu
			Rect __returnBtnRect = new Rect(
				Screen.width*0.5f - 100,
				15,
				200,
				35
				);
			
			if(GUI.Button(__returnBtnRect, "", _backBtn))
			{
				clique.Play();
				_currentState = MenuState.INITIAL;
			}
		}
		else if(_currentState == MenuState.CREDITS)
		{
			float __bgMp = Screen.width/1024f;
			Rect __bgRect = new Rect(
				Screen.width * 0.5f - (1024 * __bgMp) * 0.5f,
				Screen.height * 0.5f - (1024 * __bgMp) * 0.5f,
				(1024 * __bgMp),
				(1024 * __bgMp)
				);
			
			GUI.DrawTexture(__bgRect, _bgCredits);
			
			//btn pra voltar
			Rect __voltarBTN = new Rect(
				Screen.width * 0.5f - 250 * 0.5f,
				Screen.height - 80,
				250,
				60
				);
			
			if(GUI.Button(__voltarBTN, "", _backBtn))
			{
				clique.Play();
				_currentState = MenuState.INITIAL;
			}
		}
	}
}
