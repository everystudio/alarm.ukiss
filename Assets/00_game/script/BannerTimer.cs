using UnityEngine;
using System.Collections;



public class BannerTimer : BannerBase {

	public UILabel m_lbTimer;
	public UILabel m_lbName;

	public UI2DSprite m_sprButtonOn;
	public UI2DSprite m_sprButtonOff;

	public void Initialize( AlarmParam _param ){
		m_lbTimer.text = "12:34";
		m_lbName.text = "sample";

		SetStatus (_param.status);
	}

	public void SetStatus( int _iStatus ){
		if (_iStatus == 0) {
			m_sprButtonOn.gameObject.SetActive (false);
			m_sprButtonOff.gameObject.SetActive (true);
		} else {
			m_sprButtonOn.gameObject.SetActive (true);
			m_sprButtonOff.gameObject.SetActive (false);
		}
	}

}
