using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BattleSystem bsManager;

    public Button enemyButton;
    public Image enemyImage;
    public int entityNum;

    public string UnitName;
    public string blueprint;

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
        EnemyData data = DataManager.FetchEnemyData(bp);
        blueprint = bp;
        UnitName = DataManager.Translate(data.ID);
        MaxHealth = Random.Range(data.MinHealth, data.MaxHealth);
        MaxAP = Random.Range(data.MinAP, data.MaxAP);
        MaxEndur = Random.Range(data.MinEndur, data.MaxEndur);
        Strength = Random.Range(data.MinStrength, data.MaxStrength);
        weapon = data.Weapons[Random.Range(0, data.Weapons.Count - 1)];
        armor = data.Armors[Random.Range(0, data.Armors.Count - 1)];
        shield = data.Shields[Random.Range(0, data.Shields.Count - 1)];
        string imagePath = "Art/EnemyArt/" + data.Art;
        enemyImage.sprite = Resources.Load<Sprite>(imagePath);

        entityNum = eNum;
        ResetStats();
	}

    public void InitializeAsPlayer()
	{
        PlayerInfo playerdata = GameManager.FetchPlayerData();
        UnitName = playerdata.PlayerName;
        MaxHealth = playerdata.MaxHealth;
        MaxAP = playerdata.MaxAP;
        MaxEndur = playerdata.MaxEndur;
        Strength = playerdata.Strength;
        weapon = playerdata.EquippedWeapon;
        armor = playerdata.EquippedArmor;
        shield = playerdata.EquippedShield;
        entityNum = 0;
        ResetStats();
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

	public void OnPointerEnter(PointerEventData eventData)
	{
        bsManager.HoverEnemy(entityNum);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        bsManager.UnHoverEnemy();
	}

	//Turn Logic
    public void TakeAITurn()
	{
        int choice = Random.Range(0, 2);
        if (choice == 0)
		{
            bsManager.Attack(entityNum, 0);
		}
		else if (choice == 1)
		{
            if (Dodging)
            {
                bsManager.Attack(entityNum, 0);
            }
            else
            {
                bsManager.Dodge(entityNum);
            }
		}
		else
		{
            if (Guarding)
            {
                bsManager.Attack(entityNum, 0);
            }
            else
            {
                bsManager.Guard(entityNum);
            }
		}

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
            Destroy(this.gameObject);
		}
	}

	private void Start()
	{
        bsManager = GameObject.FindAnyObjectByType<BattleSystem>();
        if (enemyButton != null)
		{
            enemyButton.interactable = true;
            enemyButton.onClick.AddListener(EnemyClick);
		}
    }
}
