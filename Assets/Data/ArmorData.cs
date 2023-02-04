using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorData : MonoBehaviour
{
    public string ID;
    public string Desc;
    public string Art;
    public string EnemyDesc;
    public int Rating;
    public int FleeCost;
    public bool Loot;

    public ArmorData(string id, string desc, string art, string ene, int rat, int fc, bool loot)
    {
        ID = id;
        Desc = desc;
        Art = art;
        EnemyDesc = ene;
        Rating = rat;
        FleeCost = fc;
        Loot = loot;
    }
}
