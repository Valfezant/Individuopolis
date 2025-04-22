using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Day : MonoBehaviour
{
    [SerializeField] private Entity[] entityQueue;
    [SerializeField] private int entityQueueIndex;
    public Entity currentEntity;

    //Events
    public delegate void ClickAction();
    public static event ClickAction onNextInQueue;

    
    void Start()
    {
        entityQueueIndex = 0;
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
    //Display next entity in queue -- ? & Show text
    //-----Track entity queues
    //Interactions? Nah -- ?

}
