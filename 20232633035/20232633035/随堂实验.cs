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
namespace _20232633035
{
    public partial class 随堂实验 : Form
    {
        public 随堂实验()
        {

            InitializeComponent();
        }
        IFeatureLayer pDistrictLayer;
        IFeatureLayer pStationLayer;

        INewLineFeedback pNewLineFeedback;

        int mapMode = 0;


    }
}
