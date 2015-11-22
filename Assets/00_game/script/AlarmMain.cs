using UnityEngine;
using System.Collections;

public class AlarmMain : PageBase {

	public ButtonBase m_btnSetList;

	public TimeSet m_TimeSet;

	public override void Initialize ()
	{
		base.Initialize ();

		m_btnSetList.TriggerClear ();
		m_TimeSet.Initialize ();
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
			}
		}

	}

}











