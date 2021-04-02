using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Planet{
    public Color pColor;
    public string heat, q3;
    private string type;
    public int level, levelCap, size;
    private int[] c;
    public int[] prod; //merge upkeep and production
    public bool colony, maxed;
    public List<string> buildings;

    public Planet(){
        buildings = new List<string> {};
        c = new int[] {0, 0, 0, 0, 0, 0};
        prod = new int[] {0, 0, 0, 0, 0, 0};
        pColor = new Color(0.1f, 0.1f, 0.3f);
        colony = maxed = false;
        q3 = "Place Holder";
        level = levelCap = 0;
    }

    public void changeProd(int[] a){
        for(int i=0; i<6; i++){
            prod[i] += a[i];
        }
    }

    public string getInfo(){
        return type + "\n" + heat + "\nSize: " + size + "\n" + q3;
    }

    public string getCostDisplay(){
        string a = "Cost: " + c[5];
        if(c[0] != 0){
            a += " R" + c[0];
        }
        if(c[1] != 0){
            a += " B" + c[1];
        }
        if(c[2] != 0){
            a += " G" + c[2];
        }
        if(c[3] != 0){
            a += " Y" + c[3];
        }
        if(c[4] != 0){
            a += " W" + c[4];
        }
        return a;
    }

    public void setType(string a, string b){
        type = a;
        heat = b;
        if(a == "Bad"){
            c[5] = 1000;
            size = 2;
            levelCap = 2;
            prod[5] = -20;
        } else if(a == "Poor"){
            c[5] = 1500;
            size = 4;
            levelCap = 3;
            prod[5] = -30;
        } else if(a == "Okay"){
            c[5] = 2000;
            c[0] = 1;
            c[1] = 1;
            size = 6;
            levelCap = 4;
            prod[5] = -40;
        } else if(a == "Good"){
            c[5] = 2500;
            c[0] = 2;
            c[1] = 2;
            size = 8;
            levelCap = 5;
            prod[5] = -60;
        } else if(a == "Great"){
            c[5] = 3000;
            c[0] = 2;
            c[1] = 2;
            c[2] = 1;
            c[3] = 1;
            size = 10;
            levelCap = 6;
            prod[5] = -80;
        }

        if(b == "Cold Warning"){
            c[0] += 1;
            size -= 1;
            prod[0] = -1;
        } else if(b == "Heat Warning"){
            c[1] += 1;
            size -= 1;
            prod[1] = -1;
        }
    }

    public void levelUp(){
        if(level < levelCap){
            level += 1;
            if(level != 1){
                prod[5] -= 40;
                size += (level * 2);
            } else {
                for(int i=0; i<5; i++){
                    c[i] = 0;
                }
                if(heat == "Cold Warning"){
                    c[0] = 1;
                } else if(heat == "Heat Warning"){
                    c[1] = 1;
                }
                c[5] = 1000;
            }

            if(level == 2){
                c[0] += 1;
                c[1] += 1;
                c[5] += 1000;
            } else if(level == 3){
                c[0] += 1;
                c[1] += 1;
                c[5] += 1500;
            } else if(level == 4){
                c[2] = 1;
                c[3] = 1;
                c[5] += 2000;
            } else if(level == 5){
                c[2] += 1;
                c[3] += 1;
                c[5] += 2500;
            } else if(level == 6){
                c[4] = 1;
                c[5] += 3000;
            }
        }
        if(level == levelCap){
            maxed = true;
        }
    }

    public int[] getCost(){
        return c;
    }
}

public class Space : MonoBehaviour
{
    public int x, y, distance, exploration, planetCount;
    //public string value, desc;
    public Planet[] planets;
    Image myself;
    Color myColor;
    
    public int[] cost;

    // Start is called before the first frame update
    void Start()
    {   
        cost = new int[] {0,0,0,0,0,0};

        exploration = 0;
        myself = GetComponent<Image>();
        cost[5] = (100 * (exploration+1) * distance);
    }

    public void coord(int a, int b){
        x = a;
        y = b;
    }

    public void explore(){
        if(exploration < 3){
            exploration++;
            cost[5] = (100 * (exploration+1) * distance);
            float r = 1, g = 0, b = 0;
            //g = b = 0.3f * exploration;
            if(exploration == 1){
                g = b = 0.5f;
            } else if(exploration == 2){
                g = b = 0.7f;
            } else if(exploration == 3){
                g = b = 0.9f;
            }
            //Subject to change as current version is trash
            myColor = new Color(r, g, b);
            myself.color = myColor;
        }
        if(exploration == 2){
            makePlanets();
        }
    }

    public string getInfo(){
        string desc = "Space: " + x + ", " + y + "\nDistance: " + distance + "\nExploration: " + exploration;
        if(exploration > 0){
            desc += "\nPlanets: " + planetCount;
        }
        if(exploration != 3){
            desc += ("\nScan Cost: $" + cost[5]);
        } else {
            desc += ("\nFully Scanned.");
        }
        return desc;
    }

    public void pSetup(int a){
        planets = new Planet[a];
        for(int i=0; i<a; i++){
            planets[i] = new Planet();
        }
    }

    public void makePlanets(){
        string a, b;
        Random.InitState((int)System.DateTime.Now.Ticks);
        for(int i=0; i<planetCount; i++){
            int roll = Random.Range(1, 101);
            if(roll < 25){
                a = "Bad";
            } else if(roll < 50){
                a = "Poor";
            } else if(roll < 75){
                a = "Okay";
            } else if(roll < 95){
                a = "Good";
            } else {
                a = "Great";
            }
            roll = Random.Range(1, 6);
            if(roll == 1){
                b = "Cold Warning";
            } else if(roll == 5){
                b = "Heat Warning";
            } else {
                b = "Temperate";
            }
            planets[i].setType(a, b);
        }
    }

    public string getPlanetInfo(int a){
        return planets[a].getInfo();
    }

    public string getPlanetCost(int a){
        return planets[a].getCostDisplay();
    }

    public Color getPlanetColor(int a){
        //return planets[a].getColor();
        return planets[a].pColor;
    }
}

/*
    Well here we are, at a loss again. 
    I've a skeleton of a game with working mechanics and no clue for a direction I want to take it.

    Theoretically add:
    Planet resource production modifiers.
    Things other than planets to interact with.
    Resources that can't be bought.
    Sell 5 and Sell 10 variants for buttons. (Maybe have them hold ctrl or shift and check for it in the listener?)
    Some sort of overall goal beyond money? 
    A reason to do poorer planets first?
    A higher type of colony that's end-game?
*/