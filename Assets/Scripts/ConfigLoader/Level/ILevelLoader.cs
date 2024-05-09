using System.Collections.Generic;

/// <summary>
/// Загрузчик уровней
/// </summary>
public interface ILevelLoader : IService, ILoader {
    public IEnumerable<LevelData> GetLevels();
}