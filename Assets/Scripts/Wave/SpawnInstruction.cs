using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefense.Level
{
    /// <summary>
    /// Serializable class for specifying properties of spawning an agent
    /// </summary>
    [Serializable]
    public class SpawnInstruction
    {
        /// <summary>
        /// The agent to spawn - i.e. the monster for the wave
        /// </summary>
        //public AgentConfiguration agentConfiguration;

        /// <summary>
        /// The delay from the previous spawn until when this agent is spawned
        /// </summary>
        [Tooltip("The delay from the previous spawn until when this agent is spawned")]
        public float delayToSpawn;
    }
}
