using CustomEventBus.Signals;
using UnityEngine;

namespace Interactables
{
    public class Coin : Interactable
    {
        [SerializeField] private int _coinValue = 5;
        [SerializeField] private AudioClip _coinSound;
        protected override void Interact()
        {
            _eventBus.Invoke(new AddScoreSignal(_coinValue));
            ServiceLocator.Current.Get<AudioController>().PlaySoundEffect(SoundType.CoinPickup);
        }
    }
}