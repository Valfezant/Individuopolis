using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewManager_Evaluation : MonoBehaviour
{
    private NewEvaluator evaluator;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] [TextArea(1, 10)] private string evalIntro;

    [SerializeField] private GameObject resutlsTitleText;
    [SerializeField] private GameObject quitButton;

    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] [TextArea(1, 5)] private string[] evalResults;

    [Header("Typing")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float textSpeed;
    [SerializeField] private AudioClip textSound;
    [SerializeField] [Range(1, 20)] private int audioFreq;

    [SerializeField] private AudioClip statsSound;

    void Start()
    {
        evaluator = GameObject.FindWithTag("Evaluator").GetComponent<NewEvaluator>();

        resultsText.text = "";
        resutlsTitleText.SetActive(false);
        quitButton.SetActive(false);

        StartCoroutine(TypeText(evalIntro));
    }

    //Type intro text
    private IEnumerator TypeText(string textString)
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
            PlayDialogueSound(letter); 
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(0.5f);
        ShowResults();
    }

    private void PlayDialogueSound(int letter)
    {
        if (letter % audioFreq == 0)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.2f, 0.25f);
            audioSource.PlayOneShot(textSound);            
        }
    }

    //Print evaluation resutls
    private void ShowResults()
    {
        resutlsTitleText.SetActive(true);
        audioSource.pitch = 1f;
        //audioSource.PlayOneShot(statsSound);

        StopAllCoroutines();
        //StartCoroutine(PrintResults());
        EvaluateStats();
    }

    /*private IEnumerator PrintResults()
    {
        foreach (TextMeshProUGUI result in results)
        {
            DisplayResults(results[resultsIndex], statsList[resultsIndex], minStatValue[resultsIndex]);
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(textSound);
            resultsIndex ++;
        }
        
        yield return new WaitForSeconds(1.5f);
        quitButton.SetActive(true);
    }*/
    
    public void EvaluateStats()
    {
        //noise
        if (Math.Abs(evaluator.stat_noise_yes - evaluator.stat_noise_no) <= 2)
        {
            resultsText.text = evalResults[0]; //"may be polite mutes";
        }
        else if (evaluator.stat_noise_yes > evaluator.stat_noise_no)
        {
            resultsText.text = evalResults[1]; //"must talk";
        }
        else if (evaluator.stat_noise_yes < evaluator.stat_noise_no)
        {
            resultsText.text = evalResults[2]; //"must not talk";
        }


        //eyes
        if (evaluator.stat_eyes_yes < evaluator.stat_canSee)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[3]; //"must have organs";
        }
        else if (evaluator.stat_eyes_yes > evaluator.stat_eyes_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[4]; //"visible eyes";
        }
        else if (evaluator.stat_eyes_yes < evaluator.stat_eyes_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[5]; //"must not have";
        }


        //legs
        if (evaluator.stat_legs_yes < evaluator.stat_twoLegs)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[6]; //"bipedal";
        }
        else if (Math.Abs(evaluator.stat_legs_yes - evaluator.stat_legs_no) <= 2)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[7]; //"can walk";
        }
        else if (evaluator.stat_legs_yes < evaluator.stat_legs_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[8]; //"cannot walk";
        }


        //clothes
        if (Math.Abs(evaluator.stat_clothes_yes - evaluator.stat_clothes_no) <= 2)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[9]; //"can present honestly";
        }
        else if (evaluator.stat_clothes_yes > evaluator.stat_clothes_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[10]; //"distinguished";
        }
        else if (evaluator.stat_clothes_no < evaluator.stat_clothes_yes)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[11]; //"no embellishements";
        }


        //looks nice
        if (Math.Abs(evaluator.stat_looksNice_yes - evaluator.stat_looksNice_no) <= 2)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[12]; //"may be repulsive";
        }
        else if (evaluator.stat_looksNice_yes > evaluator.stat_looksNice_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[13]; //"must be appealing";
        }
        else if (evaluator.stat_looksNice_yes < evaluator.stat_looksNice_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[14]; //"must be horrid";
        }


        //temperature
        if (evaluator.stat_cold > evaluator.stat_warm)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[15]; //"cold";
        }
        else if (evaluator.stat_cold < evaluator.stat_warm)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[16]; //"warm";
        }
        else
        {
            //ignore
        }


        //edible
        if (Math.Abs(evaluator.stat_edible_yes - evaluator.stat_edible_no) <= 2)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[17]; //"may not feed";
        }
        else if (evaluator.stat_edible_yes > evaluator.stat_edible_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[18]; //"must be edible";
        }
        else if (evaluator.stat_edible_yes < evaluator.stat_edible_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[19]; //"must be sturdy";
        }


        //color
        if (evaluator.stat_blue <= 0)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[20]; //"cannot be blue";
        }
        else if (evaluator.stat_brown >= 8)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[21]; //"must be brown";
        }
        else
        {
            //ignore
        }


        //texture
        if (evaluator.stat_hair <= 3)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[22]; //"hairless";
        }
        else if (evaluator.stat_smooth_yes >= evaluator.stat_smooth_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[23]; //"smooth";
        }
        else if (evaluator.stat_smooth_yes < evaluator.stat_smooth_no)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[24]; //"coarse";
        }


        //gen
        if (evaluator.stat_fourLimbs <= 1)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[25]; //"limbless";
        }
        else
        {
            //ignore
        }


        if (evaluator.stat_electric >= 4)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[26]; //"electric";
        }
        else
        {
            //ignore
        }


        if (evaluator.stat_useful >= 10)
        {
            resultsText.text = resultsText.text + '\n' + evalResults[27]; //"useful";
        }
        else
        {
            //ignore
        }

        audioSource.PlayOneShot(statsSound);
        quitButton.SetActive(true);
    }
    
    ///prendi conteggio totale craeture passate
    ///conta frequenza tratti
    /// seleziona testo tra i disponibli
    ///scrvi testo
    /// array di textboxes che avanza quando se ne usa una?
    /// forse meglio lista interna di frasi memorizzate e stampate una alla volta...?
}
