using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public string entityName;
    public Sprite entitySprite;

    [Header("Interactions")]
    [TextArea(1,5)] public string[] introduction;
    [TextArea(1,5)] public string[] actionSpeakFeedback;
    [TextArea(1,5)] public string[] actionPokeFeedback;
    [TextArea(1,5)] public string[] sparedDialogue;

    public AudioClip killSound;

    [Header("Text")]
    public int entityFontSize;
    public float entityTextSpeed;
    public AudioClip speakSound;

    [Range(1, 20)] public int soundFrequency;
    [Range(0, 3)] public float soundMinPitch;
    [Range(0, 3)] public float soundMaxPitch;


    /*[Header("Stats")]
    public bool __makesNoise;
    public bool __hasEyes;
    public bool __hasLegs;
    public bool __wearsClothes;
    public bool __looksNice;
    public bool __feelsCool;
    public bool __canBeEaten;*/


    [Header("New Stats")]
    public bool __makesNoise;//yes/no
    public bool __hasEyes;//eyesNo
    public bool __canSee;
    public bool __hasLegs;//legsNo
    public bool __twoLegs;
    public bool __wearsClothes;//clothesNo
    public bool __looksNice;//looksNiceNo
    public bool __feelsCold;//isWarm
    public bool __isEdible;//edibleNo
    public bool __isBrown;
    public bool __isBlue;
    public bool __isSmooth;//smoothNo
    public bool __hasHair;
    public bool __fourLimbs;
    public bool __usesElectricity;
    public bool __hasUse;
}