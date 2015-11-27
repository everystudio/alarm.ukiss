using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AlarmParam : SODataParam {
	public int m_serial;
	public string m_time;
	public int m_status;
	public long m_repeat_type;
	public int m_snooze;
	public int m_voice_type;
	public int serial{ get{ return m_serial;} set{m_serial = value; } }
	public string time{ get{ return m_time;} set{m_time = value; } }
	public int status{ get{ return m_status;} set{m_status = value; } }
	public long repeat_type{ get{ return m_repeat_type;} set{m_repeat_type = value; } }
	public int snooze{ get{ return m_snooze;} set{m_snooze = value; } }
	public int voice_type{ get{ return m_voice_type;} set{m_voice_type = value; } }
}

public class AlarmData : SODataBase<AlarmParam>{

	public const string FILENAME = "alarmparam";

	protected override void save ()
	{
		StreamWriter sw = Textreader.Open (string.Format ("{0}.csv", "alarmparam"));

		string strHead = string.Format ("{0},{1},{2},{3},{4},{5}",
			"serial",
			"time",
			"status",
			"repeat_type",
			"snooze",
			"voice_type"
		);

		Textreader.Write (sw, strHead);
		foreach (AlarmParam data in list) {
			string strData = string.Format ("{0},{1},{2},{3},{4},{5}",
				data.serial,
				data.time,
				data.status,
				data.repeat_type,
				data.snooze,
				data.voice_type
			);
			Textreader.Write (sw, strData);
			//Textreader.SaveWrite (string.Format ("{0}.csv", DBItem.FILE_NAME), strData);
		}
		Textreader.Close( sw );
		return;
	}

	public bool UpdateStatus( int _iSerial , int _iStatus ){
		foreach (AlarmParam param in list) {
			if (param.serial == _iSerial) {
				param.status = _iStatus;
				return true;
			}
		}
		return false;
	}


}










