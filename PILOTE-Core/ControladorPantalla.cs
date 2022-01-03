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
		// Controla una pantalla utilizando un navegador de paginas
		public class ControladorPantalla
		{
			public ControladorPantalla(IMyTextSurface display)
			{
				Display = display;
				Nav = new NavegadorLineal();
			}

			public ControladorPantalla(IMyTextSurface display, List<Pagina> listaPaginas)
			{
				Display = display;
				Nav = new NavegadorLineal(listaPaginas);
			}

			// El display en el que se dibuja
			public IMyTextSurface Display { get; set; }
			public NavegadorLineal Nav { get; set; }


			// Dibuja la barra de titulo (donde también puede mostrarse otra informacion)
			private void DibujarBarraTitulo()
			{
				Display.WriteText(String.Format("{0,-10} {1,2:N0}\n", Nav.PaginaActual.Titulo, Nav.ListaPaginas.IndexOf(Nav.PaginaActual).ToString()));
			}

			// Actualiza el display redibujando la pagina actual
			public void Actualizar()
			{
				DibujarBarraTitulo();
				// Dibujar Nav.PaginaActual en Display
				Nav.PaginaActual.Dibujar(Display);
			}

		}

	}
}
