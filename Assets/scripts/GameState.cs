using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{
    private static GameState instance = Instance;

    private GameState() { }


    public Transform transform;
    private int money;
    private bool talkedToBum;
    private int energy;
    private float xLoc;
    private float yLoc;

    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameState();
            }
            return instance;
        }
    }

    public void saveState()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("energy", energy);
    }

    public void loadState()
    {
        money = PlayerPrefs.GetInt("money");
    }

    public void setTalkedToBum(bool talked)
    {
        Debug.Log("Talked to bum");
        talkedToBum = talked;
    }
}
