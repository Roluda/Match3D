using UnityEngine;
using NinjaCactus.Gamelogic;

namespace NinjaCactus.Level {
    public abstract class Generator : ScriptableObject {
        public abstract Matchspace Generate();
    }
}
