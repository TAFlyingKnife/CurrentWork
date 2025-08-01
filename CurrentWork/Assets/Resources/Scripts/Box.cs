using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class Box : MonoBehaviour
{
    public ArrayList Choices = new ArrayList();
    private GameObject[] Hand;
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

    public void Update()
    {
        //Rotate CLOCKWISE to next option
        if (Input.GetKey(KeyCode.E))
        {
            Selection++;
            if(Selection == Hand.Length - 1)
                Selection = 0;
            HandRotateNext();
        } else if (Input.GetKey(KeyCode.Q))
        {
            Selection++;
            if(Selection == 0)
                Selection = Hand.Length - 1;
            HandRotateLast();
        }
    }

    void HandDraw()
    {
        for (int i = 0; i < Hand.Length; i++)
        {
            Vector3 OptionSpawn = new Vector3(0, 2, 0);
            Instantiate(Hand[i], OptionSpawn, Quaternion.identity, this.transform);
        }
        for (int j = 0; j < this.transform.childCount; j++)
        {
            GameObject child = this.transform.GetChild(j).gameObject;
            child.transform.RotateAround(this.transform.position, Vector3.forward, j*-360/Hand.Length);
            child.GetComponent<Option>().OrientationDoNotTouch = j;
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void HandRotateNext()
    {
        for (int j = 0; j < this.transform.childCount; j++)
        {
            GameObject child = this.transform.GetChild(j).gameObject;
            if (child.GetComponent<Option>().OrientationDoNotTouch > Hand.Length - 1)
            {
                child.GetComponent<Option>().OrientationDoNotTouch = 0;
            }
            else
            {
                child.GetComponent<Option>().OrientationDoNotTouch++;
            }
            child.transform.RotateAround(this.transform.position, Vector3.forward, child.GetComponent<Option>().OrientationDoNotTouch*-360/Hand.Length);
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void HandRotateLast()
    {
        for (int j = 0; j < this.transform.childCount; j++)
        {
            GameObject child = this.transform.GetChild(j).gameObject;
            if (child.GetComponent<Option>().OrientationDoNotTouch < 0)
            {
                child.GetComponent<Option>().OrientationDoNotTouch = Hand.Length - 1;
            }
            else
            {
                child.GetComponent<Option>().OrientationDoNotTouch--;
            }
            child.transform.RotateAround(this.transform.position, Vector3.forward, child.GetComponent<Option>().OrientationDoNotTouch*-360/Hand.Length);
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    public void SetDefault()
    {
        var section = Resources.LoadAll("MoveOptions");
        for (int i = 0; i < section.Length; i++)
            Choices.Add(section[i]);
    }
    
    public void AddChoice(GameObject choice)
    {
        Choices.Add(choice);
    }

    public void RemoveChoice(GameObject choice)
    {
        Choices.Remove(choice);
    }
}
