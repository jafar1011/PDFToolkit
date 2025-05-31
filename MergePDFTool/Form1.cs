using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using PdfiumViewer;
using PDFToolkit;

namespace MergePDFTool
{
    public partial class Form1 : Form
    {
        int a = 1;
        int m = 1;
        private Color currentColor;
        private Color targetColor;
        private int step = 0;
        private const int maxSteps = 20;
        private PdfViewerForm viewerForm;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            
           
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.Items.Count < 2)
            {
                MessageBox.Show("Please add at least two PDF files to merge.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.FileName = "merged.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document doc = new Document();
                        PdfCopy pdf = new PdfCopy(doc, stream);
                        doc.Open();

                        foreach (string file in listBoxFiles.Items)
                        {
                            PdfReader reader = new PdfReader(file.ToString());
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                pdf.AddPage(pdf.GetImportedPage(reader, i));
                            }
                            reader.Close();
                        }

                        doc.Close();
                    }

                    MessageBox.Show("PDFs merged successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            button25.BackColor = Color.Silver;
            //2
            System.Drawing.Image original = PDFToolkit.Properties.Resources.Scissors_icon_black_svg;
            System.Drawing.Image resized = new System.Drawing.Bitmap(original, new System.Drawing.Size(40, 28));
            

            button17.Image = resized;
            button17.ImageAlign = ContentAlignment.MiddleRight;

            //1
            System.Drawing.Image original2 = PDFToolkit.Properties.Resources._1387656;
            System.Drawing.Image resized2 = new System.Drawing.Bitmap(original2, new System.Drawing.Size(36, 33));
            

            button34.Image = resized2;
            button34.ImageAlign = ContentAlignment.MiddleRight;

            //3
            System.Drawing.Image original3 = PDFToolkit.Properties.Resources.free_convert_icon_3209_thumb;
            System.Drawing.Image resized3 = new System.Drawing.Bitmap(original3, new System.Drawing.Size(36, 33));


            button6.Image = resized3;
            button6.ImageAlign = ContentAlignment.MiddleRight;

            //4
            System.Drawing.Image original4 = PDFToolkit.Properties.Resources.Merge_Files_icon_icons;
            System.Drawing.Image resized4 = new System.Drawing.Bitmap(original4, new System.Drawing.Size(36, 33));


            button25.Image = resized4;
            button25.ImageAlign = ContentAlignment.MiddleRight;
            
            
        }

        private void listBoxFiles_DragEnter(object sender, DragEventArgs e)
        {
            
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                listBoxFiles.BackColor = SystemColors.ActiveCaption; // highlight
                label1.BackColor = SystemColors.ActiveCaption;
            }
        }

        private void listBoxFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToLower() == ".pdf")
                {
                    listBoxFiles.Items.Add(file);
                }
            }
            if (listBoxFiles.Items.Count != 0)
            {
                label1.Visible = false;
            }
            else
            {
                label1.Visible = true;
            }

            listBoxFiles.BackColor = Color.WhiteSmoke;
            label1.BackColor = Color.WhiteSmoke;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    listBoxFiles.Items.Add(file);
                }
            }
            if (listBoxFiles.Items.Count != 0)
            {
                label1.Visible = false;
            }
            else
            {
                label1.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
            if (listBoxFiles.Items.Count != 0)
            {
                label1.Visible = false;
            }
            else
            {
                label1.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel4.BringToFront();
         }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Gainsboro;
           
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.WhiteSmoke;
            
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Gainsboro;
            
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.WhiteSmoke;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Gainsboro;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.WhiteSmoke;
        }

        private void listBoxFiles_DragLeave(object sender, EventArgs e)
        {
            listBoxFiles.BackColor = Color.WhiteSmoke;
            label1.BackColor = Color.WhiteSmoke;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int index = listBoxFiles.SelectedIndex;
            if (index > 0)
            {
                object selected = listBoxFiles.SelectedItem;
                listBoxFiles.Items.RemoveAt(index);
                listBoxFiles.Items.Insert(index - 1, selected);
                listBoxFiles.SelectedIndex = index - 1;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            int index = listBoxFiles.SelectedIndex;
            if (index < listBoxFiles.Items.Count - 1 && index != -1)
            {
                object selected = listBoxFiles.SelectedItem;
                listBoxFiles.Items.RemoveAt(index);
                listBoxFiles.Items.Insert(index + 1, selected);
                listBoxFiles.SelectedIndex = index + 1;
            }
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.Gainsboro;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.WhiteSmoke;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Gainsboro;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.WhiteSmoke;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
            panel2.Visible = true;
            panel2.BringToFront();

            button34.BringToFront();
            button17.BringToFront();
            button6.BringToFront();
            button25.BringToFront();

          
          
           
           


            button6.BackColor = Color.Silver;
            button34.BackColor = Color.WhiteSmoke;
            button17.BackColor = Color.WhiteSmoke;
            button25.BackColor = Color.WhiteSmoke;
          
           
        }

       

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Word Files|*.docx;*.pdf";  // Update the filter to include both Word and Image files
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    if (!listBox1.Items.Contains(file))
                        listBox1.Items.Add(file);
                }
            }
            if (listBox1.Items.Count != 0)
            {
                label12.Visible = false;
            }
            else
            {
                label12.Visible = true;
            }
        }

       

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.Gainsboro;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.WhiteSmoke;
        }


       

        

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.BringToFront();
            panel2.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
        }

       

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel3.BringToFront();
           }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Please add files to the list first.");
                return;
            }

            foreach (string inputFile in listBox1.Items)
            {
                string extension = Path.GetExtension(inputFile).ToLower();
                string mode = extension == ".pdf" ? "pdf2docx" : extension == ".docx" ? "docx2pdf" : null;

                if (mode == null)
                {
                    MessageBox.Show("Unsupported file type: " + inputFile);
                    continue;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Save Converted File";
                    saveFileDialog.Filter = mode == "pdf2docx" ? "DOCX Files (*.docx)|*.docx" : "PDF Files (*.pdf)|*.pdf";
                    saveFileDialog.FileName = Path.GetFileNameWithoutExtension(inputFile);

                    if (saveFileDialog.ShowDialog() != DialogResult.OK)
                        continue;

                    string pythonExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Python", "python.exe");
                    string script = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "converter.py");

                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = pythonExe;
                    psi.Arguments = Quote(script) + " " + mode + " " + Quote(inputFile) + " " + Quote(saveFileDialog.FileName);
                    psi.UseShellExecute = false;
                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;
                    psi.CreateNoWindow = true;

                    using (Process process = new Process())
                    {
                        process.StartInfo = psi;
                        process.Start();

                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit();

                        if (process.ExitCode != 0)
                        {
                            MessageBox.Show("Error during conversion:\n" + error);
                        }
                        else
                        {
                            MessageBox.Show("Conversion complete:\n" + saveFileDialog.FileName);
                        }
                    }
                }
            }
        }
        private string Quote(string path)
        {
            return "\"" + path + "\"";
        }

        private void RunPythonConversion(string pythonPath, string scriptPath, string mode, string input, string output)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = pythonPath;
            psi.Arguments = string.Join(" ", new[] { scriptPath, mode, input, output });
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.CreateNoWindow = true;

            try
            {
                using (Process process = Process.Start(psi))
                {
                    string stdout = process.StandardOutput.ReadToEnd();
                    string stderr = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrWhiteSpace(stderr))
                    {
                        MessageBox.Show("Python error:\n" + stderr);
                    }
                    else if (!File.Exists(output))
                    {
                        MessageBox.Show("Conversion reported success but output file not found:\n" + output);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during conversion:\n" + ex.Message);
            }
        }
       

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            pictureBox13.BackColor = Color.Gainsboro;
            pictureBox14.BackColor = Color.Gainsboro;
            pictureBox16.BackColor = Color.Gainsboro;
            pictureBox17.BackColor = Color.Gainsboro;
          
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox13.BackColor = Color.WhiteSmoke;
            pictureBox14.BackColor = Color.WhiteSmoke;
            pictureBox16.BackColor = Color.WhiteSmoke;
            pictureBox17.BackColor = Color.WhiteSmoke;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (listBox1.Items.Count != 0)
            {
                label12.Visible = false;
            }
            else
            {
                label12.Visible = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                listBox1.BackColor = SystemColors.ActiveCaption; // highlight
                label12.BackColor = SystemColors.ActiveCaption;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToLower() == ".docx" || Path.GetExtension(file).ToLower() == ".pdf")
                {
                    listBox1.Items.Add(file);
                }
            }
            if (listBox1.Items.Count != 0)
            {
                label12.Visible = false;
            }
            else
            {
                label12.Visible = true;
            }

            listBox1.BackColor = Color.WhiteSmoke;
            label12.BackColor = Color.WhiteSmoke;
        }

        private void listBox1_DragLeave(object sender, EventArgs e)
        {
            listBox1.BackColor = Color.WhiteSmoke;
            label12.BackColor = Color.WhiteSmoke;
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }
        private void ConvertPdfToDocx(string inputPdfPath, string outputDocxPath)
        {
            string appDir = Application.StartupPath;
            string pythonExe = Path.Combine(appDir, "python", "python.exe");
            string scriptPath = Path.Combine(appDir, "scripts", "convert_pdf_to_docx.py");

            string arguments = "\"" + scriptPath + "\" \"" + inputPdfPath + "\" \"" + outputDocxPath + "\"";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonExe;
            start.Arguments = arguments;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;

            try
            {
                using (Process process = Process.Start(start))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        MessageBox.Show("Error:\n" + error, "Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Conversion completed successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while starting the process:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
             using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
        openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string path = openFileDialog.FileName;
            var viewerForm = new PdfViewerForm(path);
            viewerForm.Show(); // Open as a separate window
        }
    }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selectedFile = listBox1.SelectedItem.ToString();
            string extension = Path.GetExtension(selectedFile).ToLower();

            // Check if the file is PDF
            if (extension == ".pdf")
            {
                // Open PDF directly in PdfiumViewer
                OpenPdfViewer(selectedFile);
            }
            else
            {
                // Ignore DOCX and other file types
                MessageBox.Show("This file type is not supported.");
            }
        }
         private void OpenPdfViewer(string pdfPath)
    {
        PdfViewerForm pdfForm = new PdfViewerForm(pdfPath);
        pdfForm.Show();
    }

         private void listBoxFiles_MouseDoubleClick(object sender, MouseEventArgs e)
         {
             string selectedFile = listBoxFiles.SelectedItem.ToString();
             string extension = Path.GetExtension(selectedFile).ToLower();

             // Check if the file is PDF
             if (extension == ".pdf")
             {
                 // Open PDF directly in PdfiumViewer
                 OpenPdfViewer(selectedFile);
             }
             else
             {
                 // Ignore DOCX and other file types
                 MessageBox.Show("This file type is not supported.");
             }
         }

         private void button8_MouseEnter(object sender, EventArgs e)
         {
             pictureBox10.BackColor = Color.Gainsboro;
         }

         private void button8_MouseLeave(object sender, EventArgs e)
         {
             pictureBox10.BackColor = Color.WhiteSmoke;
         }

         private void button13_MouseEnter(object sender, EventArgs e)
         {
             pictureBox18.BackColor = Color.Gainsboro;
         }

         private void button13_MouseLeave(object sender, EventArgs e)
         {
             pictureBox18.BackColor = Color.WhiteSmoke;
         }

         private void button14_MouseEnter(object sender, EventArgs e)
         {
             pictureBox19.BackColor = Color.Gainsboro;
         }

         private void button14_MouseLeave(object sender, EventArgs e)
         {
             pictureBox19.BackColor = Color.WhiteSmoke;
         }

         private void button14_Click(object sender, EventArgs e)
         {
             using (OpenFileDialog openFileDialog = new OpenFileDialog())
             {
                 openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                 if (openFileDialog.ShowDialog() == DialogResult.OK)
                 {
                     string path = openFileDialog.FileName;
                     var viewerForm = new PdfViewerForm(path);
                     viewerForm.Show(); // Open as a separate window
                 }
             }
         }
       
         private void button15_Click(object sender, EventArgs e)
         {
             if (m == 1)
             {
                 currentColor = panel1.BackColor;
                 targetColor = Color.LightSteelBlue;
                 button15.Text = "Light Mode     ";
                 button16.Text = "Light Mode     ";
                 button23.Text = "Light Mode     ";
                 button27.Text = "Light Mode     ";
                 pictureBox20.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox21.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox28.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox32.Image = PDFToolkit.Properties.Resources._9937122;
                 m = 2;
             }
             else
             {
                 currentColor = panel1.BackColor;
                 targetColor = Color.SlateGray;
                 button15.Text = "Dark Mode      ";
                 button16.Text = "Dark Mode      ";
                 button23.Text = "Dark Mode      ";
                 button27.Text = "Dark Mode      ";
                 pictureBox32.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox20.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox28.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox21.Image = PDFToolkit.Properties.Resources._6714978;
                 m = 1;
             }

             step = 0;
             timer3.Start();
         }

         private void button15_MouseEnter(object sender, EventArgs e)
         {
             pictureBox20.BackColor = Color.Gainsboro;
         }

         private void button15_MouseLeave(object sender, EventArgs e)
         {
             pictureBox20.BackColor = Color.WhiteSmoke;
         }

         private void timer3_Tick(object sender, EventArgs e)
         {
             step++;
             float progress = step / (float)maxSteps;

             Color blended = Color.FromArgb(
                 (int)(currentColor.R + (targetColor.R - currentColor.R) * progress),
                 (int)(currentColor.G + (targetColor.G - currentColor.G) * progress),
                 (int)(currentColor.B + (targetColor.B - currentColor.B) * progress)
             );

             panel1.BackColor = blended;
             panel2.BackColor = blended;
             panel3.BackColor = blended;
             panel4.BackColor = blended;
             panel5.BackColor = blended;
             panel6.BackColor = blended;
             panel7.BackColor = blended;
             panel8.BackColor = blended;
             

             if (step >= maxSteps)
                 timer3.Stop();
         }

         private void button16_Click(object sender, EventArgs e)
         {
             if (m == 1)
             {
                 currentColor = panel1.BackColor;
                 targetColor = Color.LightSteelBlue;
                 button15.Text = "Light Mode     ";
                 button16.Text = "Light Mode     ";
                 button23.Text = "Light Mode     ";
                 button27.Text = "Light Mode     ";
                 pictureBox20.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox21.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox28.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox32.Image = PDFToolkit.Properties.Resources._9937122;
                 m = 2;
             }
             else
             {
                 currentColor = panel1.BackColor;
                 targetColor = Color.SlateGray;
                 button15.Text = "Dark Mode      ";
                 button16.Text = "Dark Mode      ";
                 button23.Text = "Dark Mode      ";
                 button27.Text = "Dark Mode      ";
                 pictureBox32.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox20.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox28.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox21.Image = PDFToolkit.Properties.Resources._6714978;
                 m = 1;
             }

             step = 0;
             timer3.Start();
         }

         private void button16_MouseEnter(object sender, EventArgs e)
         {
             pictureBox21.BackColor = Color.Gainsboro;
         }

         private void button16_MouseLeave(object sender, EventArgs e)
         {
             pictureBox21.BackColor = Color.WhiteSmoke;
         }

         private void button17_Click(object sender, EventArgs e)
         {
             panel1.Visible = false;
             panel2.Visible = false;
             panel7.Visible = false;
             panel5.Visible = true;
             panel5.BringToFront();

             button34.BringToFront();
             button17.BringToFront();
             button6.BringToFront();
             button25.BringToFront();

            
            
            
            

             button17.BackColor = Color.Silver;
             button34.BackColor = Color.WhiteSmoke;
             button25.BackColor = Color.WhiteSmoke;
             button6.BackColor = Color.WhiteSmoke;
         }

         private void pictureBox22_Click(object sender, EventArgs e)
         {

         }

         private void panel5_Paint(object sender, PaintEventArgs e)
         {

         }

         private void button18_Click(object sender, EventArgs e)
         {
             OpenFileDialog openFileDialog = new OpenFileDialog();
             openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
             openFileDialog.Title = "Select a PDF file";

             if (openFileDialog.ShowDialog() == DialogResult.OK)
             {
                 textBox1.Text = openFileDialog.FileName;
             }
         }
        

         private void button19_Click(object sender, EventArgs e)
         {
             string sourcePdfPath = textBox1.Text;

             if (!File.Exists(sourcePdfPath))
             {
                 MessageBox.Show("Selected PDF file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }

             string inputRanges = textBox2.Text.Trim();

             if (string.IsNullOrEmpty(inputRanges))
             {
                 MessageBox.Show("Please enter split ranges like 1-5,6-10.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             PdfReader reader = new PdfReader(sourcePdfPath);
             int totalPages = reader.NumberOfPages;

             string[] ranges = inputRanges.Split(',');
             List<Tuple<int, int>> validRanges = new List<Tuple<int, int>>();

             int index;
             for (index = 0; index < ranges.Length; index++)
             {
                 string range = ranges[index].Trim();
                 string[] bounds = range.Split('-');

                 if (bounds.Length != 2)
                 {
                     MessageBox.Show("Invalid range format: " + range + ". Use format like 1-5.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     reader.Close();
                     return;
                 }

                 int startPage, endPage;

                 if (!int.TryParse(bounds[0], out startPage) || !int.TryParse(bounds[1], out endPage))
                 {
                     MessageBox.Show("Invalid numbers in range: " + range, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     reader.Close();
                     return;
                 }

                 if (startPage > endPage || startPage < 1 || endPage > totalPages)
                 {
                     MessageBox.Show("Page range out of bounds or reversed in: " + range + ". Must be within 1 to " + totalPages.ToString() + ".", "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     reader.Close();
                     return;
                 }

                 validRanges.Add(new Tuple<int, int>(startPage, endPage));
             }

             // Prompt save location ONCE
             SaveFileDialog saveDialog = new SaveFileDialog();
             saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
             saveDialog.Title = "Save First Split PDF";
             saveDialog.FileName = Path.GetFileName(sourcePdfPath); // use original file name

             if (saveDialog.ShowDialog() != DialogResult.OK)
             {
                 reader.Close();
                 return;
             }

             string originalName = Path.GetFileNameWithoutExtension(sourcePdfPath);
             string folderPath = Path.GetDirectoryName(saveDialog.FileName);

             for (int i = 0; i < validRanges.Count; i++)
             {
                 int start = validRanges[i].Item1;
                 int end = validRanges[i].Item2;

                 string splitPath = Path.Combine(folderPath, originalName + "_Part" + (i + 1).ToString() + ".pdf");

                 Document document = new Document();
                 PdfCopy pdfCopy = new PdfCopy(document, new FileStream(splitPath, FileMode.Create));
                 document.Open();

                 int pageNum;
                 for (pageNum = start; pageNum <= end; pageNum++)
                 {
                     PdfImportedPage page = pdfCopy.GetImportedPage(reader, pageNum);
                     pdfCopy.AddPage(page);
                 }

                 document.Close();

                 MessageBox.Show("Split " + (i + 1).ToString() + " saved: pages " + start.ToString() + " to " + end.ToString(), "Split Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }

             reader.Close();
             MessageBox.Show("All splits completed successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);



         }

         

         

         

         private void button23_Click(object sender, EventArgs e)
         {
             if (m == 1)
             {
                 currentColor = panel1.BackColor;
                 targetColor = Color.LightSteelBlue;
                 button15.Text = "Light Mode     ";
                 button16.Text = "Light Mode     ";
                 button23.Text = "Light Mode     ";
                 button27.Text = "Light Mode     ";
                 pictureBox20.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox21.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox28.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox32.Image = PDFToolkit.Properties.Resources._9937122;
                 m = 2;
             }
             else
             {
                 currentColor = panel1.BackColor;
                 targetColor = Color.SlateGray;
                 button15.Text = "Dark Mode      ";
                 button16.Text = "Dark Mode      ";
                 button23.Text = "Dark Mode      ";
                 button27.Text = "Dark Mode      ";
                 pictureBox20.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox28.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox21.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox32.Image = PDFToolkit.Properties.Resources._6714978;
                 m = 1;
             }

             step = 0;
             timer3.Start();
         }

         private void button23_MouseEnter(object sender, EventArgs e)
         {
             pictureBox28.BackColor = Color.Gainsboro;
         }

         private void button23_MouseLeave(object sender, EventArgs e)
         {
             pictureBox28.BackColor = Color.WhiteSmoke;
         }

         

         

         

         

         private void pictureBox27_Click(object sender, EventArgs e)
         {
             panel6.Visible = true;
             panel6.BringToFront();
         }

         private void button24_Click(object sender, EventArgs e)
         {
             panel6.Visible = false;
         }

         private void button18_MouseEnter(object sender, EventArgs e)
         {
             pictureBox23.BackColor = Color.Gainsboro;
         }

         private void button18_MouseLeave(object sender, EventArgs e)
         {
             pictureBox23.BackColor = Color.WhiteSmoke;
         }

         private void button19_MouseEnter(object sender, EventArgs e)
         {
             pictureBox30.BackColor = Color.Gainsboro;
         }

         private void button19_MouseLeave(object sender, EventArgs e)
         {
             pictureBox30.BackColor = Color.WhiteSmoke;
         }

         private void textBox2_TextChanged(object sender, EventArgs e)
         {

         }

         private void label15_Click(object sender, EventArgs e)
         {

         }

         private void label13_Click(object sender, EventArgs e)
         {

         }

        


         private void CompressPdfWithGhostscript(string inputPath, string outputPath)
         {
             string ghostscriptExe = Path.Combine(Application.StartupPath, "ghostscript\\gswin32c.exe");

             string args = "-sDEVICE=pdfwrite " +
                           "-dCompatibilityLevel=1.4 " +
                           "-dPDFSETTINGS=/screen " +
                           "-dNOPAUSE -dQUIET -dBATCH " +
                           "-sOutputFile=\"" + outputPath + "\" \"" + inputPath + "\"";

             ProcessStartInfo psi = new ProcessStartInfo();
             psi.FileName = ghostscriptExe;
             psi.Arguments = args;
             psi.CreateNoWindow = true;
             psi.UseShellExecute = false;
             psi.RedirectStandardOutput = true;
             psi.RedirectStandardError = true;

             using (Process process = Process.Start(psi))
             {
                 string output = process.StandardOutput.ReadToEnd();
                 string errors = process.StandardError.ReadToEnd();

                 process.WaitForExit();

                 if (process.ExitCode != 0)
                 {
                     MessageBox.Show("Compression failed:\n" + errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
                 else
                 {
                     MessageBox.Show("PDF successfully compressed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
             }
         }

         private void button32_Click(object sender, EventArgs e)
         {
             panel8.Visible = false;
         }

         private void pictureBox33_Click(object sender, EventArgs e)
         {
             panel8.Visible = true;
             panel8.BringToFront();
         }

         private void button31_Click(object sender, EventArgs e)
         {
             OpenFileDialog openFileDialog = new OpenFileDialog();
             openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
             openFileDialog.Title = "Select a PDF file";

             if (openFileDialog.ShowDialog() == DialogResult.OK)
             {
                 textBox4.Text = openFileDialog.FileName;
             }
         }

        

        

        

       

         private void button34_Click(object sender, EventArgs e)
         {
             panel7.Visible = true;
             panel7.BringToFront();
             panel1.Visible = false;
             panel2.Visible = false;
             panel5.Visible = false;

             button34.BringToFront();
             button17.BringToFront();
             button6.BringToFront();
             button25.BringToFront();

          
           
           
             
             button34.BackColor = Color.Silver;
             button25.BackColor = Color.WhiteSmoke;
             button17.BackColor = Color.WhiteSmoke;
             button6.BackColor = Color.WhiteSmoke;

           
            
           
          
         }

       

         private void button30_Click(object sender, EventArgs e)
         {
             string inputPath = textBox4.Text;

             if (!File.Exists(inputPath))
             {
                 MessageBox.Show("Please provide a valid PDF file path in TextBox1.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             SaveFileDialog saveDialog = new SaveFileDialog();
             saveDialog.Filter = "PDF Files|*.pdf";
             saveDialog.Title = "Save Compressed PDF";
             saveDialog.FileName = Path.GetFileNameWithoutExtension(inputPath) + "_compressed.pdf";

             if (saveDialog.ShowDialog() == DialogResult.OK)
             {
                 CompressPdfWithGhostscript(inputPath, saveDialog.FileName);
             }
         }

         private void button30_MouseEnter(object sender, EventArgs e)
         {
             pictureBox31.BackColor = Color.Gainsboro;
         }

         private void button30_MouseLeave(object sender, EventArgs e)
         {
             pictureBox31.BackColor = Color.WhiteSmoke;
         }

         private void button31_MouseEnter(object sender, EventArgs e)
         {
             pictureBox36.BackColor = Color.Gainsboro;
         }

         private void button31_MouseLeave(object sender, EventArgs e)
         {
             pictureBox36.BackColor = Color.WhiteSmoke;
         }

         

        

         private void button27_MouseEnter(object sender, EventArgs e)
         {
             pictureBox32.BackColor = Color.Gainsboro;
         }

         private void button27_MouseLeave(object sender, EventArgs e)
         {
             pictureBox32.BackColor = Color.WhiteSmoke;
         }

        

        

        

         private void button27_Click(object sender, EventArgs e)
         {
             if (m == 1)
             {
                 currentColor = panel1.BackColor;
                 targetColor = Color.LightSteelBlue;
                 button15.Text = "Light Mode     ";
                 button16.Text = "Light Mode     ";
                 button23.Text = "Light Mode     ";
                 button27.Text = "Light Mode     ";
                 pictureBox20.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox21.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox28.Image = PDFToolkit.Properties.Resources._9937122;
                 pictureBox32.Image = PDFToolkit.Properties.Resources._9937122;
                 m = 2;
             }
             else
             {
                 currentColor = panel1.BackColor;
                 targetColor = Color.SlateGray;
                 button15.Text = "Dark Mode      ";
                 button16.Text = "Dark Mode      ";
                 button23.Text = "Dark Mode      ";
                 button27.Text = "Dark Mode      ";
                 pictureBox32.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox20.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox28.Image = PDFToolkit.Properties.Resources._6714978;
                 pictureBox21.Image = PDFToolkit.Properties.Resources._6714978;
                 m = 1;
             }

             step = 0;
             timer3.Start();
         }

         private void button25_Click(object sender, EventArgs e)
         {
             panel1.Visible = true;
             panel1.BringToFront();
             panel7.Visible = false;
             panel2.Visible = false;
             panel5.Visible = false;

             button34.BringToFront();
             button17.BringToFront();
             button6.BringToFront();
             button25.BringToFront();

          
            
            
            

             button25.BackColor = Color.Silver;
             button34.BackColor = Color.WhiteSmoke;
             button17.BackColor = Color.WhiteSmoke;
             button6.BackColor = Color.WhiteSmoke;

          
            
            
           
         }


        

         
       


       

     

       

      

       

        

        






    }
        }
    

