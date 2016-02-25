using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Startup : MonoBehaviour {

	public enum STEP
	{
		INITIALIZW		= 0,
		CHECK_NEW_DATA	,
		COPY_PERMISSIVE	,
		START_MAIN		,
		MAX				,
	};
	public STEP m_eStep;
	public STEP m_eStepPre;

	public KvsData m_kvs;
	public int m_iNetworkSerial;
	public int m_iDownloadCount;

	public string m_strCsvDataVersion;

	public List<string> load_check = new List<string>(){
		"image_list.csv",
		"voice_list.csv",
		"voiceset_list.csv",
	};

	// Use this for initialization
	void Start () {


		m_kvs = new KvsData ();
		m_kvs.Load (KvsData.FILE_NAME);
		m_kvs.Save ();

		m_eStep = STEP.INITIALIZW;
		m_eStepPre = STEP.MAX;
	
	}
	
	// Update is called once per frame
	void Update () {

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {
		case STEP.INITIALIZW:
			if (bInit) {
				m_iDownloadCount = 0;
				foreach (string filename in load_check) {
					StartCoroutine (initialData (filename));
				}
			}
			if (m_iDownloadCount == load_check.Count) {
				m_eStep = STEP.CHECK_NEW_DATA;
			}
			break;

		case STEP.CHECK_NEW_DATA:
			if (bInit) {
				m_iNetworkSerial = EveryStudioLibrary.CommonNetwork.Instance.Recieve ("http://ad.xnosserver.com/apps/myzoo_data/ukiss/datacheck.txt");
			}
			if (EveryStudioLibrary.CommonNetwork.Instance.IsConnected (m_iNetworkSerial) == true) {

				EveryStudioLibrary.TNetworkData network_data = EveryStudioLibrary.CommonNetwork.Instance.GetData (m_iNetworkSerial);
				if (network_data.IsError ()) {
					m_eStep = STEP.START_MAIN;
				} else {
					string strRead = EveryStudioLibrary.CommonNetwork.Instance.GetString (m_iNetworkSerial);
					StringReader sr = new StringReader (strRead);
					strRead = sr.ReadLine ();

					m_strCsvDataVersion = m_kvs.Read ("csv_data_version");

					Debug.Log (m_strCsvDataVersion);
					Debug.Log (strRead);

					if (m_strCsvDataVersion.Equals (strRead) == true) {
						m_eStep = STEP.START_MAIN;
						Debug.Log ("equal");
					} else {
						Debug.Log ("not equal");
						m_eStep = STEP.COPY_PERMISSIVE;
						m_strCsvDataVersion = strRead;
					}
					/*
					string strRead = EveryStudioLibrary.CommonNetwork.Instance.GetString (m_iNetworkSerial);
					StringReader sr = new StringReader (strRead);
					m_strLimitTime = sr.ReadLine ();
					m_strTitle = sr.ReadLine ();
					m_lbTitle.text = m_strTitle;
					sr.Close ();
					*/
				}
			} else {
			}
			break;

		case STEP.COPY_PERMISSIVE:
			if (bInit) {
				m_iDownloadCount = 0;
				foreach (string filename in load_check) {
					StartCoroutine (LoadData (filename, "http://ad.xnosserver.com/apps/myzoo_data/ukiss"));
				}
			}
			if ( m_iDownloadCount == load_check.Count) {
				m_eStep = STEP.START_MAIN;

				// データを更新した場合はアセットバンドルの更新があるかも知れないので
				// キャッシュクリアをする
				Caching.CleanCache();

				m_kvs.Write ("csv_data_version", m_strCsvDataVersion);
				m_kvs.Save ();
			}
			break;

		case STEP.START_MAIN:
			if (bInit) {
				SceneManager.LoadScene ("main");
			}
			break;
		case STEP.MAX:
		default:
			break;
		}
	}


	IEnumerator initialData(string _strFilename ){
		string fromPath = EditPlayerSettingsData.GetStreamingAssetPathHead() + Application.streamingAssetsPath + "/csv/" + _strFilename;
		string toPath = Application.persistentDataPath + "/" + _strFilename;
		if (System.IO.File.Exists (toPath)) {
		//if (false) {
			m_iDownloadCount += 1;
			yield return 0;
		} else 
		{
			WWW www = new WWW (fromPath);
			while(!www.isDone){
				yield return null;
			}
			System.IO.File.WriteAllBytes (toPath, www.bytes);
			m_iDownloadCount += 1;
		}
	}
		

	IEnumerator LoadData(string _strFilename , string _strUrl ){

		/*
		#if UNITY_EDITOR
		string path = "file:///"+Application.persistentDataPath+"/";
		#elif UNITY_ANDROID
		string path = "file://"+Application.persistentDataPath+"/";
		#endif
		*/

		//string server_path = System.IO.Path.Combine (_strUrl, _strFilename);
		string server_path = _strUrl+"/"+ _strFilename;
		string local_path = Application.persistentDataPath + "/" + _strFilename;

		/*
		if (System.IO.File.Exists (local_path)) {
			System.IO.File.Delete( local_path );
		}
		*/

		Debug.Log (server_path);

		WWW www = new WWW (server_path);
		while(!www.isDone){
			yield return null;
		}
		Debug.Log("Done");
		System.IO.File.WriteAllBytes(local_path , www.bytes);
		m_iDownloadCount += 1;
	}









}







