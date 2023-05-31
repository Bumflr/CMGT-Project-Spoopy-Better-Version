using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public SerializableDictionary<string, bool> skillObjectsCollected;

    public List<Item> playerItems;

    //the values defined in this constructor will be the default values
    //the game starts when there's no data to load

    public GameData()
    {
        skillObjectsCollected = new SerializableDictionary<string, bool>();
        playerItems = new List<Item>();
    }

    public int GetPercentageComplete()
    {
        int totalCollected = 0;
        foreach(bool collected in skillObjectsCollected.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }

        int percentageCompleted = -1;
        if (skillObjectsCollected.Count != 0)
        {
            percentageCompleted = (totalCollected * 100 / skillObjectsCollected.Count);
        }
        return percentageCompleted;
    }
}
