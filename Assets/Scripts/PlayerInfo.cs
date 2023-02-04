using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public string Name;
    public string ProfileImage;

    public int CurrHealth;
    public int MaxHealth;
    public int CurrEndur;
    public int MaxEndur;

    public int MaxAP;
    public int Strength;

    public string EquippedWeapon;
    public string EquippedArmor;
    public string EquippedShield;

    public List<string> carriedWeapons;
    public List<string> storedWeapons;
    public List<string> storedArmors;
    public List<string> storedShields;

    //SetPlayerData
    public void SetName(string name)
	{
        Name = name;
	}
    public void SetProfile(string image)
	{
        ProfileImage = image;
	}
    public void SetCurrHealth(int currHealth)
	{
        CurrHealth = currHealth;
	}
    public void SetMaxHealth(int maxHealth)
	{
        MaxHealth = maxHealth;
	}
    public void SetCurrEndur(int currEndur)
	{
        CurrEndur = currEndur;
	}
    public void SetMaxEndur(int maxEndur)
	{
        MaxEndur = maxEndur;
	}
    public void SetMaxAP(int ap)
	{
        MaxAP = ap;
	}
    public void SetStrength(int st)
	{
        Strength = st;
	}

    //Private Weapon Changes
    private bool AddToCarriedW(string weap)
	{
        if (!carriedWeapons.Contains(weap))
		{
            carriedWeapons.Add(weap);
            return true;
		}
		else
		{
            return false;
		}
	}
    private bool RemoveFromCarriedW(string weap)
	{
        if (carriedWeapons.Contains(weap))
		{
            carriedWeapons.Remove(weap);
            return true;
		}
		else
		{
            return false;
		}
	}
    private bool AddToStoredW(string weap)
	{
        if (!storedWeapons.Contains(weap))
		{
            storedWeapons.Add(weap);
            return true;
		}
		else
		{
            return false;
		}
	}
    private bool RemoveFromStoredW(string weap)
	{
        if (storedWeapons.Contains(weap))
		{
            storedWeapons.Remove(weap);
            return true;
		}
		else
		{
            return false;
		}
	}
    private string SetEquippedW(string weap)
	{
        string unequip = EquippedWeapon;
        EquippedWeapon = weap;
        return unequip;
	}
    private string RemoveEquippedW()
	{
        string unequip = EquippedWeapon;
        EquippedWeapon = "None";
        return unequip;
	}
    private bool AddToStoredA(string arm)
    {
        if (!storedArmors.Contains(arm))
        {
            storedArmors.Add(arm);
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool RemoveFromStoredA(string arm)
    {
        if (storedArmors.Contains(arm))
        {
            storedArmors.Remove(arm);
            return true;
        }
        else
        {
            return false;
        }
    }
    private string SetEquippedA(string arm)
    {
        string unequip = EquippedArmor;
        EquippedArmor = arm;
        return unequip;
    }
    private string RemoveEquippedA()
    {
        string unequip = EquippedArmor;
        EquippedArmor = "None";
        return unequip;
    }
    private bool AddToStoredS(string shi)
    {
        if (!storedShields.Contains(shi))
        {
            storedShields.Add(shi);
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool RemoveFromStoredS(string shi)
    {
        if (storedShields.Contains(shi))
        {
            storedShields.Remove(shi);
            return true;
        }
        else
        {
            return false;
        }
    }
    private string SetEquippedS(string shi)
    {
        string unequip = EquippedShield;
        EquippedShield = shi;
        return unequip;
    }
    private string RemoveEquippedS()
    {
        string unequip = EquippedShield;
        EquippedShield = "None";
        return unequip;
    }

    //Swapping Around Weapons
    public void EquipWToCar()
	{
        string weap = RemoveEquippedW();
        if (weap != "None")
        {
            AddToCarriedW(weap);
        }
	}
    public void EquipWToSto()
	{
        string weap = RemoveEquippedW();
        if (weap != "None")
        {
            AddToStoredW(weap);
        }
	}
    public void StoWToEquip(string weap)
	{
        bool inSto = RemoveFromStoredW(weap);
        if (inSto)
		{
            EquipWToSto();
            SetEquippedW(weap);
		}
	}
    public void CarWToEquip(string weap)
	{
        bool inSto = RemoveFromCarriedW(weap);
        if (inSto)
        {
            EquipWToCar();
            SetEquippedW(weap);
        }
    }

    //Swapping Armor
    public void EquipAToSto()
	{
        string arm = RemoveEquippedA();
        if (arm != "None")
		{
            AddToStoredA(arm);
		}
	}
    public void StoAToEquip(string arm)
	{
        bool inSto = RemoveFromStoredA(arm);
        if (inSto)
		{
            EquipAToSto();
            SetEquippedA(arm);
		}
	}
    //Swapping Shields
    public void EquipSToSto()
    {
        string shi = RemoveEquippedS();
        if (shi != "None")
        {
            AddToStoredS(shi);
        }
    }
    public void StoSToEquip(string shi)
    {
        bool inSto = RemoveFromStoredS(shi);
        if (inSto)
        {
            EquipSToSto();
            SetEquippedS(shi);
        }
    }
}
