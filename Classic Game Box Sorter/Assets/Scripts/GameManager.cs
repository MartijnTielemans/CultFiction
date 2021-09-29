using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SoundManager soundManager;

    [Space]
    [Header("Prefabs")]
    [SerializeField] GameObject packet;

    [Space]
    [Header("Game Stats")]
    public float gameSpeedMultiplier = 1; // This increases as boxes are sorted, causing it to go faster
    [SerializeField] float maxGameSpeed = 10; // The fastest that the game go

    [SerializeField] int points; // The amount of points the player has earned
    [SerializeField] int highScore; // The highest score achieved on this game

    private void Awake()
    {
        gameObject.tag = "GameManager";

        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Game speed cannot exceed the maximum
        if (gameSpeedMultiplier > maxGameSpeed)
        {
            gameSpeedMultiplier = maxGameSpeed;
        }
    }

    // Spawns a coworker, checks if there already is a packet on the location
    // If there is, game over, if not, spawn a packet
    void SpawnCoworker()
    {

    }

    void SpawnPacket(Vector2 position, int colorId, Color color)
    {
        GameObject go = Instantiate(packet, position, Quaternion.identity);
        go.GetComponent<Packet>().SetColor(colorId, color); // Set the color of the packet
    }

    // Adds points
    void AddPoints(int value)
    {
        points += value;

        if (points > highScore)
        {
            highScore = points;
        }
    }
}
