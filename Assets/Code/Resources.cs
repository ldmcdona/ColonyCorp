using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int red, blue, green, yellow, white, cash;
    // Start is called before the first frame update
    void Start()
    {
        red = blue = green = yellow = white = 0;
        cash = 2000;
        SendMessageUpwards("display");
    }

    public void Gain(int[] p){
        red += p[0];
        blue += p[1];
        green += p[2];
        yellow += p[3];
        white += p[4];
        cash += p[5];
    }

    public bool canAfford(int[] c){
        int consolidated = 0;
        consolidated += c[5];
        if(c[0] > red){
            consolidated += (100 * (c[0] - red));
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
        if(cash > consolidated){
            return true;
        } else {
            return false;
        }
    }

    public bool canSpend(int[] c){
        if(red >= c[0] && blue >= c[1] && green >= c[2] && yellow >= c[3] && white >= c[4] && cash >= c[5]){
            return true;
        } else {
            return false;
        }
    }

    public void Spend(int[] c){
        int consolidated = 0;
        consolidated += c[5];
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
    }

    public string getInfo(){
        return "Cash: " + cash + " Red: " + red + " Blue: " + blue + " Green: " + green + " Yellow: " + yellow + " White: " + white;
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
    }

    public void market(string n){
        int[] x = {0, 0, 0, 0, 0, 0};
        int[] y = {0, 0, 0, 0, 0, 0};

        if(n == "BuyR"){
            x[5] = 100;
            y[0] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellR"){
            x[0] = 1;
            y[5] = 75;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "BuyB"){
            x[5] = 100;
            y[1] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellB"){
            x[1] = 1;
            y[5] = 75;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "BuyG"){
            x[5] = 250;
            y[2] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellG"){
            x[2] = 1;
            y[5] = 200;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "BuyY"){
            x[5] = 250;
            y[3] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellY"){
            x[3] = 1;
            y[5] = 200;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "BuyW"){
            x[5] = 500;
            y[4] = 1;
            if(canAfford(x)){
                Spend(x);
                Gain(y);
            }
        } else if(n == "SellW"){
            x[4] = 1;
            y[5] = 400;
            if(canSpend(x)){
                Spend(x);
                Gain(y);
            }
        }
    }
}
