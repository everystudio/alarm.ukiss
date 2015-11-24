using UnityEngine;
using System.Collections;
using System;

public class BannerTimer : BannerBase {

	public UILabel m_lbTimer;
	public UILabel m_lbName;

	public UI2DSprite m_sprButtonOn;
	public UI2DSprite m_sprButtonOff;

	public ButtonBase m_btnTrigger;

	public AlarmParam m_AlarmParam;
	public void Initialize( AlarmParam _param ){
		m_AlarmParam = _param;
		//Debug.Log (_param.time);
		DateTime time = TimeManager.Instance.MakeDateTime (_param.time);

		m_lbTimer.text = string.Format( "{0:D2}:{1:D2}" , time.Hour , time.Minute );
		m_lbName.text = "sample";
		Debug.Log (_param.status);
		SetStatus (_param.status);

		m_btnTrigger.TriggerClear ();
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

	void Update(){
		if (m_btnTrigger.ButtonPushed) {
			if (m_AlarmParam.status == 0) {
				m_AlarmParam.status = 1;
			} else {
				m_AlarmParam.status = 0;
			}
			SetStatus (m_AlarmParam.status);
			GameMain.Instance.m_AlarmData.UpdateStatus (m_AlarmParam.serial, m_AlarmParam.status);
			GameMain.Instance.m_AlarmData.Save ();
			m_btnTrigger.TriggerClear ();
		}
	}

}
