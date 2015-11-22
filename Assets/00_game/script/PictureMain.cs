using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PictureMain : PageBase {

	public int m_iSelectingIndex;
	public ButtonManager m_bmIconList;

	public List<IconList> m_iconList = new List<IconList> ();

	public ImageCheck m_imageCheck;

	public void IconSet( int _iSelectIndex ){
		foreach (IconList icon in m_iconList) {
			icon.Initialize (icon.Index == _iSelectIndex);
		}
		return;
	}

	// Use this for initialization
	void Start () {
		m_bmIconList.ButtonInit ();

		m_iSelectingIndex = 0;
		IconSet (m_iSelectingIndex);

		m_imageCheck.Initialize ();
	}
	
	// Update is called once per frame
	void Update () {

		if (m_bmIconList.ButtonPushed) {
			if (m_iSelectingIndex != m_bmIconList.Index) {
				m_iSelectingIndex  = m_bmIconList.Index;
				m_imageCheck.InStart ();
			}
			m_bmIconList.TriggerClearAll ();

			m_imageCheck.InStart ();
		}

		if (m_imageCheck.ButtonPushed) {

			if (m_imageCheck.Index == 0) {
			} else if (m_imageCheck.Index == 1) {
				IconSet (m_iSelectingIndex);
			} else {
			}
			m_imageCheck.OutStart();

		}


	}
}
