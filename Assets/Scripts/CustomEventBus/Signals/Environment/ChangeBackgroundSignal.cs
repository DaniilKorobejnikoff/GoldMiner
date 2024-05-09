using UnityEngine;
namespace CustomEventBus.Signals {
    public class ChangeBackgroundSignal {
        public Sprite NewBackgroundSprite { get; private set; }

        public ChangeBackgroundSignal(Sprite newBackgroundSprite) {
            NewBackgroundSprite = newBackgroundSprite;
        }
    }
}