﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class StaffBanner : MonoBehaviour {

	public UnityEventInt OnSelectStaff = new UnityEventInt();

	[SerializeField]
	private Text m_textName;

	[SerializeField]
	private CtrlRareStars m_ctrlRareStarts;

	[SerializeField]
	private Image m_imgRole;
	[SerializeField]
	private Image m_imgChara;

	[SerializeField]
	private Image m_imgBack;

	[SerializeField]
	private Slider m_slExp;

	[SerializeField]
	private Text m_textLevel;

	[SerializeField]
	private StaffParam m_paramManner;
	[SerializeField]
	private StaffParam m_paramFootwork;
	[SerializeField]
	private StaffParam m_paramCook;

	[SerializeField]
	private CtrlUserParam m_paramKeihi;

	[SerializeField]
	private Image m_imgTraining;

	private DataStaffParam m_dataStaffParam;
	public void ButtonPushed(){
		OnSelectStaff.Invoke (m_dataStaffParam.staff_serial);
	}

	public void UpdateRole( int _iRole ){
		m_imgRole.sprite = SpriteManager.Instance.LoadSprite( DataStaff.GetIconRole(_iRole));
	}

	public void UpdateTrainingType( int _iTrainingType ){
		if (_iTrainingType == (int)DataStaff.TRAINING_TYPE.NONE) {
			m_imgTraining.gameObject.SetActive (false);
		} else {
			m_imgTraining.gameObject.SetActive (true);
			m_imgTraining.sprite = SpriteManager.Instance.LoadSprite (DataStaff.GetIconTraining(_iTrainingType) );
		}
	}

	public void Initialize( DataStaffParam _param ){
		m_dataStaffParam = _param;
		gameObject.GetComponent<Button> ().onClick.AddListener (ButtonPushed);

		MasterStaffParam masterParam = DataManager.Instance.masterStaff.Get (_param.staff_id);

		m_textName.text = masterParam.name;
		m_ctrlRareStarts.Initialize (masterParam.rarity);

		UpdateRole (_param.role);
		_param.UpdateRole.AddListener (UpdateRole);

		UpdateTrainingType (_param.training_type);
		_param.UpdateTrainingType.AddListener (UpdateTrainingType);

		m_imgChara.sprite = SpriteManager.Instance.SpriteCreate ( string.Format("texture/chara/staff_{0:D4}.png" , _param.staff_id ) , new Rect (0.0f, 98.0f, 64.0f, 98.0f), new Vector2(0.0f,0.0f));

		if (5 <= masterParam.rarity) {
			m_imgBack.sprite = SpriteManager.Instance.LoadSprite ("texture/staff/staff_banner03");
		} else if (3 <= masterParam.rarity) {
			m_imgBack.sprite = SpriteManager.Instance.LoadSprite ("texture/staff/staff_banner02");
		} else {
			m_imgBack.sprite = SpriteManager.Instance.LoadSprite ("texture/staff/staff_banner01");
		}
		m_slExp.maxValue = MasterStaff.GetNextExp (_param.level);
		m_slExp.value = _param.exp;

		m_textLevel.text = _param.level.ToString ();

		m_paramManner.Set (StaffParam.TYPE.MANNER, _param);
		m_paramFootwork.Set (StaffParam.TYPE.FOOTWORK, _param);
		m_paramCook.Set (StaffParam.TYPE.COOK, _param);

		m_paramKeihi.SetNum (DataManager.USER_PARAM.COIN, masterParam.pay);

	}

}




