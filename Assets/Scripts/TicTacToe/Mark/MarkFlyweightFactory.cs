using UnityEngine;
using System.Collections.Generic;

public class MarkFlyweightFactory : MonoBehaviour
{
    [SerializeField] private List<MarkFlyweight> markFlyweights;
    private Dictionary<string, MarkFlyweight> flyweightCache = new Dictionary<string, MarkFlyweight>();

    private void Awake() {
        foreach (var flyweight in markFlyweights) {
            if (!flyweightCache.ContainsKey(flyweight.symbolIdentifier)) {
                flyweightCache.Add(flyweight.symbolIdentifier, flyweight);
            }
        }
    }

    public MarkFlyweight GetFlyweight(string symbol) {
        if (flyweightCache.TryGetValue(symbol, out var flyweight)) {
            return flyweight;
        }
        Debug.LogWarning($"Flyweight not found for symbol: {symbol}");
        return null;
    }
}
