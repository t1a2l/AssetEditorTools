using System;
using AssetEditorTools.Panels.GroupContainer;
using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools.EditorControllers.GroupContainer
{
	public class ServiceEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;

		private ServicePanel servicePanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			servicePanel = m_view.FindUIComponent<ServicePanel>("ServicePanel");

			servicePanel.m_serviceApplyButton.eventClicked += ApplyNewService;

			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;
		}

		// Sets the Service dropdown box to the currently loaded ItemClass service.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			string checkService = null;
			BuildingInfo bi = info as BuildingInfo;
			if(bi != null) 
			{
				checkService = bi.m_class.m_service.ToString();
			}
			NetInfo ni = info as NetInfo;
			if(ni != null) 
			{
				checkService = ni.m_class.m_service.ToString();
			}
			PropInfo pi = info as PropInfo;
			if(pi != null) 
			{
				checkService = pi.m_class.m_service.ToString();
			}
			TransportInfo ti = info as TransportInfo;
			if(ti != null) 
			{
				checkService = ti.m_class.m_service.ToString();
			}
			TreeInfo tri = info as TreeInfo;
			if(tri != null) 
			{
				checkService = tri.m_class.m_service.ToString();
			}
			VehicleInfo vi = info as VehicleInfo;
			if(vi != null) 
			{
				checkService = vi.m_class.m_service.ToString();
			}
			servicePanel.m_serviceDropDown.selectedIndex = Array.IndexOf(servicePanel.m_serviceDropDown.items, checkService);
		}

		// Attempts to matches the selected item class service with the selected service.
		private void ApplyNewService(UIComponent component, UIMouseEventParameter eventParam) 
		{
			ref PrefabInfo info = ref m_toolController.m_editPrefabInfo;
			foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
			{
				foreach(ItemClass itemClass in collection.m_classes)
				{
					if(servicePanel.m_serviceDropDown.selectedValue == itemClass.m_service.ToString())
					{
						BuildingInfo bi = info as BuildingInfo;
						if(bi != null) 
						{
							bi.m_class.m_service = itemClass.m_service;
						}
						NetInfo ni = info as NetInfo;
						if(ni != null) 
						{
							ni.m_class.m_service = itemClass.m_service;
						}
						PropInfo pi = info as PropInfo;
						if(pi != null) 
						{
							pi.m_class.m_service = itemClass.m_service;
						}
						TransportInfo ti = info as TransportInfo;
						if(ti != null) 
						{
							ti.m_class.m_service = itemClass.m_service;
						}
						TreeInfo tri = info as TreeInfo;
						if(tri != null) 
						{
							tri.m_class.m_service = itemClass.m_service;
						}
						VehicleInfo vi = info as VehicleInfo;
						if(vi != null) 
						{
							vi.m_class.m_service = itemClass.m_service;
						}
						return;
					}
				}

			}
			
		}

	}
}