using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Factory : Singleton<Factory> {
    [SerializeField] GameObject fishPrefab;
    [SerializeField] GameObject decorationPrefab;

    public Fish CreateFish(string name, Vector2 pos) {
        StatsDatabase.StatItem statItem = StatsDatabase.Items.Find(x => x.name == name);
        if (statItem == null) {
            Debug.LogError($"Unable to find fish data: {name}");
            return null;
        }
        
        Fish fish = Instantiate(fishPrefab, pos, Quaternion.identity).GetComponent<Fish>();
        fish.Init(statItem);
        Collider2D col = fish.AddComponent<CircleCollider2D>();
        col.isTrigger = true;

        return fish;
    }
    
    public Decoration CreateDeco(string name, Vector2 pos) {
        StatsDatabase.StatItem statItem = StatsDatabase.Items.Find(x => x.name == name);
        if (statItem == null) {
            Debug.LogError($"Unable to find decoration data: {name}");
            return null;
        }
        
        Decoration deco = Instantiate(decorationPrefab, pos, Quaternion.identity).GetComponent<Decoration>();
        deco.Init(statItem);
        Collider2D col = deco.AddComponent<CircleCollider2D>();
        col.isTrigger = true;

        return deco;
    }
}