using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FCompare : IComparer<Vertex>
{
    public int Compare(Vertex vertex1, Vertex vertex2)
    {
        try
        {
            return vertex1.f.CompareTo(vertex2.f);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // write the error 
            return 0; // if error, return they are equal 
        }
    }
}
