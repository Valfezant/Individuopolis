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

    public AudioClip killSound;

    [Header("Text")]
    public int entityFontSize;
    
    public float entityTextSpeed;

    public AudioClip speakSound;

    [Range(1, 20)] public int soundFrequency;

    [Range(0, 3)] public float soundMinPitch;
    [Range(0, 3)] public float soundMaxPitch;

    [Header("Stats")]
    public bool __makesNoise;
    public bool __hasEyes;
    public bool __hasLegs;
    //public bool __isFleshy;
    public bool __wearsClothes;
    public bool __looksNice;
    //public bool __isSmall;
    public bool __feelsCool;
    public bool __canBeEaten;
}
