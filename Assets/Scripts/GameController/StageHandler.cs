using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageHandler : MonoBehaviour
{
    // singleton 
    public static StageHandler Instance; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            
            // make sure object is not destroyed across scenes 
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // enforce singleton, there can only be one instance 
            Destroy(gameObject);
        }
    }

    [SerializeField] private GameObject startStage; 
    
    private List<GameObject> stages = new List<GameObject>();
    public GameObject CurrentStage { get; private set; }
    
    private void Start()
    {
        stages = GameObject.FindGameObjectsWithTag("Stage").ToList();
        SetCurrentStage(startStage); // set currentstage to startstage // TODO - set somewhere else 
    }

    public void SetCurrentStage(GameObject nextStage)
    {
        // set the stage 
        if (nextStage.CompareTag("Stage"))
        {
            CurrentStage = nextStage; 
        }

        foreach (GameObject stage in stages)
        {
            StageBillboarding component = stage.GetComponent<StageBillboarding>();
            
            component.DoStageBillboarding = stage == CurrentStage;
            
            // set billboarding for sprite objects in stage 
            component.SetBillboarding();
        }
    }
}
