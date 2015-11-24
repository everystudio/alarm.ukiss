using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeSet : OtherPage {

	public UIGrid m_Grid;

	public TimeEdit m_TimeEdit;

	public List<BannerTimer> m_BannerTimerList = new List<BannerTimer> ();

	public void DisplayReflresh(){

		foreach (BannerTimer bt in m_BannerTimerList) {
			Destroy (bt.gameObject);
		}

		m_BannerTimerList.Clear ();
		GameMain.Instance.m_AlarmData.Load (AlarmData.FILENAME);
		GameObject prefBannerTimer = PrefabManager.Instance.PrefabLoadInstance ("prefab/BannerTimer");
		foreach (AlarmParam param in GameMain.Instance.m_AlarmData.list) {
			GameObject obj = PrefabManager.Instance.MakeObject ("prefab/BannerTimer", m_Grid.gameObject);
			BannerTimer bt = obj.GetComponent<BannerTimer > ();
			bt.Initialize (param);

			m_BannerTimerList.Add (bt);
		}
		m_Grid.enabled = true;
		return;
	}

	public override void Initialize ()
	{
		Debug.LogError ("TimeSet.Initialize");
		base.Initialize ();
		m_TimeEdit.Initialize ();
	}
	public override void InStart ()
	{
		base.InStart ();
		DisplayReflresh ();
	}

	void Update(){

		if (ButtonPushed) {
			//Debug.Log ("Pushed");
			if (Index == 0) {
				OutStart ();
			} else if (Index == 1) {
				GameMain.Instance.EditingAlarmParam = new AlarmParam ();
				GameMain.Instance.EditingAlarmParam.time = TimeManager.StrGetTime ();
				GameMain.Instance.EditingAlarmParam.repeat_type = 0;
				GameMain.Instance.EditingAlarmParam.snooze = 2;
				m_TimeEdit.InStart (GameMain.Instance.EditingAlarmParam);

			}
			TriggerClearAll ();
		}

		foreach (BannerTimer bt in m_BannerTimerList) {
			if (bt.ButtonPushed) {
				bt.TriggerClear ();

				Debug.Log( bt.m_AlarmParam.time);
				GameMain.Instance.EditingAlarmParam = bt.m_AlarmParam;
				m_TimeEdit.InStart (GameMain.Instance.EditingAlarmParam);
			}
		}

	}





}
