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
    partial class Program : MyGridProgram
    {
		// This file contains your actual script.
		//
		// You can either keep all your code here, or you can create separate
		// code files to make your program easier to navigate while coding.
		//
		// In order to add a new utility class, right-click on your project, 
		// select 'New' then 'Add Item...'. Now find the 'Space Engineers'
		// category under 'Visual C# Items' on the left hand side, and select
		// 'Utility Class' in the main area. Name it in the box below, and
		// press OK. This utility class will be merged in with your code when
		// deploying your final script.
		//
		// You can also simply create a new utility class manually, you don't
		// have to use the template if you don't want to. Just do so the first
		// time to see what a utility class looks like.
		// 
		// Go to:
		// https://github.com/malware-dev/MDK-SE/wiki/Quick-Introduction-to-Space-Engineers-Ingame-Scripts
		//
		// to learn more about ingame scripts.


		// ************************************************************************************************
		// CONSTANTES GLOBALES
		// ************************************************************************************************

		const string NombreAsientoControl = "Flight Seat [Bisbal]";

		// ************************************************************************************************
		// VARIABLES GLOBALES
		// ************************************************************************************************

		DispatcherLegacy Despachador;

		// ************************************************************************************************
		// FUNCIONES UTILITARIAS
		// ************************************************************************************************
		public IMyTextSurface get_nice_screen(string BlockName, int ScreenId = 0)
		{
			IMyTextSurfaceProvider tsp_cabina = (IMyTextSurfaceProvider)GridTerminalSystem.GetBlockWithName(BlockName);
			return tsp_cabina.GetSurface(ScreenId);
		}


		// Pone la fuente a monospace en las surfaces del bloque indicado (para que las cosas queden bien alineadas)
		public void set_monospace_font(string BlockName)
		{
			IMyTerminalBlock bloque = (IMyTerminalBlock)GridTerminalSystem.GetBlockWithName(BlockName);
			bloque.SetValue<long>("Font", 1147350002);
		}

		public List<IMyMotorStator> cargar_datos_rotores(List<string> ListaNombres)
		{
			List<IMyMotorStator> ListaRotores = new List<IMyMotorStator>();
			foreach (string nombre in ListaNombres)
			{
				ListaRotores.Add((IMyMotorStator)GridTerminalSystem.GetBlockWithName(nombre));
			}

			return ListaRotores;
		}

		public List<IMyFunctionalBlock> cargar_datos_sistemas(List<string> ListaNombres)
		{
			List<IMyFunctionalBlock> ListaSistemas = new List<IMyFunctionalBlock>();
			foreach (string nombre in ListaNombres)
			{
				ListaSistemas.Add((IMyFunctionalBlock)GridTerminalSystem.GetBlockWithName(nombre));
			}

			return ListaSistemas;
		}

		public List<IMyBatteryBlock> cargar_datos_baterias(List<string> ListaNombres)
		{
			List<IMyBatteryBlock> ListaBaterias = new List<IMyBatteryBlock>();
			foreach (string nombre in ListaNombres)
			{
				ListaBaterias.Add((IMyBatteryBlock)GridTerminalSystem.GetBlockWithName(nombre));
			}

			return ListaBaterias;
		}

		public DispatcherLegacy program_setup_con_dispatcher(UpdateType tipo_actualizacion, IMyTextSurface display_principal)
        {
			// Creo la lista de páginas para el dispatcher
			List<Pagina> listaPaginas = new List<Pagina>();

			// Creo los "widgets"
			List<string> nombres_sistemas = new List<string>();
			nombres_sistemas.Add("Gyroscope [Bisbal]");
			nombres_sistemas.Add("O2/H2 Generator [Bisbal]");
			nombres_sistemas.Add("Refinery [Bisbal]");
			listaPaginas.Add(new PaginaSistemas(cargar_datos_sistemas(nombres_sistemas)));

			List<string> nombres_rotores = new List<string>();
			nombres_rotores.Add("L Rotor [Bisbal]");
			nombres_rotores.Add("R Rotor [Bisbal]");
			listaPaginas.Add(new PaginaRotores(cargar_datos_rotores(nombres_rotores), AnchoColumnaNombre_: 10));

			List<string> nombres_baterias = new List<string>();
			nombres_baterias.Add("Battery 1 [Bisbal]");
			nombres_baterias.Add("Battery 2 [Bisbal]");
			nombres_baterias.Add("Battery 3 [Bisbal]");
			nombres_baterias.Add("Battery 4 [Bisbal]");
			nombres_baterias.Add("Battery 5 [Bisbal]");
			nombres_baterias.Add("Battery 6 [Bisbal]");
			listaPaginas.Add(new PaginaBaterias(cargar_datos_baterias(nombres_baterias), AnchoColumnaNombre_: 10));

			// Creo el dispatcher
			return new DispatcherLegacy(new ControladorPantalla(display_principal, listaPaginas), tipo_actualizacion);
		}

		// ************************************************************************************************
		// ESQUELETO PRINCIPAL DEL PROGRAMA
		// ************************************************************************************************

		public Program()
		{
			// Especifico la frecuencia de actualización
			Runtime.UpdateFrequency = UpdateFrequency.Update10;

			// Accedo a la pantalla y la configuro
			IMyTextSurface display_principal = (IMyTextSurface)get_nice_screen(NombreAsientoControl);
			set_monospace_font(NombreAsientoControl);


			Despachador = program_setup_con_dispatcher(UpdateType.Update10, display_principal);
		}


		public void Save()
		{
			// Called when the program needs to save its state. Use
			// this method to save your state to the Storage field
			// or some other means. 
			// 
			// This method is optional and can be removed if not
			// needed. 
		}

		public void Main(string argument, UpdateType updateType)
		{
			Despachador.HandleMain(argument, updateType);
		}
	}
}
