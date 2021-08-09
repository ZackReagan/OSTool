using System.Collections.Generic;

namespace OSTool.Core
{
    public class ExpanderItemsListDesignModel : ExpanderItemsListViewModel
    {
        #region Singles

        public static ExpanderItemsListDesignModel Instance => new ExpanderItemsListDesignModel();

        #endregion

        public ExpanderItemsListDesignModel()
        {
            Items = new List<ExpanderViewModel>();
            Command = new BaseCommand(e => Test2(e as List<ExpanderViewModel>));

            for (int i = 0; i <= 10; i++)
            {
                Items.Add(new ExpanderViewModel
                {
                    Caption = "Caption" + i,
                    Info = "Info" + i,
                    ControlTag = "ctrl" + i.ToString(),
                    MainTag = "a" + i.ToString(),
                    SecondaryTag = "b" + i.ToString(),
                    ButtonCommand = new BaseCommand(e => Test(e as object[]))
                });
            }
        }
    }
}