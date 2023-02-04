using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldData : MonoBehaviour
{
    public string ID;
    public string Desc;
    public string Art;
    public string EnemyDesc;
    public int APCost;
    public int BlockPow;
    public bool Loot;

    public ShieldData(string id, string desc, string art, string ene, int ap, int bp, bool loot)
    {
        ID = id;
        Desc = desc;
        Art = art;
        EnemyDesc = ene;
        APCost = ap;
        BlockPow = bp;
        Loot = loot;
    }
}
