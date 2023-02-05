using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleDialogue : MonoBehaviour
{
    private BattleSystem bsMan;

    public TextMeshProUGUI dialogue;
    public Button attackButton;
    public Button dodgeButton;
    public Button guardButton;
    public Button equipButton;
    public Button waitButton;
    public Button fleeButton;
    public Button nextButton;

    private bool isInteract = true;

    public void SetIsInteract(bool interactable)
	{
        attackButton.gameObject.SetActive(interactable);
        dodgeButton.gameObject.SetActive(interactable);
        guardButton.gameObject.SetActive(interactable);
        equipButton.gameObject.SetActive(interactable);
        waitButton.gameObject.SetActive(interactable);
        fleeButton.gameObject.SetActive(interactable);
        nextButton.gameObject.SetActive(!interactable);
        isInteract = interactable;
    }

    //Button Actions
    private void TrigAttack()
	{
        bsMan.Attack(0, -1);
	}
    private void TrigDodge()
	{
        bsMan.Dodge(0);
	}
    private void TrigGuard()
	{
        bsMan.Guard(0);
	}
    private void TrigEquip()
	{
        bsMan.Equip();
	}
    private void TrigWait()
	{
        bsMan.Wait(0);
	}
    private void TrigFlee()
	{
        bsMan.Flee();
	}
    private void TrigNext()
	{
        bsMan.Progress();
	}

    //String Generators
    public void AttackText(Unit actor, Unit target, int damage)
	{
        string display = DataManager.Translate("ActionAttemptDisplay");
        string attemptString = DataManager.Translate("AttackDisplay");
        WeaponData weapon = DataManager.FetchWeaponData(actor.weapon);
        if (attemptString != "")
		{
            Dictionary<string, string> replaceKeys = new Dictionary<string, string>();
            replaceKeys.Add("{name}", actor.UnitName);
            replaceKeys.Add("{attackDesc}", DataManager.Translate(weapon.AttackDesc));
            replaceKeys.Add("{targetName}", target.UnitName);
            replaceKeys.Add("{weaponDesc}", DataManager.Translate(weapon.EnemyDesc));
            attemptString = DataManager.TextKeyReplacer(attemptString, replaceKeys);
		}
        string resultString = "";
        if (damage > 0)
        {
            resultString = DataManager.Translate("DamageDesc");
            Dictionary<string, string> replacements = new Dictionary<string, string>();
            replacements.Add("{name}", target.UnitName);
            replacements.Add("{damage}", damage.ToString());
            resultString = DataManager.TextKeyReplacer(resultString, replacements);
        }
		else
		{
            resultString = DataManager.Translate("MissDesc");
		}

        if (display != "") {
            Dictionary<string, string> displayReplace = new Dictionary<string, string>();
            displayReplace.Add("{ActionString}", attemptString);
            displayReplace.Add("{ResultString}", resultString);
            display = DataManager.TextKeyReplacer(display, displayReplace);
        }
        dialogue.SetText(display);
        SetIsInteract(false);

    }
    public void HoverText(Unit hover)
	{

	}
    public void DodgeText(Unit actor)
	{

	}
    public void GuardText(Unit actor)
	{

	}
    public void WaitText(Unit actor, Unit Target)
	{

	}
    public void FleeText(Unit actor)
	{

	}
    

    // Start is called before the first frame update
    void Start()
    {
        bsMan = GameObject.FindAnyObjectByType<BattleSystem>();
        
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(TrigAttack);
        }
        if (dodgeButton != null)
        {
            dodgeButton.onClick.AddListener(TrigDodge);
        }
        if (guardButton != null)
        {
            guardButton.onClick.AddListener(TrigGuard);
        }
        if (equipButton != null)
        {
            equipButton.onClick.AddListener(TrigEquip);
        }
        if (waitButton != null)
		{
            waitButton.onClick.AddListener(TrigWait);
		}
        if (fleeButton != null)
		{
            fleeButton.onClick.AddListener(TrigFlee);
		}
        if (nextButton != null)
		{
            nextButton.onClick.AddListener(TrigNext);
		}
    }
}
