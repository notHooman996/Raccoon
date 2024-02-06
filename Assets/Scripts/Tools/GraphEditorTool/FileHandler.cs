using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileHandler
{
    private string folderPath = "Assets/DataFiles"; 
    
    /// <summary>
    /// Method used to load a json file with graph data.
    /// </summary>
    /// <param name="selectedJSONFile">Name of the file that should be read, with the .json extension. </param>
    /// <returns>List of vertices in the graph.</returns>
    public List<Vertex> LoadFile(string selectedJSONFile)
    {
        List<Vertex> vertices = new List<Vertex>();

        // read the entire file as a string
        string filePath = System.IO.Path.Combine(folderPath, selectedJSONFile);
        string jsonContent = File.ReadAllText(filePath);
        
        // deserialize json string 
        JsonContainer container = JsonUtility.FromJson<JsonContainer>(jsonContent);
        
        // access and use the objects from the data 
        if (container != null && container.vertices != null)
        {
            foreach (var vertex in container.vertices)
            {
                vertices.Add(new Vertex(vertex.name, new Vector3(vertex.position.x, vertex.position.y, vertex.position.z)));
            }
        }
        
        return vertices; 
    }

    /// <summary>
    /// Method used when editing graphs. Saves the graph in new file.
    /// </summary>
    /// <param name="fileName">The file name, without the .json extension.</param>
    public void SaveNewFile(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            string filePath = System.IO.Path.Combine(folderPath, fileName + ".json");

            if (File.Exists(filePath))
            {
                Debug.Log("File name already exists.");
            }
            else
            {
                SaveToFile(filePath);
            }
        }
        else
        {
            Debug.Log("Please enter a file name.");
        }
    }

    /// <summary>
    /// Method used when editing graphs. Saves the graph by overwriting existing file. 
    /// </summary>
    /// <param name="fileName">The file name, with the .json extension.</param>
    public void OverwriteFile(string fileName)
    {
        string filePath = System.IO.Path.Combine(folderPath, fileName);
        
        SaveToFile(filePath);
    }
    
    private void SaveToFile(string filePath)
    {
        JsonContainer jsonContainer = new JsonContainer();
        jsonContainer.vertices = GraphCreator.GetVertices().ToArray();

        string data = JsonUtility.ToJson(jsonContainer, true);

        File.WriteAllText(filePath, data);
    }
}