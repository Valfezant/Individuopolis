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
    public float entityTextSpeed;

    public AudioClip speakSound;

    [SerializeField] [Range(1, 20)] public int soundFrequency;

    [SerializeField] [Range(0, 3)] public float soundMinPitch;
    [SerializeField] [Range(0, 3)] public float soundMaxPitch;
}
