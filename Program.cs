using HtmlAgilityPack;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using System.IO.Compression;
using iTextSharp.xmp.impl.xpath;
using iTextSharp.text.pdf.codec.wmf;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;	
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PdfConvert
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Write("PdfAdını giriniz;");
			string name = Console.ReadLine();
			FileInfo pdf = new FileInfo(name);
			string pdfname = pdf.FullName;
			////yukarısı kullanıcıdan klasör ismini alıyor
			//var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "index.html");

			var sb = new StringBuilder();
			sb.AppendLine("<html lang=\"tr\">");
			sb.AppendLine("<head>");
			sb.AppendLine("</head>");
			sb.AppendLine("<body>");
			sb.AppendLine("</body>");
			sb.AppendLine("</form>");
			sb.AppendLine("</html>");

			string ExeDosyaYolu = Application.Zip.ToString();
			string dosyaYolu = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName + "\\Dosyalar\\index.html";
			string css = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName + "\\Dosyalar\\main.css";
			string css1 = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName + "\\Dosyalar\\custom.css";
			// dosya yolu belirleme



			// Dikkat et
			// Read the PDF file
			PdfReader reader = new PdfReader(pdfname + ".pdf");//"C:\\Users\\Yiğit Sezen\\Desktop\\Yazılım\\c#\\ConsoleApp1\\1.pdf"
			string text = "";
			;
			for (int page = 1; page <= reader.NumberOfPages; page++)
			{
				text +="<p>"+"<h1>" + PdfTextExtractor.GetTextFromPage(reader, page)+"</h1>" + "</p>";
			}
			//StreamWriter yazı = new StreamWriter(dosyaYolu);
			// Generate the HTML code
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(sb.ToString());
			HtmlNode title = doc.DocumentNode.SelectSingleNode("//h1");
			HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
			body.InnerHtml = "<link rel=\"stylesheet\" href=\"main.css\">" + "<link rel=\"stylesheet\" href=\"custom.css\">" + "<div class=logo >" + "<img src= logo.png>" + "</div>" + text;

			// Save the HTML file	
			doc.Save(dosyaYolu);

			if (File.Exists(css))
				File.Delete(css);

			// To move a file or folder to a new location:
			File.Copy(Environment.CurrentDirectory + "\\Dosyalar\\main.css", css);



			string sourceFolder = @"C:\Users\Yiğit Sezen\Desktop\Yazılım\c#\PdfConvert\PdfConvert\Dosyalar";
			var targetZipFile = @"C:\Users\Yiğit Sezen\Desktop\Yazılım\c#\PdfConvert\PdfConvert\Sablon.zip";
			File.Delete(targetZipFile);


			ZipFile.CreateFromDirectory(sourceFolder, targetZipFile);

			//}

		}
	}
}


