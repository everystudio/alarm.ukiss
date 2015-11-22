using UnityEngine;
using System.Collections;

public class BannerRepeat : BannerBase {

	public UILabel m_lbText;

	public ButtonBase m_btnTrigger;
	public UI2DSprite m_sprYes;
	public UI2DSprite m_sprNo;

	public bool m_bFlag;

	private void button( bool _bOn ){
		if (_bOn) {
			m_sprYes.gameObject.SetActive (true);
			m_sprNo.gameObject.SetActive (false);
		} else {
			m_sprYes.gameObject.SetActive (false);
			m_sprNo.gameObject.SetActive (true);
		}
	}

	public void Initialize( string _strLabel , bool _bOn ){
		m_lbText.text = _strLabel;
		m_bFlag = _bOn;
		button (m_bFlag);
		m_btnTrigger.TriggerClear ();
	}

	void Update(){

		if (m_btnTrigger.ButtonPushed) {
			m_btnTrigger.TriggerClear ();

			m_bFlag = !m_bFlag;
			button (m_bFlag);
			return;
		}

	}

}
