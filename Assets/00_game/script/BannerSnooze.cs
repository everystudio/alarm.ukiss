using UnityEngine;
using System.Collections;

public class BannerSnooze : BannerBase {

	public UILabel m_lbText;

	public UI2DSprite m_sprSelecting;

	public bool m_bFlag;

	public void Initialize( string _strLabel , bool _bOn ){
		m_lbText.text = _strLabel;
		m_sprSelecting.gameObject.SetActive (_bOn);
	}


}
