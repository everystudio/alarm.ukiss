using UnityEngine;
using System.Collections;

public class JumpURL : ButtonBase {

	// Use this for initialization
	void Start () {
		TriggerClear ();
	}
	
	// Update is called once per frame
	void Update () {

		if (ButtonPushed) {
			TriggerClear ();
			Application.OpenURL("https://apps.rakuten.co.jp/campaign/u_kiss/");
		}
	
	}
}
