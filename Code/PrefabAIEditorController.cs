using System;
using ColossalFramework.UI;
using UnityEngine;
using AssetEditorTools.Utils;
using Object = UnityEngine.Object;

namespace AssetEditorTools
{
	public class PrefabAIEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;

		private PrefabAIPanel prefabAIPanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			prefabAIPanel = m_view.FindUIComponent<PrefabAIPanel>("PrefabAIPanel");

			prefabAIPanel.m_PrefabAIApplyButton.eventClicked += ApplyNewAI;

			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;
		}

		// Sets the ItemClass dropdown box and the UICategory dropdown box to the currently loaded prefab and category.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			prefabAIPanel.m_PrefabAIDropDown.selectedIndex = Array.IndexOf(prefabAIPanel.m_PrefabAIDropDown.items, info.GetAI().GetType().Name);
		}

		// Attempts to matches the selected item class name with an actual ItemClass.
		private void ApplyNewAI(UIComponent component, UIMouseEventParameter eventParam) 
		{
			ref PrefabInfo info = ref m_toolController.m_editPrefabInfo;
			if(info.GetAI().name != prefabAIPanel.m_PrefabAIDropDown.selectedValue)
			{
				var oldAI = info.GetComponent<PrefabAI>();
                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)info.gameObject.AddComponent(info.GetAI().GetType());
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
			}

		}

	}
}