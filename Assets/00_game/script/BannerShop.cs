using UnityEngine;
using System.Collections;

public class BannerShop : BannerVoiceBase {

	public UILabel m_lbDescription;
	public UILabel m_lbName;
	public override void initialize (CsvVoiceData _data)
	{
		base.initialize (_data);
		m_lbDescription.text = _data.description;
		m_lbName.text = _data.name;
	}


}
