using System;
using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools
{
	public class LevelEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;

		private LevelPanel levelPanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			levelPanel = m_view.FindUIComponent<LevelPanel>("LevelPanel");

			levelPanel.m_levelApplyButton.eventClicked += ApplyNewLevel;

			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;
		}

		// Sets the Level dropdown box to the currently loaded ItemClass level.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			string checkLevel = null;
			BuildingInfo bi = info as BuildingInfo;
			if(bi != null) 
			{
				checkLevel = bi.m_class.m_level.ToString();
			}
			NetInfo ni = info as NetInfo;
			if(ni != null) 
			{
				checkLevel = ni.m_class.m_level.ToString();
			}
			PropInfo pi = info as PropInfo;
			if(pi != null) 
			{
				checkLevel = pi.m_class.m_level.ToString();
			}
			TransportInfo ti = info as TransportInfo;
			if(ti != null) 
			{
				checkLevel = ti.m_class.m_level.ToString();
			}
			TreeInfo tri = info as TreeInfo;
			if(tri != null) 
			{
				checkLevel = tri.m_class.m_level.ToString();
			}
			VehicleInfo vi = info as VehicleInfo;
			if(vi != null) 
			{
				checkLevel = vi.m_class.m_level.ToString();
			}
			levelPanel.m_levelDropDown.selectedIndex = Array.IndexOf(levelPanel.m_levelDropDown.items, checkLevel);
		}

		// Attempts to matches the selected item class level with the selected level.
		private void ApplyNewLevel(UIComponent component, UIMouseEventParameter eventParam) 
		{
			ref PrefabInfo info = ref m_toolController.m_editPrefabInfo;
			foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
			{
				foreach(ItemClass itemClass in collection.m_classes)
				{
					if(levelPanel.m_levelDropDown.selectedValue == itemClass.m_level.ToString())
					{
						BuildingInfo bi = info as BuildingInfo;
						if(bi != null) 
						{
							bi.m_class.m_level = itemClass.m_level;
						}
						NetInfo ni = info as NetInfo;
						if(ni != null) 
						{
							ni.m_class.m_level = itemClass.m_level;
						}
						PropInfo pi = info as PropInfo;
						if(pi != null) 
						{
							pi.m_class.m_level = itemClass.m_level;
						}
						TransportInfo ti = info as TransportInfo;
						if(ti != null) 
						{
							ti.m_class.m_level = itemClass.m_level;
						}
						TreeInfo tri = info as TreeInfo;
						if(tri != null) 
						{
							tri.m_class.m_level = itemClass.m_level;
						}
						VehicleInfo vi = info as VehicleInfo;
						if(vi != null) 
						{
							vi.m_class.m_level = itemClass.m_level;
						}
						return;
					}
				}

			}
			
		}

	}
}