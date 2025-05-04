using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public string entityName;

    public Sprite entitySprite;

    [TextArea(1,5)] public string[] introduction;

    [TextArea(1,5)] public string[] actionSpeakFeedback;
    
    [TextArea(1,5)] public string[] actionPokeFeedback;

    public AudioClip killSound;
}
