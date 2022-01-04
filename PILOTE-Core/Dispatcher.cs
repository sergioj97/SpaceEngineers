using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {

        /*
         * Sus subclases deben implementar métodos para reaccionar a comandos y actualizaciones.
         */
        public abstract class Dispatcher
        {
            public Dispatcher(Dictionary<string,string> commandDictionary)
            {
                if (commandDictionary != null)
                {
                    _commandDictionary = commandDictionary;
                }
                else
                {
                    _commandDictionary = _generarDiccionarioPorDefecto();
                }
            }

            private Dictionary<string, string> _commandDictionary;
            private const string _unknownCommandString = "UNKNOWN_COMMAND";


            private Dictionary<string, string> _generarDiccionarioPorDefecto()
            {
                Dictionary<string, string> diccionario = new Dictionary<string, string>();
                diccionario.Add("button_previous_page", "button_previous_page");
                diccionario.Add("button_next_page", "button_next_page");
                diccionario.Add("button_inpage_nav_back", "button_inpage_nav_back");
                diccionario.Add("button_inpage_nav_next", "button_inpage_nav_next");
                diccionario.Add("button_enter", "button_enter");

                return diccionario;
            }

            // Reacciona a una ejecución de Main.
            public void HandleMain(string argument, UpdateType updateType)
            {
                if ((updateType & UpdateType.Trigger) != 0)
                {
                    HandleCommand(argument);
                }

                if ((updateType & (UpdateType.Update1 | UpdateType.Update10 | UpdateType.Update100)) != 0)
                {
                    HandleUpdate(updateType);
                }

            }

            public abstract void HandleCommand(string Command);
            public abstract void HandleUpdate(UpdateType updateType);
            public string TranslateCommand(string Command)
            {
                string valor;
                if (_commandDictionary.TryGetValue(Command, out valor))
                {
                    return valor;
                }
                else
                {
                    return _unknownCommandString;
                }
                
            }
        }
    }
}
