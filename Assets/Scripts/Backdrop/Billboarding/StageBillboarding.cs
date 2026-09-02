using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageBillboarding : MonoBehaviour
{
    private List<GameObject> sprites = new List<GameObject>();
    
    public bool DoStageBillboarding { get; set; }
    
    private void Start()
    {
        FindChildObjectsWithTag(transform, "Sprite");
    }

    private void Update()
    {
        
    }

    public void SetBillboarding()
    {
        // set all sprite objects in the stage to do or not do billboarding 
        foreach (GameObject sprite in sprites)
        {
            sprite.GetComponent<Billboarding>().DoStageBillboarding = DoStageBillboarding; 
        }
    }

    private void FindChildObjectsWithTag(Transform parent, string tagName)
    {
        foreach (Transform child in parent)
        {
            // if the child has a specified tag, add it to the list 
            if (child.CompareTag(tagName))
            {
                sprites.Add(child.gameObject);
            }
            
            // recursively search through children 
            if (child.childCount > 0)
            {
                FindChildObjectsWithTag(child, tagName);
            }
        }
    }
}
