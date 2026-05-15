using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;

namespace _20232633035_潘越
{
    public partial class Form1 : Form
    {
        int omd_control = 0;
        ILayer pLayer = null;
        IFeatureLayer pFeatureLayer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem pTOCControlItem = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap pBasicMap = null;
            object unk = null;
            object data = null;
            axTOCControl1.HitTest(e.x, e.y, ref pTOCControlItem, ref pBasicMap, ref pLayer, ref unk, ref data);

            if (e.button == 2)
            {
                if (pTOCControlItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    contextMenuStrip1.Show(axTOCControl1, new System.Drawing.Point(e.x, e.y));
                    if (pLayer is IFeatureLayer)
                    {
                        pFeatureLayer = pLayer as IFeatureLayer;
                        唯一值渲染ToolStripMenuItem.Enabled = true;
                        圆形空间查询ToolStripMenuItem.Enabled = true;
                    }
                }
            }
        }

        private void 唯一值渲染ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
            IUniqueValueRenderer pUniqueValueRenderer = new UniqueValueRenderer();
            pUniqueValueRenderer.FieldCount = 1;
            pUniqueValueRenderer.Field[0] = "CLASS";

            IFeatureCursor pFeatureCursor = pFeatureLayer.Search(null, true);
            IFeature pFeature = pFeatureCursor.NextFeature();
            if (pFeature == null) return;

            int fieldIndex = pFeatureLayer.FeatureClass.FindField("CLASS");

            IRandomColorRamp pRandomColorRamp = new RandomColorRamp();
            pRandomColorRamp.StartHue = 100;
            pRandomColorRamp.MinSaturation = 50;
            pRandomColorRamp.MinValue = 20;
            pRandomColorRamp.EndHue = 300;
            pRandomColorRamp.MaxSaturation = 243;
            pRandomColorRamp.MaxValue = 40;
            pRandomColorRamp.Size = 20;
            bool bTrue = true;
            pRandomColorRamp.CreateRamp(out bTrue);
            IEnumColors pEnumColors = pRandomColorRamp.Colors;
            pEnumColors.Reset();

            string value;
            IColor pColor;

            do
            {
                pColor = pEnumColors.Next();
                if (pColor == null)
                {
                    pEnumColors.Reset();
                    pColor = pEnumColors.Next();
                }
                value = pFeature.Value[fieldIndex].ToString();

                switch (pFeatureLayer.FeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbol();
                        pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                        pSimpleMarkerSymbol.Size = 10;
                        pSimpleMarkerSymbol.Color = pColor;
                        pUniqueValueRenderer.AddValue(value, "", pSimpleMarkerSymbol as ISymbol);
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbol();
                        pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                        pSimpleLineSymbol.Width = 1;
                        pSimpleLineSymbol.Color = pColor;
                        pUniqueValueRenderer.AddValue(value, "", pSimpleLineSymbol as ISymbol);
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbol();
                        pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
                        pSimpleFillSymbol.Color = pColor;
                        pUniqueValueRenderer.AddValue(value, "", pSimpleFillSymbol as ISymbol);
                        break;
                }
                pFeature = pFeatureCursor.NextFeature();
            } while (pFeature != null);

            pGeoFeatureLayer.Renderer = pUniqueValueRenderer as IFeatureRenderer;
            axMapControl1.Refresh();
        }

        private void 圆形空间查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            omd_control = 1;
            MessageBox.Show("请在地图上点击一个点作为圆心");
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (omd_control == 1)
            {
                if (pFeatureLayer == null)
                {
                    omd_control = 0;
                    return;
                }

                IPoint pPoint = axMapControl1.ToMapPoint(e.x, e.y);
                ITopologicalOperator pTopo = pPoint as ITopologicalOperator;
                IGeometry pBuffer = pTopo.Buffer(150);

                ISpatialFilter pSpatialFilter = new SpatialFilter();
                pSpatialFilter.Geometry = pBuffer;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                pSpatialFilter.WhereClause = "Shape_Area > 1600 AND CLASS = '生活区'";

                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
                pFeatureSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

                IRgbColor pRgbColor = new RgbColor();
                pRgbColor.Red = 255;
                pRgbColor.Green = 0;
                pRgbColor.Blue = 0;
                pFeatureSelection.SelectionColor = pRgbColor;

                axMapControl1.Refresh();
                omd_control = 0;

            }
        }

        private void 标注要素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //指定标注图层
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;

            //声明标注实例集，并关联为标注图层的标注属性
            IAnnotateLayerPropertiesCollection pAnnotateLayerPropertiesCollection = pGeoFeatureLayer.AnnotationProperties;
            pAnnotateLayerPropertiesCollection.Clear();

            //标注文本的基础属性
            ITextSymbol pTextSymbol = new TextSymbol();
            stdole.StdFont pFont = new stdole.StdFont();
            pFont.Name = "宋体";
            pFont.Size = 10;
            pTextSymbol.Font = pFont as stdole.IFontDisp;
            IRgbColor pRgbColor = new RgbColor();

            //声明标注引擎，并设置基本属性
            ILabelEngineLayerProperties plabelEngineLayerProperties = new LabelEngineLayerProperties() as ILabelEngineLayerProperties;
            plabelEngineLayerProperties.Symbol = pTextSymbol;
            plabelEngineLayerProperties.Expression = "[NAME]";

            //声明放置权重，并设置基本属性:
            IBasicOverposterLayerProperties4 pBasicOverposterLayerProperties = new BasicOverposterLayerProperties() as IBasicOverposterLayerProperties4;
            pBasicOverposterLayerProperties.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
            pBasicOverposterLayerProperties.PolygonPlacementMethod = esriOverposterPolygonPlacementMethod.esriAlwaysStraight;
            plabelEngineLayerProperties.BasicOverposterLayerProperties = pBasicOverposterLayerProperties as IBasicOverposterLayerProperties;
            //声明一个标注实例，并赋值为上述的标准引擎；
            IAnnotateLayerProperties pAnnotateLayerProperties = plabelEngineLayerProperties as IAnnotateLayerProperties;

            //将标注实例添加到标注实例集
            pAnnotateLayerPropertiesCollection.Add(pAnnotateLayerProperties);

            //显示标注
            pGeoFeatureLayer.DisplayAnnotation = true;
            axMapControl1.Refresh();



        }

        private void axToolbarControl1_OnMouseDown(object sender, IToolbarControlEvents_OnMouseDownEvent e)
        {

        }
    }
}
