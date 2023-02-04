using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private BattleSystem bsManager;
    private DataManager dManager;
    private GameManager gManager;

    public Button enemyButton;
    public int entityNum;

    public string UnitName;

    public int MaxHealth;
    public int CurrHealth;

    public int CurrAP;
    public int MaxAP;

    public int CurrEndur;
    public int MaxEndur;

    public int Strength;


    public string weapon;
    public string shield;
    public string armor;

    public bool Guarding = false;
    public bool Dodging = false;
    
    //Set all data values from blueprint
    public void InitializeFromBP(string bp, int eNum)
	{
        EnemyData data = dManager.FetchEnemyData(bp);
        UnitName = dManager.Translate(data.ID);
        MaxHealth = Random.Range(data.MinHealth, data.MaxHealth);
        MaxAP = Random.Range(data.MinAP, data.MaxAP);
        MaxEndur = Random.Range(data.MinEndur, data.MaxEndur);
        Strength = Random.Range(data.MinStrength, data.MaxStrength);
        weapon = data.Weapons[Random.Range(0, data.Weapons.Count - 1)];
        armor = data.Armors[Random.Range(0, data.Armors.Count - 1)];
        shield = data.Shields[Random.Range(0, data.Shields.Count - 1)];
        entityNum = eNum;
        ResetStats();
	}

    public void InitializeAsPlayer()
	{

        entityNum = 0;
	}

    public void ChangeName(string name)
	{
        UnitName = name;
	}
    public void ResetHealth()
	{
        CurrHealth = MaxHealth;
	}
    public void ModHealth(int change)
	{
        CurrHealth += change;
	}
    public void ResetAP()
	{
        CurrAP = MaxAP;
	}
    public void ModAP(int change)
	{
        CurrAP += change;
	}
    public void ResetEndur()
	{
        CurrEndur = MaxEndur;
	}
    public void ModEndur(int change)
	{
        CurrEndur += change;
	}

    public void SetDodge(bool val)
	{
        Dodging = val;
	}
    public void SetGuard(bool val)
	{
        Guarding = val;
	}

    //Button Events
    public void EnemyClick()
	{
        bsManager.SelectEnemy(entityNum);
	}

	public void OnMouseEnter()
	{
        bsManager.HoverEnemy(entityNum);
	}

	public void OnMouseExit()
	{
        bsManager.UnHoverEnemy();
	}

	//Turn Logic
    public void TakeAITurn()
	{
        // if can't do anything, wait
        if (CurrAP <= 0) {
            bsManager.Wait(entityNum);
            return;
        }

        // if low on health, try guarding or dodging
        int dieRoll = Random.Range(1, 11);  // 1d10
        if (dieRoll > CurrHealth)
        {
            dieRoll = Random.Range(1, 7); // 1d6
            if (dieRoll > CurrHealth)
            {
                bsManager.Dodge(entityNum);
            }
            else
            {
                bsManager.Guard(entityNum);
            }
            return;
        }

        // now choose between guarding and dodging, based on whichever's cheap
        shieldData = dManager.FetchShieldData(shield);
        armorData = dManager.FetchArmorData(armor);
        weaponData = dManager.FetchWeaponData(weapon);

        bool dodgeDefend;
        int defendCost;
        if (armorData.APCost <= shieldData.APCost)
        {
            dodgeDefend = true;
            defendCost = armorData.APCost;
        }
        else
        {
            dodgeDefend = false;
            defendCost = shieldData.APCost;
        }

        // if can attack without going into deficit, do so
        if (CurrAP >= weaponData.APCost + defendCost)
        {
            bsManager.Attack(entityNum, 0); // 0 for Player
            return;
        }

        // TODO: Swapping to other equipment

        // if can afford an extra defend before final attack, defend
        if (CurrAP > defendCost)
        {
            if (dodgeDefend)
            {
                bsManager.Dodge(entityNum);
            }
            else
            {
                bsManager.Guard(entityNum);
            }
            return;
        }

        // by default, attack
        bsManager.Attack(entityNum, 0); // 0 for Player
	}


	public void ResetStats()
	{
        ResetHealth();
        ResetAP();
        ResetEndur();
	}

    public void Kill()
	{
        if (entityNum == 0)
		{
            bsManager.TriggerLose();
		}
		else
		{
            enemyButton.interactable = false;
		}
	}

	private void Start()
	{
        bsManager = BattleSystem.instance;
        dManager = DataManager.instance;
        gManager = GameManager.instance;
        if (enemyButton != null)
		{
            enemyButton.interactable = true;
            enemyButton.onClick.AddListener(EnemyClick);
		}
    }
}
