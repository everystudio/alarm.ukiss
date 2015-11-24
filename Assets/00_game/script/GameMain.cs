using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMain : PageBase {

	private static GameMain instance;
	public static GameMain Instance {
		get {
			if (instance == null) {
				GameObject obj = GameObject.Find ("GameMain");
				if (obj == null) {
					obj = new GameObject("GameMain");
					//Debug.LogError ("Not Exist AtlasManager!!");
				}
				instance = obj.GetComponent<GameMain> ();
				if (instance == null) {
					//Debug.LogError ("Not Exist AtlasManager Script!!");
					instance = obj.AddComponent<GameMain>() as GameMain;
				}
			}
			return instance;
		}
	}

	public AlarmParam EditingAlarmParam;
	public AlarmData m_AlarmData = new AlarmData ();

	public PageBase m_PageNow;
	public List<PageBase> m_PageBaseList = new List<PageBase> ();



	public PageFooter m_PageFooter;
	public int m_iPagePre;

	public TimeSet m_TimeSet;
	public void TimeSetRefresh(){
		m_TimeSet.DisplayReflresh ();
	}


	void Start(){
		instance = this;
		EditingAlarmParam = new AlarmParam ();
		m_iPagePre = 0;
		m_PageNow = m_PageBaseList [m_iPagePre];
		InitPage (m_PageNow);

		if (m_AlarmData == null) {
			m_AlarmData = new AlarmData ();
		}
		m_AlarmData.Load (AlarmData.FILENAME);


		/*
		int iShift = 0;
		iShift = iShift | (1 << 0);
		iShift = iShift | (1 << 1);
		Debug.Log (iShift);
		//iShift = iShift | ~(1 << 1);
		iShift &= ~(1 << 1);
		iShift &= ~(1 << 1);
		iShift &= ~(1 << 1);
		iShift &= ~(1 << 1);
		Debug.Log (iShift);
	
		Debug.Log (iShift &(1 << 0));
		Debug.Log (iShift &(1 << 1));
		Debug.Log (iShift &(1 << 2));
		Debug.Log (iShift &(1 << 3));
		*/



	}

	void Update(){

		if (m_PageFooter.ButtonPushed) {
			if (m_iPagePre != m_PageFooter.Index) {

				ClosePage (m_PageNow, m_iPagePre);
				m_PageNow = m_PageBaseList [m_PageFooter.Index];
				InitPage (m_PageNow);
				m_iPagePre = m_PageFooter.Index;
			}
			m_PageFooter.TriggerClearAll ();
		}
	}

	public void InitPage( PageBase _pageBase ){
		//_obj.SetActive (true);
		_pageBase.gameObject.transform.localPosition = Vector3.zero;
		_pageBase.Initialize ();
	}

	public void ClosePage(PageBase _pageBase , int _iIndex ){
		//_obj.SetActive (false);
		_pageBase.gameObject.transform.localPosition = new Vector3( 640.0f , 1136.0f * (2 - _iIndex ) , 0.0f );
		_pageBase.Close ();
	}



}
