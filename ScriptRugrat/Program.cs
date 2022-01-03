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
		// FUNCIONES UTILITARIAS CORE
		// ************************************************************************************************


		// Devuelve la surface pedida del bloque indicado
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

		//==============================================================================================================
		//==============================================================================================================

		// ************************************************************************************************
		// CONSTANTES GLOBALES
		// ************************************************************************************************

		const string NombreAsientoControl = "Industrial Cockpit [Rugrat]";

		// ************************************************************************************************
		// VARIABLES GLOBALES
		// ************************************************************************************************

		IMyTextSurface displayPrincipal;
		ControladorPantalla miControladorPantalla;

		// ************************************************************************************************
		// FUNCIONES UTILITARIAS
		// ************************************************************************************************

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
				//Echo(GridTerminalSystem.GetBlockWithName(nombre).DisplayNameText.ToString());
			}

			return ListaBaterias;
		}

		public void program_setup1()
		{
			displayPrincipal = (IMyTextSurface)get_nice_screen(NombreAsientoControl, ScreenId: 1);
			set_monospace_font(NombreAsientoControl);

			List<Pagina> listaPaginas = new List<Pagina>();

			List<string> nombres_sistemas = new List<string>();

			nombres_sistemas.Add("Spotlight [Rugrat]");
			nombres_sistemas.Add("Ore Detector [Rugrat]");
			nombres_sistemas.Add("Antenna [Rugrat]");
			nombres_sistemas.Add("Gyroscope 1 [Rugrat]");
			nombres_sistemas.Add("Gyroscope 2 [Rugrat]");
			nombres_sistemas.Add("Gyroscope 3 [Rugrat]");
			listaPaginas.Add(new PaginaSistemas(cargar_datos_sistemas(nombres_sistemas)));

			List<string> nombres_rotores = new List<string>();
			nombres_rotores.Add("Advanced Rotor L [Rugrat]");
			nombres_rotores.Add("Advanced Rotor R [Rugrat]");
			listaPaginas.Add(new PaginaRotores(cargar_datos_rotores(nombres_rotores), AnchoColumnaNombre_: 10));

			/*
			List<string> nombres_baterias = new List<string>();
			nombres_baterias.Add("Battery 1 [Bisbal]");
			nombres_baterias.Add("Battery 2 [Bisbal]");
			nombres_baterias.Add("Battery 3 [Bisbal]");
			nombres_baterias.Add("Battery 4 [Bisbal]");
			nombres_baterias.Add("Battery 5 [Bisbal]");
			nombres_baterias.Add("Battery 6 [Bisbal]");
			listaPaginas.Add( new PaginaBaterias(cargar_datos_baterias(nombres_baterias), AnchoColumnaNombre_:10) );
			*/



			miControladorPantalla = new ControladorPantalla(displayPrincipal, listaPaginas);

		}

		// ************************************************************************************************
		// ESQUELETO PRINCIPAL DEL PROGRAMA
		// ************************************************************************************************

		public Program()
		{
			Runtime.UpdateFrequency = UpdateFrequency.Update10;

			program_setup1();
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

		public void Main(string argument, UpdateType updateSource)
		{

			// Si se llama a Main desde un trigger (boton-comando),
			// interpretar el comando y ejecutar el código correspondiente
			if ((updateSource & UpdateType.Trigger) != 0)
			{
				switch (argument)
				{
					// Añadir aqui los posibles comandos como casos

					case "button_previous_page": // Debe estar en el 1
						miControladorPantalla.Nav.PreviousPage();
						break;

					case "button_next_page": // Debe estar en el 2
						miControladorPantalla.Nav.NextPage();
						break;

					case "button_inpage_nav_back": // Debe estar en el 3
						miControladorPantalla.Nav.PaginaActual.HandleCommand(argument);
						break;

					case "button_enter": // Debe estar en el 4
						miControladorPantalla.Nav.PaginaActual.HandleCommand(argument);
						break;

					case "button_inpage_nav_next": // Debe estar en el 5
						miControladorPantalla.Nav.PaginaActual.HandleCommand(argument);
						break;

					default:
						break;
				}
			}


			// Si es un update periodico
			if ((updateSource & UpdateType.Update10) != 0)
			{
				miControladorPantalla.Actualizar();
			}


		}

	}
}
