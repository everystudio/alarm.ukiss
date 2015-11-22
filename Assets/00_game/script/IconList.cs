using UnityEngine;
using System.Collections;

public class IconList : IconBase {

	public UI2DSprite m_sprSelecting;
	public UI2DSprite m_sprIcon;

	public ImageCheck m_imageCheck;

	public void Initialize(  bool _bIsSelect ){

		m_sprSelecting.gameObject.SetActive (_bIsSelect);


	}

	// Update is called once per frame
	void Update () {
	
	}
}
