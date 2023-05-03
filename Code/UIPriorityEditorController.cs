using System.Reflection;
using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools
{
	public class UIPriorityEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;

		private UIPriorityPanel UIPriorityPanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			UIPriorityPanel = m_view.FindUIComponent<UIPriorityPanel>("UIPriorityPanel");
		
			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;

			UIPriorityPanel.m_UIPriorityTextField.eventTextChanged += new PropertyChangedEventHandler<string>(OnConfirmChange);

		}

		// Sets the ItemClass dropdown box and the UICategory dropdown box to the currently loaded prefab and category.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			UIPriorityPanel.m_UIPriorityTextField.objectUserData = info;
			UIPriorityPanel.m_UIPriorityTextField.stringUserData = "m_UIPriority";
			UIPriorityPanel.m_UIPriorityTextField.text = info.m_UIPriority.ToString();
		}

		//public class DecorationPropertiesPanel : ToolsModifierControl
		private void OnConfirmChange(UIComponent comp, string text) 
		{
			if (comp == null || comp.objectUserData == null) {
				return;
			}
			FieldInfo field = comp.objectUserData.GetType().GetField(comp.stringUserData, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (field.FieldType == typeof(int)) 
			{
				if (int.TryParse(text, out int num)) 
				{
					comp.color = Color.white;
					field.SetValue(comp.objectUserData, num);
				} 
				else 
				{
					comp.color = Color.red;
				}
			} 
			else if (field.FieldType == typeof(float)) 
			{
				if (float.TryParse(text, out float num2)) 
				{
					comp.color = Color.white;
					field.SetValue(comp.objectUserData, num2);
				} 
				else 
				{
					comp.color = Color.red;
				}
			}
		}


	}
}