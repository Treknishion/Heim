using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
	public TextMeshProUGUI Name;
	public TextMeshProUGUI Health;
	public TextMeshProUGUI AP;

	public GameObject GuardS;
	public GameObject DodgeS;

	public void UpdateHUD(Unit entityData)
	{
		Name.SetText(entityData.UnitName);
		Health.SetText(entityData.CurrHealth + " \\ " + entityData.MaxHealth);
		AP.SetText(entityData.CurrAP + " \\ " + entityData.MaxAP);
		GuardS.SetActive(entityData.Guarding);
		DodgeS.SetActive(entityData.Dodging);
	}

}
