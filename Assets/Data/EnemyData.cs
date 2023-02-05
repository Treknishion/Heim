using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    public string ID;
    public string Desc;
    public string Art;
    public int MaxHealth;
    public int MinHealth;
    public int MaxAP;
    public int MinAP;
    public int MaxStrength;
    public int MinStrength;
    public int MaxEndur;
    public int MinEndur;
    public List<string> Weapons;
    public List<string> Armors;
    public List<string> Shields;
    public int Rank;

    public EnemyData(string id, string desc, string art, string maxh, string minh, string maxap, string minap, string maxs, string mins, string maxe, string mine, string weapons, string armors, string shields, string rank)
	{
        ID = id;
        Desc = desc;
        Art = art;
        MaxHealth = int.Parse(maxh);
        MinHealth = int.Parse(minh);
        MaxAP = int.Parse(maxap);
        MinAP = int.Parse(minap);
        MaxStrength = int.Parse(maxs);
        MinStrength = int.Parse(mins);
        MaxEndur = int.Parse(maxe);
        MinEndur = int.Parse(mine);
        Weapons = new List<string>(weapons.Split(';'));
        Armors = new List<string>(armors.Split(';'));
        Shields = new List<string>(shields.Split(';'));
        Rank = int.Parse(rank);
	}
}
