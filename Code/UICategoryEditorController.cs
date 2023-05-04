using System;
using System.Reflection;
using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools
{
	public class UICategoryEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;

		private UICategoryPanel UICategoryPanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			UICategoryPanel = m_view.FindUIComponent<UICategoryPanel>("UICategoryPanel");

			UICategoryPanel.m_UICategoryApplyButton.eventClicked += UICategorySelectedChanged;
		
			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;
		}

		// Sets the ItemClass dropdown box and the UICategory dropdown box to the currently loaded prefab and category.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			UICategoryPanel.m_UICategoryDropDown.selectedIndex = Array.IndexOf(UICategoryPanel.m_UICategoryDropDown.items, info.category);
		}

		// Attempts to shove the selected UICategory into the Prefab.
		private void UICategorySelectedChanged(UIComponent component, UIMouseEventParameter eventParam) 
		{
			UIDropDown dropdown = (UIDropDown) component;
			typeof(PrefabInfo).GetField("m_UICategory", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(m_toolController.m_editPrefabInfo, dropdown.selectedValue);
		}

	}
}