using System;
using OSTool.Core;
using System.Linq;
using System.Collections.Generic;
using static OSTool.Core.StringOrInt;
using static OSTool.Core.XmlReadWrite;
using static OSTool.CoreExtension.MainMenuData;

namespace OSTool.CoreExtension
{
    public class ExpanderItemsListViewModel : BaseViewModel
    {
        #region Private

        #region Design

        private List<ExpanderViewModel> wItems;

        private object wName;

        #endregion

        #region Changes Check

        private bool Completed;

        private bool RevertRun;

        private bool ApplyRun;

        private bool Partial;

        private bool Revert;

        private bool Apply;

        #endregion

        #endregion

        #region Commands

        public BaseCommand Command { get; set; }

        #endregion

        #region Public

        public List<ExpanderViewModel> Items
        {
            get { return wItems; }

            set
            {
                if (wItems == value)
                    return;

                wItems = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public object Name
        {
            get { return wName; }

            set
            {
                if (value == wName)
                    return;

                wName = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        #endregion

        #region Voids

        public void Select(object[] multi)
        {
            List<ExpanderViewModel> expander = multi[0] as List<ExpanderViewModel>;

            for (int i = 0; i <= expander.Count - 1; i++)
            {
                if (expander[i].ControlTag == multi[1])
                {
                    if (expander[i].MainTag == multi[2])
                    {
                        expander[i].MainSelected = multi[3];
                    }
                    if (expander[i].SecondaryTag == multi[2])
                    {
                        expander[i].SecondarySelected = multi[3];
                    }
                }
            }
        }

        public void Changes(List<ExpanderViewModel> expander)
        {
            try
            {
                #region Filtered Lists

                #region Selected Expanders List

                List<ExpanderViewModel> select = expander.Where(a => a.MainSelected.ToString() == "True" || a.SecondarySelected.ToString() == "True").ToList();

                #endregion

                #region Xml Revert List

                List<string> xml = List(Name.ToString(), Name.ToString() != "Services" ? "True" : ":Disabled", XmlListEnums.Value);

                #endregion

                #region Main & Secondary Revert Lists

                List<string> main = xml.Where(a => select.Any(b => a.Contains(Name.ToString() != "Services" ? b.MainContent.ToString() : b.Caption.ToString()))).ToList();

                List<string> secondary = xml.Where(a => select.Any(b => a.Contains(b.SecondaryContent.ToString()))).ToList();

                #endregion

                #region Apply & Revert Lists

                List<string[]> apply = new List<string[]>();

                List<string[]> revert = new List<string[]>();

                #endregion

                #endregion

                #region Select Check

                if (select.Count == 0)
                {
                    Dialog.Show("Please select an option");
                }

                #endregion

                #region Filter

                else
                {
                    for (int i = 0; i <= select.Count - 1; i++)
                    {
                        #region Main

                        if (select[i].MainSelected.ToString() == "True")
                        {
                            #region Revert Main

                            if (main.Contains(Name.ToString() != "Services" ? select[i].MainContent.ToString() : select[i].Caption.ToString()))
                            {
                                revert.Add(new string[] { Split(select[i].MainTag.ToString())[0], Split(select[i].MainTag.ToString())[1], Name.ToString() != "Services" ? select[i].MainContent.ToString() : select[i].Caption.ToString() });
                                Revert = true;
                            }

                            #endregion

                            #region Apply Main

                            else
                            {
                                if (select[i].MainSelected.ToString() == "True")
                                {
                                    apply.Add(new string[] { Split(select[i].MainTag.ToString())[0], Split(select[i].MainTag.ToString())[1], Name.ToString() != "Services" ? select[i].MainContent.ToString() : select[i].Caption.ToString() });
                                    Apply = true;
                                }
                            }

                            #endregion
                        }

                        #endregion

                        #region Secondary

                        if (select[i].SecondarySelected.ToString() == "True")
                        {
                            #region Revert Secondary

                            if (secondary.Contains(select[i].SecondaryContent.ToString()))
                            {
                                revert.Add(new string[] { Split(select[i].SecondaryTag.ToString())[0], Split(select[i].SecondaryTag.ToString())[1], select[i].SecondaryContent.ToString() });
                                Revert = true;
                            }

                            #endregion

                            #region Apply Secondary

                            else
                            {
                                if (select[i].SecondarySelected.ToString() == "True")
                                {
                                    apply.Add(new string[] { Split(select[i].SecondaryTag.ToString())[0], Split(select[i].SecondaryTag.ToString())[1], select[i].SecondaryContent.ToString() });
                                    Apply = true;
                                }
                            }

                            #endregion
                        }

                        #endregion
                    }
                }
                

                #endregion

                #region Apply/Revert Dialog

                #region Partial Check

                if (Apply && Revert)
                {
                    Partial = true;
                }

                #endregion

                if (Apply || Partial)
                {
                    if (Dialog.Show("Would you like to apply changes?", Partial ? "This won't apply all values selected since you've already applied some of them before." : null, DialogBoxType.YesNo) == DialogBoxResult.Yes)
                    {
                        ApplyRun = true;
                    }
                }

                if (Revert || Partial)
                {
                    if (Dialog.Show("Would you like to revert changes?", Partial ? "This won't revert the values you've just applied, only the ones you previously applied." : "It seems that you've applied this value before.", DialogBoxType.YesNo) == DialogBoxResult.Yes)
                    {
                        RevertRun = true;
                    }
                }

                #endregion

                #region Apply Changes

                if (Apply && ApplyRun)
                {
                    foreach (var item in apply)
                    {
                        Execute(Name.ToString(), item[0], Convert.ToInt32(item[1]), item[2]);
                        Completed = true;
                    }
                }

                #endregion

                #region Revert Changes

                if (Revert && RevertRun)
                {
                    foreach (var item in revert)
                    {
                        Execute(Name.ToString(), item[0], Convert.ToInt32(item[1]), item[2], 1);
                        Completed = true;
                    }
                }

                #endregion

                #region Apply/Revert Completion Dialog

                #region Completed Check

                if (ApplyRun && !Completed || RevertRun && !Completed)
                {
                    Dialog.Show("Changes weren't applied and/or reverted", "Please contact the creator if you see this message at EverythingTech#1898 on Discord.");
                }

                #endregion

                #region Non-Partial Dialog

                if (!Partial)
                {
                    if (Apply && Completed)
                    {
                        Dialog.Show("Changes were applied!");
                    }

                    else if (Apply)
                    {
                        Dialog.Show("Changes weren't applied");
                    }

                    else if (Revert && Completed)
                    {
                        Dialog.Show("Changes were reverted!");
                    }

                    else if (Revert)
                    {
                        Dialog.Show("Changes weren't reverted");
                    }
                }

                #endregion

                #region Partial Dialog

                else
                {
                    if (Revert && Apply && Completed)
                    {
                        Dialog.Show("Changes were applied and/or reverted!");
                    }

                    else if (Revert && Apply && !Completed)
                    {
                        Dialog.Show("Changes weren't applied or reverted");
                    }
                }

                #endregion

                #endregion

                #region Reset Variables

                Completed = false;

                RevertRun = false;

                ApplyRun = false;

                Partial = false;

                Revert = false;

                Apply = false;

                #endregion
            }

            catch (Exception error)
            {
                CrashLogger.Log(error);
                Dialog.Show("Changes weren't applied or reverted", "Program will continue as usual. For more info, read the log(s).");
            }
        }

        #endregion
    }
}