using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class Star : MonoBehaviour
{
    // Grabs the GameObjects from the files to be put into the Array
    public ArrayList Choices = new ArrayList(); //TODO: Research List
    
    // The Array. As redundant as it seems, it is necessary to instantiate the objects
    private GameObject[] Hand;
    
    // Saves the positions each object should return to BEING REDONE!!!
    private Vector3[] HandSpots;

    //Struct that holds the GameObject action and the coordinates on the wheel it should be at
    public struct HandData
    {
        public HandData(Vector3 coords, GameObject choice)
        {
            C = coords;
            G = choice;
        }
        
        public Vector3 C;
        public GameObject G;

        public override string ToString() => $"({C}, {G})";
    }
    
    // Selected GameObject, in integer form
    private int Selection;

    public void Start()
    {
        SetDefault();
        Hand = new GameObject[Choices.Count];
        for (int i = 0; i < Choices.Count; i++)
        {
            Hand[i] = (GameObject)Choices[i];
        }
        Selection = 0;
        HandDraw();
    }
    
    public void SetDefault()
    {
        var section = Resources.LoadAll("MoveOptions");
        for (int i = 0; i < section.Length; i++)
            Choices.Add(section[i]);
    }

    public void Update()
    {
        //Rotate CLOCKWISE to next option
        if (Input.GetKeyDown(KeyCode.E))
        {
            Selection++;
            if(Selection == Hand.Length - 1)
                Selection = 0;
            //HandRotateNext();
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            Selection++;
            if(Selection == 0)
                Selection = Hand.Length - 1;
            //HandRotateLast();
        }
    }

    void HandDraw()
    {
        HandSpots = new Vector3[Hand.Length];
        for (int i = 0; i < Hand.Length; i++)
        {
            Vector3 OptionSpawn = new Vector3(0, 2, 0);
            Instantiate(Hand[i], OptionSpawn, Quaternion.identity, this.transform);
        }
        for (int j = 0; j < this.transform.childCount; j++)
        {
            GameObject child = this.transform.GetChild(j).gameObject;
            child.transform.RotateAround(this.transform.position, Vector3.forward, j*-360/Hand.Length);
            HandSpots[j] = child.transform.position;
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
