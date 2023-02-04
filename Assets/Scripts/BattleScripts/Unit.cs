using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName;

    public int MaxHealth;
    public int CurrHealth;

    public int CurrEndur;
    public int MaxEndur;

    public int CurrAP;
    public int MaxAP;

    public int Strength;

    public int AttackCost;
    public int Damage;

    public int AR;

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

    public void ResetAll()
	{
        ResetHealth();
        ResetAP();
        ResetEndur();
	}
}
