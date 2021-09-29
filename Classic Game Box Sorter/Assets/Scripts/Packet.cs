using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    public int colorId;

    // Sets the packet's color and colorId
    public void SetColor(int id, Color color)
    {
        colorId = id;
        sprite.color = color;
    }
}
