using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;
	private void Awake()
	{
		//if there's an instance, delete myself
        if (instance != null && instance != this)
		{
            Destroy(this);
		}
		else
		{
            instance = this;
		}
	}

    private GameManager gManager;
    private DataManager dManager;

    public List<string> enemyTypes;

	public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public BattleHUD PHUD;
    public BattleHUD EHUD;
    public BattleDialogue bDialogue;
    public GameObject Roulette;

    Unit playerUnit;
    Dictionary<int,Unit> entities;

    int curActor;

    public BattleState state;
    private int selectedEntity;
    private int displayEntity;

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameManager.instance;
        dManager = DataManager.instance;

        state = BattleState.START;
        SetupBattle();
        UpdateHUDs();
        curActor = GetActor(false, false);

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
            enemyTypes = gManager.getEnemyTypes();
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
            bDialogue.SetPlayerTurn(true);
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
            bDialogue.SetPlayerTurn(false);
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
	}

    public void UnHoverEnemy()
	{
        displayEntity = selectedEntity;
        UpdateHUDs();
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
        WeaponData weapon = dManager.FetchWeaponData(actor.weapon);
        ShieldData shield = dManager.FetchShieldData(target.shield);
        ArmorData armor = dManager.FetchArmorData(target.armor);
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
                if (damage > target.CurrHealth)
				{
                    target.ModHealth(-target.CurrHealth);
                    target.Kill();
				}
				else
				{
                    target.ModHealth(-damage);
				}
			}
		}
        UpdateHUDs();
	}

    public void Dodge(int a)
	{
        Unit actor = entities[a];
        actor.SetDodge(true);
        ArmorData armor = dManager.FetchArmorData(actor.armor);
        actor.CurrAP -= armor.FleeCost;
        UpdateHUDs();
	}

    public void Guard(int a)
	{
        Unit actor = entities[a];
        actor.SetGuard(true);
        ShieldData shield = dManager.FetchShieldData(actor.shield);
        actor.CurrAP -= shield.APCost;
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
