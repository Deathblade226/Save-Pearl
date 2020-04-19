using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DunGen;
using UnityEngine.AI;

public class RuntimeSetup : MonoBehaviour {

[SerializeField] DunGen.RuntimeDungeon dungeon = null;
[SerializeField] bool Debug = false;
[SerializeField] [Range(0,2)]int DebugDifficulty = 0;
[SerializeField] int Progress = 1;
[SerializeField] List<DunGen.Graph.DungeonFlow> difficulty = new List<DunGen.Graph.DungeonFlow>();

private void Awake() {
    if (!Debug) DebugDifficulty = Game.Instance.Data.Difficulty;
    if (!Debug) Progress = Game.Instance.Data.Progress;
    dungeon.Generator.DungeonFlow = difficulty[DebugDifficulty];
    dungeon.Generator.LengthMultiplier = (DebugDifficulty + 3) + Progress / 2;
}
private void Start() {
    dungeon.Generate();
}

}
