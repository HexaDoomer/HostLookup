namespace HostLookup
{
    partial class FormLadebalken
    {
        private System.ComponentModel.IContainer components = null;
        public ProgressBar progressBar;
        public Label lblStatus;

        private void InitializeComponent()
        {
            this.progressBar = new ProgressBar();
            this.lblStatus = new Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 42);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 23);
            this.progressBar.Style = ProgressBarStyle.Continuous;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(360, 23);
            this.lblStatus.Text = "Ladevorgang läuft...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormLadebalken
            // 
            this.ClientSize = new System.Drawing.Size(384, 81);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "FormLadebalken";
            this.Text = "Bitte warten";
            this.ResumeLayout(false);
        }
    }

}
