using ColossalFramework.UI;
using AssetEditorTools.UI;
using AssetEditorTools.EditorControllers;

namespace AssetEditorTools.Panels
{
	public class UIPriorityPanel : UIPanel
	{
		public UITextField m_UIPriorityTextField;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 35;

			m_UIPriorityTextField = UITextFields.AddLabelledTextField(this, 90.0f, 0f, "UIPriority", 90.0f, 20.0f, 1, 4, "Determines the horizontal position inside the group toolstrip.");

			gameObject.AddComponent<UIPriorityEditorController>();
		}

	}
}
