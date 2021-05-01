using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    //public Sprite spritePrefab;
    //public Font fontPrefab;
    public GameObject bobPrefab;
    public List<GameObject> control;

    void Awake(){
        control = new List<GameObject>();
    }

    public void neoGenesis(int[] a, int c){             //Call neoGenesis in Colonizer so we don't have duplicate display objects.
        foreach (GameObject victim in control){
            Destroy(victim);
        }
        control.Clear();
        genesis(a, c);
    }

    public void genesis(int[] a, int c){
        int count = 0;
        for(int i=0; i<6; i++){ //i<8 in the future.
            if(a[i] > 0){
                GameObject textBox = Instantiate(bobPrefab);
                control.Add(textBox);
                textBox.transform.SetParent(transform, false);
                Text tbText = textBox.GetComponent<Text>();
                SpriteRenderer tbImage = textBox.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

                tbText.text = "" + a[i];

                if(i == 0){
                    tbImage.color = new Color(1, 0, 0); //Red
                } else if(i == 1){
                    tbImage.color = new Color(0, 0, 1); //Blue
                } else if(i == 2){
                    tbImage.color = new Color(0, 1, 0); //Green
                } else if(i == 3){
                    tbImage.color = new Color(1, 1, 0); //Yellow
                } else if(i == 4){
                    tbImage.color = new Color(1, 1, 1); //White
                } else if(i == 5){
                    tbImage.color = new Color(0.3f, 0.3f, 0.3f); //Cash
                    tbText.color = new Color(0, 1, 0);
                    tbText.text = "$" + tbText.text;
                } else if(i == 6){
                    tbImage.color = new Color(1, 0, 1); //Purple
                } else if(i == 7){
                    tbImage.color = new Color(0.75f, 1, 0); //Lime
                } else if(i == 8){
                    tbImage.color = new Color(0, 0, 0); //Black
                }

                if(c == 1){
                    textBox.transform.localPosition = new Vector3(-7 + (count * 4), -11.7f, 0);
                } else if(c == 2){
                    textBox.transform.localPosition = new Vector3(-11.5f + (count * 4), -15.1f, 0);
                } else if(c == 3){
                    textBox.transform.localPosition = new Vector3(-15.5f + (count * 4), -18.2f, 0);
                }

                count++; 
            }
        }
        //Debug.Log("Called");
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Called");
        GameObject textBox = Instantiate(bobPrefab);
        textBox.transform.SetParent(transform, false);
        Text tbText = textBox.GetComponent<Text>();
        SpriteRenderer tbImage = textBox.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        tbText.text = "Hello World";
        tbImage.color = Color.blue;

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

        //Okay, so, this *works*, spawns both the image and the text, but ther are a few problems.
        //Firstly, the width and height are too big, pushing the text off the screen. Width:4.5 Height:3.5 seems to work best.
        //Secondly, for reasons I don't understand, it spawns 2. No idea why. 
        //Other than that, just a matter of spawning multiple and moving them to the correct position.

        //Okay, this *works*. 

        //Gonna try something potentially far easier.
    }
    */
}