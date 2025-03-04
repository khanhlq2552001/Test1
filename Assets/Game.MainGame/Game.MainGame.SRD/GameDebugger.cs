using UnityEngine;
using System.ComponentModel;
using SRDebugger;

public partial class SROptions
{
    // Default Value for property
    private int levelUser = 0;

    // Options will be grouped by category
    [Category("LevelUser")]
    public int LevelUser
    {
        get
        {
            return levelUser;
        }
        set
        {
            levelUser = value;
            Debug.Log("LevelUser: " + levelUser);
        }
    }

    private int LevelGame = 0;

    [Category("Level Game")] // Options will be grouped by category
    public void NextLevel()
    {
        if(LevelGame < 10)
        {
            LevelGame++;
            Debug.Log("LevelGame: "+ LevelGame);
        }
        else
        {
            Debug.Log("Max Level");
        }
    }
}


