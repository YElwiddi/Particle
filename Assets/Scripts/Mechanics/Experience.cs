using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{

    [SerializeField] GameObject mainCanvasObj;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject uiPrefab1;
    [SerializeField] GameObject uiPrefab2;
    [SerializeField] GameObject uiPrefabLvlUp;

    private int experiencePoints = 0;
    private int maxExperience = 100; // Set your maximum experience value.
    private int currentLevel = 1;
    public float distanceFromCamera = 5f; // Adjust this distance as needed



    public void IncrementExperience(int amount)
    {
        experiencePoints += amount;

        // Check for level-up conditions.
        if (experiencePoints >= maxExperience)
        {
            LevelUp();
        }

        // You can add any additional logic here, such as checking for level-up conditions.
        Debug.Log($"Experience increased by {amount}. Total experience: {experiencePoints}");
    }

    private void LevelUp()
    {
        experiencePoints = 0; // Reset experience points after leveling up.
        //maxExperience *= 2; // Double the maximum experience for the next level.
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

        // Pause the game
        Time.timeScale = 0f;

        // Add any logic for the options (e.g., button click events) here

        // Example: Resume game when an option is selected
        //option1UI.GetComponent<Button>().onClick.AddListener(() => ResumeGame());
        //option2UI.GetComponent<Button>().onClick.AddListener(() => ResumeGame());
    }




    private void ResumeGame()
    {
        // Unpause the game
        Time.timeScale = 1f;

        // Destroy UI elements
        Destroy(GameObject.FindWithTag("OptionUI")); // Assuming you set a tag for your UI elements
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