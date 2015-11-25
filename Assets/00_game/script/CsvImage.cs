using UnityEngine;
using System.Collections;

public class CsvImageData : MasterBase
{
	public int id { get; private set; }
	public int type { get; private set; }
	public string name { get; private set; }
	public string description { get; private set; }
	public string name_image { get; private set; }
	public string name_icon { get; private set; }
}


public class CsvImage : CsvBase<CsvImageData> {
	private static readonly string FilePath = "csv/image_list";
	public void Load() { Load(FilePath); }
}


