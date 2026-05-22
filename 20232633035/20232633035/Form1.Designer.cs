
namespace _20232633035
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axMapControl3 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl3 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axLicenseControl3 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移除图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.属性查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空间查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开属性表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.符号化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单一符号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分类符号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分级符号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.比例符号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注释要素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成要素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动要素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动要素节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl3)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axMapControl3
            // 
            this.axMapControl3.Location = new System.Drawing.Point(221, 12);
            this.axMapControl3.Name = "axMapControl3";
            this.axMapControl3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl3.OcxState")));
            this.axMapControl3.Size = new System.Drawing.Size(615, 505);
            this.axMapControl3.TabIndex = 0;
            this.axMapControl3.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl3_OnMouseDown);
            this.axMapControl3.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl3_OnMouseUp);
            this.axMapControl3.OnDoubleClick += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnDoubleClickEventHandler(this.axMapControl3_OnDoubleClick);
            this.axMapControl3.OnKeyUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnKeyUpEventHandler(this.axMapControl3_OnKeyUp);
            // 
            // axToolbarControl2
            // 
            this.axToolbarControl2.Location = new System.Drawing.Point(20, 12);
            this.axToolbarControl2.Name = "axToolbarControl2";
            this.axToolbarControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl2.OcxState")));
            this.axToolbarControl2.Size = new System.Drawing.Size(185, 28);
            this.axToolbarControl2.TabIndex = 1;
            // 
            // axTOCControl3
            // 
            this.axTOCControl3.Location = new System.Drawing.Point(20, 46);
            this.axTOCControl3.Name = "axTOCControl3";
            this.axTOCControl3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl3.OcxState")));
            this.axTOCControl3.Size = new System.Drawing.Size(185, 471);
            this.axTOCControl3.TabIndex = 2;
            this.axTOCControl3.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl3_OnMouseDown);
            // 
            // axLicenseControl3
            // 
            this.axLicenseControl3.Enabled = true;
            this.axLicenseControl3.Location = new System.Drawing.Point(662, 421);
            this.axLicenseControl3.Name = "axLicenseControl3";
            this.axLicenseControl3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl3.OcxState")));
            this.axLicenseControl3.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl3.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移除图层ToolStripMenuItem,
            this.属性查询ToolStripMenuItem,
            this.空间查询ToolStripMenuItem,
            this.打开属性表ToolStripMenuItem,
            this.符号化ToolStripMenuItem,
            this.注释要素ToolStripMenuItem,
            this.地图编辑ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 180);
            // 
            // 移除图层ToolStripMenuItem
            // 
            this.移除图层ToolStripMenuItem.Name = "移除图层ToolStripMenuItem";
            this.移除图层ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.移除图层ToolStripMenuItem.Text = "移除图层";
            this.移除图层ToolStripMenuItem.Click += new System.EventHandler(this.移除图层ToolStripMenuItem_Click);
            // 
            // 属性查询ToolStripMenuItem
            // 
            this.属性查询ToolStripMenuItem.Enabled = false;
            this.属性查询ToolStripMenuItem.Name = "属性查询ToolStripMenuItem";
            this.属性查询ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.属性查询ToolStripMenuItem.Text = "属性查询";
            this.属性查询ToolStripMenuItem.Click += new System.EventHandler(this.属性查询ToolStripMenuItem_Click);
            // 
            // 空间查询ToolStripMenuItem
            // 
            this.空间查询ToolStripMenuItem.Enabled = false;
            this.空间查询ToolStripMenuItem.Name = "空间查询ToolStripMenuItem";
            this.空间查询ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.空间查询ToolStripMenuItem.Text = "空间查询";
            this.空间查询ToolStripMenuItem.Click += new System.EventHandler(this.空间查询ToolStripMenuItem_Click);
            // 
            // 打开属性表ToolStripMenuItem
            // 
            this.打开属性表ToolStripMenuItem.Enabled = false;
            this.打开属性表ToolStripMenuItem.Name = "打开属性表ToolStripMenuItem";
            this.打开属性表ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.打开属性表ToolStripMenuItem.Text = "打开属性表";
            this.打开属性表ToolStripMenuItem.Click += new System.EventHandler(this.打开属性表ToolStripMenuItem_Click);
            // 
            // 符号化ToolStripMenuItem
            // 
            this.符号化ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单一符号ToolStripMenuItem,
            this.分类符号ToolStripMenuItem,
            this.分级符号ToolStripMenuItem,
            this.比例符号ToolStripMenuItem});
            this.符号化ToolStripMenuItem.Name = "符号化ToolStripMenuItem";
            this.符号化ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.符号化ToolStripMenuItem.Text = "符号化";
            // 
            // 单一符号ToolStripMenuItem
            // 
            this.单一符号ToolStripMenuItem.Name = "单一符号ToolStripMenuItem";
            this.单一符号ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.单一符号ToolStripMenuItem.Text = "单一符号";
            this.单一符号ToolStripMenuItem.Click += new System.EventHandler(this.单一符号ToolStripMenuItem_Click);
            // 
            // 分类符号ToolStripMenuItem
            // 
            this.分类符号ToolStripMenuItem.Name = "分类符号ToolStripMenuItem";
            this.分类符号ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.分类符号ToolStripMenuItem.Text = "分类符号";
            this.分类符号ToolStripMenuItem.Click += new System.EventHandler(this.分类符号ToolStripMenuItem_Click);
            // 
            // 分级符号ToolStripMenuItem
            // 
            this.分级符号ToolStripMenuItem.Name = "分级符号ToolStripMenuItem";
            this.分级符号ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.分级符号ToolStripMenuItem.Text = "分级符号";
            this.分级符号ToolStripMenuItem.Click += new System.EventHandler(this.分级符号ToolStripMenuItem_Click);
            // 
            // 比例符号ToolStripMenuItem
            // 
            this.比例符号ToolStripMenuItem.Name = "比例符号ToolStripMenuItem";
            this.比例符号ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.比例符号ToolStripMenuItem.Text = "比例符号";
            this.比例符号ToolStripMenuItem.Click += new System.EventHandler(this.比例符号ToolStripMenuItem_Click);
            // 
            // 注释要素ToolStripMenuItem
            // 
            this.注释要素ToolStripMenuItem.Name = "注释要素ToolStripMenuItem";
            this.注释要素ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.注释要素ToolStripMenuItem.Text = "注释要素";
            // 
            // 地图编辑ToolStripMenuItem
            // 
            this.地图编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.生成要素ToolStripMenuItem,
            this.移动要素ToolStripMenuItem,
            this.移动要素节点ToolStripMenuItem});
            this.地图编辑ToolStripMenuItem.Name = "地图编辑ToolStripMenuItem";
            this.地图编辑ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.地图编辑ToolStripMenuItem.Text = "地图编辑";
            // 
            // 生成要素ToolStripMenuItem
            // 
            this.生成要素ToolStripMenuItem.Name = "生成要素ToolStripMenuItem";
            this.生成要素ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.生成要素ToolStripMenuItem.Text = "生成要素";
            this.生成要素ToolStripMenuItem.Click += new System.EventHandler(this.生成要素ToolStripMenuItem_Click_1);
            // 
            // 移动要素ToolStripMenuItem
            // 
            this.移动要素ToolStripMenuItem.Name = "移动要素ToolStripMenuItem";
            this.移动要素ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.移动要素ToolStripMenuItem.Text = "移动要素";
            this.移动要素ToolStripMenuItem.Click += new System.EventHandler(this.移动要素ToolStripMenuItem_Click);
            // 
            // 移动要素节点ToolStripMenuItem
            // 
            this.移动要素节点ToolStripMenuItem.Name = "移动要素节点ToolStripMenuItem";
            this.移动要素节点ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.移动要素节点ToolStripMenuItem.Text = "移动要素节点";
            this.移动要素节点ToolStripMenuItem.Click += new System.EventHandler(this.移动要素节点ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(889, 566);
            this.Controls.Add(this.axLicenseControl3);
            this.Controls.Add(this.axTOCControl3);
            this.Controls.Add(this.axToolbarControl2);
            this.Controls.Add(this.axMapControl3);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl3)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl axMapControl3;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl3;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 移除图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 属性查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空间查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开属性表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 符号化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单一符号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分类符号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分级符号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 比例符号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注释要素ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成要素ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动要素节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动要素ToolStripMenuItem;
    }
}

