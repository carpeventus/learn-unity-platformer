using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public static bool isGameAlive = true;
    public static bool isGamePause = false;
    
    
    public static bool isPlayerInteractiveCollider(Collider2D col) {
        return col.CompareTag("Player") && col.GetType() == typeof(PolygonCollider2D);
    }


    public static bool isGamePlay() {
        return isGameAlive && !isGamePause;
    }
    
    
}
