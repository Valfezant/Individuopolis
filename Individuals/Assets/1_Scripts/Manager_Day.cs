using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_Day : MonoBehaviour
{
    [Header("Queue Manager")]
    [SerializeField] private GameObject hudPanel;

    [SerializeField] [Range(0, 5)] public int minObjectEstimate;
    [SerializeField] [Range(0, 5)] public int maxObjectEstimate;
    [SerializeField] private TextMeshProUGUI harvestSliderText; 
    public int harvestCounter;

    [SerializeField] private Entity[] entityQueue;
    private int entityQueueIndex;
    [HideInInspector] public Entity currentEntity; 

    //Events
    public delegate void ClickAction();
    public static event ClickAction onNextInQueue;

    //Introduction text
    [Header("Panels")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private TextMeshProUGUI dayTitleText;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private GameObject beginButton;
    [SerializeField] private GameObject endButton;

    [Header("Text Manager")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float textSpeed;
    [SerializeField] private AudioClip textSound;
    [SerializeField] [Range(1, 20)] private int audioFreq;

    [SerializeField] private string dayTitle;
    [SerializeField] [TextArea(1, 10)] private string[] dayIntroduction;
    
    [SerializeField] private string dayTitleEnd;
    [SerializeField] [TextArea(1, 10)] private string[] dayEnd;

    private int textIndex;
    private bool _isDayOver;
    [SerializeField] private bool _isLastDay;


    public Color32 hColor1;
    public Color32 hColor2;
    private string targetText;
    private bool _shouldHighlight;

    
    void Awake()
    {
        onNextInQueue = null;
    }
    
    void Start()
    {
        _isDayOver = false;
        harvestCounter = 0;
        targetText = "";

        hudPanel.SetActive(false);      
        dayTitleText.text = "";
        dayText.text = "";
        beginButton.SetActive(false);
        endButton.SetActive(false);

        introPanel.SetActive(true);

        textIndex = 0;
        _shouldHighlight = false;
        StartCoroutine(TypeText(dayTitleText, dayTitle));
        
        entityQueueIndex = 0;
    }

    ///Text typer
    private IEnumerator TypeText(TextMeshProUGUI textArea, string textString)
    {
        textArea.text = "";
        foreach (char letter in textString)
        {
            if (Input.GetMouseButton(0))
            {
                textArea.text = textString;
                break;
            }

            textArea.text += letter;
            PlayDialogueSound(letter); 
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(0.5f);
        textIndex ++;

        if (_shouldHighlight)
        {
            dayText.ForceMeshUpdate();
            HighlightValue (targetText, hColor1);
        }

        NextText();
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

    
    //Highlight effect
    public void HighlightValue(string target, Color32 color)
    {
        StopAllCoroutines();
        dayText.ForceMeshUpdate();
        TMP_TextInfo textInfo = dayText.textInfo;

        string text = dayText.text;
        int startIndex = text.IndexOf(target, System.StringComparison.OrdinalIgnoreCase);

        if (startIndex == -1) return;

        for (int i = 0; i < target.Length; i++)
        {
            int charIndex = startIndex + i;

            if (charIndex >= textInfo.characterInfo.Length) break;

            if (!textInfo.characterInfo[charIndex].isVisible) continue;

            int meshIndex = textInfo.characterInfo[charIndex].materialReferenceIndex;
            int vIndex = textInfo.characterInfo[charIndex].vertexIndex;
            
            if (meshIndex >= textInfo.meshInfo.Length) continue;
            Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;
            if (vertexColors == null || vIndex + 3 >= vertexColors.Length) continue;

            vertexColors[vIndex + 0] = color;
            vertexColors[vIndex + 1] = color;
            vertexColors[vIndex + 2] = color;
            vertexColors[vIndex + 3] = color;
        }

        dayText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        if (_shouldHighlight)
        {
            StartCoroutine(AlternateHighlight(targetText, color));
            //Debug.Log(color);
        }
    }

    private IEnumerator AlternateHighlight(string highlightedText, Color32 currentColor)
    {
        yield return new WaitForSeconds(1.3f);
        if (currentColor.Equals(hColor1))
        {
            dayText.ForceMeshUpdate();
            HighlightValue(highlightedText, hColor2);
            
        }
        else if (currentColor.Equals(hColor2))
        {
            dayText.ForceMeshUpdate();
            HighlightValue(highlightedText, hColor1);
        }
    }
    ///


    private void NextText()
    {
        if (textIndex == 1 && !_isDayOver)
        {
            /*string dayIntroductionUpdated = dayIntroduction[0].Replace("minEstimate", minObjectEstimate.ToString()).Replace("maxEstimate", maxObjectEstimate.ToString());
            dayText.enabled = false;
            dayText.text = dayIntroductionUpdated;
            string parsedtext = dayText.GetParsedText();
            Debug.Log("test" + parsedtext);
            dayText.enabled = true;*/
            //StartCoroutine(TypeText(dayText, parsedtext));
            //StartCoroutine(TypeText(dayText, dayIntroductionUpdated));
            
            string dayIntroductionUpdated = dayIntroduction[0].Replace("minEstimate", minObjectEstimate.ToString()).Replace("maxEstimate", maxObjectEstimate.ToString());
            dayText.text = dayIntroductionUpdated;

            _shouldHighlight = true;
            StartCoroutine(TypeText(dayText, dayIntroductionUpdated));
            
            dayText.ForceMeshUpdate();
            string minString = minObjectEstimate.ToString();
            string maxString = maxObjectEstimate.ToString(); 
            targetText = "between " + minString + " and " + maxString;
            Debug.Log(targetText);

            //HighlightValue (targetText, hColor);

        }
        else if (textIndex > 1 && !_isDayOver)
        {
            beginButton.SetActive(true);
        }

        else if (textIndex == 1 && _isDayOver)
        {
            if (_isLastDay)
            {
                if (PlayerPrefs.GetInt("TotalHarvest") > 4)
                {
                    //LAST DAY -- TOTAL POSITIVE
                    string dayEndUpdated = dayEnd[1].Replace("harvestValue", PlayerPrefs.GetInt("TotalHarvest").ToString());
                    _shouldHighlight = false;
                    StartCoroutine(TypeText(dayText, dayEndUpdated));
                    PlayerPrefs.SetInt("EndingInt", 1);
                }
                else if (PlayerPrefs.GetInt("TotalHarvest") <= 4)
                {
                    //LAST DAY -- TOTAL NEGATIVE
                    _shouldHighlight = false;
                    string dayEndUpdated = dayEnd[0].Replace("harvestValue", PlayerPrefs.GetInt("TotalHarvest").ToString());
                    StartCoroutine(TypeText(dayText, dayEndUpdated));
                    PlayerPrefs.SetInt("EndingInt", 0);
                }
            }
            else 
            {
                if (harvestCounter < minObjectEstimate)
                {
                    //Less than required harvest (DAILY)
                    _shouldHighlight = false;
                    string dayEndUpdated = dayEnd[0].Replace("harvestValue", harvestCounter.ToString());
                    StartCoroutine(TypeText(dayText, dayEndUpdated));
                    //PlayerPrefs.SetInt("EndingInt", 0);
                }
                else if (harvestCounter >= minObjectEstimate)
                {
                    //Sufficient harvest (DAILY)
                    _shouldHighlight = false;
                    string dayEndUpdated = dayEnd[1].Replace("harvestValue", harvestCounter.ToString());
                    StartCoroutine(TypeText(dayText, dayEndUpdated));
                    //PlayerPrefs.SetInt("EndingInt", 1);
                }
            }
        }
        else if (textIndex > 1 && _isDayOver)
        {
            endButton.SetActive(true);
        }
    }

    public void StartQueue()
    {
        _shouldHighlight = false;
        
        introPanel.SetActive(false);
        hudPanel.SetActive(true);
        harvestSliderText.text = minObjectEstimate.ToString();

        NextEntityInQueue();
    }

    public void NextEntityInQueue()
    {
        //Debug.Log(entityQueueIndex);
        
        if (entityQueueIndex < entityQueue.Length)
        {
            currentEntity = entityQueue[entityQueueIndex];
            entityQueueIndex ++;
            //Debug.Log(entityQueueIndex + " " + currentEntity.entityName);

            if (onNextInQueue != null)
            {
                onNextInQueue();
            }
        }
        else
        {
            //Debug.Log("Queue over");
            //Invoke("EndDay", 1f);
            EndDay();
        }
    }

    private void EndDay()
    {
        _isDayOver = true;

        var totalHarvest = PlayerPrefs.GetInt("TotalHarvest") + harvestCounter;
        
        PlayerPrefs.SetInt("TotalHarvest", totalHarvest);
        Debug.Log("Total Harvest= " + PlayerPrefs.GetInt("TotalHarvest"));

        hudPanel.SetActive(false);
        
        dayTitleText.text = "";
        dayText.text = "";
        beginButton.SetActive(false);
        endButton.SetActive(false);
        introPanel.SetActive(true);

        textIndex = 0;
        _shouldHighlight = false;
        StartCoroutine(TypeText(dayTitleText, dayTitleEnd));
    }


    //Remind player of supposed n of objects
    //Track entities spared and destroyed

            /*
        Debug.Log("2");
        dayText.ForceMeshUpdate();
        TMP_TextInfo textInfo = dayText.textInfo;

        /*string text = dayText.text;
        int startIndex = text.IndexOf(target);

        if (startIndex == -1) return;*/

        //TMP_TextInfo textInfo = dayText.textInfo;

        /*for (int w = 0; w < textInfo.wordCount; w++)
        {
            TMP_WordInfo wordInfo = textInfo.wordInfo[w];
            string foundWord = wordInfo.GetWord();

            if (foundWord.Equals(target, System.StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < wordInfo.characterCount; i++)
                {
                    //int charIndex = startIndex + 1;
                    int charIndex = wordInfo.firstCharacterIndex + i;

                    Debug.Log("3");

                    if (!textInfo.characterInfo[charIndex].isVisible) continue;

                    int materialIndex = textInfo.characterInfo[charIndex].materialReferenceIndex;
                    int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;

                    Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                    newVertexColors[vertexIndex + 0] = color;
                    newVertexColors[vertexIndex + 1] = color;
                    newVertexColors[vertexIndex + 2] = color;
                    newVertexColors[vertexIndex + 3] = color;

                    dayText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                    Debug.Log("4");
                }
                break;
            }
        }*/
}
