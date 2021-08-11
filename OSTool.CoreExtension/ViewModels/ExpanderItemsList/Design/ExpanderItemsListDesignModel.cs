using OSTool.Core;
using System.Collections.Generic;
using static OSTool.CoreExtension.MainMenuData;

namespace OSTool.CoreExtension
{
    public class ExpanderItemsListDesignModel : ExpanderItemsListViewModel
    {
        #region Singles

        public static ExpanderItemsListDesignModel Instance => new ExpanderItemsListDesignModel();

        #endregion

        public ExpanderItemsListDesignModel()
        {
            Items = new List<ExpanderViewModel>();
            Name = IoC.Get<ApplicationViewModel>().ButtonName;
            Command = new BaseCommand(e => ApplyChanges(e as List<ExpanderViewModel>));

            for (int i = 0; i <= Count(IoC.Get<ApplicationViewModel>().ButtonName); i++)
            {
                Items.Add(new ExpanderViewModel
                {
                    Caption = Load(Name, 0, i),
                    Info = Load(Name, 1, i),
                    ControlTag = "ctrl" + i,
                    MainContent = Load(Name, 2, i),
                    MainTag = "main" + i,
                    SecondaryContent = Load(Name, 3, i),
                    SecondaryVisibility = Visible(Name, i),
                    SecondaryTag = "secondary" + i,
                    ButtonCommand = new BaseCommand(e => CheckSelect(e as object[]))
                });
            }
        }
    }
}