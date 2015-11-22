using UnityEngine;
using System.Collections;

public class OtherPage : ButtonManager {

	private const float MOVE_TIME = 0.1f;
	private Vector3 POS_OUT = new Vector3 (0.0f, -1136.0f, 0.0f);

	public bool m_bInitialized;

	virtual public void Initialize(){
		if (m_bInitialized == false) {
			myTransform.localPosition = POS_OUT;
			ButtonInit ();
		}
		m_bInitialized = true;
	}

	virtual public void InStart(){
		ButtonInit ();
		TweenPosition.Begin (gameObject, MOVE_TIME, Vector3.zero);
	}

	public void OutStart(){
		TweenPosition.Begin (gameObject, MOVE_TIME, POS_OUT);
	}
}
