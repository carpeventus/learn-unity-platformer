using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakeablePlatform : MonoBehaviour {
    public Vector2 offset;
    private Tilemap _tilemap;
    void Start() {
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Bullet")) {
            Vector2 point =col.bounds.ClosestPoint(col.transform.position);
            Vector2 left = new Vector2(point.x - offset.x, point.y);
            Vector2 top = new Vector2(point.x, point.y + offset.y);
            Vector2 right = new Vector2(point.x + offset.x, point.y);
            Vector2 down = new Vector2(point.x, point.y - offset.y);
      
            Vector3Int[] breakPoints = { _tilemap.WorldToCell(left), _tilemap.WorldToCell(top), _tilemap.WorldToCell(right), _tilemap.WorldToCell(down) };

            _tilemap.SetTiles(breakPoints, new TileBase[4]);
   
        }
    }
}
