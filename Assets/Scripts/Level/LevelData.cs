using System.Collections.Generic;
using Interactables;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    /// <summary>
    /// Уникальный ID уровня
    /// </summary>
    [SerializeField] private int id;
    /// <summary>
    /// Длительность уровня в секундах
    /// </summary>
    [SerializeField] private float levelLength;
    /// <summary>
    /// Стартовая скорость падения препятствий и бонусов
    /// </summary>
    [SerializeField] private float startSpeed;
    /// <summary>
    /// Финальная скорость падения препятствий и бонусов
    /// </summary>
    [SerializeField] private float endSpeed;
    /// <summary>
    /// Количество голды за первое прохождение
    /// </summary>
    [SerializeField] private int goldForPass;
    /// <summary>
    /// Спрайт для фона уровня
    /// </summary>    
    [SerializeField] private Sprite _levelBackgroundSprite;
    /// <summary>
    /// Инфа о спавне препятствий и бонусов
    /// </summary>
    [SerializeField] private List<InteractableSpawnData> interactableSpawnData;



    public int ID => id;
    public float LevelLength => levelLength;
    public float StartSpeed => startSpeed;
    public float EndSpeed => endSpeed;
    public int GoldForPass => goldForPass;
    public Sprite LevelBackgroundSprite => _levelBackgroundSprite;
    public List<InteractableSpawnData> InteractableData => interactableSpawnData;
}