﻿using BeepEnterprize.Vis.Module;
using Beep.Winform.Vis.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beep.Winform.Vis.Controls
{
    public partial class uc_Container : UserControl,IDisplayContainer
    {
        public ContainerTypeEnum ContainerType { get; set; }
        private Panel ContainerPanel=new Panel();
        private TabControl TabContainerPanel = new TabControl();
        Image CloseImage;
        Point _imageLocation = new Point(20, 4);
        Point _imgHitArea = new Point(20, 4);


        public uc_Container()
        {
            InitializeComponent();
          
        }

        private void TabContainerPanel_MouseClick(object sender, MouseEventArgs e)
        {
            for (var i = 0; i < this.TabContainerPanel.TabPages.Count; i++)
            {
                var tabRect = this.TabContainerPanel.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                var imageRect = new Rectangle(tabRect.Right - CloseImage.Width,
                                         tabRect.Top + (tabRect.Height - CloseImage.Height) / 2,
                                         CloseImage.Width,
                                         CloseImage.Height);
                if (imageRect.Contains(e.Location))
                {
                    this.TabContainerPanel.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        private void TabContainerPanel_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                Image img = new Bitmap(CloseImage);
                Rectangle r = e.Bounds;
                r = this.TabContainerPanel.GetTabRect(e.Index);
                r.Offset(2, 2);
                Brush TitleBrush = new SolidBrush(Color.Black);
                Font f = this.Font;
                string title = this.TabContainerPanel.TabPages[e.Index].Text;
                SizeF titlesize=e.Graphics.MeasureString(title, f);
                
                e.Graphics.DrawString(title, f, TitleBrush, new PointF(r.X, r.Y));
                
                e.Graphics.DrawImage(img, new Point(r.X + (this.TabContainerPanel.GetTabRect(e.Index).Width - _imageLocation.X), _imageLocation.Y));

            }
            catch (Exception ex) { }
        }

        public bool AddControl(string TitleText, object pcontrol, ContainerTypeEnum pcontainerType)
        {
            Control control = (Control)pcontrol;
            ContainerType = pcontainerType;
            if (control == null || control != null && control.Controls.Contains(control)) { return false; }
            switch (pcontainerType)
            {
                case ContainerTypeEnum.SinglePanel:
                    AddToSingle(TitleText, control);
                    break;
                case ContainerTypeEnum.TabbedPanel:
                    AddToTabbed(TitleText, control);
                    break;
                default:
                    break;
            }
            return control != null && control.Controls.Contains(control);
        }
        private bool AddToTabbed(string TitleText,Control control)
        {
            if(ContainerType== ContainerTypeEnum.SinglePanel)
            {
                if (Controls.Contains(ContainerPanel))
                {
                    Controls.Remove(ContainerPanel);
                }
              
            }
            if (!Controls.Contains(TabContainerPanel))
            {
                TabContainerPanel = new TabControl();
                TabContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));
                TabContainerPanel.Location = new System.Drawing.Point(17,0);
                TabContainerPanel.Name = "ControlPanel";
                TabContainerPanel.Size = new System.Drawing.Size(this.Width-20, this.Height-20);
                TabContainerPanel.TabPages.Clear();
                Controls.Add(TabContainerPanel);
                CloseImage = Vis.Properties.Resources.close;
                this.TabContainerPanel.Multiline = true;
                TabContainerPanel.DrawMode = TabDrawMode.OwnerDrawFixed;
                TabContainerPanel.DrawItem += TabContainerPanel_DrawItem;
                TabContainerPanel.MouseClick += TabContainerPanel_MouseClick;
                TabContainerPanel.Padding = new Point(30, 4);
              

            }
            TabContainerPanel.TabPages.Add(TitleText, TitleText);
            TabContainerPanel.TabPages[TabContainerPanel.TabPages.Count-1].Controls.Add(control);
            TabContainerPanel.SelectedTab = TabContainerPanel.TabPages[TabContainerPanel.TabPages.Count - 1];
            control.Location = new System.Drawing.Point(0, 0);
            control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));
            control.Size = new System.Drawing.Size(TabContainerPanel.Width - 20, TabContainerPanel.Height - 10);
            //    this.TitleLabel.Text = TitleText;
            return true;
        }
        private bool AddToSingle(string TitleText, Control control)
        {
            if (ContainerType == ContainerTypeEnum.TabbedPanel)
            {
                if (Controls.Contains(TabContainerPanel))
                {
                    Controls.Remove(TabContainerPanel);
                }

            }
            if (!Controls.Contains(ContainerPanel))
            {
                ContainerPanel = new Panel();
                ContainerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
         | System.Windows.Forms.AnchorStyles.Left)
         | System.Windows.Forms.AnchorStyles.Right)));
                ContainerPanel.Location = new System.Drawing.Point(20, 2);
                ContainerPanel.Name = "ControlPanel";
                ContainerPanel.Dock = DockStyle.None;
                ContainerPanel.Size = new System.Drawing.Size(this.Width, this.Height );
                Controls.Add(ContainerPanel);
            }
            ContainerPanel.Controls.Add(control);
            control.Dock = DockStyle.Fill;
         //   this.TitleLabel.Text = TitleText;
            return true;
        }
        public static Rectangle GetRTLCoordinates(Rectangle container, Rectangle drawRectangle)
        {
            return new Rectangle(
                container.Width - drawRectangle.Width - drawRectangle.X,
                drawRectangle.Y,
                drawRectangle.Width,
                drawRectangle.Height);
        }

        public bool RemoveControl(string TitleText, object control)
        {
            throw new NotImplementedException();
        }
    }
   
}