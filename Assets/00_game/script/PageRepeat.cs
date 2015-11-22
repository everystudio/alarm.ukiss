using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageRepeat : OtherPage {

	public UIGrid m_Grid;
	public List<BannerRepeat> m_BannerRepeatList = new List<BannerRepeat>();

	public string [] week = new string[7]{
		"Monday",
		"Tuesday",
		"Wednesday",
		"Thursday",
		"Friday",
		"Saturday",
		"Sunday"
	};

	public override void Initialize ()
	{
		if (m_bInitialized == false) {
			base.Initialize ();

			for (int i = 0; i < week.Length; i++) {
				GameObject obj = PrefabManager.Instance.MakeObject ("prefab/BannerRepeat", m_Grid.gameObject);
				obj.name = string.Format ("{0}", i);
				BannerRepeat script = obj.GetComponent<BannerRepeat> ();
				script.Initialize (week [i], false);
				m_BannerRepeatList.Add (script);
			}
			m_Grid.enabled = true;
		}
	}

	void Update(){
		if (ButtonPushed) {
			OutStart ();
			TriggerClearAll ();
		}
	}

}
