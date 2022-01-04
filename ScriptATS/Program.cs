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

		const string NombreAsientoControl = "Cockpit [ATS]";
		const string NombreGiroscopio = "Gyroscope [ATS]";
		const string NombreBateria = "Battery [ATS]";
		const string NombreOreDetector = "Ore Detector [ATS]";

        // ************************************************************************************************
        // VARIABLES GLOBALES
        // ************************************************************************************************

        DispatcherLegacy Despachador;

		// ************************************************************************************************
		// FUNCIONES UTILITARIAS
		// ************************************************************************************************

		
		public List<IMyFunctionalBlock> cargar_datos_sistemas(List<string> ListaNombres)
		{
			List<IMyFunctionalBlock> ListaSistemas = new List<IMyFunctionalBlock>();
			foreach (string nombre in ListaNombres)
			{
				ListaSistemas.Add((IMyFunctionalBlock)GridTerminalSystem.GetBlockWithName(nombre));
			}

			return ListaSistemas;
		}


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

		// ************************************************************************************************
		// ESQUELETO PRINCIPAL DEL PROGRAMA
		// ************************************************************************************************

		public DispatcherLegacy program_setup_con_dispatcher(UpdateType tipo_actualizacion, IMyTextSurface display_principal)
		{
			List<Pagina> listaPaginas = new List<Pagina>();

			List<string> nombres_sistemas = new List<string>();
			nombres_sistemas.Add(NombreGiroscopio);
			nombres_sistemas.Add(NombreBateria);
			nombres_sistemas.Add(NombreOreDetector);
			listaPaginas.Add(new PaginaSistemas(cargar_datos_sistemas(nombres_sistemas)));

			listaPaginas.Add(new PaginaPruebaMensaje("", "Peso vacio: 18.4 T\nCap. Carga: 32 T"));

			return new DispatcherLegacy(new ControladorPantalla(display_principal, listaPaginas), tipo_actualizacion);
		}

		public Program()
		{
			// Especifico la frecuencia de actualización
			Runtime.UpdateFrequency = UpdateFrequency.Update10;

			// Accedo a la pantalla y la configuro
			IMyTextSurface display_principal = (IMyTextSurface)get_nice_screen(NombreAsientoControl, ScreenId: 1);
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

		public void Main(string argument, UpdateType updateSource)
		{
			Despachador.HandleMain(argument, updateSource);
		}

	}
}
