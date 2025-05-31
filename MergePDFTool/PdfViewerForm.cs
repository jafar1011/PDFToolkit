using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PdfiumViewer;


namespace PDFToolkit
{
    public partial class PdfViewerForm : Form
    {
        private PdfViewer pdfViewer;
        

        public PdfViewerForm(string pdfPath)
        {
            InitializeComponent();







            // Set form title
            this.Text = "PDF Viewer";

            // Center the form on the screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Set form size (width x height)
            this.Width = 600; // Adjust width
            this.Height = 600; // Adjust height

            pdfViewer = new PdfiumViewer.PdfViewer
            {
                Dock = DockStyle.Fill
            };
            pdfViewer.ShowToolbar = true;
            pdfViewer.Renderer.Cursor = Cursors.IBeam;
            



            this.Controls.Add(pdfViewer);
            

            try
            {
                var document = PdfiumViewer.PdfDocument.Load(pdfPath);
                pdfViewer.Document = document;
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading PDF: " + ex.Message);
                this.Close();
            }
        }

        private void PdfViewerForm_Load(object sender, EventArgs e)
        {
           
        }

    }
}