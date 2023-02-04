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



    private List<string> enemyTypes;
    private PlayerInfo player;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    public List<string> getEnemyTypes()
	{
        return enemyTypes;
	}
}
