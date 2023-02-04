using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleDialogue : MonoBehaviour
{
    private BattleSystem bsMan;
    private DataManager dMan;

    public TextMeshProUGUI dialogue;
    public Button attackButton;
    public Button dodgeButton;
    public Button guardButton;
    public Button equipButton;
    public Button waitButton;
    public Button fleeButton;
    public Button nextButton;

    private bool playerTurn = true;

    public void SetPlayerTurn(bool val)
	{
        attackButton.enabled = val;
        dodgeButton.enabled = val;
        guardButton.enabled = val;
        equipButton.enabled = val;
        waitButton.enabled = val;
        fleeButton.enabled = val;
        nextButton.enabled = !val;
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
        bsMan.Wait();
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
        bsMan = BattleSystem.instance;
        dMan = DataManager.instance;
        
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
