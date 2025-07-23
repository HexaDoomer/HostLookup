using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace HostLookup
{
    public partial class Form1 : Form
    {
        #region Excel-Objekte & Variablen

        private Excel.Application? xlApp;
        private Excel.Workbook? workbook;
        private Excel.Worksheet? worksheet;
        private Dictionary<string, (string MAC, string UUID)> excelDaten = new();
        private List<string> gesammelteDaten = new();
        private bool dateiGeladen = false;
        private System.Windows.Forms.Timer fadeTimer;

        #endregion

        #region Konstruktor

        public Form1()
        {
            InitializeComponent();

            this.Opacity = 0;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Fade-in
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 4;
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();

            // Start-Sound
            try
            {
                string soundPfad = @"C:\Users\kristian.pena\Documents\Projekt\HostLookup\HostLookup\start.wav";
                if (File.Exists(soundPfad))
                {
                    SoundPlayer player = new SoundPlayer(soundPfad);
                    player.Play();
                }
            }
            catch
            {
                // Fehler ignorieren
            }

            // Eingabefeld vorbereiten
            txtHostname.Multiline = true;
            txtHostname.ScrollBars = ScrollBars.Vertical;
            btnAdd.Enabled = false;
            btnExportCSV.Enabled = false;
        }

        private void FadeTimer_Tick(object? sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.05;
            else
            {
                fadeTimer.Stop();
                fadeTimer.Dispose();
            }
        }

        #endregion

        #region Excel-Objekte freigeben

        private void ReleaseExcelObjects()
        {
            try
            {
                if (worksheet != null) Marshal.ReleaseComObject(worksheet); worksheet = null;
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                    workbook = null;
                }
                if (xlApp != null)
                {
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                }
            }
            catch { }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        #endregion

        #region Excel laden

        private void btnDateiWählen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Excel-Dateien (*.xlsx;*.xls;*.xlsm)|*.xlsx;*.xls;*.xlsm",
                Title = "Excel-Datei auswählen"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using FormLadebalken ladeFenster = new FormLadebalken();
                ladeFenster.Show();
                ladeFenster.BringToFront();
                ladeFenster.ControlBox = false;

                Enabled = false;
                UseWaitCursor = true;

                try
                {
                    ReleaseExcelObjects();

                    xlApp = new Excel.Application();
                    workbook = xlApp.Workbooks.Open(ofd.FileName);
                    worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    Excel.Range usedRange = worksheet.UsedRange;
                    int rows = usedRange.Rows.Count;

                    ladeFenster.progressBar.Minimum = 0;
                    ladeFenster.progressBar.Maximum = rows;
                    ladeFenster.progressBar.Value = 0;

                    excelDaten.Clear();

                    for (int i = 1; i <= rows; i++)
                    {
                        string? hostname = ((Excel.Range)worksheet.Cells[i, 11])?.Value2?.ToString()?.Trim();
                        string? mac = ((Excel.Range)worksheet.Cells[i, 9])?.Value2?.ToString()?.Trim();
                        string? uuid = ((Excel.Range)worksheet.Cells[i, 10])?.Value2?.ToString()?.Trim();

                        if (!string.IsNullOrEmpty(hostname) && !excelDaten.ContainsKey(hostname))
                            excelDaten.Add(hostname, (mac ?? "", uuid ?? ""));

                        ladeFenster.progressBar.Value = i;
                        ladeFenster.lblStatus.Text = $"Lade Zeile {i} von {rows}";
                        Application.DoEvents();
                    }

                    Marshal.ReleaseComObject(usedRange);

                    dateiGeladen = true;
                    btnAdd.Enabled = true;

                    MessageBox.Show("Datei erfolgreich geladen.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen der Datei:\n" + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dateiGeladen = false;
                    btnAdd.Enabled = false;
                }
                finally
                {
                    Enabled = true;
                    UseWaitCursor = false;
                    ladeFenster.Close();
                }
            }
        }

        #endregion

        #region Hostnames hinzufügen

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dateiGeladen)
            {
                MessageBox.Show("Bitte zuerst eine Excel-Datei auswählen.", "Warnung",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string input = txtHostname.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Bitte mindestens einen Hostnamen eingeben.");
                return;
            }

            string[] hostnames = input.Split(new[] { ',', '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int added = 0, notFound = 0;

            foreach (string raw in hostnames)
            {
                string hostname = raw.Trim();
                if (gesammelteDaten.Exists(d => d.StartsWith(hostname + ";")))
                {
                    listBoxLogs.Items.Add($"'{hostname}' bereits hinzugefügt.");
                    continue;
                }

                if (excelDaten.TryGetValue(hostname, out var daten))
                {
                    gesammelteDaten.Add($"{hostname};CAP;1;{daten.MAC};{daten.UUID};1");
                    listBoxLogs.Items.Add($"Hinzugefügt: {hostname} | MAC: {daten.MAC} | UUID: {daten.UUID}");
                    added++;
                    btnExportCSV.Enabled = true;
                }
                else
                {
                    listBoxLogs.Items.Add($"'{hostname}' nicht gefunden.");
                    notFound++;
                }
            }

            MessageBox.Show($"Fertig: {added} hinzugefügt, {notFound} nicht gefunden.",
                "Ergebnis", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Export CSV

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            if (gesammelteDaten.Count == 0)
            {
                MessageBox.Show("Keine Daten zum Exportieren vorhanden.");
                return;
            }

            SaveFileDialog sfd = new()
            {
                Filter = "CSV-Dateien (*.csv)|*.csv",
                Title = "CSV-Datei speichern",
                FileName = $"KDO_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var lines = new List<string>
                    {
                        "Computer.Computername;Computer.Domäne;Computer.Domäne J/N;Computer.MAC-Adresse;Computer.UUID;Computer.PXE fähig"
                    };
                    lines.AddRange(gesammelteDaten);

                    File.WriteAllLines(sfd.FileName, lines, System.Text.Encoding.UTF8);

                    MessageBox.Show("CSV erfolgreich gespeichert.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset
                    gesammelteDaten.Clear();
                    listBoxLogs.Items.Clear();
                    btnExportCSV.Enabled = false;
                    btnAdd.Enabled = false;
                    dateiGeladen = false;

                    ReleaseExcelObjects();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Speichern:\n" + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Form schließen

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ReleaseExcelObjects();
            base.OnFormClosing(e);
        }

        #endregion
    }
}
