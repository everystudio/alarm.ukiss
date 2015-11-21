using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMain : PageBase {

	public PageBase m_PageNow;
	public List<PageBase> m_PageBaseList = new List<PageBase> ();

	public PageFooter m_PageFooter;
	public int m_iPagePre;

	void Start(){
		m_iPagePre = 0;
		m_PageNow = m_PageBaseList [m_iPagePre];
		InitPage (m_PageNow);
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
