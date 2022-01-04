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
        public class DispatcherLegacy : Dispatcher
        {
            public DispatcherLegacy(ControladorPantalla Controlador, UpdateType ScreenUpdateFrequency, Dictionary<string, string> commandDictionary = null) : base(commandDictionary)
            {
                _controladorPantalla = Controlador;
                _screenUpdateFrequency = ScreenUpdateFrequency;
            }

            private ControladorPantalla _controladorPantalla;
            private UpdateType _screenUpdateFrequency;

            
            public override void HandleCommand(string command)
            {
                switch (command)
                {
                    case "button_previous_page": // Debe estar en el 1
                        _controladorPantalla.Nav.PreviousPage();
                        break;

                    case "button_next_page": // Debe estar en el 2
                        _controladorPantalla.Nav.NextPage();
                        break;

                    case "button_inpage_nav_back": // Debe estar en el 3
                        _controladorPantalla.Nav.PaginaActual.HandleCommand(command);
                        break;

                    case "button_enter": // Debe estar en el 4
                        _controladorPantalla.Nav.PaginaActual.HandleCommand(command);
                        break;

                    case "button_inpage_nav_next": // Debe estar en el 5
                        _controladorPantalla.Nav.PaginaActual.HandleCommand(command);
                        break;

                    default:
                        break;
                }
            }

            public override void HandleUpdate(UpdateType updateType)
            {
                if ((updateType & _screenUpdateFrequency) != 0)
                {
                    _controladorPantalla.Actualizar();
                }
            }

        }
    }
}
