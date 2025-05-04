using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_Day : MonoBehaviour
{
    [Header("Queue Manager")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private Entity[] entityQueue;
    [SerializeField] private int entityQueueIndex;
    public Entity currentEntity;

    //Events
    public delegate void ClickAction();
    public static event ClickAction onNextInQueue;

    //Introduction text
    [Header("Introduction Manager")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private TextMeshProUGUI dayTitleText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private GameObject beginButton;

    [SerializeField] private string dayTitle;
    [SerializeField] [TextArea(1, 10)] private string dayIntroduction;
    [SerializeField] private float textSpeed;
    private int textIndex;


    
    void Start()
    {
        introPanel.SetActive(true);
        dayText.text = "";
        dayText.text = "";
        beginButton.SetActive(false);

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
        if (textIndex == 1)
        {
            StartCoroutine(TypeText(dayText, dayIntroduction));
        }
        if (textIndex > 1)
        {
            beginButton.SetActive(true);
        }
    }

    public void StartQueue()
    {
        introPanel.SetActive(false);
        hudPanel.SetActive(true);
        NextEntityInQueue();
    }

    void Update()
    {
        
        //DEBUG
        if (Input.GetKeyDown(KeyCode.X))
        {
            NextEntityInQueue();
        }
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
        }
    }






    //Remind player of supposed n of objects
    //Track entities spared and destroyed


}
