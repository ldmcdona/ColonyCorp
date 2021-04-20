using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Planet{
    public Color pColor;
    public string heat;
    private string type;
    public int level, levelCap, size;
    private int[] colCost, modifiers;
    public int[] production, upkeep;
    public bool colony, maxed, barren, super;
    public List<string> buildings;//Unused.

    public Planet(){
        buildings = new List<string> {};
        colCost = new int[] {0, 0, 0, 0, 0, 0};
        modifiers = new int[] {0, 0, 0, 0, 0, 0};
        production = new int[] {0, 0, 0, 0, 0, 0};
        pColor = new Color(0.1f, 0.1f, 0.3f);
        colony = maxed = false;
        level = levelCap = 0;
    }

    public int[] getMod(){
        return modifiers;
    }

    public int[] getProd(){
        return production;
    }

    public int[] getUpke(){
        return upkeep;
    }

    public void rollModifiers(int[] mod){
        for(int i=0; i<5; i++){
            int roll = Random.Range(1, 101);
            roll += (mod[i] * 10);
            if(roll < 11){
                modifiers[i] = -2;
            } else if(roll < 36){
                modifiers[i] = -1;
            } else if(roll < 66){
                modifiers[i] = 0;
            } else if(roll < 91){
                modifiers[i] = 1;
            } else {
                modifiers[i] = 2;
            }
        }
        modifiers[5] = mod[5]; //Currently a bit of a joke. Need to change how 'changeProd' handles this.
    }

    public void changeProd(int[] a){
        double temp;
        for(int i=0; i<5; i++){
            temp = a[i] + modifiers[i];
            if(temp < 0){
                temp = 0;
            }
            production[i] += (int)temp;
        }
        temp = a[5] * (1 + 0.1 * modifiers[5]);
        int b = (int) temp;
        production[5] += b;
    }

    public string getInfo(){
        return (type + "\n" + heat + "\nSize: " + size + "\nModifiers:\nProduction:\nUpkeep:");
    }

    public string getCostDisplay(){
        string a = "Cost: " + colCost[5];
        if(colCost[0] != 0){
            a += " R" + colCost[0];
        }
        if(colCost[1] != 0){
            a += " B" + colCost[1];
        }
        if(colCost[2] != 0){
            a += " G" + colCost[2];
        }
        if(colCost[3] != 0){
            a += " Y" + colCost[3];
        }
        if(colCost[4] != 0){
            a += " W" + colCost[4];
        }
        return a;
    }

    public void setType(string a, string b){
        int[] col;
        int[] mod;
        int[] upk;
        type = a;
        heat = b;
        if(a == "Asteroid Belt"){
            col = new int[] {0, 0, 0, 0, 0, 1000};                //Blurg, okay, lets think. R&B is rocks? G&Y is Gases? W is organics?
            mod = new int[] {0, 0, -2, -2, -10, 0};
            upk = new int[] {0, 0, 0, 0, 0, 50};
            size = 2;
            levelCap = 2;
        } else if(a == "Barren Moon"){
            col = new int[] {1, 1, 0, 0, 0, 1000};
            mod = new int[] {1, 1, -1, -1, -10, 0};
            upk = new int[] {1, 1, 0, 0, 0, 75};
            size = 6;
            levelCap = 2;
        } else if(a == "Featured Moon"){
            col = new int[] {2, 2, 0, 0, 0, 1250};
            mod = new int[] {1, 1, 0, 0, -10, 0};
            upk = new int[] {1, 1, 0, 0, 0, 100};
            size = 6;
            levelCap = 3;
        } else if(a == "Dangerous Planet"){
            col = new int[] {1, 1, 1, 1, 0, 2000};
            mod = new int[] {3, 3, 1, 1, -3, -1};
            upk = new int[] {2, 2, 0, 0, 1, 300};
            size = 10;
            levelCap = 2;
        } else if(a == "Barren Planet"){
            col = new int[] {2, 2, 0, 0, 0, 1500};
            mod = new int[] {1, 1, 0, 0, -2, 0};
            upk = new int[] {1, 1, 1, 1, 1, 200};
            size = 12;
            levelCap = 3;
        } else if(a == "Featured Planet"){
            col = new int[] {2, 2, 0, 0, 0, 1750};
            mod = new int[] {1, 1, 1, 1, -1, 0};
            upk = new int[] {1, 1, 1, 1, 1, 250};
            size = 12;
            levelCap = 4;
        } else if(a == "Fertile Planet"){
            col = new int[] {2, 2, 1, 1, 0, 2000};
            mod = new int[] {1, 1, 1, 1, 0, 1};
            upk = new int[] {2, 2, 1, 1, 1, 300};
            size = 14;
            levelCap = 5;
        } else if(a == "Terran Planet"){
            col = new int[] {2, 2, 2, 2, 0, 2500};
            mod = new int[] {1, 1, 1, 1, 1, 2};
            upk = new int[] {2, 2, 2, 2, 1, 350};
            size = 16;
            levelCap = 6;
        } else {
            col = new int[] {0, 0, 0, 0, 0, 0};
            mod = new int[] {-10, -10, -10, -10, -10, -10};
            upk = new int[] {0, 0, 0, 0, 0, 0};
            Debug.Log("Planet Generation Error");
        }

        if(b == "Cold Warning"){
            col[0] += 1;
            size -= 1;
            upk[0] = 1;
        } else if(b == "Heat Warning"){
            col[1] += 1;
            size -= 1;
            upk[1] = 1;
        }

        colCost = col;
        upkeep = upk;
        
        rollModifiers(mod);
    }

    public void levelUp(){          //This is gonna need to be rebuilt for the new system.
        if(level < levelCap){
            level += 1;
            if(level != 1){
                production[5] -= 40;
                size += (level * 2);
            } else {
                for(int i=0; i<5; i++){
                    colCost[i] = 0;
                }
                if(heat == "Cold Warning"){
                    colCost[0] = 1;
                } else if(heat == "Heat Warning"){
                    colCost[1] = 1;
                }
                colCost[5] = 1000;
            }

            if(level == 2){
                colCost[0] += 1;
                colCost[1] += 1;
                colCost[5] += 1000;
            } else if(level == 3){
                colCost[0] += 1;
                colCost[1] += 1;
                colCost[5] += 1500;
            } else if(level == 4){
                colCost[2] = 1;
                colCost[3] = 1;
                colCost[5] += 2000;
            } else if(level == 5){
                colCost[2] += 1;
                colCost[3] += 1;
                colCost[5] += 2500;
            } else if(level == 6){
                colCost[4] = 1;
                colCost[5] += 3000;
            }
        }
        if(level == levelCap){
            maxed = true;
        }
    }

    public int[] getCost(){
        return colCost;
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
            int roll = Random.Range(1, 201);

            if(roll < 41){
                a = "Asteroid Belt";
            } else if(roll < 61){
                a = "Barren Moon";
            } else if(roll < 81){
                a = "Featured Moon";
            } else if(roll < 131){
                a = "Dangerous Planet";
            } else if(roll < 161){
                a = "Barren Planet";
            } else if(roll < 181){
                a = "Featured Planet";
            } else if(roll < 196){
                a = "Fertile Planet";
            } else {
                a = "Terran Planet";
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