using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace RealTimeGraphX.UWP
{
    public class LimGraphSurface : UwpGraphSurface
    {
        private List<System.Drawing.PointF> Points;
        private List<System.Drawing.PointF> cibf;
  
        public void ClonePoints()
        {
            if(Points != null)
            {
                cibf = new List<PointF>(Points);
            } 
        }
        public override void DrawSeries(UwpGraphDataSeries dataSeries, IEnumerable<System.Drawing.PointF> points)
        {
            Points = new List<PointF>(points); 
            if (cibf != null)
            {
                var lastX = points.Last().X;
                var srch = cibf.Where(a => a.X > lastX).FirstOrDefault();
                var ix = Array.IndexOf(cibf.ToArray(), srch);
                if (ix != -1) cibf = cibf.Skip(ix + 1).ToList();
                if (cibf.Count() > 1) base._session.DrawPolyline(cibf.Select(x => new Vector2(x.X, x.Y)).ToList(),
                dataSeries.Stroke, dataSeries.StrokeThickness);
            } 
            base.DrawSeries(dataSeries, points); 
        }
    }
}
