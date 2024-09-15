using Microsoft.Extensions.Configuration;
using PayslipGenerator.Classes;
using PayslipGenerator.Properties;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Configuration;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.PortableExecutable;
using PdfSharp.UniversalAccessibility.Drawing;

namespace PayslipGenerator
{
    public partial class Form1 : Form
    {

        public IConfiguration config = Program.AppConfiguration;

        public Form1()
        {
            InitializeComponent();
            BindEmployeesToComboBox();
            //pictureBox1.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\logo.png");

            txtdailyRate.KeyPress += new KeyPressEventHandler(txtNumeric_KeyPress);
            txtDaysIn.KeyPress += new KeyPressEventHandler(txtNumeric_KeyPress);
            txtAbsents.KeyPress += new KeyPressEventHandler(txtNumeric_KeyPress);
            txtOthers.KeyPress += new KeyPressEventHandler(txtNumeric_KeyPress);
            txtAdjustments.KeyPress += new KeyPressEventHandler(txtNumeric_KeyPress);
            txtWorkingDays.KeyPress += new KeyPressEventHandler(txtNumeric_KeyPress);

            ClearFields();
        }

        private void BindEmployeesToComboBox()
        {
            // Retrieve list of roles from appsettings.json
            var roles = config.GetSection("AppSettings:Employees")
                .Get<List<string>>();

            // Bind roles to the ComboBox
            if (roles != null)
                cmbEmp.DataSource = roles;
        }

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox? txt = sender as TextBox;

            // Allow control characters like backspace and delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;  // Block non-numeric input
            }

            // If a decimal point is entered, ensure only one exists in the textbox
            if (e.KeyChar == '.' && txt.Text.Contains('.'))
            {
                e.Handled = true;
            }

            // Limit the number of decimal places to 2
            if (txt.Text.Contains("."))
            {
                int decimalIndex = txt.Text.IndexOf('.');
                string afterDecimal = txt.Text.Substring(decimalIndex + 1);

                // Block input if more than 2 digits are entered after the decimal point
                if (afterDecimal.Length >= 2 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            foreach (TextBox txt in this.Controls.OfType<TextBox>())
            {
                if (txt.Name != "txtdailyRate")
                {
                    if (txt.Text == string.Empty)
                    {

                    }

                }
            }

            GeneratePayslip();
        }

        public void GeneratePayslip()
        {
            // Define the file path where the PDF will be saved
            var path = config["AppSettings:PayslipPath"];
            var showGovBenefits = Convert.ToBoolean(config["AppSettings:ShowGovBenefits"]);
            string filePath = $"{path}\\{cmbEmp.SelectedItem.ToString().Replace(" ", "")}_{DateTime.Now:yyyyMMdd}.pdf";

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("No file path assigned. Please check appsettings.");
                return;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            // Ensure the directory exists

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Payslip";

            // Create an empty page
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromCentimeter(21); // A4 width in centimeters
            page.Height = XUnit.FromCentimeter(29.7); // A4 height in centimeters


            // Create an XGraphics object
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Define fonts
            XFont font = new XFont("Verdana", 12, XFontStyleEx.Regular);
            XFont boldFont = new XFont("Verdana", 12, XFontStyleEx.Bold);


            //// PAGE TITLE
            XFont titleFont = new XFont("Verdana", 24, XFontStyleEx.Regular);
            gfx.DrawString("CONFIDENTIAL PAYSLIP", titleFont, XBrushes.Black, new XRect(185, 80, page.Width, 30), XStringFormats.CenterLeft);


            // Load image (make sure the image path is correct)
            //string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\logo.png");
            XImage xImage = ConvertResourceImageToXImage("logo");
            if (xImage != null)
            {

                // Calculate aspect ratio and fit the image proportionally within specified bounds
                double imageWidth = xImage.PixelWidth;
                double imageHeight = xImage.PixelHeight;

                // Max dimensions for the image on the PDF
                double maxImageWidth = 100;
                double maxImageHeight = 100;

                // Calculate the scaling factor to maintain aspect ratio
                double scale = Math.Min(maxImageWidth / imageWidth, maxImageHeight / imageHeight);

                // Calculate new image size based on scaling factor
                double scaledWidth = imageWidth * scale;
                double scaledHeight = imageHeight * scale;

                // Draw image at the top-left corner with scaled size
                gfx.DrawImage(xImage, 50, 30, scaledWidth, scaledHeight);
            }
            else
            {
                MessageBox.Show("Image not found!");
            }

            // Define table dimensions
            double tableX = 50;
            double tableY = 150;
            double rowHeight = 20;
            double columnWidth1 = 200;
            double columnWidth2 = 100;
            double columnWidth3 = 100;
            double columnWidth4 = 100;

            var particulars = GetPayroll();


            // Draw table headers
            DrawCell(gfx, tableX, tableY, columnWidth1, rowHeight, "Period", boldFont, false, true);
            DrawCell(gfx, tableX + columnWidth1, tableY, columnWidth2, rowHeight, "Gross", boldFont, false, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2, tableY, columnWidth3, rowHeight, "Deductions", boldFont, false, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, tableY, columnWidth4, rowHeight, "Net", boldFont, false, true);

            // Draw table rows
            double currentY = tableY + rowHeight;
            DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "   Basic Salary", font);
            DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, particulars.BasicSalary.ToString("N2"), font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth2, rowHeight, string.Empty, font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, currentY, columnWidth2, rowHeight, string.Empty, font, true);

            currentY += rowHeight;
            DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "   Add: Adjustments", font);
            DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, particulars.Adjustments.ToString("N2"), font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth2, rowHeight, string.Empty, font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, currentY, columnWidth2, rowHeight, string.Empty, font, true);

            currentY += rowHeight;
            DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "   Less: Absences", font);
            DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, string.Empty, font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth2, rowHeight, particulars.Absents.ToString("N2"), font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, currentY, columnWidth2, rowHeight, string.Empty, font, true);

            currentY += rowHeight;
            DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "   Others", font);
            DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, string.Empty, font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth2, rowHeight, particulars.Others.ToString("N2"), font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, currentY, columnWidth2, rowHeight, string.Empty, font, true);

            if (showGovBenefits)
            {
                currentY += rowHeight;
                DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "   Government Benefits", font, true);
                DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, string.Empty, font);
                DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth2, rowHeight, string.Empty, font);
                DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, currentY, columnWidth2, rowHeight, string.Empty, font);

                currentY += rowHeight;
                DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "   SSS", font, true);
                DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, string.Empty, font);
                DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth2, rowHeight, "<t3>", font);
                DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, currentY, columnWidth2, rowHeight, string.Empty, font);

                currentY += rowHeight;
                DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "   PHIC", font, true);
                DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, string.Empty, font);
                DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth2, rowHeight, "<t3>", font);
                DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, currentY, columnWidth2, rowHeight, string.Empty, font);

                currentY += rowHeight;
                DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "   HDMF", font, true);
                DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, string.Empty, font);
                DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth2, rowHeight, "<t3>", font);
                DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth3, currentY, columnWidth2, rowHeight, string.Empty, font);
            }

            currentY += rowHeight;
            DrawCell(gfx, tableX, currentY, columnWidth1, rowHeight, "Total    ", boldFont, true);
            DrawCell(gfx, tableX + columnWidth1, currentY, columnWidth2, rowHeight, particulars.TotalGross.ToString("N2"), font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2, currentY, columnWidth3, rowHeight, particulars.TotalDeduction.ToString("N2"), font, true);
            DrawCell(gfx, tableX + columnWidth1 + columnWidth2 + columnWidth4, currentY, columnWidth3, rowHeight, particulars.TotalNet.ToString("N2"), font, true);




            //// SIGNATORY
            XFont signatory = new XFont("Verdana", 12, XFontStyleEx.Regular);
            XPen pen = new XPen(XColors.Black, 2);
            gfx.DrawString("Certified by : ", signatory, XBrushes.Black, new XRect(50, 400, page.Width, 30), XStringFormats.CenterLeft);
            gfx.DrawLine(pen, 50, 470, 250, 470);
            gfx.DrawString(config["AppSettings:Signatory"].ToString().ToUpper(), signatory, XBrushes.Black, new XRect(50, 480, page.Width, 30), XStringFormats.CenterLeft);
            gfx.DrawString(config["AppSettings:Position"].ToString().ToUpper(), signatory, XBrushes.Black, new XRect(50, 500, page.Width, 30), XStringFormats.CenterLeft);

            // Save the document
            document.Save(filePath);

            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }

        static void DrawCell(XGraphics gfx, double x, double y, double width, double height, string text, XFont font, bool isRight = false, bool isHeaders = false)
        {
            // Draw the cell border
            gfx.DrawRectangle(XPens.Black, x, y, width, height);

            // Draw the text inside the cell
            gfx.DrawString(text, font, XBrushes.Black, new XRect(x, y, width, height), (isRight ? XStringFormats.CenterRight : (isHeaders ? XStringFormats.Center : XStringFormats.CenterLeft)));
        }

        public Payroll GetPayroll()
        {

            var asdasd = (Convert.ToDouble(txtdailyRate.Text) * Convert.ToDouble(txtDaysIn.Text)) + Convert.ToDouble(txtAdjustments.Text);
            var qweqwe = ((Convert.ToDouble(txtAbsents.Text) * Convert.ToDouble(txtdailyRate.Text)) + Convert.ToDouble(txtOthers.Text));
            Payroll payroll = new()
            {
                DailyRate = Convert.ToDouble(txtdailyRate.Text),
                BasicSalary = (Convert.ToDouble(txtdailyRate.Text) * Convert.ToDouble(txtWorkingDays.Text)),
                DaysIn = Convert.ToDouble(txtDaysIn.Text),
                Absents = Convert.ToDouble(txtdailyRate.Text) * Convert.ToDouble(txtAbsents.Text),
                Adjustments = Convert.ToDouble(txtdailyRate.Text) * Convert.ToDouble(txtAdjustments.Text),
                Others = Convert.ToDouble(txtOthers.Text),
                TotalGross = (Convert.ToDouble(txtdailyRate.Text) * Convert.ToDouble(txtWorkingDays.Text) +
                                Convert.ToDouble(txtdailyRate.Text) * Convert.ToDouble(txtAdjustments.Text)),
                TotalDeduction = (((Convert.ToDouble(txtAbsents.Text) * Convert.ToDouble(txtdailyRate.Text)) + Convert.ToDouble(txtOthers.Text))),
                TotalNet = ((Convert.ToDouble(txtdailyRate.Text) * Convert.ToDouble(txtWorkingDays.Text))) -
                            ((Convert.ToDouble(txtAbsents.Text) * Convert.ToDouble(txtdailyRate.Text)) + Convert.ToDouble(txtOthers.Text))
            };

            return payroll;
        }

        private void cmbEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtdailyRate.Text = config[$"AppSettings:Rate{cmbEmp.SelectedIndex}"].ToString();

            ClearFields();
        }


        public void ClearFields()
        {
            foreach (TextBox txt in this.Controls.OfType<TextBox>())
            {
                if (txt.Name != "txtdailyRate")
                    txt.Text = string.Empty;

                txt.TextAlign = HorizontalAlignment.Right;
            }
        }


        public static XImage ConvertResourceImageToXImage(string imageName)
        {
            // Load image from resources
            Bitmap bitmap = Resources.ResourceManager.GetObject(imageName) as Bitmap;

            if (bitmap == null)
            {
                throw new ArgumentException("Image not found in resources.");
            }

            // Convert Bitmap to MemoryStream
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0; // Reset stream position

                // Create XImage from MemoryStream
                return XImage.FromStream(stream);
            }
        }
    }
}