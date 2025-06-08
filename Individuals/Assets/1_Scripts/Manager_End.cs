using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_End : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private TextMeshProUGUI endText;

    [SerializeField] private GameObject continueButton;

    [Header("Text Manager")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float textSpeed;
    [SerializeField] private AudioClip textSound;
    [SerializeField] [Range(1, 20)] private int audioFreq;

    [SerializeField] [TextArea(1, 10)] private string[] endings;
    private int endingsIndex;


    void Start()
    {
        endText.text = "";
        continueButton.SetActive(false);

        endingsIndex = PlayerPrefs.GetInt("EndingInt");
        
        StartCoroutine(TypeText(endings[endingsIndex]));
    }

    ///Text typer
    private IEnumerator TypeText(string textString)
    {
        endText.text = "";
        foreach (char letter in textString)
        {
            if (Input.GetMouseButton(0))
            {
                endText.text = textString;
                break;
            }

            endText.text += letter;
            PlayDialogueSound(letter); 
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(0.5f);
        ShowButton();
    }

    private void PlayDialogueSound(int letter)
    {
        if (letter % audioFreq == 0)
        {
            //audioSource.pitch = Random.Range(0., 1);
            audioSource.PlayOneShot(textSound);            
        }
    }
    ///
    
    public void ShowButton()
    {
        continueButton.SetActive(true);
    }
}
