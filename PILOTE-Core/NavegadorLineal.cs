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
		public class NavegadorLineal
		{

			public NavegadorLineal()
			{
				ListaPaginas = new List<Pagina>();
				PaginaActual = null;
			}


			public NavegadorLineal(List<Pagina> listaPaginas)
			{
				ListaPaginas = listaPaginas;
				PaginaActual = listaPaginas[0];
			}

			// Referencia a la pagina actual
			public Pagina PaginaActual { get; set; }
			// TODO: make private
			public List<Pagina> ListaPaginas { get; set; }

			// Avanza a la pagina siguiente, si es posible
			public void NextPage()
			{
				int ind = ListaPaginas.IndexOf(PaginaActual);
				if (ind + 1 < ListaPaginas.Count)
				{
					PaginaActual = ListaPaginas[ind + 1];
				}
			}

			// Retrocede a la pagina anterior, si es posible
			public void PreviousPage()
			{
				int ind = ListaPaginas.IndexOf(PaginaActual);
				if (ind - 1 > -1)
				{
					PaginaActual = ListaPaginas[ind - 1];
				}
			}

			// Inserta una pagina al final de la lista de paginas
			public void AppendPage(Pagina Page)
			{
				ListaPaginas.Add(Page);
			}

			// Set the page list
			public void SetPageset(List<Pagina> ListaPaginas_)
			{
				ListaPaginas = ListaPaginas_;
				PaginaActual = ListaPaginas_[0];
			}

		}
	}
}
