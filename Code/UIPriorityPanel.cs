using ColossalFramework.UI;
using AssetEditorTools.UI;

namespace AssetEditorTools
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
			height = 25;

			m_UIPriorityTextField = UITextFields.AddLabelledTextField(this, 90, 20, "UIPriority", 200, 22, 1, 4, "Determines the horizontal position inside the group toolstrip.");

			gameObject.AddComponent<EditorController>();
		}

	}
}
