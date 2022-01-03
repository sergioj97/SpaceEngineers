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
		public class PaginaBaterias : Pagina
		{
			public PaginaBaterias(List<IMyBatteryBlock> ListaBaterias_, string titulo = "Baterias", int AnchoColumnaNombre_ = 15) : base(titulo)
			{
				ListaBaterias = ListaBaterias_;
				AnchoColumnaNombre = AnchoColumnaNombre_;
			}

			private List<IMyBatteryBlock> ListaBaterias { get; set; }
			public int AnchoColumnaNombre { get; set; }

			public override void DibujarContenido(IMyTextSurface Display)
			{
				string state_text = "X"; // X:apagado, O:encendido
				foreach (IMyBatteryBlock bateria in ListaBaterias)
				{
					state_text = "X";
					if (bateria.Enabled)
						state_text = "O";


					Display.WriteText(String.Format("{0,-" + AnchoColumnaNombre.ToString() + "} {1,2}    {2,4}\n", bateria.DisplayNameText.ToString(), state_text, ((bateria.CurrentStoredPower / bateria.MaxStoredPower) * 100).ToString("##.##")), true);
				}
			}
		}
	}
}
