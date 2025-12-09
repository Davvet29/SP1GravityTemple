using System;
using UnityEngine;

public class StickyTileScript : MonoBehaviour
{
    private GameObject StickyTile;

    private void Awake()
    {
        StickyTile.tag = "StickyTile";
    }
}
