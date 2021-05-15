﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IAUS.NPCSO;
public class TestSpawn : MonoBehaviour
{
    public List<NPCSpawn> test;


    public void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 5);
    }



    void Spawn()
    {
        for (int i = 0; i < test.Count; i++)
        {
            if (test[i].SpawnSomething)
            {
                for (int j = 0; j < test[i].SpawnPerCall; j++)
                {
                    test[i].SOToSpawn.Spawn(this.transform.position);
                    test[i].spawned++;
                }
            }   
        }

    }
    [System.Serializable]
   public class NPCSpawn {
        public NPCSO SOToSpawn;
        [Range(1, 20)]
        public int SpawnPerCall;
        [Range(1, 2000)]
        public int spawnCNT;
        public int spawned;
        public bool SpawnSomething => spawned < spawnCNT;
    }
}
