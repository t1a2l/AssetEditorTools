using System;
using AssetEditorTools.Panels.GroupContainer;
using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools.EditorControllers.GroupContainer
{
	public class SubServiceEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;

		private SubServicePanel subServicePanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			subServicePanel = m_view.FindUIComponent<SubServicePanel>("SubServicePanel");

			subServicePanel.m_subServiceApplyButton.eventClicked += ApplyNewSubService;

			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;
		}

		// Sets the Sub Service dropdown box to the currently loaded ItemClass Service.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			string checkSubService = null;
			BuildingInfo bi = info as BuildingInfo;
			if(bi != null) 
			{
				checkSubService = bi.m_class.m_subService.ToString();
			}
			NetInfo ni = info as NetInfo;
			if(ni != null) 
			{
				checkSubService = ni.m_class.m_subService.ToString();
			}
			PropInfo pi = info as PropInfo;
			if(pi != null) 
			{
				checkSubService = pi.m_class.m_subService.ToString();
			}
			TransportInfo ti = info as TransportInfo;
			if(ti != null) 
			{
				checkSubService = ti.m_class.m_subService.ToString();
			}
			TreeInfo tri = info as TreeInfo;
			if(tri != null) 
			{
				checkSubService = tri.m_class.m_subService.ToString();
			}
			VehicleInfo vi = info as VehicleInfo;
			if(vi != null) 
			{
				checkSubService = vi.m_class.m_subService.ToString();
			}
			subServicePanel.m_subServiceDropDown.selectedIndex = Array.IndexOf(subServicePanel.m_subServiceDropDown.items, checkSubService);
		}

		// Attempts to matches the selected item class sub service with the selected sub services.
		private void ApplyNewSubService(UIComponent component, UIMouseEventParameter eventParam) 
		{
			ref PrefabInfo info = ref m_toolController.m_editPrefabInfo;
			foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
			{
				foreach(ItemClass itemClass in collection.m_classes)
				{
					if(subServicePanel.m_subServiceDropDown.selectedValue == itemClass.m_subService.ToString())
					{
						BuildingInfo bi = info as BuildingInfo;
						if(bi != null) 
						{
							bi.m_class.m_subService = itemClass.m_subService;
						}
						NetInfo ni = info as NetInfo;
						if(ni != null) 
						{
							ni.m_class.m_subService = itemClass.m_subService;
						}
						PropInfo pi = info as PropInfo;
						if(pi != null) 
						{
							pi.m_class.m_subService = itemClass.m_subService;
						}
						TransportInfo ti = info as TransportInfo;
						if(ti != null) 
						{
							ti.m_class.m_subService = itemClass.m_subService;
						}
						TreeInfo tri = info as TreeInfo;
						if(tri != null) 
						{
							tri.m_class.m_subService = itemClass.m_subService;
						}
						VehicleInfo vi = info as VehicleInfo;
						if(vi != null) 
						{
							vi.m_class.m_subService = itemClass.m_subService;
						}
						return;
					}
				}

			}
			
		}

	}
}