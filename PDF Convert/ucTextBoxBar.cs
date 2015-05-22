using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PDF_Convert
{
    [DefaultEvent("Click")]
    public partial class ucTextBoxBar : UserControl
    {
        public string OutText
        {
            get { return this.txtOutPath.Text; }
            set { this.txtOutPath.Text = value; }
        }
        public bool IsReadOnly
        {
            get { return this.txtOutPath.ReadOnly; }
            set { this.txtOutPath.ReadOnly = value; }
        }

        public ucTextBoxBar()
        {
            InitializeComponent();

        }





    }
}
