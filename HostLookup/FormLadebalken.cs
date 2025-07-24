using System.Windows.Forms;

namespace HostLookup
{
    public partial class FormLadebalken : Form
    {
        public FormLadebalken()
        {
            InitializeComponent();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Verhindert das Schließen, solange das Fenster aktiv ist
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // blockiert das Schließen durch Benutzer
            }
            base.OnFormClosing(e);
        }

    }
}
