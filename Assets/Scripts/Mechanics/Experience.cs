using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Platformer.Mechanics;

public class Experience : MonoBehaviour
{

    [SerializeField] GameObject mainCanvasObj;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject uiPrefab1;
    [SerializeField] GameObject uiPrefab2;
    [SerializeField] GameObject uiPrefabLvlUp;
    [SerializeField] PlayerController playerController;
    private int experiencePoints = 0;
    private int maxExperience = 100; // Set your maximum experience value.
    private int currentLevel = 1;
    private int distanceFromCamera = 5;

    private void Start()
    {
        // Get a reference to the PlayerController script attached to the same GameObject
        playerController = GetComponent<PlayerController>();
    }

    public void IncrementExperience(int amount)
    {
        experiencePoints += amount;

        // Check for level-up conditions.
        if (experiencePoints >= maxExperience)
        {
            LevelUp();
        }

    }

    private void LevelUp()
    {
        experiencePoints = 0; // Reset experience points after leveling up.
        maxExperience += 20; // Increase the amount of XP needed to level up
        currentLevel++;

        // Add any additional logic for level-up effects or actions here.
        // Debug.Log($"Level up! New level: {currentLevel}");


        // Calculate the position relative to the camera
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 spawnPosition = cameraPosition + Camera.main.transform.forward * distanceFromCamera;

        // Instantiate the UI prefabs at the calculated position
        //GameObject uiInstance1 = Instantiate(uiPrefab1, spawnPosition, Quaternion.identity);
        Vector3 pos = Camera.main.transform.TransformPoint(Vector3.forward * distanceFromCamera);

        // Instantiating the UI elements
        GameObject uiInstance1 = Instantiate(uiPrefab1, new Vector3(-250f, -100, 0f), Quaternion.identity);
        GameObject uiInstance2 = Instantiate(uiPrefab2, new Vector3(50, -100, 0f), Quaternion.identity);
        GameObject uiInstanceLvlUp = Instantiate(uiPrefabLvlUp, new Vector3(-175f, -275f, 0f), Quaternion.identity);

        // Setting the UI canvas to be the parent of the UI prefabs
        uiInstance1.transform.SetParent(mainCanvasObj.transform, false);
        uiInstance2.transform.SetParent(mainCanvasObj.transform, false);
        uiInstanceLvlUp.transform.SetParent(mainCanvasObj.transform, false);

        // Add a clickable event to the UI element
        // Hard coding for now, can add if statement to check what uiInstance was passed in the ResumeGame method later
        AddClickEvent(uiInstance1, () => ResumeGameJump(uiInstance1, uiInstance2, uiInstanceLvlUp));
        AddClickEvent(uiInstance2, () => ResumeGameMove(uiInstance1, uiInstance2, uiInstanceLvlUp));

        // Pause the game
        Time.timeScale = 0f;

    }

    private void AddClickEvent(GameObject clickableObject, UnityEngine.Events.UnityAction clickAction)
    {
        // Add an EventTrigger component if it doesn't exist
        EventTrigger eventTrigger = clickableObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = clickableObject.AddComponent<EventTrigger>();
        }

        // Add a PointerClick event trigger
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { clickAction.Invoke(); });

        // Add the event to the trigger
        eventTrigger.triggers.Add(entry);
    }


    private void ResumeGameMove(GameObject UIPrefab1, GameObject UIPrefab2, GameObject UIPrefab3)
    {
        // Increase movement speed by 20%
        playerController.maxSpeed = (playerController.maxSpeed * 1.2f);

        // Unpause the game
        Time.timeScale = 1f;

        // Destroy UI elements
        Destroy(UIPrefab1); 
        Destroy(UIPrefab2); 
        Destroy(UIPrefab3); 
    }

        private void ResumeGameJump(GameObject UIPrefab1, GameObject UIPrefab2, GameObject UIPrefab3)
    {
        // Increase jump power by 20%
        playerController.jumpTakeOffSpeed = (playerController.jumpTakeOffSpeed * 1.2f);

        // Unpause the game
        Time.timeScale = 1f;

        // Destroy UI elements
        Destroy(UIPrefab1); 
        Destroy(UIPrefab2); 
        Destroy(UIPrefab3); 
    }

    public int GetExperience()
    {
        return experiencePoints;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public float GetExperiencePercentage()
    {
        if (maxExperience <= 0)
        {
            Debug.LogError("Max experience should be greater than 0 to calculate percentage.");
            return 0.0f;
        }

        return (float)experiencePoints / maxExperience;
    }

    // Additional methods and logic...
}