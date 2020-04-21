using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {
[SerializeField] float range = 5.0f;
[SerializeField] bool Debug = false;
private void Update() {
    GameObject player = AIUtilities.GetNearestGameObject(gameObject, "Player", range, 180.0f, true);
    if (player != null) {
    
    Game.Instance.Data.Progress++;
    SaveSystem.SaveObject(Game.Instance.Data, $"SavePearlFile{Game.Instance.FileNumber + 1}GameData.gme");
    SceneManager.LoadScene("Game");
    }    
}
void OnDrawGizmosSelected() {
    // Draw a yellow sphere at the transform's position
    if (Debug) { 
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, range);
    }
}

}
