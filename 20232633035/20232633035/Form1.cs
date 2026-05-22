using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;

namespace _20232633035
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int omd_control = 0; //控制OnMouseDown
        ILayer pLayer = null;
        IFeatureLayer pFeatureLayer;
        INewPolygonFeedback pNewPolygonFeedback = null;
        INewLineFeedback pNewLineFeedback = null;

        IFeature pMoveFeature;
        IFeature pMovePointFeature;
        IMovePolygonFeedback pMovePolygonFeedback = null;
        IPolygonMovePointFeedback pPolygonMovePointFeekback = null;
        IFeatureLayer pDistrictLayer;
        IFeatureLayer pStationLayer;
        IPoint pStartStation;
        IPoint pEndStation;
  
        private void 添加文本数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //axMapControl3.AddShapeFile(@"D:\gisdata", @"guangfo");
            OpenFileDialog cOpenFileDialog = new OpenFileDialog();  // 创建打开文件对话框实例，用于选择要加载的GIS文件
            cOpenFileDialog.InitialDirectory = @"D:\学习\大三下\gis应用开发\scnu";  // 设置对话框初始打开路径为D盘根目录，方便用户快速定位文件
            cOpenFileDialog.Filter = @"Shapefile文件(*.shp)|*.shp|地图文档(*.mxd)|*.mxd|图层文件(*.lyr)|*.lyr"; // 设置文件筛选规则，仅显示.shp和.mxd类型文件（避免选择无关文件）格式说明："显示文本|文件后缀"
            cOpenFileDialog.Multiselect = false;    // 禁用多选功能，确保每次只加载一个文件（符合ArcGIS Engine常规使用习惯）
            if (cOpenFileDialog.ShowDialog() == DialogResult.OK)   // 显示文件选择对话框，等待用户选择文件并确认
            {
                // 解析用户选择的文件路径和名称
                string pPath = System.IO.Path.GetDirectoryName(cOpenFileDialog.FileName);
                string pFileName = cOpenFileDialog.SafeFileName;
                string pExtension = System.IO.Path.GetExtension(cOpenFileDialog.FileName);
                // 根据文件扩展名判断类型，调用对应加载方法
                switch (pExtension)
                {
                    case ".shp": axMapControl3.AddShapeFile(pPath, pFileName); break;
                    case ".mxd": axMapControl3.LoadMxFile(cOpenFileDialog.FileName); break;
                    case ".lyr": axMapControl3.AddLayerFromFile(cOpenFileDialog.FileName); break;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IQueryFilter pQueryFilter = new QueryFilter();  //声明并创建查询工具器
            pQueryFilter.WhereClause = "Shape_Area > 1500"; //设置查询条件

            if (axMapControl3.Map.LayerCount == 0) return;

            ILayer pLayer = axMapControl3.Map.Layer[0]; //获取索引为0的图层（第一个图层），ILayer是所有图层的基类
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;    //接口转换

            // 1.直接弹出查询结果
            /*
            IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pQueryFilter, true);   //1.执行查询；2.声明对象记录查询结果
            
            IFeature pFeature = pFeatureCursor.NextFeature();   //获取第一个查询结果
            if (pFeature == null)
            {
                MessageBox.Show("查询结果设置有误！");
                return;
            }
            int i = pFeatureCursor.FindField(@"NAME");  //获取NAME字段的序号
            do
            {
                MessageBox.Show(pFeature.get_Value(i));
                pFeature = pFeatureCursor.NextFeature();
            } while (pFeature != null); //循环并用对话框返回结果要素的NAME信息
            */

            IFeatureSelection pFeatureSelection = (IFeatureSelection)pFeatureLayer; //接口转换
            pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);  //执行

            IRgbColor pRgbColor = new RgbColor();   //设置颜色
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;

            pFeatureSelection.SelectionColor = pRgbColor;   //颜色赋值查询结果
            axMapControl3.Refresh();    //地图刷新
        }

        private void axTOCControl3_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem pTOCControlItem = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap pBasicMap = null;
            object unk = null;
            object data = null;
            axTOCControl3.HitTest(e.x, e.y, ref pTOCControlItem, ref pBasicMap, ref pLayer, ref unk, ref data);//点击测试

            if (e.button == 2)//当右键点击
            {
                if (pTOCControlItem == esriTOCControlItem.esriTOCControlItemLayer)//当点击的是图层
                {
                    contextMenuStrip1.Show(axTOCControl3, new System.Drawing.Point(e.x, e.y));//弹出右键菜单
                    if (pLayer is IFeatureLayer)
                    {
                        pFeatureLayer = pLayer as IFeatureLayer;
                        属性查询ToolStripMenuItem.Enabled = true;
                        空间查询ToolStripMenuItem.Enabled = true;
                        打开属性表ToolStripMenuItem.Enabled = true;
                        符号化ToolStripMenuItem.Enabled = true;
                    }
                }
            }
        }

        private void 移除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl3.Map.DeleteLayer(pLayer);
        }

        private void 属性查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //当点击目录树控件右键菜单的属性查询时
            Form2 frmAttributeQuery = new Form2();
            frmAttributeQuery.listBox1.Items.Clear();//清空listbox控件中的数据
            IFields pFields = pFeatureLayer.FeatureClass.Fields;//读取所选矢量图层子字段
            IField pField;
            int count = pFields.FieldCount;
            int i;
            //将所有字段添加到Form2的ComboBox中
            for (i = 0; i < count; i++)
            {
                pField = pFields.Field[i];
                frmAttributeQuery.listBox1.Items.Add(pField.Name);
            }
            if (frmAttributeQuery.ShowDialog() == DialogResult.OK)
            {
                IQueryFilter pQueryFilter = new QueryFilter();
                pQueryFilter.WhereClause = frmAttributeQuery.textBox1.Text;//设置查询表达式
                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                IRgbColor pRgbColor = new RgbColor();//颜色
                pRgbColor.Red = 0;
                pRgbColor.Green = 200;
                pRgbColor.Blue = 50;
                pFeatureSelection.SelectionColor = pRgbColor;
                axMapControl3.Refresh();//刷新
            }
        }

        private void 空间查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            omd_control = 1;
        }

        private void 生成要素ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            omd_control = 2;
        }
        private void 移动要素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            omd_control = 3;
        }
        private void 移动要素节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            omd_control = 4;
        }

        private void axMapControl3_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            Form3 frmSpatialRel = new Form3();
            if (omd_control == 1)//空间查询
            {
                IEnvelope pEnvelope = axMapControl3.TrackRectangle();//画矩形作为查询主体
                ISpatialFilter pSpatialFilter = new SpatialFilter();
                pSpatialFilter.Geometry = pEnvelope as IGeometry;//设置查询主体

                frmSpatialRel.ShowDialog();

                switch (frmSpatialRel.SearchType)
                {
                    case (1): pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects; break;
                    case (2): pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains; break;
                    case (3): pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin; break;
                    case (4): pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses; break;
                    case (5): pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches; break;
                    case (6): pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelOverlaps; break;
                }

                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
                pFeatureSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                IRgbColor pRgbColor = new RgbColor();//设置颜色
                pRgbColor.Red = 255;
                pRgbColor.Green = 0;
                pRgbColor.Blue = 0;
                pFeatureSelection.SelectionColor = pRgbColor;//把设置好的颜色赋值给featureselection的selectioncolor属性
                axMapControl3.Refresh();//地图控件刷新

            }
            if (omd_control == 2)//生成要素
            {
                IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.PutCoords(e.mapX, e.mapY);

                if (pFeatureLayer.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
                {
                    if (pNewPolygonFeedback == null)
                    {
                        pNewPolygonFeedback = new NewPolygonFeedback();
                        // ↓ 新增：绑定显示和设置绘图样式 ↓
                        IDisplayFeedback pDF = pNewPolygonFeedback as IDisplayFeedback;
                        pDF.Display = axMapControl3.ActiveView.ScreenDisplay;

                        ISimpleLineSymbol pOutline = new SimpleLineSymbol();
                        pOutline.Style = esriSimpleLineStyle.esriSLSSolid;
                        pOutline.Width = 2;
                        IRgbColor pColor = new RgbColor();
                        pColor.Red = 255; pColor.Green = 0; pColor.Blue = 0;
                        pOutline.Color = pColor;

                        ISimpleFillSymbol pFill = new SimpleFillSymbol();
                        pFill.Style = esriSimpleFillStyle.esriSFSNull;  // 透明填充只看边框
                        pFill.Outline = pOutline;
                        pDF.Symbol = pFill as ISymbol;
                        // ↑ 新增结束 ↑

                        pNewPolygonFeedback.Start(pPoint);
                    }
                    else
                        pNewPolygonFeedback.AddPoint(pPoint);
                }
                else if (pFeatureLayer.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                {
                    if (pNewLineFeedback == null)
                    {
                        pNewLineFeedback = new NewLineFeedback();
                        // ↓ 新增 ↓
                        IDisplayFeedback pDF = pNewLineFeedback as IDisplayFeedback;
                        pDF.Display = axMapControl3.ActiveView.ScreenDisplay;

                        ISimpleLineSymbol pLineSym = new SimpleLineSymbol();
                        pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
                        pLineSym.Width = 2;
                        IRgbColor pColor = new RgbColor();
                        pColor.Red = 255; pColor.Green = 0; pColor.Blue = 0;
                        pLineSym.Color = pColor;
                        pDF.Symbol = pLineSym as ISymbol;
                        // ↑ 新增结束 ↑

                        pNewLineFeedback.Start(pPoint);
                    }
                    else
                        pNewLineFeedback.AddPoint(pPoint);
                }
            }

            if (omd_control ==3)//移动要素
            {
                IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.PutCoords(e.mapX, e.mapY);
                ISpatialFilter pSpatialFilter = new SpatialFilter();
                pSpatialFilter.Geometry = pPoint;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin;
                IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pSpatialFilter, true);
                pMoveFeature = pFeatureCursor.NextFeature();
                if (pMoveFeature == null) return;
                if (pMovePolygonFeedback == null)
                {
                    pMovePolygonFeedback = new MovePolygonFeedback();
                    pMovePolygonFeedback.Display = axMapControl3.ActiveView.ScreenDisplay;
                    IPolygon pPolygon = pMoveFeature.Shape as IPolygon;
                    pMovePolygonFeedback.Start(pPolygon, pPoint);
                }
            }
            if(omd_control ==4)//移动要素节点
            {
                IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.PutCoords(e.mapX, e.mapY);
                ITopologicalOperator pTopologicalOperator = pPoint as ITopologicalOperator;
                IPolygon pPolygon = pTopologicalOperator.Buffer(10) as IPolygon;
                ISpatialFilter pSpatialFilter = new SpatialFilter();
                pSpatialFilter.Geometry = pPoint;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelWithin;
                IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pSpatialFilter, true);
                pMoveFeature = pFeatureCursor.NextFeature();
                if (pMoveFeature == null) return;
                //这个是老师的
                //IHitTest pHitTest = pMovePointFeature.Shape as IHitTest;
                pMovePointFeature = pMoveFeature;   // 先把拾取到的要素赋给它
                //这个是修复的
                IHitTest pHitTest = pMovePointFeature.Shape as IHitTest;
                IPoint pHitPoint = new ESRI.ArcGIS.Geometry.Point();

                double pHistDistance = 0;
                int pPartlndex = 0 ;
                int pSegmentIndex = 0;
                bool pHitResult= pHitTest.HitTest(pPoint, 10, esriGeometryHitPartType.esriGeometryPartVertex, pHitPoint, ref pHistDistance, ref pPartlndex, ref pSegmentIndex, true);
                if (pHitResult == false) return;

                pPolygonMovePointFeekback = new PolygonMovePointFeedback();
                pPolygonMovePointFeekback.Display = axMapControl3.ActiveView.ScreenDisplay;
                pPolygonMovePointFeekback.Start(pMovePointFeature.Shape as IPolygon, pSegmentIndex, pPoint);

             
            }
        }
        private void axMapControl3_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            
            if (omd_control == 2)
            {
                IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.PutCoords(e.mapX, e.mapY);
                if (pNewPolygonFeedback != null) pNewPolygonFeedback.MoveTo(pPoint);
                if (pNewLineFeedback != null) pNewLineFeedback.MoveTo(pPoint);
            }
            if (omd_control == 3)
            {
                IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.PutCoords(e.mapX, e.mapY);
                if (pMovePolygonFeedback != null) pMovePolygonFeedback.MoveTo(pPoint);
            }
            if (omd_control == 4)
            {
                IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.PutCoords(e.mapX, e.mapY);
                if (pPolygonMovePointFeekback != null) pPolygonMovePointFeekback.MoveTo(pPoint);
            }
        }
        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable pTable = new DataTable();

            IFeatureCursor pFeatureCursor = pFeatureLayer.Search(null, true);
            IFeature pFeature = pFeatureCursor.NextFeature();
            if (pFeature == null) return;

            IFields pFields = pFeatureLayer.FeatureClass.Fields;
            DataColumn col;
            //循环输出字段名到数据表
            for (int i = 0;i < pFields.FieldCount;i++)
            {
                col = new DataColumn(pFields.Field[i].AliasName);
                col.DataType = System.Type.GetType("System.String");
                pTable.Columns.Add(col);
            }
            //循环输出结果
            DataRow row;
            do
            {
                row = pTable.NewRow();
                for (int j = 0; j < pFields.FieldCount; j++) row[j] = pFeature.Value[j].ToString();
                pTable.Rows.Add(row);
                pFeature = pFeatureCursor.NextFeature();
            } while (pFeature != null);

            Form4 frmAttributeTable = new Form4();
            frmAttributeTable.dataGridView1.DataSource = pTable;
            frmAttributeTable.ShowDialog();
        }

        private void 单一符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //指定需要符号化的图层
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
            IRgbColor pRgbColor = new RgbColor();

            //配置符号,简单点符号
            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbol();
            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            pRgbColor = new RgbColor(); // 重新new
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            pSimpleMarkerSymbol.OutlineColor = pRgbColor;
            pSimpleMarkerSymbol.OutlineSize = 1;
            pSimpleMarkerSymbol.Outline = true;
            pRgbColor = new RgbColor(); // 重新new
            pRgbColor.Red = 0;
            pRgbColor.Green = 255;
            pRgbColor.Blue = 0;
            pSimpleMarkerSymbol.Color = pRgbColor;
            pSimpleMarkerSymbol.Size = 8;


            //配置符号：简单线符号；
            ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbol();
            pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDot;
            pSimpleLineSymbol.Width = 1;
            pRgbColor = new RgbColor(); // 重新new
            pRgbColor.Red = 0;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 255;
            pSimpleLineSymbol.Color = pRgbColor;

            //配置符号：简单面符号：
            ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbol();
            pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            pRgbColor = new RgbColor(); // 重新new
            pRgbColor.Red = 100;
            pRgbColor.Green = 200;
            pRgbColor.Blue = 100;
            pSimpleFillSymbol.Color = pRgbColor;
            pSimpleFillSymbol.Outline = pSimpleLineSymbol;

            IMarkerFillSymbol pMarkerFillSymbol = new MarkerFillSymbol();
            pMarkerFillSymbol.Style = esriMarkerFillStyle.esriMFSGrid;
            pMarkerFillSymbol.GridAngle = 10;
            pMarkerFillSymbol.MarkerSymbol = pSimpleMarkerSymbol;

            ILineFillSymbol pLineFillSymbol = new LineFillSymbol();
            pLineFillSymbol.Angle = 30;
            pLineFillSymbol.Separation = 4;
            pLineFillSymbol.LineSymbol = pSimpleLineSymbol;

            //判断图层的几何类型
            ISimpleRenderer pSimpleRenderer = new SimpleRenderer();
            switch (pFeatureLayer.FeatureClass.ShapeType)
            {
                case (esriGeometryType.esriGeometryPoint):
                    pSimpleRenderer.Symbol = pSimpleMarkerSymbol as ISymbol;
                    break;
                case (esriGeometryType.esriGeometryPolyline):
                    pSimpleRenderer.Symbol = pSimpleLineSymbol as ISymbol;
                    break;
                case (esriGeometryType.esriGeometryPolygon):
                    pSimpleRenderer.Symbol = pLineFillSymbol as ISymbol;
                    break;
                default:break;
            }
            //图层渲染：
            pGeoFeatureLayer.Renderer = pSimpleRenderer as IFeatureRenderer;
          ///  ITransparencyRenderer pTransparencyRenderer = pSimpleRenderer as ITransparencyRenderer;
          ///  pTransparencyRenderer.TransparencyField = "Size_";
         ///   pGeoFeatureLayer.Renderer = pTransparencyRenderer as IFeatureRenderer;

            axMapControl3.Refresh();
        }

        private void 分类符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //指定符号化图层
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
            IUniqueValueRenderer pUniqueValueRenderer = new UniqueValueRenderer();

            Form5 frmAttributeQuery = new Form5();
            IFields pFields = pFeatureLayer.FeatureClass.Fields;
            IField pField;
            int count = pFields.FieldCount;
            int i;
            for (i = 0; i < count; i++)
            {
                pField = pFields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeString)
                {
                    frmAttributeQuery.listBox1.Items.Add(pField.Name);
                }
            }
            if (frmAttributeQuery.ShowDialog() == DialogResult.OK)
            {
                string selectField = frmAttributeQuery.listBox1.SelectedItem.ToString();
                if (string.IsNullOrEmpty(selectField)) return;
                IGeoFeatureLayer ppGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
                if (ppGeoFeatureLayer == null) return;

                //设计符号


                IFeatureCursor pFeatureCursor = pFeatureLayer.Search(null, true);//获取图层中的所有要素
                IFeature pFeature = pFeatureCursor.NextFeature();
                if (pFeature == null) return;
                int j = pFeatureLayer.FeatureClass.FindField("CLASS");
                IRandomColorRamp pRandomColorRamp = new RandomColorRamp();
                pRandomColorRamp.StartHue = 100;
                pRandomColorRamp.MinSaturation = 50;
                pRandomColorRamp.MinValue = 20;
                pRandomColorRamp.EndHue = 300;
                pRandomColorRamp.MaxSaturation = 243;
                pRandomColorRamp.MaxValue = 40;
                pRandomColorRamp.Size = 20;
                bool bTrue = true;
                pRandomColorRamp.CreateRamp(out bTrue);//创建随机色带
                IEnumColors pEnumColors = pRandomColorRamp.Colors;
                pEnumColors.Reset();//返回至第一个颜色
                string value;
                IColor pColor;
                ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbol();
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbol();
                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbol();

                do
                {
                    pColor = pEnumColors.Next();
                    if (pColor == null)
                    {
                        pEnumColors.Reset();
                        pColor = pEnumColors.Next();
                    }
                    value = pFeature.Value[j];
                    switch (pFeatureLayer.FeatureClass.ShapeType)
                    {
                        case (ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint):
                            pSimpleMarkerSymbol = new SimpleMarkerSymbol();
                            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                            pSimpleMarkerSymbol.Size = 10;
                            pSimpleMarkerSymbol.Color = pColor;
                            pUniqueValueRenderer.AddValue(value, "", pSimpleMarkerSymbol as ISymbol);
                            break;
                        case (ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline):
                            pSimpleLineSymbol = new SimpleLineSymbol();
                            pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                            pSimpleLineSymbol.Width = 1;
                            pSimpleLineSymbol.Color = pColor;
                            pUniqueValueRenderer.AddValue(value, "", pSimpleLineSymbol as ISymbol);
                            break;
                        case (ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon):
                            pSimpleFillSymbol = new SimpleFillSymbol();
                            pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                            pSimpleFillSymbol.Color = pColor;
                            pUniqueValueRenderer.AddValue(value, "", pSimpleFillSymbol as ISymbol);
                            break;
                    }
                    pFeature = pFeatureCursor.NextFeature();//导至下一个要素
                } while (pFeature != null);//下个要素为空时，循环终止
            }


            //图层渲染
            pGeoFeatureLayer.Renderer = pUniqueValueRenderer as IFeatureRenderer;
            axMapControl3.Refresh();
        }

        private void 分级符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //指定符号化的图层
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;

            Form6 frmRenderer = new Form6();
            frmRenderer.listBox1.Items.Clear();
            IFields pFields = pFeatureLayer.FeatureClass.Fields;
            IField pField;
            int count = pFields.FieldCount;

            //使用循环，将所有字段添加到listbox中
            for (int j = 0; j < count; j++)
            {
                pField = pFields.Field[j];
                if (pField.Type == esriFieldType.esriFieldTypeDouble || pField.Type == esriFieldType.esriFieldTypeSingle || pField.Type == esriFieldType.esriFieldTypeString)
                    frmRenderer.listBox1.Items.Add(pField.Name)
                        ;
            }
            IClassBreaksRenderer pClassBreaksRenderer = new ClassBreaksRenderer();

            if (frmRenderer.ShowDialog () == DialogResult.OK)
            {

                pClassBreaksRenderer.Field = frmRenderer.listBox1.SelectedItem.ToString();
                pClassBreaksRenderer.BreakCount = Convert.ToInt16(frmRenderer.comboBox1.SelectedItem);


                //设计符号
               // IClassBreaksRenderer pClassBreakRenderer = new ClassBreaksRenderer();
               // pClassBreakRenderer.Field = "SIZE_";
               // pClassBreakRenderer.BreakCount = 5;


                //创建一个连续颜色带并取色
                IAlgorithmicColorRamp pAlogrithmicColorRamp = new AlgorithmicColorRamp();
                IRgbColor pRgbColor = new RgbColor();
                pRgbColor.Red = 255;
                pRgbColor.Green = 255;
                pRgbColor.Blue = 0;

                pAlogrithmicColorRamp.FromColor = pRgbColor;
                pRgbColor.Red = 100;
                pRgbColor.Green = 0;
                pRgbColor.Blue = 0;
                pAlogrithmicColorRamp.ToColor = pRgbColor;
                pAlogrithmicColorRamp.Size = 10;
                bool bTrue = true;
                pAlogrithmicColorRamp.CreateRamp(out bTrue);
                IEnumColors pIEnumColors = pAlogrithmicColorRamp.Colors;
                pIEnumColors.Reset();

            //截断点数值的数组
            //  double[] breaks  =new double [pClassBreaksRender.BreakCount];
            //  breaks[0] = 400;
            //  breaks[1] = 700;
            ////  breaks[2] = 1200;
            // breaks[3] = 2000;
            // breaks[4] = 6000;

            //直方图分析
                ITableHistogram pTableHistogram = new BasicTableHistogram() as ITableHistogram;
                pTableHistogram.Table = pFeatureLayer.FeatureClass as ITable;
                pTableHistogram.Field = pClassBreaksRenderer.Field;
                IBasicHistogram pBasicHistogram = pTableHistogram as IBasicHistogram;
                object dataValue, dataFrequency;
                pBasicHistogram.GetHistogram(out dataValue, out dataFrequency);
                //分级
                IClassifyGEN pClassifyGen = new NaturalBreaks();
                switch (frmRenderer.comboBox2.SelectedIndex)
                {
                    case (0):pClassifyGen = new NaturalBreaks();break;
                    case (1):pClassifyGen = new Quantile();break;
                    case (2):pClassifyGen = new EqualInterval();break;
                    case (3):pClassifyGen = new GeometricalInterval();break;
                    case (4):pClassifyGen = new StandardDeviation();break;
                }


            //自然断点分级
     
                pClassifyGen.Classify(dataValue, dataFrequency, pClassBreaksRenderer.BreakCount);
                double[] breaks;
                //用数组保存分级的结果
                breaks = pClassifyGen.ClassBreaks;

            //循环的初始化
                ISimpleMarkerSymbol pSimpleMarkerSymbol;
                ISimpleLineSymbol pSimpleLineSymbol;
                ISimpleFillSymbol pSimpleFillSymbol;
                IColor pColor;

                for (int i = 0; i < pClassBreaksRenderer.BreakCount; i++)
                {
                    pColor = pIEnumColors.Next();
                    if (pColor == null)
                    {
                        pIEnumColors.Reset();
                        pColor = pIEnumColors.Next();
                    }
                    switch (pFeatureLayer.FeatureClass.ShapeType)
                    {
                        case (ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint):
                            pSimpleMarkerSymbol = new SimpleMarkerSymbol();
                            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                            pSimpleMarkerSymbol.Size = 10;
                            pSimpleMarkerSymbol.Color = pColor;
                            pClassBreaksRenderer.Break[i] = breaks[i];
                            pClassBreaksRenderer.Symbol[i] = pSimpleMarkerSymbol as ISymbol;
                            break;
                        case (ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline):
                            pSimpleLineSymbol = new SimpleLineSymbol();
                            pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                            pSimpleLineSymbol.Width = 1;
                            pSimpleLineSymbol.Color = pColor;
                            pClassBreaksRenderer.Break[i] = breaks[i];
                            pClassBreaksRenderer.Symbol[i] = pSimpleLineSymbol as ISymbol;
                            break;
                        case (ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon):
                            pSimpleFillSymbol = new SimpleFillSymbol();
                            pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                            pSimpleFillSymbol.Color = pColor;
                            pClassBreaksRenderer.Break[i] = breaks[i];
                            pClassBreaksRenderer.Symbol[i] = pSimpleFillSymbol as ISymbol;
                            break;


                    }
                }
            }





            pGeoFeatureLayer.Renderer = pClassBreaksRenderer as IFeatureRenderer;
            axMapControl3.Refresh();


        }

        private void 比例符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //指定符号化的图层
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;

            //设计符号
            IProportionalSymbolRenderer pProportionalSymbolRenderer = new ProportionalSymbolRenderer() as IProportionalSymbolRenderer;
            pProportionalSymbolRenderer.Field = "SIZE_";
            IDataStatistics pDataStatistics = new DataStatistics();
            pDataStatistics.Cursor = pFeatureLayer.Search(null, true)as ICursor;
            pDataStatistics.Field = pProportionalSymbolRenderer.Field;
            pProportionalSymbolRenderer.MaxDataValue = pDataStatistics.Statistics.Maximum;
            pProportionalSymbolRenderer.MinDataValue = pDataStatistics.Statistics.Minimum;
            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbol();
            IRgbColor pRgbcolor = new RgbColor();
            pRgbcolor.Red = 0;
            pRgbcolor.Green = 255;
            pRgbcolor.Blue = 0;
            pSimpleMarkerSymbol.Color = pRgbcolor;
            pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            pSimpleMarkerSymbol.Size = 2;
            pProportionalSymbolRenderer.MinSymbol = pSimpleMarkerSymbol as ISymbol;
            if(pFeatureLayer.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
            {
                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbol();
                pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                pRgbcolor.Red = 100;
                pRgbcolor.Green = 0;
                pRgbcolor.Blue = 150;
                pSimpleFillSymbol.Color = pRgbcolor;
                pProportionalSymbolRenderer.BackgroundSymbol = pSimpleFillSymbol;
            }

           
            //图层渲染
            pGeoFeatureLayer.Renderer = pProportionalSymbolRenderer as IFeatureRenderer;
            axMapControl3.Refresh();


        }
        
        private void axMapControl3_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            if (omd_control == 2)
            {
                IGeometry pGeometry = null;
                switch (pFeatureLayer.FeatureClass.ShapeType)
                {
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                        if (pNewPolygonFeedback != null) pGeometry = pNewPolygonFeedback.Stop();
                        pNewPolygonFeedback = null;
                        break;
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                        if (pNewLineFeedback != null) pGeometry = pNewLineFeedback.Stop();
                        pNewLineFeedback = null;
                        break;
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                        IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                        pPoint.PutCoords(e.mapX, e.mapY);
                        pGeometry = pPoint;
                        break;
                    default: break;
                }

                ISpatialFilter pSpatialFilter = new SpatialFilter();
                pSpatialFilter.Geometry = pGeometry;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pSpatialFilter, true);
                if (pFeatureCursor.NextFeature() != null) return;

                IFeature pFeature = pFeatureLayer.FeatureClass.CreateFeature();
                pFeature.Shape = pGeometry;
                pFeature.Store();
                axMapControl3.Refresh();
            }

        }

        private void axMapControl3_OnKeyUp(object sender, IMapControlEvents2_OnKeyUpEvent e)
        {
            if (omd_control == 3)
            {
                if (pMovePolygonFeedback != null)
                {
                    IGeometry pGeometry = pMovePolygonFeedback.Stop();
                    pMovePolygonFeedback = null;
                    pMoveFeature.Shape = pGeometry;
                    pMoveFeature.Store();
                    axMapControl3.Refresh();
                }
            }
        }

       
        private void axMapControl3_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (omd_control == 3)
            {
                if (pMovePolygonFeedback != null)
                {
                    IGeometry pGeometry = pMovePolygonFeedback.Stop();
                    pMovePolygonFeedback = null;

                    ISpatialFilter pSpatialFilter = new SpatialFilter();
                    pSpatialFilter.Geometry = pGeometry;
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pSpatialFilter, true);
                    IFeature pFeature = pFeatureCursor.NextFeature();
                    if ((pFeature == null) || (pFeature.OID == pMoveFeature.OID))
                    {
                        pMoveFeature.Shape = pGeometry;
                        pMoveFeature.Store();
                        axMapControl3.Refresh();
                    }
                }
            }
            if(omd_control == 4)
            {
                if (pMovePolygonFeedback != null)
                {
                    IGeometry pGeometry = pMovePolygonFeedback.Stop();
                    pMovePolygonFeedback = null;

                    ISpatialFilter pSpatialFilter = new SpatialFilter();
                    pSpatialFilter.Geometry = pGeometry;
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    IFeatureCursor pFeatureCursor = pFeatureLayer.Search(pSpatialFilter, true);
                    IFeature pFeature = pFeatureCursor.NextFeature();
                    if ((pFeature == null) || (pFeature.OID == pMoveFeature.OID))
                    {
                        pMoveFeature.Shape = pGeometry;
                        pMoveFeature.Store();
                        axMapControl3.Refresh();
                    }
                }
            }
        }

      
    }
}
