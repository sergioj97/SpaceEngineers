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
		public class PaginaRotores : Pagina
		{
			public PaginaRotores(List<IMyMotorStator> ListaRotores_, string titulo = "Rotores", int AnchoColumnaNombre_ = 15) : base(titulo)
			{
				ListaRotores = ListaRotores_;
				ShowToggle = false;
				AnchoColumnaNombre = AnchoColumnaNombre_;
			}

			public List<IMyMotorStator> ListaRotores { get; set; }
			private bool ShowToggle;
			public int AnchoColumnaNombre { get; set; }

			public override void DibujarContenido(IMyTextSurface Display)
			{
				bool lok = false;
				string angel;
				string unlocked_text;

				foreach (IMyMotorStator rot in ListaRotores)
				{
					lok = rot.RotorLock;
					angel = MathHelper.ToDegrees(rot.Angle).ToString("f2");
					unlocked_text = "";

					if (!lok & ShowToggle)
					{
						unlocked_text = "UNLOCKED";
					}

					Display.WriteText(String.Format("{0,6} {2,-" + AnchoColumnaNombre.ToString() + "} {1,10:N0}\n", angel, unlocked_text, rot.DisplayNameText.ToString()), true);
				}

				// Flip show_toggle
				ShowToggle = !ShowToggle;
			}
		}
	}
}
