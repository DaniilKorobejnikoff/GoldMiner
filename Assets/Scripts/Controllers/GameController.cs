﻿using CustomEventBus;
using CustomEventBus.Signals;
using UI;
using UI.Dialogs;

/// <summary>
/// Принимает решение о запуске и остановке игры
/// Уведомляет о старте и конце игры
/// </summary>
public class GameController : IService, IDisposable {
    private EventBus _eventBus;

    public void Init() {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
        _eventBus.Subscribe<LevelFinishedSignal>(LevelFinished);
        _eventBus.Subscribe<SetLevelSignal>(StartGame, -1);
    }

    public void StartGame(SetLevelSignal signal) {
        _eventBus.Invoke(new GameStartedSignal());
    }

    public void StopGame() {
        _eventBus.Invoke(new GameStopSignal());
    }

    private void OnPlayerDead(PlayerDeadSignal signal) {
        StopGame();

        // Вывести окно конца игры
        var scoreController = ServiceLocator.Current.Get<ScoreController>();
        YouLoseDialog youLoseDialog = DialogManager.ShowDialog<YouLoseDialog>();
        youLoseDialog.Init(scoreController.Score);
    }

    private void LevelFinished(LevelFinishedSignal signal) {
        StopGame();

        // Вывести окно победы
        var scoreController = ServiceLocator.Current.Get<ScoreController>();
        YouWinDialog youWinDialog = DialogManager.ShowDialog<YouWinDialog>();
        youWinDialog.Init(scoreController.Score);
    }

    public void Dispose() {
        _eventBus.Unsubscribe<PlayerDeadSignal>(OnPlayerDead);
        _eventBus.Unsubscribe<LevelFinishedSignal>(LevelFinished);
    }
}