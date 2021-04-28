using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    //public Sprite spritePrefab;
    //public Font fontPrefab;
    public GameObject bobPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Having a stroke. gonna call it a night and pick up again next time.
        //

        /*
        GameObject bob = new GameObject();
        bob.AddComponent<Text>();
        bob.AddComponent<SpriteRenderer>();
        RectTransform rt = bob.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(4.5f, 3.5f);

        GameObject textBox = Instantiate(bob);
        textBox.transform.SetParent(transform, false);
        Text textBoxText = textBox.GetComponent<Text>();
        SpriteRenderer cube = textBox.GetComponent<SpriteRenderer>();
        
        textBoxText.fontSize = 3;
        textBoxText.text = "-20";
        textBoxText.font = fontPrefab;
        cube.color = Color.blue;
        cube.sprite = spritePrefab;

        Destroy(bob);
        */

        //Okay, so, this *works*, spawns both the image and the text, but ther are a few problems.
        //Firstly, the width and height are too big, pushing the text off the screen. Width:4.5 Height:3.5 seems to work best.
        //Secondly, for reasons I don't understand, it spawns 2. No idea why. 
        //Other than that, just a matter of spawning multiple and moving them to the correct position.

        //Okay, this *works*. 

        //Gonna try something potentially far easier.
    }
}