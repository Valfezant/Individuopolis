using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEvaluator : MonoBehaviour
{
    [Header("Person stats counter")]
    public int stat_noise_yes;
    public int stat_noise_no;

    public int stat_eyes_yes;
    public int stat_eyes_no;
    public int stat_canSee;

    public int stat_legs_yes;
    public int stat_legs_no;
    public int stat_twoLegs;

    public int stat_clothes_yes;
    public int stat_clothes_no;

    public int stat_looksNice_yes;
    public int stat_looksNice_no;
    
    public int stat_cold;
    public int stat_warm;

    public int stat_edible_yes;
    public int stat_edible_no;

    public int stat_brown;
    public int stat_blue;

    public int stat_smooth_yes;
    public int stat_smooth_no;
    public int stat_hair;

    public int stat_fourLimbs;

    public int stat_electric;

    public int stat_useful;

    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerPrefs.SetInt("TotalHarvest", 0);
        
        //ClearStats();
    }

    public void ClearStats()
    {
        stat_noise_yes = 0;
        stat_noise_no = 0;

        stat_eyes_yes = 0;
        stat_eyes_no = 0;
        stat_canSee = 0;

        stat_legs_yes = 0;
        stat_legs_no = 0;
        stat_twoLegs = 0;

        stat_clothes_yes = 0;
        stat_clothes_no = 0;

        stat_looksNice_yes = 0;
        stat_looksNice_no = 0;

        stat_cold = 0;
        stat_warm = 0;

        stat_edible_yes = 0;
        stat_edible_no = 0;

        stat_brown = 0;
        stat_blue = 0;

        stat_smooth_yes = 0;
        stat_smooth_no = 0;
        stat_hair = 0;

        stat_fourLimbs = 0;
        stat_electric = 0;
        stat_useful = 0;
    }

    public void CountEntity(Entity currentEntity)
    {
        //noise
        if (currentEntity.__makesNoise)
        {
            stat_noise_yes ++;
        }
        else
        {
            stat_noise_no ++;
        }

        //sight
        if (currentEntity.__hasEyes)
        {
            stat_eyes_yes ++;
        }
        else
        {
            stat_eyes_no ++;
        }

        if (currentEntity.__canSee)
        {
            stat_canSee ++;
        }

        //legs
        if (currentEntity.__hasLegs)
        {
            stat_legs_yes ++;
        }
        else
        {
            stat_legs_no ++;
        }

        if (currentEntity.__twoLegs)
        {
            stat_twoLegs ++;
        }

        //clothes
        if (currentEntity.__wearsClothes)
        {
            stat_clothes_yes ++;
        }
        else
        {
            stat_clothes_no ++;
        }

        //looks
        if (currentEntity.__looksNice)
        {
            stat_looksNice_yes ++;
        }
        else
        {
            stat_looksNice_no ++;
        }
        
        ///temp
        if (currentEntity.__feelsCold)
        {
            stat_cold ++;
        }
        else
        {
            stat_warm ++;
        }

        //edible
        if (currentEntity.__isEdible)
        {
            stat_edible_yes ++;
        }
        else
        {
            stat_edible_no ++;
        }

        //color
        if (currentEntity.__isBrown)
        {
            stat_brown ++;
        }

        if (currentEntity.__isBlue)
        {
            stat_blue ++;
        }

        //texture
        if (currentEntity.__isSmooth)
        {
            stat_smooth_yes ++;
        }
        else
        {
            stat_smooth_no ++;
        }

        if (currentEntity.__hasHair)
        {
            stat_hair ++;
        }

        //gen
        if (currentEntity.__fourLimbs)
        {
            stat_fourLimbs ++;
        }

        if (currentEntity.__usesElectricity)
        {
            stat_electric ++;
        }

        if (currentEntity.__hasUse)
        {
            stat_useful ++;
        }
    }

    ///traccia tutte le creature passate
    /// poi fai conteggio
}
