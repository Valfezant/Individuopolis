using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluator : MonoBehaviour
{
    [Header("Spare stats counter")]
    public int stat_makesNoise;
    public int stat_hasEyes;
    public int stat_hasLegs;
    public int stat_isFleshy;
    public int stat_wearsClothes;
    public int stat_looksNice;
    public int stat_isSmall;
    public int stat_feelsCool;
    public int stat_canBeEaten;


    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CountEntity(Entity currentEntity)
    {
        if (currentEntity.__makesNoise)
        {
            stat_makesNoise ++;
        }

        if (currentEntity.__hasEyes)
        {
            stat_hasEyes ++;
        }

        if (currentEntity.__hasLegs)
        {
            stat_hasLegs ++;
        }

        if (currentEntity.__isFleshy)
        {
            stat_isFleshy ++;
        }

        if (currentEntity.__wearsClothes)
        {
            stat_wearsClothes ++;
        }

        if (currentEntity.__looksNice)
        {
            stat_looksNice ++;
        }

        if (currentEntity.__isSmall)
        {
            stat_isSmall ++;
        }

        if (currentEntity.__feelsCool)
        {
            stat_feelsCool ++;
        }

        if (currentEntity.__canBeEaten)
        {
            stat_canBeEaten ++;
        }
    }
}
