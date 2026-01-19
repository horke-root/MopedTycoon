using UnityEngine;

public interface ISaveRepository
{
    public void Save<PlayerData>(string key, PlayerData data);
    public PlayerData Load<PlayerData>(string key);
}
