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

    
    void Awake()
    {
        onNextInQueue = null;
    }
    
    void Start()
    {
        _isDayOver = false;
        harvestCounter = 0;

        hudPanel.SetActive(false);      
        dayTitleText.text = "";
        dayText.text = "";
        beginButton.SetActive(false);
        endButton.SetActive(false);

        introPanel.SetActive(true);

        textIndex = 0;
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

    private void NextText()
    {
        if (textIndex == 1 && !_isDayOver)
        {
            string dayIntroductionUpdated = dayIntroduction[0].Replace("minEstimate", minObjectEstimate.ToString()).Replace("maxEstimate", maxObjectEstimate.ToString());
            StartCoroutine(TypeText(dayText, dayIntroductionUpdated));
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
                    StartCoroutine(TypeText(dayText, dayEndUpdated));
                    PlayerPrefs.SetInt("EndingInt", 1);
                }
                else if (PlayerPrefs.GetInt("TotalHarvest") <= 4)
                {
                    //LAST DAY -- TOTAL NEGATIVE
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
                    string dayEndUpdated = dayEnd[0].Replace("harvestValue", harvestCounter.ToString());
                    StartCoroutine(TypeText(dayText, dayEndUpdated));
                    //PlayerPrefs.SetInt("EndingInt", 0);
                }
                else if (harvestCounter >= minObjectEstimate)
                {
                    //Sufficient harvest (DAILY)
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
        StartCoroutine(TypeText(dayTitleText, dayTitleEnd));
    }


    //Remind player of supposed n of objects
    //Track entities spared and destroyed


}
