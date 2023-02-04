using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<string> enemyTypes;


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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public List<string> getEnemyTypes()
	{
        return enemyTypes;
	}
}
