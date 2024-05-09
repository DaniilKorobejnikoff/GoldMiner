using CustomEventBus.Signals;
using UnityEngine;

namespace Interactables
{
    public class Bomb : Interactable
    {
        [SerializeField] private int _bombValue = 1;
        [SerializeField] private AudioClip _explosionSound;
        protected override void Interact()
        {
            _eventBus.Invoke(new PlayerDamagedSignal(_bombValue));
            ServiceLocator.Current.Get<AudioController>().PlaySoundEffect(SoundType.Explosion);
        }
    }
}