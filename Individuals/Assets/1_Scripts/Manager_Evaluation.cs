using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_Evaluation : MonoBehaviour
{
    private Evaluator evaluator;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI evalText;
    [SerializeField] [TextArea(1, 10)] private string[] evalIntro;
    [SerializeField] private GameObject resutlsTitle;
    [SerializeField] private GameObject quitButton;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float textSpeed;
    [SerializeField] private AudioClip textSound;
    [SerializeField] [Range(1, 20)] private int audioFreq;


    [Header("Stats")]
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;
    [SerializeField] private AudioClip statsSound;

    [SerializeField] private List<int> statsList;
    
    [SerializeField] private TextMeshProUGUI[] results;
    private int resultsIndex;


    void Start()
    {
        evaluator = GameObject.FindWithTag("Evaluator").GetComponent<Evaluator>();

        evalText.text = "";
        resutlsTitle.SetActive(false);
        quitButton.SetActive(false);
        
        resultsIndex = 0;
        TakeStats();

        StartCoroutine(TypeText(evalIntro[0]));
    }

    private void TakeStats()
    {
        //List<int> statsList = new List<int>();

        statsList.Add(evaluator.stat_makesNoise);
        statsList.Add(evaluator.stat_hasEyes);
        statsList.Add(evaluator.stat_hasLegs);
        statsList.Add(evaluator.stat_wearsClothes);
        statsList.Add(evaluator.stat_looksNice);
        //statsList.Add(evaluator.stat_isSmall);
        statsList.Add(evaluator.stat_feelsCool);
        statsList.Add(evaluator.stat_canBeEaten);
    }

    ///Text typer
    private IEnumerator TypeText(string textString)
    {
        evalText.text = "";
        foreach (char letter in textString)
        {
            if (Input.GetMouseButton(0))
            {
                evalText.text = textString;
                break;
            }

            evalText.text += letter;
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
            audioSource.pitch = Random.Range(0.2f, 0.25f);
            audioSource.PlayOneShot(textSound);            
        }
    }
    ///

    private void ShowResults()
    {
        resutlsTitle.SetActive(true);
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(statsSound);

        StopAllCoroutines();
        StartCoroutine(PrintResults());
    }
    
    /// 
    private IEnumerator PrintResults()
    {
        foreach (TextMeshProUGUI result in results)
        {
            DisplayResults(results[resultsIndex], statsList[resultsIndex]);
            audioSource.PlayOneShot(textSound);
            resultsIndex ++;
        }

        
        yield return new WaitForSeconds(1.5f);
        quitButton.SetActive(true);
    }



    public void DisplayResults(TextMeshProUGUI resultText, int stat)
    {
        string text = resultText.text;
        string[] subs = text.Split(" / ");

        if (stat > 6)
        {
            subs[0] = "<color=#DDB72F>" + subs[0] + "</color>";
            subs[1] = "<color=#5C5641>" + subs[1] + "</color>";
            resultText.text = subs[0] + " / " + subs[1];
        }
        else
        {
            subs[1] = "<color=#DDB72F>" + subs[1] + "</color>";
            subs[0] = "<color=#5C5641>" + subs[0] + "</color>";
            resultText.text = subs[0] + " / " + subs[1];
        }

        var textObject = resultText.gameObject;
        textObject.SetActive(true);
    }


    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.A))
        {
            DisplayResults(resultText, evaluator.stat_makesNoise);
            Debug.Log("A");
        }*/
    }
}
