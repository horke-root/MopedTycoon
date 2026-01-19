using System.Threading.Tasks;
using UnityEngine;

public interface ISaveRepository
{
    public void Save(PlayerData data);
    public PlayerData Load();
}
