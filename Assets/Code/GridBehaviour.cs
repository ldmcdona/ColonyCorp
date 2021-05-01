using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class GridBehaviour : MonoBehaviour
{
    public Space spacePrefab;
    public Space[] grid;
    public int holdingX, holdingY;

    // Start is called before the first frame update
    void Start()
    {
        holdingX = 4;
        holdingY = 8;
        grid = new Space[61];

        int x = 5, y = 0, c = 0;
        for(int i = 0; i < 9; i++){
            for(int j=0; j < x; j++){
                makeHex(j, i, c);
                c++;
            }
            if(y < 4){
                x++;
            } else {
                x--;
            }
            y++;
        }
        coordinates();
        distances(4, 4);
        SendMessageUpwards("tryNow");
    }

    public void makeHex(int x, int y, int i){
        Vector3 position;
        if(y < 5){
            position.x = (x * 6.8f) - (y * 3.4f);
        } else {
            position.x = (x * 6.8f) - ((8 % y) * 3.4f);
        }
        position.y = y * 5.75f;
        position.z = 0f;
        Space cell = grid[i] = Instantiate<Space>(spacePrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        
        Random.InitState((int)System.DateTime.Now.Ticks);
        int roll = Random.Range(1, 5);
        cell.planetCount = roll;
        cell.pSetup(roll);
        cell.distance = 100;
    }

    public void coordinates(){
        int x = 4, y = 8;
        for(int i=0; i < 61; i++){
            grid[i].coord(x, y);
            x--;
            y--;
            if(y < 0){
                x = holdingX;
                holdingY--;
                y = holdingY;
            }
            if(x < 0){
                holdingX++;
                x = holdingX;
                y = holdingY;
            }
        }
    }

    public void distances(int a, int b){
        int dx, dy, d;
        for(int i=0; i<61; i++){
            dx = a - grid[i].x;
            dy = b - grid[i].y;
            if((dx < 0 && dy < 0) || (dx > 0 && dy > 0)){
                d = Math.Max(Math.Abs(dx), Math.Abs(dy));
            } else {
                d = Math.Abs(dx) + Math.Abs(dy);
            }
            if(d < grid[i].distance){
                grid[i].distance = d;
            }
        }
    }
}
