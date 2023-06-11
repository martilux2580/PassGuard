using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.PDF
{
	internal class PDFCreator : IPDF
	{
		//Create a PDF given the results of all the rows, name of Vault, Email and SK.
		public void CreatePDF(List<String[]> results, String Vault, String Email, String sk)
		{
			var pdfLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VaultTable-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".pdf"; //Name of file
			if (!File.Exists(pdfLocation))
			{
				var file = File.Create(pdfLocation);
				file.Close(); //Close so Create is not using the file.

				PdfWriter writer = new(pdfLocation);
				PdfDocument pdf = new(writer);
				Document doc = new(pdf);
				doc.SetMargins(8, 8, 8, 8);

				var title = new Paragraph("PassGuard: Vault Content").SetBold().SetFontSize(15); //Title
				title.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
				doc.Add(title);

				var intro = new Paragraph("Date: " + DateTime.Now.ToString("D", new CultureInfo("en-US")) + ", " + DateTime.Now.ToString("HH:mm:ss")).SetFontSize(12); //Date
				intro.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
				intro.SetFixedLeading(14);
				intro.SetMarginBottom(0f);
				doc.Add(intro);

				//Data of Vault
				var intro2 = new Paragraph("Vault Name: " + Vault + "\nVault Filename: " + Vault + ".encrypted" + "\nPassGuard Saved Email: " + Email + "\nPassGuard Saved Security Key (SK): " + sk).SetFontSize(12);
				intro2.SetPaddingLeft(40f);
				intro2.SetFixedLeading(14);
				intro2.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
				doc.Add(intro2);

				//Note
				var note = new Paragraph("Note: Saved Email and SK may not correspond to the Vault. Those values are the ones PassGuard had stored the day the backup was done." +
					"\nNote2: If the column Important has value 1, it means it was saved as an important password. If it has value 0, it was not saved as an important password.").SetFontSize(8);
				note.SetMarginTop(0f);
				note.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
				note.SetMarginBottom(16f);
				doc.Add(note);

				//Table with headers
				Table content = new Table(numColumns: 7).UseAllAvailableWidth();
				content.SetWidth(UnitValue.CreatePercentValue(100));
				content.SetFixedLayout();
				content.SetBorderCollapse(iText.Layout.Properties.BorderCollapsePropertyValue.SEPARATE);
				content.SetBorderBottom(null);
				content.SetMarginBottom(0f);
				content.SetPaddingBottom(0f);

				content.AddCell("URL").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("Name").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("Site Username").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("Site Password").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("Category").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("Notes").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("Important").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));

				doc.Add(content);

				//Table with content
				Table content2 = new Table(numColumns: 7).UseAllAvailableWidth();
				content2.SetWidth(UnitValue.CreatePercentValue(100));
				content2.SetFixedLayout();
				content2.SetMarginBottom(0.1f);

				for (int i = 0; i < results.Count; i++)
				{
					for (int j = 0; j < 7; j++)
					{
						content2.AddCell(results[i][j]).SetFontSize(9);
					}
				}

				doc.Add(content2);

				doc.Close();

				MessageBox.Show(text: "PDF with the content of the Vault was generated successfully in your Documents Folder :)", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
			}
			else { MessageBox.Show(text: "There is already a file with the name of the PDF. Please try again later.", caption: "File with same name at path", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error); }
		}

		//Create a PDF given the results of all the rows, name of Vault, Email and SK.
		public void CreateOutlinePDF()
		{
			var values = new SortedDictionary<String, List<int>>(JsonSerializer.Deserialize<Dictionary<String, List<int>>>(ConfigurationManager.AppSettings["OutlineSavedColours"]));
			var names = values.Keys.ToList();
			var rgb = values.Values.ToList();

			var pdfLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ColoursTable-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".pdf"; //Name of file
			if (!File.Exists(pdfLocation))
			{
				var file = File.Create(pdfLocation);
				file.Close(); //Close so Create is not using the file.

				PdfWriter writer = new(pdfLocation);
				PdfDocument pdf = new(writer);
				Document doc = new(pdf);
				doc.SetMargins(8, 8, 8, 8);

				var title = new Paragraph("PassGuard: Outline Color Configuration").SetBold().SetFontSize(15); //Title
				title.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
				doc.Add(title);

				var intro = new Paragraph("Date: " + DateTime.Now.ToString("D", new CultureInfo("en-US")) + ", " + DateTime.Now.ToString("HH:mm:ss")).SetFontSize(12); //Date
				intro.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
				intro.SetFixedLeading(14);
				intro.SetMarginBottom(4f);
				doc.Add(intro);

				var intro2 = new Paragraph("Configs: ").SetFontSize(12); //Title2
				intro.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
				intro.SetFixedLeading(14);
				intro.SetMarginBottom(0f);
				doc.Add(intro2);

				//Table with headers
				Table content = new Table(numColumns: 6).UseAllAvailableWidth();
				content.SetWidth(UnitValue.CreatePercentValue(100));
				content.SetFixedLayout();
				content.SetBorderCollapse(iText.Layout.Properties.BorderCollapsePropertyValue.SEPARATE);
				content.SetBorderBottom(null);
				content.SetMarginBottom(0f);
				content.SetPaddingBottom(0f);

				content.AddCell("Name").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("R").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("G").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("B").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("Viewer").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
				content.AddCell("Favourite").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));

				doc.Add(content);

				//Table with content
				Table content2 = new Table(numColumns: 6).UseAllAvailableWidth();
				content2.SetWidth(UnitValue.CreatePercentValue(100));
				content2.SetFixedLayout();
				content2.SetMarginBottom(0.1f);

				for (int i = 0; i < values.Count; i++)
				{
					content2.AddCell(names[i]).SetFontSize(9);
					content2.AddCell(rgb[i][0].ToString()).SetFontSize(9);
					content2.AddCell(rgb[i][1].ToString()).SetFontSize(9);
					content2.AddCell(rgb[i][2].ToString()).SetFontSize(9);

					var cell = new Cell();
					cell.SetBackgroundColor(WebColors.GetRGBColor("#" + rgb[i][0].ToString("X2") + rgb[i][1].ToString("X2") + rgb[i][2].ToString("X2")));
					content2.AddCell(cell);

					if (Convert.ToBoolean(rgb[i][4])) { content2.AddCell("Yes").SetFontSize(9); }
					else { content2.AddCell("No").SetFontSize(9); }

				}

				doc.Add(content2);

				doc.Close();

				MessageBox.Show(text: "PDF with your Outline Color Configurations was generated successfully in your Documents Folder :)", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
			}
			else { MessageBox.Show(text: "There is already a file with the name of the PDF. Please try again later.", caption: "File with same name at path", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error); }
		}
	}
}
