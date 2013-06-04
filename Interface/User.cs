using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CbhLib.Interface
{
    public class User
    {
        public static bool ConfirmAction(string text)
        {
            DialogResult dr = MessageBox.Show(text, "Are you sure?",  MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                return true;
            return false;
        }

        public static bool ConfirmAction()
        {
            return ConfirmAction("Are you sure?");
        }

        public static bool GetSaveFilename(out string filename)
        {
            filename = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
            }
            sfd.Dispose();
            if (string.IsNullOrEmpty(filename))
                return false;
            return true;
        }

        public static bool GetOpenFilename(out string filename)
        {
            filename = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
            }
            ofd.Dispose();

            if (string.IsNullOrEmpty(filename))
                return false;
            return true;

        }
    }
}
