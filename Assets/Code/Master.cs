using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
    //Neither of these are referenced. 
    public Button[] buttons;
    public Space[] spaces;

    public GameObject planetPrefab;
    GameObject[] planets;
    private List<Planet> pLog;

    //Gonna try to do market without storing them.
    public Button btemp;
    //Worked, if messy.
    //Might remove 'buttons' and 'spaces'

    public Space selected;
    private GridBehaviour grid;
    private GameObject temp, highlight, cam;
    private GameObject[] UIs;
    private Button scanButton, marketButton, solarButton, turnButton;
    private Text sideText, topText, upkeepText, MBtx, SVtx, turnText;
    private Vector3 outB, normal, market, solar;
    private Resources inv;
    private Colonizer col;
    private bool view, autoS;
    private int turn;

    // Start is called before the first frame update
    void Start()
    {
        pLog = new List<Planet> {};
        turn = 1;
        view = true;
        autoS = false;
        outB.x = 13.6f;
        outB.y = 23f;
        outB.z = -1f;
        normal.x = 8f;
        normal.y = 25.3f;
        normal.z = market.z = solar.z = -10f;
        market.x = -175f;
        market.y = solar.y = 32f;
        solar.x = -358.2f;

        grid = transform.GetChild(0).gameObject.GetComponent<GridBehaviour>();
        highlight = transform.GetChild(1).gameObject;
        inv = transform.GetChild(2).gameObject.GetComponent<Resources>();

        UIs = new GameObject[6]; //In order these are: SideBar, SBCanvas, ScanUI, MarketUI, SolarUI, EndUI

        UIs[0] = transform.GetChild(3).gameObject;
        UIs[1] = transform.GetChild(4).gameObject;
        sideText = UIs[1].transform.GetChild(0).gameObject.GetComponent<Text>();
        turnText = UIs[1].transform.GetChild(1).gameObject.GetComponent<Text>();
        UIs[2] = transform.GetChild(5).gameObject;
        UIs[2].SetActive(false);
        temp = UIs[2].transform.GetChild(0).gameObject;
        scanButton = temp.GetComponent<Button>();
        scanButton.onClick.AddListener(scan);
        cam = transform.GetChild(6).gameObject;
        temp = cam.transform.GetChild(0).gameObject;
        topText = temp.transform.GetChild(0).gameObject.GetComponent<Text>();
        upkeepText = temp.transform.GetChild(1).gameObject.GetComponent<Text>();
        UIs[3] = cam.transform.GetChild(2).gameObject;
        temp = UIs[3].transform.GetChild(0).gameObject;
        marketButton = temp.GetComponent<Button>();
        marketButton.onClick.AddListener(marketView);
        temp  = temp.transform.GetChild(0).gameObject;
        MBtx = temp.GetComponent<Text>();
        UIs[4] = cam.transform.GetChild(3).gameObject;
        temp = UIs[4].transform.GetChild(0).gameObject;
        solarButton = temp.GetComponent<Button>();
        solarButton.onClick.AddListener(solarView);
        temp = temp.transform.GetChild(0).gameObject;
        SVtx = temp.GetComponent<Text>();
        UIs[4].SetActive(false);

        //Alright, this is setting up listeners for the market.
        temp = transform.GetChild(7).gameObject;//Market
        temp = temp.transform.GetChild(1).gameObject;//Canvas
        temp = temp.transform.GetChild(0).gameObject;//Buy
        for(int i = 0; i<5; i++){
            GameObject temp2;
            temp2 = temp.transform.GetChild(i).gameObject;
            btemp = temp2.GetComponent<Button>();
            btemp.onClick.AddListener(delegate {transaction(temp2.name); });
        }
        temp = transform.GetChild(7).gameObject;//Market
        temp = temp.transform.GetChild(1).gameObject;//Canvas
        temp = temp.transform.GetChild(1).gameObject;//Sell        
        for(int i = 0; i<5; i++){
            GameObject temp2;
            temp2 = temp.transform.GetChild(i).gameObject;
            btemp = temp2.GetComponent<Button>();
            btemp.onClick.AddListener(delegate {transaction(temp2.name); });
        }

        temp = transform.GetChild(7).gameObject;//Market
        temp = temp.transform.GetChild(1).gameObject;//Canvas
        btemp = temp.transform.GetChild(2).gameObject.GetComponent<Button>();
        btemp.onClick.AddListener(cashOut);
        btemp = temp.transform.GetChild(3).gameObject.GetComponent<Button>();
        btemp.onClick.AddListener(AutoSell);

        col = transform.GetChild(8).gameObject.GetComponent<Colonizer>();

        UIs[5] = temp = transform.GetChild(9).gameObject;
        turnButton = temp.transform.GetChild(0).gameObject.GetComponent<Button>();
        turnButton.onClick.AddListener(endTurn);
    }

    void display(){
        topText.text = inv.getInfo();
        upkeepText.text = "";
    }

    void tryNow(){
        buttons = new Button[61];
        spaces = new Space[61];
        temp = transform.GetChild(0).gameObject;
        //temp is the canvas
        for(int i=0; i<61; i++){
            if(i != 30){
                int j = i;
                buttons[i] = temp.transform.GetChild(i).gameObject.GetComponent<Button>();
                spaces[i] = temp.transform.GetChild(i).gameObject.GetComponent<Space>();
                buttons[i].onClick.AddListener(delegate {oof(spaces[j]); });
            } else {
                Destroy(temp.transform.GetChild(30).gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Consider removing this and replacing it with the option to just click on the player spawn.
        if(Input.GetMouseButtonDown(1) && view == true){
            highlight.transform.position = outB;
            sideText.text = "Welcome to the game.\n\nCurrently in Alpha.\n\nClick a space to select it.\nRight-click to deselect it.\n\nExploration 1:\nNumber of Planets\nExploration 2:\nView System\nExploration 3:\nColonization";
            UIs[2].SetActive(false);
            UIs[4].SetActive(false);
            selected = null;
        }

        if(Input.GetKey("escape")){
            Application.Quit();
        }
    }

    void oof(Space target){
        selected = target;
        //Debug.Log(target.x + " " + target.y + " Distance: " + target.distance);
        if(selected.exploration < 3){
            UIs[2].SetActive(true);
        } else {
            UIs[2].SetActive(false);
        }
        highlight.transform.position = selected.transform.position;
        sideText.text = selected.getInfo();
        if(selected.exploration >= 2){
            UIs[4].SetActive(true);
        } else {
            UIs[4].SetActive(false);
        }
    }

    void scan(){
        if(selected.exploration < 3){
            if(inv.canAfford(selected.cost)){
                inv.Spend(selected.cost);
                selected.explore();
                sideText.text = selected.getInfo();
                topText.text = inv.getInfo();
                if(selected.exploration >= 2){
                    UIs[4].SetActive(true);
                }
                if(selected.exploration == 3){
                    UIs[2].SetActive(false);
                }
            } else {
                sideText.text = selected.getInfo() + "\n\nInsufficient Cash to scan.";
            }
        }
    }

    void transaction(string n){
        if(Input.GetKey("right shift") || Input.GetKey("left shift")){
            for(int i=0; i<5; i++){
                inv.market(n);
            }
        } else if(Input.GetKey("right ctrl") || Input.GetKey("left ctrl")){
            for(int i=0; i<10; i++){
                inv.market(n);
            }
        } else {
            inv.market(n);
        }
        topText.text = inv.getInfo();
    }

    void marketView(){
        if(view){
            //highlight.transform.position = outB;
            //sideText.text = "Nothing Selected."; // replace w/ disable sidebar
            UIs[2].SetActive(false); //Like this.
            UIs[4].SetActive(false);
            UIs[5].SetActive(false);
            MBtx.text = "Main\nScreen";
            view = false;
            //Move cam to market coords.
            cam.transform.position = market;
            //selected = null;
        } else {
            MBtx.text = "Galactic\nMarket";
            view = true;
            //move cam to main screen coords.
            cam.transform.position = normal;
            if(selected != null){
                if(selected.exploration < 3){
                    UIs[2].SetActive(true);
                }
                if(selected.exploration > 1){
                    UIs[4].SetActive(true);
                }
            }
            UIs[5].SetActive(true);
        }
    }

    void solarView(){
        if(view){
            //highlight.transform.position = outB;
            //sideText.text = "Nothing Selected.";
            UIs[2].SetActive(false);
            UIs[3].SetActive(false);
            UIs[5].SetActive(false);
            SVtx.text = "Main\nScreen";
            view = false;
            cam.transform.position = solar;
            SpawnPlanets();
            //selected = null;
        } else {
            if(selected != null){
                if(selected.exploration < 3){
                    UIs[2].SetActive(true);
                }
                UIs[3].SetActive(true);
            }
            UIs[5].SetActive(true);
            SVtx.text = "View\nSystem";
            view = true;
            cam.transform.position = normal;
            KillPlanets();
            sideText.text = selected.getInfo();
            //selected = null;
        }
    }

    void SpawnPlanets(){
        //Planet ptemp;
        Vector3 spot;
        spot.z = -1f;
        planets = new GameObject[selected.planetCount];
        for(int i=0; i<selected.planetCount; i++){
            int j = i;
            //ptemp = selected.planets[j]; // I think this line is what is causing the reload issues. //Yep. Confirmed.
            if(i == 0){
                spot.x = -390f;
                spot.y = 43.3f;
            } else if(i == 1){
                spot.x = -390f;
                spot.y = 17f;
            } else if(i == 2){
                spot.x = -345f;
                spot.y = 43.3f;
            } else {
                spot.x = -345f;
                spot.y = 17f;
            }
            planets[j] = Instantiate<GameObject>(planetPrefab);
            GameObject pcan, temp2, buildObj;
            Text coordText, infoText, costText, pbtx, modText;
            Button pbu1, pbu2;
            SpriteRenderer pim;
            Dropdown pdd;
            pcan = planets[i].transform.GetChild(0).gameObject;//Canvas
            coordText = pcan.transform.GetChild(1).gameObject.GetComponent<Text>(); //coords
            infoText = pcan.transform.GetChild(2).gameObject.GetComponent<Text>(); //info
            costText = pcan.transform.GetChild(3).gameObject.GetComponent<Text>(); //cost
            temp2 = pcan.transform.GetChild(4).gameObject;
            pbu1 = temp2.GetComponent<Button>(); //Colonize/Upgrade
            pbtx = temp2.transform.GetChild(0).gameObject.GetComponent<Text>();
            pim = planets[i].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
            buildObj = pcan.transform.GetChild(0).gameObject;
            pdd = buildObj.transform.GetChild(1).gameObject.GetComponent<Dropdown>();
            pbu2 = buildObj.transform.GetChild(0).gameObject.GetComponent<Button>(); //Build

            pbu1.onClick.AddListener(delegate {col.colonize(inv, selected.planets[j], topText, planets[j], grid, selected.x, selected.y, pLog); });
            pbu2.onClick.AddListener(delegate {col.build(inv, selected.planets[j], pdd, topText, infoText, planets[j]); });

            pdd.onValueChanged.AddListener(delegate {col.ddDisplay(pdd, buildObj); });

            coordText.text = "Planet (" + selected.x + ", " + selected.y + ") - " + (i+1);
            infoText.text = selected.planets[j].getInfo();
            costText.text = selected.planets[j].getCostDisplay();
            pim.color = selected.planets[j].pColor;

            if(!selected.planets[j].colony){
                buildObj.SetActive(false);           
            } else {
                if(selected.planets[j].level != selected.planets[j].levelCap){
                    pbtx.text = "Upgrade";
                } else {
                    temp2 = pcan.transform.GetChild(4).gameObject;
                    temp2.SetActive(false);
                    costText.text = "Colony Level Maxed";
                }
            }

            if(selected.exploration < 3){
                buildObj.SetActive(false);
                temp2 = pcan.transform.GetChild(4).gameObject;
                temp2.SetActive(false);
            }

            temp2 = pcan.transform.GetChild(5).gameObject;
            int[] modTemp = selected.planets[j].getMod();
            for(int k=0; k<5; k++){
                modText = temp2.transform.GetChild(k).gameObject.GetComponent<Text>();
                if(modTemp[k] > -1){
                    modText.text = "+" + modTemp[k];
                } else {
                    modText.text = "" + modTemp[k];
                }
            }
            modText = temp2.transform.GetChild(5).gameObject.GetComponent<Text>();
            if(modTemp[5] > -1){
                modText.text = "$: +" + (10* modTemp[5]) + "%";
            } else {
                modText.text = "$: " + (10* modTemp[5]) + "%";
            }

            temp2 = pcan.transform.GetChild(6).gameObject;
            modTemp = selected.planets[j].getProd();
            for(int k=0; k<5; k++){
                modText = temp2.transform.GetChild(k).gameObject.GetComponent<Text>();
                modText.text = "" + modTemp[k];
            }
            modText = temp2.transform.GetChild(5).gameObject.GetComponent<Text>();
            modText.text = "$: " + modTemp[5];

            temp2 = pcan.transform.GetChild(7).gameObject;
            modTemp = selected.planets[j].getUpke();
            for(int k=0; k<5; k++){
                modText = temp2.transform.GetChild(k).gameObject.GetComponent<Text>();
                modText.text = "" + modTemp[k];
            }
            modText = temp2.transform.GetChild(5).gameObject.GetComponent<Text>();
            modText.text = "$: " + modTemp[5];

            planets[j].transform.position = spot;
            //col.setDD(selected.planets[j], pdd);
        }
    }

    void KillPlanets(){
        for(int i=0; i<selected.planetCount; i++){
            GameObject pcan, temp2;
            Button pbu;
            pcan = planets[i].transform.GetChild(0).gameObject;//Canvas
            temp2 = pcan.transform.GetChild(1).gameObject;
            pbu = temp2.GetComponent<Button>();

            //pbu.onClick.RemoveListener(delegate {col.build(inv, selected.planets[i], pdd, pim, pbtx); });

            Destroy(planets[i]);
        }
    }

    void endTurn(){
        int[] x = {0, 0, 0, 0, 0, 100};
        int[] y = {0, 0, 0, 0, 0, 0};
        int[] z = {0, 0, 0, 0, 0, 0};
        turn++;
        turnText.text = "Turn: " + turn;

        foreach(Planet p in pLog){
            for(int i=0; i<6; i++){
                x[i] += p.production[i];
                y[i] += p.upkeep[i];
                z[i] = x[i] - y[i];
            }
        }

        inv.Gain(x);
        inv.Spend(y);
        upkeepText.text = "Cash: " + z[5] + " Red: " + z[0] + " Blue: " + z[1] + " Green: " + z[2] + " Yellow: " + z[3] + " White: " + z[4];

        if(inv.cash < 0){
            //Debug.Log("Sell All Triggered.");
            inv.sellAll();
            if(inv.cash < 0){
                //Debug.Log("Game Over");
            }
        }

        if(autoS){
            inv.sellAll();
        }
        topText.text = inv.getInfo();

        if(turn == 21){
            //Debug.Log("End Game");
        }
    }

    void cashOut(){
        inv.sellAll();
        topText.text = inv.getInfo();
    }

    void AutoSell(){
        if(autoS){
            autoS = false;
        } else {
            autoS = true;
        }
    }
}