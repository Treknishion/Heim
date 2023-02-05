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
        translations = new SortedDictionary<string, string>();
        armors = new SortedDictionary<string, ArmorData>();
        enemies = new SortedDictionary<string, EnemyData>();
        shields = new SortedDictionary<string, ShieldData>();
        weapons = new SortedDictionary<string, WeaponData>();
        readWeapons();
        readShields();
        readEnemies();
        readArmors();
        readTranslations();
    }

    public TextAsset weaponFile;
    public TextAsset shieldFile;
    public TextAsset enemyFile;
    public TextAsset armorFile;
    public TextAsset translationFile;

    private static SortedDictionary<string, WeaponData> weapons;
    private static SortedDictionary<string, ShieldData> shields;
    private static SortedDictionary<string, EnemyData> enemies;
    private static SortedDictionary<string, ArmorData> armors;
    private static SortedDictionary<string, string> translations;



    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    //Collect and Store Data from csvs/tsvs
    private void readWeapons()
	{
        List<string> records = new List<string>(weaponFile.text.Split('\n'));
        records.RemoveAt(0);
        foreach (string record in records)
		{
            string thisRecord = record.Trim('\r');
            string[] fields = thisRecord.Split(',');
            WeaponData weapon = new WeaponData(fields[0], fields[1], fields[2], fields[3], fields[4], int.Parse(fields[5]), int.Parse(fields[6]), fields[7].Contains("Y"));
            weapons.Add(fields[0], weapon);
		}
	}
    private void readShields()
    {
        List<string> records = new List<string>(shieldFile.text.Split('\n'));
        records.RemoveAt(0);
        foreach (string record in records)
        {
            string thisRecord = record.Trim('\r');
            string[] fields = thisRecord.Split(',');
            ShieldData shield = new ShieldData(fields[0], fields[1], fields[2], fields[3], int.Parse(fields[4]), int.Parse(fields[5]), fields[6].Contains("Y"));
            DataManager.shields.Add(fields[0], shield);
        }
    }
    private void readEnemies()
	{
        List<string> records = new List<string>(enemyFile.text.Split('\n'));
        records.RemoveAt(0);
        foreach ( string record in records)
		{
            string thisRecord = record.Trim('\r');
            string[] fields = thisRecord.Split(',');
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
            string thisRecord = record.Trim('\r');
            string[] fields = thisRecord.Split(',');
            ArmorData armor = new ArmorData(fields[0], fields[1], fields[2], fields[3], int.Parse(fields[4]), int.Parse(fields[5]), fields[6].Contains("Y"));
            armors.Add(fields[0], armor);
        }
    }
    private void readTranslations()
	{
        List<string> records = new List<string>(translationFile.text.Split('\n'));
        foreach (string record in records)
		{
            string thisRecord = record.Trim('\r');
            string[] fields = thisRecord.Split('\t');
            translations.Add(fields[0], fields[1]);
		}
	}

    //fetch data
    public static WeaponData FetchWeaponData(string ID)
    {
        return weapons[ID];
    }
    public static ShieldData FetchShieldData(string ID)
    {
        return shields[ID];
    }
    public static EnemyData FetchEnemyData(string ID)
    {
        return enemies[ID];
    }
    public static ArmorData FetchArmorData(string ID)
    {
        return armors[ID];
    }
    public static string TextKeyReplacer(string inputString, Dictionary<string,string> values)
	{
        foreach (KeyValuePair<string, string> entry in values)
		{
            inputString = inputString.Replace(entry.Key, entry.Value);
		}
        return inputString;
	}
    public static string Translate(string ID)
    {
        if (translations.ContainsKey(ID))
		{
            return translations[ID];
		}
		else
		{
            return "";
		}
    }

    public static void SaveData(string saveName)
	{

	}

    public static void LoadData(string saveName)
	{

	}
}
