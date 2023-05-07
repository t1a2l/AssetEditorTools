using ColossalFramework.UI;

namespace AssetEditorTools
{
	public class ItemClassGroupContianerPanel : UIPanel
	{

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 360;

			var servicePanel = AddUIComponent<ServicePanel>();
			AttachUIComponent(servicePanel.gameObject);

			var subServicePanel = AddUIComponent<SubServicePanel>();
			AttachUIComponent(subServicePanel.gameObject);

			var levelPanel = AddUIComponent<LevelPanel>();
			AttachUIComponent(levelPanel.gameObject);

		}

	}
}
