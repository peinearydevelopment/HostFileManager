using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HostFileManager
{
    public class HostFileManagerApplicationContext : ApplicationContext
    {
        private NotifyIcon _notifyIcon;
        private const string IconFileName = @"icons\Development.ico";

        public HostFileManagerApplicationContext()
        {
            InitializeContext();
        }

        private void InitializeContext()
        {
            var components = new Container();
            _notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(IconFileName),
                Visible = true
            };

            _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Development", showDetailsItem_Click));
            _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("QualityAssurance", showDetailsItem_Click));
            _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Production", showDetailsItem_Click));

            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Open HOSTS file", openHostsFileItem_Click));
            _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("Open HOSTS folder", openHostsFolderItem_Click));
        }

        private void showDetailsItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Icon = new Icon(@"icons\" + ((ToolStripMenuItem) sender).Text + ".ico");
        }

        private static ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, EventHandler eventHandler)
        {
            var item = new ToolStripMenuItem(displayText);
            if (eventHandler != null) { item.Click += eventHandler; }
            if (File.Exists($@"icons\{displayText}.ico"))
            {
                item.Image = Image.FromFile($@"icons\{displayText}.ico");
            }
            item.ToolTipText = displayText;
            return item;
        }

        private static void openHostsFileItem_Click(object sender, EventArgs e)
        {
            try { Process.Start("notepad.exe", HostFileManager.HostsFileName); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Cannot Open HOSTS File", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private static void openHostsFolderItem_Click(object sender, EventArgs e)
        {
            try { Process.Start(new FileInfo(HostFileManager.HostsFileName).DirectoryName); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Cannot Open HOSTS Folder", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
