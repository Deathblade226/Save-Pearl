using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DunGen;

public class RuntimeSetup : MonoBehaviour {

[SerializeField] DunGen.RuntimeDungeon dungeon = null;
[SerializeField] [Range(0,2)]int DebugDifficulty = 0;
[SerializeField] List<DunGen.Graph.DungeonFlow> difficulty = new List<DunGen.Graph.DungeonFlow>();

private void Awake() {
    GameObject go = GameObject.FindGameObjectWithTag("GameController");
    Game game = go.GetComponent<Game>();
    if (game != null) { DebugDifficulty = game.Data.Difficulty; }
    dungeon.Generator.DungeonFlow = difficulty[DebugDifficulty];
    dungeon.Generator.LengthMultiplier = 5.0f;
}
private void Start() {
    dungeon.Generate();
}

}
