using UnityEngine;
using System.Collections;

public class CsvVoiceData : CsvDataParam
{
	public int id { get; private set; }
	public int type { get; private set; }
	public string name { get; private set; }
	public string description { get; private set; }
	public string name_icon { get; private set; }
	public string name_voice { get; private set; }
}


public class CsvVoice : CsvData<CsvVoiceData> {
	private static readonly string FilePath = "csv/voice_list";
	public void Load() { Load(FilePath); }
}



