using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
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

    public TextAsset weaponFile;
    public TextAsset shieldFile;
    public TextAsset enemyFile;
    public TextAsset armorFile;
    public TextAsset translationFile;

    private SortedDictionary<string, WeaponData> weapons;
    private SortedDictionary<string, ShieldData> shields;
    private SortedDictionary<string, EnemyData> enemies;
    private SortedDictionary<string, ArmorData> armors;
    private SortedDictionary<string, string> translations;



    // Start is called before the first frame update
    void Start()
    {
        readWeapons();
        readShields();
        readEnemies();
        readArmors();
        readTranslations();
    }
    //Collect and Store Data from csvs/tsvs
    private void readWeapons()
	{
        List<string> records = new List<string>(weaponFile.text.Split('\n'));
        records.RemoveAt(0);
        foreach (string record in records)
		{
            string[] fields = record.Split(',');
            WeaponData weapon = new WeaponData(fields[0], fields[1], fields[2], fields[3], fields[4], int.Parse(fields[5]), int.Parse(fields[6]), fields[7] == "Y");
            weapons.Add(fields[0], weapon);
		}
	}
    private void readShields()
    {
        List<string> records = new List<string>(shieldFile.text.Split('\n'));
        records.RemoveAt(0);
        foreach (string record in records)
        {
            string[] fields = record.Split(',');
            ShieldData shield = new ShieldData(fields[0], fields[1], fields[2], fields[3], int.Parse(fields[4]), int.Parse(fields[5]), fields[6] == "Y");
            shields.Add(fields[0], shield);
        }
    }
    private void readEnemies()
	{
        List<string> records = new List<string>(enemyFile.text.Split('\n'));
        records.RemoveAt(0);
        foreach ( string record in records)
		{
            string[] fields = record.Split(',');
            EnemyData enemy = new EnemyData(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], fields[7], fields[8], fields[9], fields[10], fields[11], fields[12], fields[13], fields[14]);
            enemies.Add(fields[0], enemy);
		}
	}
    private void readArmors()
	{
        List<string> records = new List<string>(armorFile.text.Split('\n'));
        records.RemoveAt(0);
        foreach (string record in records)
        {
            string[] fields = record.Split(',');
            ArmorData armor = new ArmorData(fields[0], fields[1], fields[2], fields[3], int.Parse(fields[4]), int.Parse(fields[5]), fields[6] == "Y");
            armors.Add(fields[0], armor);
        }
    }
    private void readTranslations()
	{
        List<string> records = new List<string>(translationFile.text.Split('\n'));
        foreach (string record in records)
		{
            string[] fields = record.Split('\t');
            translations.Add(fields[0], fields[1]);
		}
	}

    //fetch data
    public WeaponData FetchWeaponData(string ID)
    {
        return weapons[ID];
    }
    public ShieldData FetchShieldData(string ID)
    {
        return shields[ID];
    }
    public EnemyData FetchEnemyData(string ID)
    {
        return enemies[ID];
    }
    public ArmorData FetchArmorData(string ID)
    {
        return armors[ID];
    }
    public string Translate(string ID)
    {
        return translations[ID];
    }

    public void SaveData(string saveName)
	{

	}

    public void LoadData(string saveName)
	{

	}
}
