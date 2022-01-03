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
		public class PaginaSistemas : Pagina
		{
			public PaginaSistemas(List<IMyFunctionalBlock> ListaSistemas_, string titulo = "Sistemas", int AnchoColumnaNombre_ = 15, bool MostrarConsumos_ = false) : base(titulo)
			{
				ListaSistemas = ListaSistemas_;

				SistemaSeleccionado = ListaSistemas_[0];
				AnchoColumnaNombre = AnchoColumnaNombre_;
				MostrarConsumos = MostrarConsumos_;
			}

			private List<IMyFunctionalBlock> ListaSistemas { get; set; }
			private IMyFunctionalBlock SistemaSeleccionado { get; set; }
			public int AnchoColumnaNombre { get; set; }
			public bool MostrarConsumos { get; set; }

			public override void DibujarContenido(IMyTextSurface Display)
			{
				string selection_text = " ";
				string state_text = "X"; // X:apagado, O:encendido
				foreach (IMyFunctionalBlock sistema in ListaSistemas)
				{
					state_text = "X";
					if (sistema.Enabled)
						state_text = "O";

					selection_text = " ";
					if (SistemaSeleccionado == sistema)
						selection_text = ">";

					Display.WriteText(String.Format("{0,-2} {1,-" + AnchoColumnaNombre.ToString() + "} {2,2}\n", selection_text, sistema.DisplayNameText.ToString(), state_text), true);
				}
			}

			public void DownSelection()
			{
				int index = ListaSistemas.IndexOf(SistemaSeleccionado);
				if (index + 1 < ListaSistemas.Count)
				{
					SistemaSeleccionado = ListaSistemas[index + 1];
				}
			}

			public void UpSelection()
			{
				int index = ListaSistemas.IndexOf(SistemaSeleccionado);
				if (index - 1 > -1)
				{
					SistemaSeleccionado = ListaSistemas[index - 1];
				}
			}

			public void Select()
			{
				SistemaSeleccionado.Enabled = !SistemaSeleccionado.Enabled;
			}

			public override void HandleCommand(string Command)
			{
				switch (Command)
				{
					case "button_inpage_nav_back":
						UpSelection();
						break;
					case "button_inpage_nav_next":
						DownSelection();
						break;
					case "button_enter":
						Select();
						break;
					default:
						break;
				}
			}
		}
	}
}
