using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeEdit : OtherPage {

	#region SerializeFieldでの設定が必要なメンバー変数
	[SerializeField]
	private UICenterOnChild m_csCenterOnChildHour;
	[SerializeField]
	private UICenterOnChild m_csCenterOnChildMinute;
	#endregion
	private GameObject m_goCenterHour;
	private GameObject m_goCenterMinute;
	public List<GameObject> m_goHourList = new List<GameObject> ();
	public List<GameObject> m_goMinuteList = new List<GameObject> ();

	public UIGrid m_GridHour;
	public UIGrid m_GridMinute;

	public ButtonBase m_btnRepeat;
	public ButtonBase m_btnSnooze;
	public ButtonBase m_btnVoice;

	public PageRepeat m_PageRepeat;

	private void _setup_time( UIGrid _grid , int _iMax , string _strTail , ref List<GameObject> _list ){
		for (int i = 0; i < _iMax; i++) {
			GameObject obj = PrefabManager.Instance.MakeObject ("prefab/Hour" , _grid.gameObject );
			obj.name = string.Format ("{0}", i);
			obj.GetComponent<UILabel> ().text = string.Format ("{0}{1}", i , _strTail );
			_list.Add (obj);
		}
		_grid.enabled = true;
	}


	public override void Initialize ()
	{
		base.Initialize ();
		m_csCenterOnChildHour.onCenter = DragBannerHour;
		m_csCenterOnChildMinute.onCenter = DragBannerMinute;

		_setup_time (m_GridHour, 24, "時" , ref m_goHourList );
		_setup_time (m_GridMinute, 60, "分", ref m_goMinuteList);

		m_btnRepeat.TriggerClear();
		m_btnSnooze.TriggerClear();
		m_btnVoice.TriggerClear();

		m_PageRepeat.Initialize ();


	}

	public override void InStart ()
	{
		base.InStart ();
	}

	void Update(){

		if (m_btnRepeat.ButtonPushed ) {
			m_btnRepeat.TriggerClear ();
			m_PageRepeat.InStart ();
		}



	}
	#region scroll関連
	// バナーがドラッグされて切り替わった際に呼ばれるイベント
	public void DragBannerHour(GameObject _goBanner) {
		//Debug.Log (_goBanner.name);
		int iBannerNo = 0;
		SetBannerHour(_goBanner);
		return;
	}
	public void SetBannerHour( int _iBannerId ){
		foreach (GameObject obj in m_goHourList) {
			int banner_id = int.Parse (obj.name);
			if (banner_id == _iBannerId) {
				SetBannerHour (obj);
				break;
			}
		}
		return;
	}
	public void SetBannerHour( GameObject _goBanner ){
		//Debug.Log (_goBanner.name);
		if (m_goCenterHour != _goBanner) {
			m_goCenterHour = _goBanner;
			m_csCenterOnChildHour.CenterOn (_goBanner.transform);
		} else {
		}
	}

	// バナーがドラッグされて切り替わった際に呼ばれるイベント
	public void DragBannerMinute(GameObject _goBanner) {
		//Debug.Log (_goBanner.name);
		int iBannerNo = 0;
		SetBannerMinute(_goBanner);
		return;
	}
	public void SetBannerMinute( int _iBannerId ){
		foreach (GameObject obj in m_goMinuteList) {
			int banner_id = int.Parse (obj.name);
			if (banner_id == _iBannerId) {
				SetBannerMinute (obj);
				break;
			}
		}
		return;
	}
	public void SetBannerMinute( GameObject _goBanner ){
		//Debug.Log (_goBanner.name);
		if (m_goCenterMinute != _goBanner) {
			m_goCenterMinute = _goBanner;
			m_csCenterOnChildMinute.CenterOn (_goBanner.transform);
		} else {
		}
	}
	#endregion

}
