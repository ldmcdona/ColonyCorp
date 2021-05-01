using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building{
    public int[] cost, production, sp, upkeep;
    public int size, points;
    public string name;
}

public class Colonizer : MonoBehaviour
{
    public Building[] buildings;
    public List<string> ddOptions;

    // Start is called before the first frame update
    void Start()
    {
        //buildings = new Building[8];    //Number is subject to change.
        buildings = new Building[10]; //testing an idea
        defineBuildings();
        ddOptions = new List<string> {};
    }

    void defineBuildings(){             //This entire section is subject to rebuild.
        buildings[0] = new Building();
        buildings[0].name = "Toll Station";
        buildings[0].cost = new int[] {0, 0, 0, 0, 0, 100};
        buildings[0].production = new int[] {-2, -2, -2, -2, -2, 250};
        buildings[0].size = 1;

        buildings[1] = new Building();
        buildings[1].name = "Red Mine";
        buildings[1].cost = new int[] {0, 0, 0, 0, 0, 300};
        buildings[1].production = new int[] {2, -2, -2, -2, -2, 0};
        buildings[1].size = 2;

        buildings[2] = new Building();
        buildings[2].name = "Blue Mine";
        buildings[2].cost = new int[] {0, 0, 0, 0, 0, 300};
        buildings[2].production = new int[] {-2, 2, -2, -2, -2, 0};
        buildings[2].size = 2;

        buildings[3] = new Building();
        buildings[3].name = "Green Pump";
        buildings[3].cost = new int[] {1, 1, 0, 0, 0, 500};
        buildings[3].production = new int[] {-2, -2, 2, -2, -2, 0};
        buildings[3].size = 4;

        buildings[4] = new Building();
        buildings[4].name = "Yellow Pump";
        buildings[4].cost = new int[] {1, 1, 0, 0, 0, 500};
        buildings[4].production = new int[] {-2, -2, -2, 2, -2, 0};
        buildings[4].size = 4;

        buildings[5] = new Building();
        buildings[5].name = "White Farm";
        buildings[5].cost = new int[] {2, 2, 1, 1, 0, 750};
        buildings[5].production = new int[] {-2, -2, -2, -2, 2, 0};
        buildings[5].size = 6;

        // --- IDEAS AND TESTING ---

        buildings[6] = new Building();
        buildings[6].name = "Civilian Center";
        buildings[6].cost = new int[] {1, 1, 1, 1, 1, 250};
        buildings[6].size = 4;
        buildings[6].points = 3;

        buildings[7] = new Building();
        buildings[7].name = "Purple Refinery";
        buildings[7].cost = new int[] {1, 1, 0, 0, 0, 600};
        buildings[7].size = 2;
        buildings[7].sp = new int[] {2, 0, 0};
        buildings[7].upkeep = new int[] {1, 1, 0, 0, 0, 0};

        buildings[8] = new Building();
        buildings[8].name = "Lime Refinery";
        buildings[8].cost = new int[] {0, 0, 1, 1, 0, 1000};
        buildings[8].size = 4;
        buildings[8].sp = new int[] {0, 2, 0};
        buildings[8].upkeep = new int[] {0, 0, 1, 1, 0, 0};

        buildings[9] = new Building();
        buildings[9].name = "Black Refinery";
        buildings[9].cost = new int[] {0, 0, 0, 0, 1, 1500};
        buildings[9].size = 6;
        buildings[9].sp = new int[] {0, 0, 1};
        buildings[9].upkeep = new int[] {0, 0, 0, 0, 1, 100};
    }

    public void colonize(Resources inv, Planet selected, Text top, GameObject display, GridBehaviour grid, int a, int b, List<Planet> pLog, Display bob){
        GameObject cbuObj, canv, buildObj; //spare;
        Text buttonText, infoText; //costText, modText; //modText isn't used yet because colony level doesn't affect upkeep yet.
        Dropdown pdd;
        SpriteRenderer psr;

        canv = display.transform.GetChild(0).gameObject;
        psr = display.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();

        infoText = canv.transform.GetChild(2).gameObject.GetComponent<Text>();
        //costText = canv.transform.GetChild(3).gameObject.GetComponent<Text>();
        cbuObj = canv.transform.GetChild(3).gameObject;
        buildObj = canv.transform.GetChild(0).gameObject;

        //spare = canv.transform.GetChild(7).gameObject;
        //modText = spare.transform.GetChild(5).gameObject.GetComponent<Text>(); //Eventually want this to be done for-loop style like in Master.

        buttonText = cbuObj.transform.GetChild(0).gameObject.GetComponent<Text>();
        pdd = buildObj.transform.GetChild(1).gameObject.GetComponent<Dropdown>();

        if(!selected.colony){
            if(inv.canAfford(selected.getCost())){
                selected.colony = true;
                grid.distances(a, b);
                inv.Spend(selected.getCost());
                selected.levelUp();
                top.text = inv.getInfo();
                //setDD(selected, pdd);
                Color tempC = new Color(0.3f, 0.1f, 0.1f);
                selected.pColor = tempC;
                psr.color = tempC;
                buttonText.text = "Upgrade";
                buildObj.SetActive(true); //here
                //costText.text = selected.getCostDisplay();
                pLog.Add(selected);
                bob.neoGenesis(selected.getProd(), 1);
                bob.genesis(selected.getUpke(), 2);
                if(!selected.maxed){
                    bob.genesis(selected.getCost(), 3);
                }
            }
        } else {
            if(inv.canAfford(selected.getCost())){
                inv.Spend(selected.getCost());
                selected.levelUp();
                top.text = inv.getInfo();
                int[] upkeep = selected.getUpke();
                //modText.text = "$: " + upkeep[5];
                infoText.text = selected.getInfo();
                Color tempC = new Color(0.3f, (0.1f * selected.level), 0.1f);
                selected.pColor = tempC;
                psr.color = tempC;
                //setDD(selected, pdd);
                //costText.text = selected.getCostDisplay();
                if(selected.maxed){
                    cbuObj.SetActive(false);
                    //costText.text = "Colony Level Maxed";
                }
                bob.neoGenesis(selected.getProd(), 1);
                bob.genesis(selected.getUpke(), 2);
                if(!selected.maxed){
                    bob.genesis(selected.getCost(), 3);
                }
            }
        }
    }

    public void build(Resources inv, Planet selected, Dropdown pdd, Text top, Text infoText, GameObject display, Display bob){
        //Text modText;
        GameObject temp;
        temp = display.transform.GetChild(0).gameObject;
        //temp = temp.transform.GetChild(6).gameObject;

        //if(buildings[pdd.value].sp != None || buildings[pdd.value].points != None)
            //Special conditions for special buildings. Might be easier to do a boolean.
        //else 

        if(inv.canAfford(buildings[pdd.value].cost) && buildings[pdd.value].size <= selected.size){
            selected.buildings.Add(buildings[pdd.value].name);
            inv.Spend(buildings[pdd.value].cost); //pdd.value is the index of the selection dd option. Meaning this needs to be changed.
            selected.changeProd(buildings[pdd.value].production);
            top.text = inv.getInfo();
            selected.size -= buildings[pdd.value].size;
            infoText.text = selected.getInfo();
            bob.neoGenesis(selected.getProd(), 1);
            bob.genesis(selected.getUpke(), 2);
            if(!selected.maxed){
                bob.genesis(selected.getCost(), 3);
            }
            //setDD(selected, pdd);
            /*
            int[] prod = selected.getProd();
            for(int i=0; i<5; i++){
                modText = temp.transform.GetChild(i).gameObject.GetComponent<Text>();
                modText.text = "" + prod[i];
            }
            modText = temp.transform.GetChild(5).gameObject.GetComponent<Text>();
            modText.text = "$: " + prod[5];
            */
        }
    }

    public void setDD(Planet selected, Dropdown pdd){
        //Really wish I hadn't gotten rid of the original of this.
        //Basically, certain buildings are locked to certain planet types.
        //Call only on colonization and going to the solar screen.

        //Also means I have to clean the Dropdowns current default options.
        List<string> ddOptions = new List<string> {"Toll Station", "Red Mine", "Blue Mine"};

    }

    public void ddDisplay(Dropdown pdd, GameObject buildObj){
        Text sizeText, costText;
        sizeText = buildObj.transform.GetChild(2).gameObject.GetComponent<Text>();
        costText = buildObj.transform.GetChild(3).gameObject.GetComponent<Text>();

        sizeText.text = "Size: " + buildings[pdd.value].size;

        string a = "Cost: " + buildings[pdd.value].cost[5];
        if(buildings[pdd.value].cost[0] != 0){
            a += " R" + buildings[pdd.value].cost[0];
        }
        if(buildings[pdd.value].cost[1] != 0){
            a += " B" + buildings[pdd.value].cost[1];
        }
        if(buildings[pdd.value].cost[2] != 0){
            a += " G" + buildings[pdd.value].cost[2];
        }
        if(buildings[pdd.value].cost[3] != 0){
            a += " Y" + buildings[pdd.value].cost[3];
        }
        if(buildings[pdd.value].cost[4] != 0){
            a += " W" + buildings[pdd.value].cost[4];
        }
        costText.text = a;
    }

    //https://docs.unity3d.com/2018.4/Documentation/ScriptReference/UI.Dropdown.html
}

/*
Okay Liam, let's talk.

Planetary modifiers are OP as hell.
+2 per mine doubles mine effectiveness, and when you can build like 20 that's a problem.

Maybe have resources actually be super cheap but raise upkeep rates?
Or got full Starsector and vastly lower the building amount per planet.

Also, figure out how to display cash production and upkeep. (And mod theoretically).

Then figure out the Purple, Lime, and Black resources.

Also also, the shift/ctrl click for buy/sell 5/10. That would be nice.

Remember you are working on the 'Free Build' mode.

*/