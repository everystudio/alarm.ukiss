using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AlarmMain : PageBase {

	public UtilSwitchSprite m_switchSprite;
	public ButtonBase m_btnSetList;

	public TimeSet m_TimeSet;

	public UILabel m_lbNextTime;
	public UILabel m_lbNextWeek;

	public void setNextTimer( List<AlarmReserve> _list ){
		if (0 < _list.Count) {
			string strTime = _list [0].m_strTime;
			Debug.Log (strTime);
			DateTime dateTime = TimeManager.Instance.MakeDateTime (strTime);
			m_lbNextTime.text = string.Format ("{0};{1}", dateTime.Hour, dateTime.Minute);
			m_lbNextWeek.text = DataManager.Instance.STR_WEEK_SHORT_ARR[TimeManager.Instance.GetWeekIndex(strTime)];

		} else {
			m_lbNextTime.text = "--:--";
			m_lbNextWeek.text = "";
		}
	}

	public override void Initialize ()
	{
		base.Initialize ();

		m_btnSetList.TriggerClear ();
		m_TimeSet.Initialize ();

		Debug.Log (GameMain.Instance.reserve_list.Count);
		setNextTimer (GameMain.Instance.reserve_list);

		int iSelectingImageId = GameMain.Instance.kvs_data.ReadInt (DataManager.KEY_SELECTING_IMAGE_ID);
		Debug.LogError (iSelectingImageId);
		foreach (CsvImageData data in DataManager.master_image_list) {
			if (data.id == iSelectingImageId) {
				m_switchSprite.SetSprite (data.name_image);
			}
		}

	}

	public override void Close ()
	{
		base.Close ();
	}

	void Update(){


		if (m_btnSetList.ButtonPushed) {
			m_btnSetList.TriggerClear ();
			m_TimeSet.InStart ();
		}
		if (m_TimeSet.ButtonPushed) {
			if (m_TimeSet.Index == 0) {
				m_TimeSet.OutStart ();
				setNextTimer (GameMain.Instance.reserve_list);
			}
		}

	}



}











