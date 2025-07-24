namespace HostLookup
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnDateiWählen;
        private Button btnAdd;
        private Button btnExportCSV;
        private ListBox listBoxLogs;
        private TextBox txtHostname;
        private Label label1;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            txtHostname = new TextBox();
            btnDateiWählen = new Button();
            btnAdd = new Button();
            btnExportCSV = new Button();
            listBoxLogs = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 25);
            label1.Name = "label1";
            label1.Size = new Size(117, 15);
            label1.TabIndex = 0;
            label1.Text = "Hostname eingeben:";
            // 
            // txtHostname
            // 
            txtHostname.Location = new Point(150, 22);
            txtHostname.Name = "txtHostname";
            txtHostname.Size = new Size(250, 23);
            txtHostname.TabIndex = 1;
            // 
            // btnDateiWählen
            // 
            btnDateiWählen.Location = new Point(28, 60);
            btnDateiWählen.Name = "btnDateiWählen";
            btnDateiWählen.Size = new Size(372, 30);
            btnDateiWählen.TabIndex = 2;
            btnDateiWählen.Text = "Excel-Datei auswählen";
            btnDateiWählen.UseVisualStyleBackColor = true;
            btnDateiWählen.Click += btnDateiWählen_Click;
            // 
            // btnAdd
            // 
            btnAdd.Enabled = false;
            btnAdd.Location = new Point(28, 100);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(372, 30);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Hinzufügen";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnExportCSV
            // 
            btnExportCSV.Enabled = false;
            btnExportCSV.Location = new Point(28, 320);
            btnExportCSV.Name = "btnExportCSV";
            btnExportCSV.Size = new Size(372, 30);
            btnExportCSV.TabIndex = 5;
            btnExportCSV.Text = "CSV exportieren";
            btnExportCSV.UseVisualStyleBackColor = true;
            btnExportCSV.Click += btnExportCSV_Click;
            // 
            // listBoxLogs
            // 
            listBoxLogs.ItemHeight = 15;
            listBoxLogs.Location = new Point(28, 190);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(372, 109);
            listBoxLogs.TabIndex = 4;
            // 
            // Form1
            // 
            ClientSize = new Size(430, 370);
            Controls.Add(label1);
            Controls.Add(txtHostname);
            Controls.Add(btnDateiWählen);
            Controls.Add(btnAdd);
            Controls.Add(listBoxLogs);
            Controls.Add(btnExportCSV);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HostLookup";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
