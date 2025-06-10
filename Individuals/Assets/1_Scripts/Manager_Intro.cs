using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_Intro : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    
    [SerializeField] private TextMeshProUGUI introText1;
    [SerializeField] private TextMeshProUGUI introText2;

    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    private bool _isSecondText;

    [Header("Text Manager")]
    [SerializeField] private float textSpeed;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip textSound1;
    [SerializeField] private AudioClip textSound2;
    [SerializeField] [Range(1, 20)] private int audioFreq;

    [SerializeField] [TextArea(1, 10)] private string[] intro;
    

    void Start()
    {
        panel2.SetActive(false);
        button2.SetActive(false);
        introText2.text = "";

        button1.SetActive(false);
        introText1.text = "";

        panel1.SetActive(true);        
        StartCoroutine(TypeText(introText1, intro[0], textSound1));
        _isSecondText = false;
    }

    ///Text typer
    private IEnumerator TypeText(TextMeshProUGUI introText, string textString, AudioClip textSound)
    {
        introText.text = "";
        foreach (char letter in textString)
        {
            if (Input.GetMouseButton(0))
            {
                introText.text = textString;
                break;
            }

            introText.text += letter;
            PlayDialogueSound(letter, textSound); 

            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(0.5f);
        ShowButton();
    }

    private void PlayDialogueSound(int letter, AudioClip textSound)
    {
        if (letter % audioFreq == 0)
        {
            audioSource.pitch = Random.Range(0.2f, 0.25f);
            audioSource.PlayOneShot(textSound);            
        }
    }
    ///
    
    private void ShowButton()
    {
        if (!_isSecondText)
        {
            button1.SetActive(true);
        }
        else
        {
            button2.SetActive(true);
        }
    }

    public void ShowNextText()
    {
        panel1.SetActive(false);
        button1.SetActive(false);

        button2.SetActive(false);

        panel2.SetActive(true);        
        StartCoroutine(TypeText(introText2, intro[1], textSound2));
        _isSecondText = true;
    }
}
