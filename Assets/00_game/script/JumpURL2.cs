using UnityEngine;
using System.Collections;

public class JumpURL2 : ButtonBase {

	public string url = "";

	// Use this for initialization
	void Start () {
		TriggerClear ();
	}
	
	// Update is called once per frame
	void Update () {

		if (ButtonPushed) {
			TriggerClear ();
			Application.OpenURL(url);
		}
	
	}
}
