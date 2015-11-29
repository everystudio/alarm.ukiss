﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMain : PageBase {

	private static GameMain instance;
	public static GameMain Instance {
		get {
			if (instance == null) {
				GameObject obj = GameObject.Find ("GameMain");
				if (obj == null) {
					obj = new GameObject("GameMain");
					//Debug.LogError ("Not Exist AtlasManager!!");
				}
				instance = obj.GetComponent<GameMain> ();
				if (instance == null) {
					//Debug.LogError ("Not Exist AtlasManager Script!!");
					instance = obj.AddComponent<GameMain>() as GameMain;
				}
			}
			return instance;
		}
	}

	public AlarmMain m_AlarmMain;
	public AlarmParam EditingAlarmParam;
	public AlarmData m_AlarmData = new AlarmData ();

	public KvsData kvs_data{
		get{ 
			if (m_KvsData == null) {

				m_KvsData = new KvsData ();

			}
			return m_KvsData;
		}
	}
	public KvsData m_KvsData;

	public PageBase m_PageNow;
	public List<PageBase> m_PageBaseList = new List<PageBase> ();



	public PageFooter m_PageFooter;
	public int m_iPagePre;

	public TimeSet m_TimeSet;
	public void TimeSetRefresh(){
		m_TimeSet.DisplayReflresh ();
	}

	public void timeRefreshFromToday(){

		string strNow = TimeManager.StrNow ();
		DateTime dateTime = TimeManager.GetNow ();
		foreach( AlarmParam alarm in m_AlarmData.list ){
			TimeSpan ts = TimeManager.Instance.GetDiff (strNow , alarm.time);
			if (0 < ts.Seconds) {
			} else {
			}

		}




	}

	public void reloadTime(AlarmParam _param){
		if (_param.status == 0) {
			return;
		}
	}

	public AlarmParam GetNearParam(){

		int iSerial = 0;

		return new AlarmParam ();


	}
	public List<AlarmReserve> reserve_list = new List<AlarmReserve> ();

	public void setupAlarmReserve( ref List<AlarmReserve> _insertList , List<AlarmParam> _alarmList ){

		LocalNotificationManager.Instance.ClearLocalNotification ();
		_insertList.Clear ();

		DateTime datetimeNow = TimeManager.GetNow();
		foreach (AlarmParam param in _alarmList) {
			if (param.status == 0) {
				continue;
			}
			//Debug.Log ( string.Format( "serial:{0} repeat_type:{1}",param.serial,  param.repeat_type));
			if (param.repeat_type == 0) {
				DateTime checkDate = TimeManager.Instance.MakeDateTime (param.time);
				//string strCheckDate = string.Format ("{0}-{1:D2}-{2:D2} {3:D2}:{4:D2}:00", datetimeNow.Year, datetimeNow.Month, datetimeNow.Day, checkDate.Hour, checkDate.Minute);
				//Debug.Log (TimeManager.Instance.GetDiffNow (strCheckDate).TotalSeconds);
				string strCheckDate = param.time;
				TimeSpan time_span = TimeManager.Instance.GetDiffNow (strCheckDate);
				AlarmReserve insert_data = new AlarmReserve ();
				if (0 < time_span.TotalSeconds) {
					insert_data.m_strTime = strCheckDate;
					insert_data.m_iVoiceType = param.voice_type;
					insert_data.m_lTime = (long)TimeManager.Instance.GetDiffNow (insert_data.m_strTime).TotalSeconds;
					_insertList.Add (insert_data);
				} else {

				}
				/*
				 * こっちは旧式
				else {

					DateTime tomorrowDateTime = TimeManager.GetNow();
					tomorrowDateTime = tomorrowDateTime.AddDays (1);
					string strTomorrow = string.Format ("{0}-{1:D2}-{2:D2} {3:D2}:{4:D2}:00", tomorrowDateTime.Year, tomorrowDateTime.Month, tomorrowDateTime.Day, checkDate.Hour, checkDate.Minute);
					insert_data.m_strTime = strTomorrow;
				}
				insert_data.m_iVoiceType = param.voice_type;
				insert_data.m_lTime = (long)TimeManager.Instance.GetDiffNow (insert_data.m_strTime).TotalSeconds;
				_insertList.Add (insert_data);
				*/
			} else {
				int iNowWeek = TimeManager.Instance.GetWeekIndex (TimeManager.StrGetTime ());

				for (int i = 0; i < DataManagerAlarm.Instance.STR_WEEK_ARR.Length; i++) {
					if (0 < (param.repeat_type & (1<<i))) {
						// 曜日にひっかかった
						string strStartDate = "";
						int iOffset = i - iNowWeek;
						DateTime checkDate = TimeManager.Instance.MakeDateTime (param.time);
						if (iOffset == 0) {
							string strCheckDate = string.Format ("{0}-{1:D2}-{2:D2} {3:D2}:{4:D2}:00", datetimeNow.Year, datetimeNow.Month, datetimeNow.Day, checkDate.Hour, checkDate.Minute);
							TimeSpan time_span = TimeManager.Instance.GetDiffNow (strCheckDate);
							if (0 < time_span.TotalSeconds) {
							} else {
								iOffset = 7;
							}
						} else if (iOffset < 0) {
							iOffset += DataManagerAlarm.Instance.STR_WEEK_ARR.Length;
						} else {
							// そのまま
						}
						DateTime nextDateTime = TimeManager.GetNow();
						nextDateTime = nextDateTime.AddDays (iOffset);

						for (int count = 0; count < 10; count++) {
							string strNext = string.Format ("{0}-{1:D2}-{2:D2} {3:D2}:{4:D2}:00", nextDateTime.Year, nextDateTime.Month, nextDateTime.Day, checkDate.Hour, checkDate.Minute);
							strStartDate = strNext;
							AlarmReserve insert_data = new AlarmReserve ();
							insert_data.m_strTime = strNext;
							insert_data.m_iVoiceType = param.voice_type;
							insert_data.m_lTime = (long)TimeManager.Instance.GetDiffNow (insert_data.m_strTime).TotalSeconds;
							_insertList.Add (insert_data);

							// 次の準備
							nextDateTime = nextDateTime.AddDays (7);
						}
					}
				}
			}
			_insertList.Sort ((a, b) => (int)(a.m_lTime - b.m_lTime));
		}
		foreach (AlarmReserve reserve in _insertList) {
			LocalNotificationManager.Instance.AddLocalNotification (reserve.m_lTime, ConfigManager.Instance.GetEditPlayerSettingsData ().projectData.m_strProductName, "時刻になりました");
		}

		return;
	}


	public void reserveTimeReset(){
		m_AlarmData.Load (AlarmData.FILENAME);
		setupAlarmReserve (ref reserve_list, m_AlarmData.list);
		m_AlarmMain.setNextTimer (reserve_list);

	}
	void Start(){

		instance = this;
		EditingAlarmParam = new AlarmParam ();
		m_iPagePre = 0;

		if (m_AlarmData == null) {
			m_AlarmData = new AlarmData ();
		}
		reserveTimeReset ();

		kvs_data.Load (KvsData.FILE_NAME);
		int iTest = kvs_data.ReadInt ("test");
		iTest += 1;
		kvs_data.WriteInt ("test", iTest );
		kvs_data.Save ();
		m_PageNow = m_PageBaseList [m_iPagePre];
		InitPage (m_PageNow);
	}

	public float m_fCheckIntervalTime;

	void Update(){

		if (m_PageFooter.ButtonPushed) {
			if (m_iPagePre != m_PageFooter.Index) {

				ClosePage (m_PageNow, m_iPagePre);
				m_PageNow = m_PageBaseList [m_PageFooter.Index];
				InitPage (m_PageNow);
				m_iPagePre = m_PageFooter.Index;
			}
			m_PageFooter.TriggerClearAll ();
		}

		List <AlarmReserve> remove_list = new List<AlarmReserve>();
		m_fCheckIntervalTime += Time.deltaTime;
		if (2.0f < m_fCheckIntervalTime) {
			m_fCheckIntervalTime -= 2.0f;

			List<int> remove_index_list = new List<int> ();
			int iRemoveNum = 0;

			int iCount = 0;
			foreach (AlarmReserve reserve_param in reserve_list) {
				bool bRemove = false;
				if (TimeManager.Instance.GetDiffNow (reserve_param.m_strTime).TotalSeconds < 0 ) {
					bRemove = true;
					iRemoveNum += 1;
				}
				if (bRemove) {
					remove_list.Add (reserve_param);
				}
			}

			foreach( AlarmReserve remove_param in remove_list ){
				reserve_list.Remove( remove_param );
			}

			if (0 < iRemoveNum) {
				Debug.LogError ("remove");
				m_AlarmMain.setNextTimer (reserve_list);
			}


		}




	}

	public void InitPage( PageBase _pageBase ){
		//_obj.SetActive (true);
		_pageBase.gameObject.transform.localPosition = Vector3.zero;
		_pageBase.Initialize ();
	}

	public void ClosePage(PageBase _pageBase , int _iIndex ){
		//_obj.SetActive (false);
		_pageBase.gameObject.transform.localPosition = new Vector3( 640.0f , 1136.0f * (2 - _iIndex ) , 0.0f );
		_pageBase.Close ();
	}



}
