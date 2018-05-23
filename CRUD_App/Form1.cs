using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_App
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        void SetSymbolCount(int count);
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
        event EventHandler LockText; // ***
    }

    public partial class Form1 : Form, IMainForm
    {
        public Form1()
        {
            InitializeComponent();
            btnOpen.Click += BtnOpen_Click;
            btnSave.Click += BtnSave_Click;
            fldContent.TextChanged += FldContent_TextChanged;
            btnSelect.Click += btnSelect_Click;
            numFont.Click += lblCounter_Click;
            btnLock.Click += BtnLock_Click;
        }

        #region Проброс событий
        private void FldContent_TextChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null) ContentChanged(this, EventArgs.Empty);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (FileSaveClick != null) FileSaveClick(this, EventArgs.Empty); 
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }
        #endregion

        #region IMainForm

        public string FilePath
        {
            get { return fldPath.Text; }
        }

        public string Content
        {
            get { return fldContent.Text; }
            set { fldContent.Text = value; }
        }

        public void SetSymbolCount (int count)
        {
            lblSymbolCount.Text = count.ToString();
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;
        public event EventHandler LockText; // ****
        #endregion

        private void BtnLock_Click(object sender, EventArgs e) // ***
        {
            if (LockText != null) LockText(this, EventArgs.Empty);
            if (fldContent.ReadOnly == true)
            {
                fldContent.ReadOnly = false;

            }
            else fldContent.ReadOnly = true;
            fldContent.BackColor = System.Drawing.SystemColors.Window;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files|*.txt|All files|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fldPath.Text = dlg.FileName;

                if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
            }
        }

        private void lblCounter_Click(object sender, EventArgs e)
        {
            fldContent.Font = new Font("Calibri", (float)numFont.Value);
        }
    }
}
