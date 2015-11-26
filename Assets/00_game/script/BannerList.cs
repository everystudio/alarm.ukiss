using UnityEngine;
using System.Collections;

public class BannerList : BannerVoiceBase {

	public UILabel m_lbDescription;
	public override void initialize (CsvVoiceData _data)
	{
		base.initialize (_data);
		m_lbDescription.text = _data.description;
	}


}
