using UnityEngine;
using System.Collections;
using System;

public class BannerTimer : BannerBase {

	public UILabel m_lbTimer;
	public UILabel m_lbName;

	public UI2DSprite m_sprButtonOn;
	public UI2DSprite m_sprButtonOff;

	public AlarmParam m_AlarmParam;
	public void Initialize( AlarmParam _param ){
		m_AlarmParam = _param;

		Debug.Log (_param.time);
		DateTime time = TimeManager.Instance.MakeDateTime (_param.time);

		m_lbTimer.text = string.Format( "{0:D2}:{1:D2}" , time.Hour , time.Minute );
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
