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
    [Header("Text Manager")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private TextMeshProUGUI dayTitleText;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private GameObject beginButton;
    [SerializeField] private GameObject endButton;

    [SerializeField] private float textSpeed;

    [SerializeField] private string dayTitle;
    [SerializeField] [TextArea(1, 10)] private string dayIntroduction;
    
    [SerializeField] private string dayTitleEnd;
    [SerializeField] [TextArea(1, 10)] private string[] dayEnd;

    private int textIndex;
    private bool _isDayOver;

    
    void Start()
    {
        _isDayOver = false;
        harvestCounter = 0;

        hudPanel.SetActive(false);      
        dayText.text = "";
        dayText.text = "";
        beginButton.SetActive(false);
        endButton.SetActive(false);

        introPanel.SetActive(true);

        textIndex = 0;
        StartCoroutine(TypeText(dayTitleText, dayTitle));
        
        entityQueueIndex = 0;
    }

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
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(0.5f);
        textIndex ++;
        NextText();
    }

    private void NextText()
    {
        if (textIndex == 1 && !_isDayOver)
        {
            string dayIntroductionUpdated = dayIntroduction.Replace("minEstimate", minObjectEstimate.ToString()).Replace("maxEstimate", maxObjectEstimate.ToString());
            StartCoroutine(TypeText(dayText, dayIntroductionUpdated));
        }
        else if (textIndex > 1 && !_isDayOver)
        {
            beginButton.SetActive(true);
        }

        else if (textIndex == 1 && _isDayOver)
        {
            if (harvestCounter < minObjectEstimate)
            {
                //Less than required harvest
                string dayEndUpdated = dayEnd[0].Replace("harvestValue", harvestCounter.ToString());
                StartCoroutine(TypeText(dayText, dayEndUpdated));
            }
            else if (harvestCounter >= minObjectEstimate)
            {
                //Sufficient harvest
                string dayEndUpdated = dayEnd[1].Replace("harvestValue", harvestCounter.ToString());
                StartCoroutine(TypeText(dayText, dayEndUpdated));
            }
            else if (harvestCounter >= minObjectEstimate)
            {
                //Greatly exceeding harvest
                string dayEndUpdated = dayEnd[2].Replace("harvestValue", harvestCounter.ToString());
                StartCoroutine(TypeText(dayText, dayEndUpdated));
            }
        }
        else if (textIndex > 1 && _isDayOver)
        {
            endButton.SetActive(true);
        }
    }

    private void EndDay()
    {
        _isDayOver = true;
        PlayerPrefs.SetInt("TotalHarvest", harvestCounter);
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

    public void StartQueue()
    {
        introPanel.SetActive(false);
        hudPanel.SetActive(true);
        harvestSliderText.text = "Estimated number of objects: " + minObjectEstimate + "-" + maxObjectEstimate;

        NextEntityInQueue();
    }

    public void NextEntityInQueue()
    {
        Debug.Log(entityQueueIndex);
        
        if (entityQueueIndex < entityQueue.Length)
        {
            currentEntity = entityQueue[entityQueueIndex];
            entityQueueIndex ++;
            Debug.Log(entityQueueIndex + " " + currentEntity.entityName);

            if (onNextInQueue != null)
            {
                onNextInQueue();
            }
        }
        else
        {
            Debug.Log("Queue over");
            EndDay();
        }
    }

    void Update()
    {
        
        //DEBUG
        if (Input.GetKeyDown(KeyCode.X))
        {
            NextEntityInQueue();
        }
    }





    //Remind player of supposed n of objects
    //Track entities spared and destroyed


}
