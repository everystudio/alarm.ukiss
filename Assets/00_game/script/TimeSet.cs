using UnityEngine;
using System.Collections;

public class TimeSet : OtherPage {

	public AlarmData m_AlarmData = new AlarmData ();

	public UIGrid m_Grid;

	public TimeEdit m_TimeEdit;

	public override void Initialize ()
	{
		base.Initialize ();
		if (m_AlarmData == null) {
			m_AlarmData = new AlarmData ();
		}
		m_AlarmData.Load (AlarmData.FILENAME);

		GameObject prefBannerTimer = PrefabManager.Instance.PrefabLoadInstance ("prefab/BannerTimer");
		foreach (AlarmParam param in m_AlarmData.list) {
			GameObject obj = PrefabManager.Instance.MakeObject ("prefab/BannerTimer", m_Grid.gameObject);
			BannerTimer bt = obj.GetComponent<BannerTimer > ();
			bt.Initialize (param);
		}
		m_TimeEdit.Initialize ();
	}

	void Update(){

		if (ButtonPushed) {
			Debug.Log ("Pushed");
			if (Index == 0) {
				OutStart ();
			} else if (Index == 1) {
				m_TimeEdit.InStart ();
			}
			TriggerClearAll ();
		}
	}





}
