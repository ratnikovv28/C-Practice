using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_3
{
    public partial class ParamForm : Form
    {
        private Form1 parent;
        public ParamForm(Form1 parentForm)
        {
            InitializeComponent();
            parent = parentForm;

            pointSizeUpDown.Value = parent.PointSize.Width;
            lineWidthUpDown.Value = parent.LineWidth;

            pointSizeUpDown.ValueChanged += pointSizeUpDown_ValueChanged;
            lineWidthUpDown.ValueChanged += lineWidthUpDown_ValueChanged;
        }

        void pointSizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            parent.PointSize = new Size((int)pointSizeUpDown.Value, (int)pointSizeUpDown.Value);
            parent.Refresh();
        }

        void lineWidthUpDown_ValueChanged(object sender, EventArgs e)
        {
            parent.LineWidth = (int)lineWidthUpDown.Value;
            parent.Refresh();
        }
    }
}
