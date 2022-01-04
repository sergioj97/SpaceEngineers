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

            public abstract void HandleCommand(string command);
            public abstract void HandleUpdate(UpdateType updateType);
        }
    }
}
