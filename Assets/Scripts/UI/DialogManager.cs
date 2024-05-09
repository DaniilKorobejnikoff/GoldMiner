using System;
using System.Collections.Generic;
using UI.Dialogs;
using UnityEngine;

namespace UI {
    public class DialogManager {
        private const string PrefabsFilePath = "Dialogs/";

        // Все новые окно нужно вручную указать в этом словаре
        private static readonly Dictionary<Type, string> PrefabsDictionary = new Dictionary<Type, string>() {
            {typeof(YouLoseDialog),"YouLoseDialog"},
            {typeof(YouWinDialog),"YouWinDialog"},
            {typeof(MessageDialog),"MessageDialog"},
            {typeof(LoadingDialog),"LoadingDialog"},

            {typeof(MenuDialog),"MenuDialogs/MenuDialog"},
            {typeof(SettingsDialog),"MenuDialogs/SettingsDialog"},
        };

        public static T ShowDialog<T>() where T : Dialog {
            var go = GetPrefabByType<T>();
            if (go == null) {
                Debug.LogError("Show window - object not found");
                return null;
            }

            return GameObject.Instantiate(go, GuiHolder);
        }

        private static T GetPrefabByType<T>() where T : Dialog {
            var prefabName = PrefabsDictionary[typeof(T)];
            if (string.IsNullOrEmpty(prefabName)) {
                Debug.LogError("Cant find prefab type of " + typeof(T) + "Do you added it in PrefabsDictionary?");
            }

            var path = PrefabsFilePath + PrefabsDictionary[typeof(T)];
            var dialog = Resources.Load<T>(path);
            if (dialog == null) {
                Debug.LogError("Cant find prefab at path " + path);
            }

            return dialog;
        }

        /// <summary>
        /// Ссылка на канвас, который будет родительским для всех создаваемых окон
        /// </summary>
        public static Transform GuiHolder {
            get { return ServiceLocator.Current.Get<GUIHolder>().transform; }
        }
    }
}