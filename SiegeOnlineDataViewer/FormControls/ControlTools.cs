using System.Drawing;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.FormControls
{
	public static class ControlTools
	{
		private delegate void RichTextAddMessageCallback(RichTextBox richText, string message, Color color, FontStyle? font);

		public static void RichTextAddMessage(RichTextBox richText, string message)
		{
			RichTextAddMessage(richText, message, null);
		}

		public static void RichTextAddMessage(RichTextBox richText, string message, FontStyle? font)
		{
			Color color = Color.Empty;
			string searchedString = message.ToLowerInvariant();
			if (searchedString.Contains("failed")
				|| searchedString.Contains("error")
				|| searchedString.Contains("warning"))
			{
				color = Color.Red;
			}
			else if (searchedString.Contains("success"))
			{
				color = Color.Green;
			}

			RichTextAddMessage(richText, message, color, font);
		}

		public static void RichTextAddMessage(RichTextBox richText, string message, Color color, FontStyle? font)
		{
			if (richText.InvokeRequired)
			{
				var cb = new RichTextAddMessageCallback(RichTextAddMessageInternal);
				richText.BeginInvoke(cb, message, color);
			}
			else
			{
				RichTextAddMessageInternal(richText, message, color, font);
			}
		}

		private static void RichTextAddMessageInternal(RichTextBox richText, string message, Color color, FontStyle? font)
		{
			//string formattedMessage = String.Format("{0:G}   {1}{2}", DateTime.Now, message, Environment.NewLine);
			string formattedMessage = message;
			
			if (color != Color.Empty)
			{
				richText.SelectionColor = color;
			}

			if (font.HasValue)
				richText.SelectionFont = new Font(richText.Font, font.Value);
	
			richText.SelectedText = formattedMessage;
			richText.SelectionStart = richText.Text.Length;
		}
	}
}
