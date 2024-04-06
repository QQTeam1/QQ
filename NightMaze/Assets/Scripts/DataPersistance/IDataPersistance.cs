using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IDataPersistance
{
    void LoadData(GameData gameData);
    void SaveData(ref GameData gameData);
}
