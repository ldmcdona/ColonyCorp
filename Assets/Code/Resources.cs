using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int red, blue, green, yellow, white, cash, points;
    public int purple, lime, black;
    // Start is called before the first frame update
    void Start()
    {
        red = blue = green = yellow = white = 0; //purple = lime = black = 0;
        purple = lime = black = 1; //for testing reasons
        cash = 3000;
        SendMessageUpwards("display");
        points = 0;
    }

    //Purple, Lime, and Black are going to be refined resources, needing buildings to produce. 

    public void pointChange(int p){
        points += p;
    }

    public void Gain(int[] p){
        red += p[0];
        blue += p[1];
        green += p[2];
        yellow += p[3];
        white += p[4];
        purple += p[5];
        lime += p[6];
        black += p[7];
        cash += p[8];
    }

    public bool canAfford(int[] c){
        int consolidated = 0;
        consolidated += c[8];
        if(c[5] > purple){
            return false;
        }
        if(c[6] > lime){
            return false;
        }
        if(c[7] > black){
            return false;
        }
        if(c[0] > red){
            consolidated += (100 * (c[0] - red)); //red is less than or equal to c[0], so this won't cause an error (I think).
        }
        if(c[1] > blue){
            consolidated += (100 * (c[1] - blue));
        }
        if(c[2] > green){
            consolidated += (250 * (c[2] - green));
        }
        if(c[3] > yellow){
            consolidated += (250 * (c[3] - yellow));
        }
        if(c[4] > white){
            consolidated += (500 * (c[4] - white));
        }
        if(cash >= consolidated){
            return true;
        } else {
            return false;
        }
    }

    public bool canSpend(int[] c){
        if(red >= c[0] && blue >= c[1] && green >= c[2] && yellow >= c[3] && white >= c[4] && purple >= c[5] && lime >= c[6] && black >= c[7] && cash >= c[8]){
            return true;
        } else {
            return false;
        }
    }

    public void Spend(int[] c){
        int consolidated = 0;
        consolidated += c[8];
        if(c[0] > red){
            consolidated += (100 * (c[0] - red));
            red = 0;
        } else {
            red -= c[0];
        }
        if(c[1] > blue){
            consolidated += (100 * (c[1] - blue));
            blue = 0;
        } else {
            blue -= c[1];
        }
        if(c[2] > green){
            consolidated += (250 * (c[2] - green));
            green = 0;
        } else {
            green -= c[2];
        }
        if(c[3] > yellow){
            consolidated += (250 * (c[3] - yellow));
            yellow = 0;
        } else {
            yellow -= c[3];
        }
        if(c[4] > white){
            consolidated += (500 * (c[4] - white));
            white = 0;
        } else {
            white -= c[4];
        }
        cash -= consolidated;
        purple -= c[5];
        lime -= c[6];
        black -= c[7];
    }

    public string getInfo(){
        return "Cash: "+cash+" Red: "+red+" Blue: "+blue+" Green: "+green+" Yellow: "+yellow+" White: "+white+" Purple: "+purple+" Lime: "+lime+" Black: "+black;
    }

    public void sellAll(){
        cash += (75 * red);
        red = 0;
        cash += (75 * blue);
        blue = 0;
        cash += (200 * green);
        green = 0;
        cash += (200 * yellow);
        yellow = 0;
        cash += (400 * white);
        white = 0;
        cash += (225 * purple);
        purple = 0;
        cash += (600 * lime);
        lime = 0;
        cash += (1200 * black);
        black = 0;
    }

    public void market(string n){
        int[] x = {0, 0, 0, 0, 0, 0, 0, 0, 0};
        int[] y = {0, 0, 0, 0, 0, 0, 0, 0, 0};

        if(n == "BuyR"){
            x[8] = 100;
            y[0] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellR"){
            x[0] = 1;
            y[8] = 75;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "BuyB"){
            x[8] = 100;
            y[1] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellB"){
            x[1] = 1;
            y[8] = 75;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "BuyG"){
            x[8] = 250;
            y[2] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellG"){
            x[2] = 1;
            y[8] = 200;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "BuyY"){
            x[8] = 250;
            y[3] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellY"){
            x[3] = 1;
            y[8] = 200;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "BuyW"){
            x[8] = 500;
            y[4] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellW"){
            x[4] = 1;
            y[8] = 400;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellP"){
            x[5] = 1;
            y[8] = 225;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellL"){
            x[6] = 1;
            y[8] = 600;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellBl"){
            x[7] = 1;
            y[8] = 1200;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        }
    }
}