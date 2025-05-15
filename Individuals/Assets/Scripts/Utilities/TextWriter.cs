using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    private int currentChar;

    public bool _isActive;

    public IEnumerator TypeText(TextMeshProUGUI textArea, string textString, float textSpeed, AudioClip textSound, int audioFreq, float minPitch, float maxPitch)
    {
        textArea.text = "";
        
        while (_isActive)
        {
            foreach (char letter in textString)
            {
                if (textSound != null)
                {
                    currentChar ++;
                    PlayDialogueSound(textSound, audioFreq, minPitch, maxPitch);
                }
                
                textArea.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }

            break;
        }
        
    }

    private void PlayDialogueSound(AudioClip textSound, int audioFreq, float minPitch, float maxPitch)
    {
        if (currentChar % audioFreq == 0)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(textSound);            
        }
    }
}
