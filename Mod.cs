using ICities;
using System;
using AssetEditorTools.Utils;
using ColossalFramework.UI;

namespace AssetEditorTools
{
    /// <summary>
    /// AI Changer main mod class; provides Mod Info and handles loading
    /// </summary>
    public class Mod : LoadingExtensionBase, IUserMod
    {
        public string Name => "Asset Editor Tools";

        public string Description => "Allows you to change the PrefabAI, UICategory, UIPriority and ItemClass in the Asset Editor. including building, vehicle, citizen, network etc.";
    
        public override void OnLevelLoaded(LoadMode mode) 
        {
			try 
            {
				if (mode == LoadMode.LoadAsset || mode == LoadMode.NewAsset) 
                {
					var view = UIView.GetAView();
			        var FullScreenContainer = view.FindUIComponent("FullScreenContainer");
			        var DecorationProperties = FullScreenContainer.Find<UIPanel>("DecorationProperties");
			        var Container = DecorationProperties.Find<UIScrollablePanel>("Container");

			        var itemClassPanel = Container.AddUIComponent<ItemClassPanel>();
                    Container.AttachUIComponent(itemClassPanel.gameObject);

                    var uICategoryPanel = Container.AddUIComponent<UICategoryPanel>();
                    Container.AttachUIComponent(uICategoryPanel.gameObject);

                    var aIPanel = Container.AddUIComponent<AIPanel>();
                    Container.AttachUIComponent(aIPanel.gameObject);

                    Container.AddUIComponent<UIPriorityPanel>();
                    Container.AddUIComponent<ShowPropertiesPanel>();
                    Container.AddUIComponent<SpritePanel>();
				}
			} 
            catch (Exception ex) 
            {
                LogHelper.Error(ex.Message);
                LogHelper.Error("Asset Item Class Changer caused an error");
			}
		}
    }

}