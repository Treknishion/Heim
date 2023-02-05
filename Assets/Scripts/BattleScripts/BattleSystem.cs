using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public List<string> enemyTypes;

	public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public BattleHUD PHUD;
    public BattleHUD EHUD;
    public BattleDialogue bDialogue;
    public GameObject Roulette;

    Unit playerUnit;
    Dictionary<int,Unit> entities = new Dictionary<int, Unit>();

    int curActor;

    public BattleState state;
    private int selectedEntity = 1;
    private int displayEntity = 1;

    // Start is called before the first frame update
    void Start()
    {

        state = BattleState.START;
        SetupBattle();
        UpdateHUDs();
        curActor = GetActor(false, false);
        StartTurn();
        Cursor.visible = true;
    }


    //Battle Management
    public void TriggerWin()
	{

	}
    public void TriggerLose()
	{

	}
    private void SetupBattle()
	{
        GameObject playerGO = Instantiate(playerPrefab);
        playerGO.GetComponent<Unit>().InitializeAsPlayer();
        entities.Add(0, playerGO.GetComponent<Unit>());

        int enemyCount = 0;
        if (enemyTypes.Count == 0)
		{
            enemyTypes = GameManager.getEnemyTypes();
		}

        if (enemyTypes.Count != 0)
		{
            foreach (string type in enemyTypes)
			{
                enemyCount += 1;
                GameObject enemyGO = Instantiate(enemyPrefab, Roulette.transform);
                Unit enemy = enemyGO.GetComponent<Unit>();
                enemy.InitializeFromBP(type, enemyCount);
                entities.Add(enemyCount, enemy);
			}
		}
		else
		{

		}
    }

    public int GetActor(bool secondHigh, bool resetAfter)
	{
        int curHigh = -15;
        int secHigh = -15;
        int highEntity = 0;
        int secEntity = 0;
        
        foreach (KeyValuePair<int, Unit> entry in entities)
		{
            Unit unit = entry.Value;
            if (unit.CurrAP > curHigh)
			{
                secHigh = curHigh;
                secEntity = highEntity;
                curHigh = unit.CurrAP;
                highEntity = entry.Key;
			}
            else if (unit.CurrAP > secHigh){
                secHigh = unit.CurrAP;
                secEntity = entry.Key;
			}
		}

        if (curHigh <= 0 && resetAfter)
		{
            RefreshRound();
            return GetActor(false, true);
		}
        else if (secondHigh)
		{
            return secEntity;
		}
		else
		{
            return highEntity;
		}
	}
    public void RefreshRound()
	{
        foreach (KeyValuePair<int, Unit> entry in entities)
		{
            Unit unit = entry.Value;
            unit.ModAP(unit.MaxAP);
		}
	}

    private void StartTurn()
    {
        if (state == BattleState.WON)
		{
            TriggerWin();
		}
        else if (state == BattleState.LOST)
		{
            TriggerLose();
		}
        else if (curActor == 0)
		{
            bDialogue.SetIsInteract(true);
            foreach (KeyValuePair<int,Unit> entry in entities)
			{
                if (entry.Value.enemyButton != null && entry.Value.CurrHealth > 0)
				{
                    entry.Value.enemyButton.interactable = true;
				}
			}
		}
		else
		{
            bDialogue.SetIsInteract(false);
            foreach (KeyValuePair<int, Unit> entry in entities)
            {
                if (entry.Value.enemyButton != null)
                {
                    entry.Value.enemyButton.interactable = false;
                }
            }
            entities[curActor].TakeAITurn();
        }
    }


    public void UpdateHUDs()
    {
        PHUD.UpdateHUD(entities[0]);
        EHUD.UpdateHUD(entities[displayEntity]);
    }

    public void DisplayString(string display)
	{

	}

    //selection and hovering
    public void SelectEnemy(int unit)
	{
        selectedEntity = unit;
        displayEntity = selectedEntity;
        UpdateHUDs();
	}

    public void HoverEnemy(int unit)
	{
        displayEntity = unit;
        UpdateHUDs();
        bDialogue.HoverText(entities[unit]);
	}

    public void UnHoverEnemy()
	{
        displayEntity = selectedEntity;
        UpdateHUDs();
        bDialogue.DisplayLastString();
	}

    public int GetSelected()
	{
        return selectedEntity;
	}

    //CombatActions
    public void Attack(int a, int b)
	{
        if (b == -1)
		{
            b = selectedEntity;
		}

        int damage = 0;
        Unit actor = entities[a];
        Unit target = entities[b];
        WeaponData weapon = DataManager.FetchWeaponData(actor.weapon);
        ShieldData shield = DataManager.FetchShieldData(target.shield);
        ArmorData armor = DataManager.FetchArmorData(target.armor);
        actor.CurrAP -= weapon.APCost;
        int numSuccesses = 0;
        for (int i = 0; i < actor.Strength; i++)
		{
            int roll = Random.Range(1, 10);
            if (roll == 10)
			{
                numSuccesses += 2;
			}
            else if(roll > 5)
			{
                numSuccesses++;
			}
		}

        int targetNum = armor.Rating;
        if (target.Dodging == true)
		{
            targetNum++;
		}

        if (numSuccesses > targetNum)
		{
            damage = (numSuccesses - targetNum) * weapon.Dam;
            if (target.Guarding)
			{
                damage -= shield.BlockPow;
			}
            if (damage > 0)
			{
                if (damage >= target.CurrHealth)
				{
                    target.ModHealth(-target.CurrHealth);
                    target.Kill();
                    if (b != 0)
                    {
                        entities.Remove(target.entityNum);
                        if (entities.Count <= 1)
                        {
                            TriggerWin();
                        }
                        else
                        {
                            foreach (KeyValuePair<int, Unit> pair in entities)
                            {
                                if (pair.Key != 0)
                                {
                                    selectedEntity = pair.Key;
                                    displayEntity = selectedEntity;
                                    break;
                                }
                            }
                        }
                    }
				}
				else
				{
                    target.ModHealth(-damage);
                    target.SetGuard(false);
                    target.SetDodge(false);
				}
			}
		}
        bDialogue.AttackText(actor, target, damage);
        UpdateHUDs();
	}

    public void Dodge(int a)
	{
        Unit actor = entities[a];
        actor.SetDodge(true);
        ArmorData armor = DataManager.FetchArmorData(actor.armor);
        actor.CurrAP -= armor.FleeCost;
        bDialogue.DodgeText(actor);
        UpdateHUDs();
	}

    public void Guard(int a)
	{
        Unit actor = entities[a];
        actor.SetGuard(true);
        ShieldData shield = DataManager.FetchShieldData(actor.shield);
        actor.CurrAP -= shield.APCost;
        bDialogue.GuardText(actor);
        UpdateHUDs();
    }
    public void Flee()
	{

	}
    public void Equip()
	{

	}
    public void Wait(int a)
	{
        int nextEnt = GetActor(true, false);
        int modAmount = entities[nextEnt].CurrAP - entities[a].CurrAP - 1;
        entities[a].ModAP(modAmount);
	}
    public void Progress()
	{
        curActor = GetActor(false, true);
        StartTurn();
	}

}
