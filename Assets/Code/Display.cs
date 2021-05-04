using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public GameObject bobPrefab;
    public List<GameObject> control, Qcontrol;

    void Awake(){
        control = new List<GameObject>();
    }

    public void buildingGenesis(int[] a){
        foreach (GameObject victim in Qcontrol){
            Destroy(victim);
        }
        Qcontrol.Clear();

        int count = 0;
        for(int i=0; i<9; i++){
            if(a[i] > 0){
                GameObject textBox = Instantiate(bobPrefab);
                Qcontrol.Add(textBox);
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
                    tbImage.color = new Color(1, 0, 1); //Purple
                } else if(i == 6){
                    tbImage.color = new Color(0.75f, 1, 0); //Lime
                } else if(i == 7){
                    tbImage.color = new Color(0, 0, 0); //Black
                    tbText.color = new Color(1, 1, 1);
                } else if(i == 8){
                    tbImage.color = new Color(0.3f, 0.3f, 0.3f); //Cash
                    tbText.color = new Color(0, 1, 0);
                    tbText.text = "$" + tbText.text;
                }

                textBox.transform.localPosition = new Vector3(18.7f + (count * 4), 17.9f, 0);

                count++; 
            }
        }        
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
        for(int i=0; i<9; i++){
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
                    tbImage.color = new Color(1, 0, 1); //Purple
                } else if(i == 6){
                    tbImage.color = new Color(0.75f, 1, 0); //Lime
                } else if(i == 7){
                    tbImage.color = new Color(0, 0, 0); //Black
                    tbText.color = new Color(1, 1, 1);
                } else if(i == 8){
                    tbImage.color = new Color(0.3f, 0.3f, 0.3f); //Cash
                    tbText.color = new Color(0, 1, 0);
                    tbText.text = "$" + tbText.text;
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
    }
}