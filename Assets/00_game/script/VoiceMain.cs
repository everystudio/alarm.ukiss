using UnityEngine;
using System.Collections;

public class VoiceMain : PageBase {

	public ButtonManager m_TabButtonManager;
	public GameObject m_goPanelList;
	public GameObject m_goPanelStore;

	public int m_iTabIndex;

	public override void Initialize ()
	{
		m_TabButtonManager.ButtonInit ();
		m_TabButtonManager.TriggerClearAll ();
		m_iTabIndex = m_TabButtonManager.Index;
		tab_switch (m_iTabIndex);
	}

	public override void Close ()
	{
		base.Close ();
	}

	private void tab_switch( int _iIndex ){
		if (_iIndex == 0) {
			m_goPanelList.SetActive (true);
			m_goPanelStore.SetActive (false);
		} else if (_iIndex == 1) {
			m_goPanelList.SetActive (false);
			m_goPanelStore.SetActive (true);
		}

	}

	void Update(){

		if (m_TabButtonManager.ButtonPushed) {
			if (m_iTabIndex != m_TabButtonManager.Index) {
				m_iTabIndex = m_TabButtonManager.Index;
				tab_switch (m_iTabIndex);
			}

			m_TabButtonManager.TriggerClearAll ();
		}

	}

}




