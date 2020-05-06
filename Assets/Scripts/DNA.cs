using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public int State;
    public Color Initial_Color;
    SpriteRenderer Square_SR;

    void OnMouseDown() {
        if (State == 1){State = 0;}
        else if (State == 0){State = 1;}
        Debug.Log(State);
    }

    // Start is called before the first frame update
    void Start()
    {
        Square_SR = GetComponent<SpriteRenderer>();
        Square_SR.color = Initial_Color;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == 1){Square_SR.color = Color.white;}
        else if (State == 0){Square_SR.color = Color.black;}
    }
}
