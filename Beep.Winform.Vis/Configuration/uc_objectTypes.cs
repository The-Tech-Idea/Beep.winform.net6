﻿using System;

using System.Data;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;
using BeepEnterprize.Vis.Module;
using TheTechIdea;
using TheTechIdea.Beep;
using TheTechIdea.Beep.Addin;
using TheTechIdea.Beep.DataBase;
using TheTechIdea.Beep.Vis;
using TheTechIdea.Logger;
using TheTechIdea.Util;



namespace Beep.Winform.Vis
{
    [AddinAttribute(Caption = "Object Types", Name = "uc_objectTypes", misc = "Config", menu = "Configuration", addinType = AddinType.Control, displayType = DisplayType.Popup)]
    [AddinVisSchema(BranchID = 1, RootNodeName = "Configuration", Order = 9, ID = 9, BranchText = "Object Types", BranchType = EnumPointType.Function, IconImageName = "box.ico", BranchClass = "ADDIN", BranchDescription = "Data Sources Connection Drivers Setup Screen")]
    public partial class uc_objectTypes : UserControl, IDM_Addin, IAddinVisSchema
    {

        #region "IAddinVisSchema"
        public string RootNodeName { get; set; } = "Configuration";
        public string CatgoryName { get; set; }
        public int Order { get; set; } = 9;
        public int ID { get; set; } = 9;
        public string BranchText { get; set; } = "Object Types";
        public int Level { get; set; }
        public EnumPointType BranchType { get; set; } = EnumPointType.Entity;
        public int BranchID { get; set; } = 1;
        public string IconImageName { get; set; } = "box.ico";
        public string BranchStatus { get; set; }
        public int ParentBranchID { get; set; }
        public string BranchDescription { get; set; } = "";
        public string BranchClass { get; set; } = "ADDIN";
        public IVisManager Visutil { get; set; }
        #endregion "IAddinVisSchema"
        public uc_objectTypes()
        {
            InitializeComponent();
        }

        public string ParentName { get; set; }
        public string AddinName { get; set; } = "Object Types";
        public string Description { get; set; } = "Object Types";
        public string ObjectName { get; set; }
        public string ObjectType { get; set; } = "UserControl";
        public Boolean DefaultCreate { get; set; } = true;
        public string DllPath { get ; set ; }
        public string DllName { get ; set ; }
        public string NameSpace { get ; set ; }
        public DataSet Dset { get ; set ; }
        public IErrorsInfo ErrorObject { get ; set ; }
        public IDMLogger Logger { get ; set ; }
        public IDMEEditor DMEEditor { get ; set ; }
        public EntityStructure EntityStructure { get ; set ; }
        public string EntityName { get ; set ; }
        public IPassedArgs Passedarg { get ; set ; }
       
        public void Run(IPassedArgs pPassedarg)
        {
            throw new NotImplementedException();
        }

        public void SetConfig(IDMEEditor pbl, IDMLogger plogger, IUtil putil, string[] args, IPassedArgs e, IErrorsInfo per)
        {
            Passedarg = e;
            Logger = plogger;
            ErrorObject = per;
            DMEEditor = pbl;
            Visutil = (IVisManager)e.Objects.Where(c => c.Name == "VISUTIL").FirstOrDefault().obj;
            this.objectTypesBindingSource.DataSource = DMEEditor.ConfigEditor.objectTypes;
            uc_bindingNavigator1.bindingSource = objectTypesBindingSource;
            uc_bindingNavigator1.SaveCalled += Uc_bindingNavigator1_SaveCalled;
            uc_bindingNavigator1.SetConfig(DMEEditor, DMEEditor.Logger, DMEEditor.Utilfunction, new string[] { }, e, DMEEditor.ErrorObject);
            uc_bindingNavigator1.HightlightColor = Color.Yellow;

            // this.objectTypesDataGridView.Sort(this.ObjectTypeinGrid, ListSortDirection.Ascending);
        }

        private void Uc_bindingNavigator1_SaveCalled(object sender, BindingSource e)
        {
            try

            {

                this.objectTypesBindingSource.EndEdit();
                DMEEditor.ConfigEditor.SaveObjectTypes();
                MessageBox.Show("Object Names successfully", "Beep");

            }
            catch (Exception ex)
            {

                ErrorObject.Flag = Errors.Failed;
                string errmsg = "Error Saving Object Names ";
                ErrorObject.Message = $"{errmsg}:{ex.Message}";
                errmsg = ErrorObject.Message;
                MessageBox.Show(errmsg, "Beep");
                Logger.WriteLog($" {errmsg} :{ex.Message}");
            }
        }

    }
}