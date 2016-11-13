using UnityEngine;
using System.Collections;

public class UIEditIdle : CPanel {

	private DataMapchipParam m_paramMove;

	private MapRootRestaurant mapRoot {
		get{
			return RestaurantMain.Instance.mapRoot;
		}
	}

	protected override void panelStart ()
	{
		//Debug.LogError ("UIEditIdle:panelStart");
		InputManager.Instance.Info.TouchUp = false;

		UIAssistant.main.SetPrevPage ("MainIdle");
		m_bSwipeCheck = false;

	}

	protected override void panelEndStart ()
	{
		//Debug.LogError ("UIEditIdle:panelEnd");
	}

	private bool m_bSwipeCheck;

	void Update(){

		if(InputManager.Instance.Info.Swipe)
		{
			m_bSwipeCheck = m_bSwipeCheck == false ? true : false;
		}


		if (InputManager.Instance.Info.TouchUp) {
			InputManager.Instance.Info.TouchUp = false;
			if (m_bSwipeCheck)
			{
				m_bSwipeCheck = false;
			}
			else {
				int iGridX = 0;
				int iGridY = 0;

				if (mapRoot.GetGrid(InputManager.Instance.Info.TouchPoint, out iGridX, out iGridY))
				{
					//Debug.Log (string.Format ("grid({0},{1})", iGridX, iGridY));

					if (DataManager.Instance.dataMapchip.GetExist(iGridX, iGridY, out m_paramMove))
					{
						UIParam.Instance.m_iEditMapChipSerial = m_paramMove.mapchip_serial;
						UIAssistant.main.ShowPage("EditMove");
					}
					Debug.Log(string.Format("hit:x={0} y={1}", iGridX, iGridY));

				}
				else {
					Debug.Log("no hit");
				}
			}
		}
	}


}


