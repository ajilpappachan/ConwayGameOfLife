using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCells : MonoBehaviour
{
    ParticleSystem ps;
    public int spawnRate = 3;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = ps.main;
        main.startSpeed = speed;
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.rateOverTime = spawnRate;
    }
}
