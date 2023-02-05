using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public string ID;
    public string Desc;
    public string Art;
    public string AttackDesc;
    public string EnemyDesc;
    public int APCost;
    public int Dam;
    public bool Loot;

    public WeaponData(string id, string desc, string art, string att, string ene, int ap, int dam, bool loot)
	{
        ID = id;
        Desc = desc;
        Art = art;
        AttackDesc = att;
        EnemyDesc = ene;
        APCost = ap;
        Dam = dam;
        Loot = loot;
	}
}
