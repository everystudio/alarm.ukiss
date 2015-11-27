using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManagerAlarm : DataManagerBase<DataManagerAlarm> {


	public CsvImage m_csvImage = new CsvImage();
	public List<CsvImageData> master_image_list {
		get{ 
			return Instance.m_csvImage.All;
		}
	}
	public CsvVoice m_csvVoice = new CsvVoice();
	public List<CsvVoiceData> master_voice_list {
		get{ 
			return Instance.m_csvVoice.All;
		}
	}

	public override void Initialize ()
	{
		base.Initialize ();

		m_csvImage.Load ();
		m_csvVoice.Load ();

	}

	public string [] STR_WEEK_ARR = new string[7]{
		"Monday",
		"Tuesday",
		"Wednesday",
		"Thursday",
		"Friday",
		"Saturday",
		"Sunday"
	};
	public string [] STR_WEEK_SHORT_ARR = new string[7]{
		"Mon",
		"Tue",
		"Wed",
		"Thu",
		"Fri",
		"Sat",
		"Sun"
	};
	public string [] STR_SNOOZE_ARR = new string[3]{
		"5 min",
		"10 min",
		"None",
	};
	public const string KEY_SELECTING_IMAGE_ID = "selecting_image_id";

}

[System.Serializable]
public class AlarmReserve{
	public long m_lTime;
	public string m_strTime;
	public int m_iVoiceType;
}


