using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton stuff
    public static GameManager instance;
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



    private static List<string> enemyTypes = new List<string>();
    private static PlayerInfo player = new PlayerInfo();




    // Start is called before the first frame update
    void Start()
    {
     
    }

    public static List<string> getEnemyTypes()
	{
        return enemyTypes;
	}
    public static PlayerInfo FetchPlayerData()
	{
        return player;
	}
}
